using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    class Laser : asd.TextureObject2D
    {
        // Player, Bossのインスタンスをコンストラクタでもらう
        private Player player;
        private Boss boss;

        // レーザーの終端・始端
        private asd.Vector2DF destPos;
        private asd.Vector2DF globalPos;

        // レーザーの（終端-始端）のベクトル
        private asd.Vector2DF direction;

        // チャージ・予測線の初期アルファ値
        private byte startAlpha;

        // レーザーの角速度・長さ
        private float angleVelocity;
        private float length;

        // カウンタ
        private int count;

        // レーザーが発射されるカウント・照射している間のカウント
        private int startCount;
        private int validCount;

        

        public Laser(Player playerRef, Boss bossRef)
        {
            // 目標点となるプレイヤー・ボスの参照を保持する
            player = playerRef;
            boss = bossRef;

            // レーザーの角速度を設定する
            angleVelocity = 0.001f;

            // チャージの初期透明度を50とする
            startAlpha = 50;

            // チャージの画像にsplitEnemyBulletを使用する
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/splitEnemyBullet.png");

            // 中心座標をチャージ画像中心に設定する
            CenterPosition = new asd.Vector2DF(Texture.Size.X/2.0f, Texture.Size.Y/2.0f);

            // レーザーが画面内で途切れないような長さをlengthとして保持する
            direction = new asd.Vector2DF(asd.Engine.WindowSize.X, asd.Engine.WindowSize.Y);
            length = direction.Length;

            // bossの子となることで、Positionがboss.Positionを原点とした相対的な座標となる
            boss.AddChild(this, ChildManagementMode.Nothing, ChildTransformingMode.Position);

            // 絶対座標として globalPos を取得する
            globalPos = GetGlobalPosition();

            // direction の初期の向き・長さを決める
            direction = player.Position - globalPos;
            direction.Length = length;
            
            // 目標座標を割り出す
            // （単純にプレイヤーの座標を割り当てるとレーザーが画面内で途切れてしまう）
            destPos = globalPos + direction;

            // カウンタを初期化する
            count = 0;

            // レーザーが発射されるカウント・照射している間のカウントを保持する 
            startCount = 60;
            validCount = 200;
        }

        protected override void OnUpdate()
        {
            // startCount から validCount 経過したら
            if (count >= validCount + startCount)
            {
                // bossとの親子関係を解消する
                boss.RemoveChild(this);

                // レーザー消滅
                Dispose();
            }

            // startCount に達するまでは
            else if (count < startCount)
            {
                // Scaleでチャージの拡大率を上げる
                Scale += new asd.Vector2DF(0.1f, 0.1f);

                // Colorでアルファ値を設定する
                Color = new asd.Color(255, 255, 255, (int)startAlpha);

                // startCountの数だけアルファ値にこれを足したら255（不透明）になるよう設定する
                startAlpha += (byte)((255-startAlpha)/startCount);

                // 予測線を表示する
                DrawLineAdditionally(GetGlobalPosition(), destPos, new asd.Color(120, 160, 255, (int)startAlpha), 5, asd.AlphaBlendMode.Add, 0);
            }

            // レーザーが照射されている時の処理
            // プレイヤーのいる方へレーザーの向きを調整する
            else
            {
                // アルファ値を0（透明）にする
                Color = new asd.Color(255, 255, 255, 0);

                // 現在の絶対座標を取得する
                globalPos = GetGlobalPosition();

                // レーザーを向けたい目標の方向ベクトルを設定する
                var destDirection = player.Position - globalPos;
                
                // ベクトルの内積を用いて destDirection の direction に平行な成分を parallelVector として取り出す
                var parallelVector = asd.Vector2DF.Dot(destDirection, direction) / direction.Length * direction.Normal;

                // ベクトルの差を用いて destDirection の direction に垂直な成分を verticalVector として取り出す
                var verticalVector = (destDirection - parallelVector);

                // player が生きている時のみ当たり判定処理をする
                if (player.IsAlive)
                {
                    // レーザーの当たり判定の範囲内にプレイヤーがいない場合は
                    if (verticalVector.Length > 5 + player.Radius)
                    {
                        // 角速度に応じて verticalVector の大きさを設定する
                        direction += verticalVector.Normal * direction.Length * (float)Math.Abs(Math.Tan(angleVelocity));

                        // direction の長さは既定の length を使用する
                        direction.Length = length;

                        // レーザーの目標地点を更新する
                        destPos = globalPos + direction;
                    }

                    else
                    {
                        // 衝突処理
                        player.OnCollide(null);
                    }
                }

                // レーザーを加算合成で描画する
                DrawLineAdditionally(GetGlobalPosition(), destPos, new asd.Color(0, 20, 255, 255), 20, asd.AlphaBlendMode.Add, 0);
                DrawLineAdditionally(GetGlobalPosition(), destPos, new asd.Color(40, 50, 255, 255), 16, asd.AlphaBlendMode.Add, 0);
                DrawLineAdditionally(GetGlobalPosition(), destPos, new asd.Color(80, 90, 255, 255), 12, asd.AlphaBlendMode.Add, 0);
                DrawLineAdditionally(GetGlobalPosition(), destPos, new asd.Color(120, 160, 255, 255), 10, asd.AlphaBlendMode.Add, 0);
            
            }

            // bossがDisposeされていたら
            if(!boss.IsAlive)
            {
                // Disposeする
                Dispose();
            }

            // 更新ごとに1つカウントする
            count++;
        }

    }
}
