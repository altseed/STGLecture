using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class EnemyBullet : asd.TextureObject2D
    {
        // 敵弾の速度ベクトル。
        private asd.Vector2DF moveVelocity;

        // コンストラクタ(敵の初期位置を引数として受け取る。)
        public EnemyBullet(asd.Vector2DF pos, asd.Vector2DF movevelocity)
            : base()
        {
            // 敵弾のインスタンスの位置を設定する。
            Position = pos;

            // 画像を読み込み、敵弾のインスタンスに画像を設定する。
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/EnemyBullet.png");

            // 敵弾のインスタンスに画像の中心位置を設定する。
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            // 敵弾の速度ベクトルを設定する。
            moveVelocity = movevelocity;

        }

        protected override void OnUpdate()
        {
            //　毎フレーム、速度ベクトル分移動する。
            Position += moveVelocity;

            // 画面外に出たら
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
           　    // 削除する。
          　     Dispose();
            }
        }
    }
}