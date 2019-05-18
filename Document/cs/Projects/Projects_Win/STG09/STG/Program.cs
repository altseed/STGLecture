using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	public class Bullet : CollidableObject
	{
		public Bullet(asd.Vector2DF position)
		{
			// 画像を読み込み、弾のインスタンスに画像を設定する。
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/PlayerBullet.png");

			// 弾のインスタンスに画像の中心位置を設定する。
			CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

			// 弾のインスタンスの位置を設定する。
			Position = position;
		}

		protected override void OnUpdate()
		{
			Position = Position + new asd.Vector2DF(0, -2);

			// 弾の画像が画面外に出たら
			if (Position.Y < -Texture.Size.Y)
			{
				// 削除する。
				Dispose();
			}
		}

		public override void OnCollide(CollidableObject obj)
		{
			Dispose();
		}
	}

	public class Player : CollidableObject
	{
		public Player()
		{
			// 画像を読み込み、プレイヤーのインスタンスに画像を設定する。
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png");

			// プレイヤーのインスタンスに画像の中心位置を設定する。
			CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

			// プレイヤーのインスタンスの位置を設定する。
			Position = new asd.Vector2DF(320, 480);

			// プレイヤーの Radius は小さめにしておく
			Radius = Texture.Size.X / 8.0f;
		}


		protected override void OnUpdate()
		{
			foreach(var obj in Layer.Objects)
			{
				CollideWith(obj as CollidableObject);
			}

			// もし、上ボタンが押されていたら、位置に(0,-1)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.KeyState.Hold)
			{
				Position = Position + new asd.Vector2DF(0, -1);
			}

			// もし、下ボタンが押されていたら、位置に(0,+1)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.KeyState.Hold)
			{
				Position = Position + new asd.Vector2DF(0, +1);
			}

			// もし、左ボタンが押されていたら、位置に(-1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold)
			{
				Position = Position + new asd.Vector2DF(-1, 0);
			}

			// もし、左ボタンが押されていたら、位置に(+1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold)
			{
				Position = Position + new asd.Vector2DF(+1, 0);
			}

			// もし、Zキーを押したら{}内の処理を行う。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
			{
				// 弾のインスタンスを生成する。
				Bullet bullet = new Bullet(Position + new asd.Vector2DF(0, -30));

				// 弾のインスタンスをエンジンに追加する。
				asd.Engine.AddObject2D(bullet);
			}

			// プレイヤーの位置を取得する。
			asd.Vector2DF position = Position;

			// プレイヤーの位置を、(テクスチャの大きさ/2)～(ウインドウの大きさ-テクスチャの大きさ/2)の範囲に制限する。
			position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
			position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

			// プレイヤーの位置を設定する。
			Position = position;
		}

		public override void OnCollide(CollidableObject obj)
		{
			Dispose();
		}

		// 敵の弾との当たり判定をコントロールするメソッド
		protected void CollideWith(CollidableObject obj)
		{
			// 当たり判定の相手が見つかってない場合はメソッドを終了
			if (obj == null)
				return;

			// obj が EnemyBulletである場合にのみelse内を動作させる
			if (obj is EnemyBullet)
			{
				// obj が enemyBullet であることを明示
				CollidableObject enemyBullet = obj;

				// bulletと衝突した場合には、衝突時処理を行う
				if (IsCollide(enemyBullet))
				{
					OnCollide(enemyBullet);
					enemyBullet.OnCollide(this);
				}
			}
		}
	}

	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Altseedを初期化する。
			asd.Engine.Initialize("STG", 640, 480, new asd.EngineOption());

			// プレイヤーのインスタンスを生成する。
			Player player = new Player();

			// エンジンにプレイヤーのインスタンスを追加する。
			asd.Engine.AddObject2D(player);

			// エンジンに反復して移動する敵のインスタンスを生成する。
			asd.Engine.AddObject2D(new VortexShotEnemy(new asd.Vector2DF(320.0f, 100.0f), player));

			// Altseedのウインドウが閉じられていないか確認する。
			while (asd.Engine.DoEvents())
			{
				// もし、Escキーが押されていたらwhileループを抜ける。
				if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.KeyState.Push)
				{
					break;
				}

				// Altseedを更新する。
				asd.Engine.Update();
			}

			// Altseedの終了処理をする。
			asd.Engine.Terminate();
		}
	}
}
