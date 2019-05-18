using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Enemy : asd.TextureObject2D
    {
        //毎フレーム1増加し続けるカウンタ変数（継承先のクラスで使いまわすため、protectedに設定する。）
        protected int count;

        //プレイヤーへの参照（継承先のクラスで使いまわすため、protectedに設定する。）
        protected Player player;

        //コンストラクタ(敵の初期位置を引数として受け取る。)
        public Enemy(asd.Vector2DF pos, Player player)
            : base()
        {
            // 敵のインスタンスの位置を設定する。
            Position = pos;

            //　画像を読み込み、敵のインスタンスに画像を設定する。
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Enemy.png");

            // 敵のインスタンスに画像の中心位置を設定する。
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            // カウンタ変数を0に初期化する。
            count = 0;

            // Playerクラスへの参照を保持する。
            this.player = player;

        }

        protected override void OnUpdate()
        {
            ++count;
        }

        // 画面外に出た時に削除する関数。
        protected void DisposeFromGame()
        {
            // 画面外に出たら
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                // 削除する。
                Dispose();
            }
        }

        // 渦状に弾を拡散する関数。
        protected void VortexShot(float degree)
        {
            asd.Vector2DF dirVector = new asd.Vector2DF(1, 0);
            dirVector.Degree = degree;
            asd.Engine.AddObject2D(new StraightMovingEnemyBullet(Position, dirVector));
        }

        // 分裂弾を発射する。
        protected void SplitShot(int splitCount)
        {
            // 自機に向かって分裂する弾を撃つ。(速度ベクトルの長さは5.0でsplitCountで指定した回数フレームが経過すると分裂)
            asd.Vector2DF dir = player.Position - Position;
            asd.Vector2DF moveVector = dir.Normal * 5.0f;
            asd.Engine.AddObject2D(new SplitEnemyBullet(Position, moveVector, splitCount));
        }
    }
}