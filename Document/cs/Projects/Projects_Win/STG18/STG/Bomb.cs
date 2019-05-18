using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	// ボムの魔法陣１つを表すクラス。
	class Bomb : CollidableObject
	{
		// 魔法陣の移動速度
		asd.Vector2DF velocity;

		// 時間を数えるカウンタ
		int count;

		public Bomb(asd.Vector2DF position, float angle)
		{
			// 初期位置を設定
			Position = position;

			// 魔法陣の画像を読み込み
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Circuit.png");

			// 描画原点を画像の中心に設定
			CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);

			// 移動速度を設定
			velocity = new asd.Vector2DF(1, 0);
			velocity.Length = 2.0f;     // 速度を設定
			velocity.Degree = angle;    // 角度を設定

			count = 0;

			// 半径をテクスチャサイズの半分に設定
			Radius = Texture.Size.X / 2;
		}

		protected override void OnUpdate()
		{
			// 速度ベクトルの分だけ移動させる
			Position += velocity;

			// 画像を回転する
			Angle += 5;

			// 時間を数える
			count++;

			// 120フレーム経っていたら消える
			if (count >= 120)
			{
				Dispose();
			}

			// レイヤー内のオブジェクトと当たり判定をする
			foreach (var item in Layer.Objects)
			{
				CollideWith(item as CollidableObject);
			}
		}

		// 指定したオブジェクトと当たり判定をするメソッド
		private void CollideWith(CollidableObject obj)
		{
			// オブジェクトがnullか、消滅していたら当たり判定をしない
			if (obj == null || !obj.IsAlive)
			{
				return;
			}

			if (obj is Enemy)	// オブジェクトがEnemyだった場合
			{
				if (IsCollide(obj))
				{
					obj.OnCollide(this);
				}
			}
			else if (obj is EnemyBullet)	// オブジェクトがEnemyBulletだった場合
			{
				if (IsCollide(obj))
				{
					obj.OnCollide(this);
				}
			}
		}
	}
}
