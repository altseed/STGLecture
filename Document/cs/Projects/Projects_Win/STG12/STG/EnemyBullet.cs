using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	public abstract class EnemyBullet : CollidableObject
	{
		// コンストラクタ(敵の初期位置を引数として受け取る。)
		public EnemyBullet(asd.Vector2DF pos)
			: base()
		{
			// 敵弾のインスタンスの位置を設定する。
			Position = pos;

			//　画像を読み込み、敵弾のインスタンスに画像を設定する。
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/EnemyBullet.png");

			// 敵弾のインスタンスに画像の中心位置を設定する。
			CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

			// 画像の半分の大きさを Radius とする
			Radius = Texture.Size.X / 2.0f;
		}


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

		protected override void OnUpdate()
		{

		}

		public override void OnCollide(CollidableObject obj)
		{
			Dispose();
		}
	}
}