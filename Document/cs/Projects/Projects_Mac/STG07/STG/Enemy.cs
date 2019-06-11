using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Enemy : asd.TextureObject2D
    {
        // 速度ベクトル
        private asd.Vector2DF moveVector;

        // 毎フレーム1増加し続けるカウンタ変数
        private int count;

        //プレイヤーへの参照
        private Player player;

        // コンストラクタ(敵の初期位置を引数として受け取る。)
        public Enemy(asd.Vector2DF pos, asd.Vector2DF movevector, Player player)
            : base()
        {
            // 敵のインスタンスの位置を設定する。
            Position = pos;

            // 画像を読み込み、敵のインスタンスに画像を設定する。
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Enemy.png");

            // 弾のインスタンスに画像の中心位置を設定する。
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            // 敵の移動速度ベクトルを設定する。
            moveVector = movevector;

            // カウンタ変数を0に初期化する。
            count = 0;

            // Playerクラスへの参照を保持
            this.player = player;
        }

        protected override void OnUpdate()
        {
            //毎フレーム、速度ベクトル分移動する。
            Position += moveVector;

            // 画面外に出たら
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                // 削除する。
                Dispose();
            }

            // カウンタ変数が60の倍数の時
            if (count % 60 == 0)
            {
                // 自分の位置から自機の位置へ対するベクトルを計算。
                asd.Vector2DF dir = player.Position - Position;

                // ベクトルの長さを1.5に変更。
                asd.Vector2DF moveVelocity = dir.Normal * 1.5f;

                // 弾を発射する。
                asd.Engine.AddObject2D(new EnemyBullet(Position, moveVelocity));

                // moveVelocity2を時計方向に10.0度回転させたベクトルmoveVelocity2を作成。
                asd.Vector2DF moveVelocity2 = moveVelocity;
                moveVelocity2.Degree += 10.0f;

                // moveVelocity2を速度ベクトルとして弾を発射する。
                asd.Engine.AddObject2D(new EnemyBullet(Position, moveVelocity2));

                // moveVelocity3を反時計方向に10.0度回転させたベクトルmoveVelocity3を作成。
                asd.Vector2DF moveVelocity3 = moveVelocity;
                moveVelocity3.Degree -= 10.0f;

                // moveVelocity3を速度ベクトルとして弾を発射する。
                asd.Engine.AddObject2D(new EnemyBullet(Position, moveVelocity3));
            }

            ++count;
        }
    }
}