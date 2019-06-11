using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	class Bullet : asd.TextureObject2D
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
	}

	class Player : asd.TextureObject2D
	{

		public Player()
		{
			// 画像を読み込み、プレイヤーのインスタンスに画像を設定する。
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png");

			// プレイヤーのインスタンスに画像の中心位置を設定する。
			CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

			// プレイヤーのインスタンスの位置を設定する。
			Position = new asd.Vector2DF(320, 240);
		}

		protected override void OnUpdate()
		{
			// もし、上ボタンが押されていたら、位置に(0,-1)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Hold)
			{
				Position = Position + new asd.Vector2DF(0, -1);
			}

			// もし、下ボタンが押されていたら、位置に(0,+1)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Hold)
			{
				Position = Position + new asd.Vector2DF(0, +1);
			}

			// もし、左ボタンが押されていたら、位置に(-1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.ButtonState.Hold)
			{
				Position = Position + new asd.Vector2DF(-1, 0);
			}

			// もし、左ボタンが押されていたら、位置に(+1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.ButtonState.Hold)
			{
				Position = Position + new asd.Vector2DF(+1, 0);
			}

			// もし、Zキーを押したら{}内の処理を行う。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.ButtonState.Push)
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

			// Altseedのウインドウが閉じられていないか確認する。
			while (asd.Engine.DoEvents())
			{
				// もし、Escキーが押されていたらwhileループを抜ける。
				if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.ButtonState.Push)
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
