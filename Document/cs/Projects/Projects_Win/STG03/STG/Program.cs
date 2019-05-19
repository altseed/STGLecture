using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
		// Altseedを初期化する。
		asd.Engine.Initialize("STG", 640, 480, new asd.EngineOption());

		// プレイヤーのインスタンスを生成する。
		asd.TextureObject2D player = new asd.TextureObject2D();

		// 画像を読み込み、プレイヤーのインスタンスに画像を設定する。
		player.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png");

		// プレイヤーのインスタンスに画像の中心位置を設定する。
		player.CenterPosition = new asd.Vector2DF(player.Texture.Size.X / 2.0f, player.Texture.Size.Y / 2.0f);

		// エンジンにプレイヤーのインスタンスを追加する。
		asd.Engine.AddObject2D(player);

		// プレイヤーのインスタンスの位置を変更する。
		player.Position = new asd.Vector2DF(320, 240);

		// Altseedのウインドウが閉じられていないか確認する。
		while (asd.Engine.DoEvents())
		{
			// もし、Escキーが押されていたらwhileループを抜ける。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.ButtonState.Push)
			{
				break;
			}

			// もし、上ボタンが押されていたら、位置に(0,-1)を足す。
			if(asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Hold)
			{
				player.Position = player.Position + new asd.Vector2DF(0, -1);
			}

			// もし、下ボタンが押されていたら、位置に(0,+1)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Hold)
			{
				player.Position = player.Position + new asd.Vector2DF(0, +1);
			}

			// もし、左ボタンが押されていたら、位置に(-1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.ButtonState.Hold)
			{
				player.Position = player.Position + new asd.Vector2DF(-1, 0);
			}

			// もし、左ボタンが押されていたら、位置に(+1,0)を足す。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.ButtonState.Hold)
			{
				player.Position = player.Position + new asd.Vector2DF(+1, 0);
			}

			// プレイヤーの位置を取得する。
			asd.Vector2DF position = player.Position;
		
			// プレイヤーの位置を、(テクスチャの大きさ/2)～(ウインドウの大きさ-テクスチャの大きさ/2)の範囲に制限する。
			position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - player.Texture.Size.X / 2.0f, player.Texture.Size.X / 2.0f);
			position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - player.Texture.Size.Y / 2.0f, player.Texture.Size.Y / 2.0f);
		
			// プレイヤーの位置を設定する。
			player.Position = position;

			// Altseedを更新する。
			asd.Engine.Update();
		}

		// Altseedの終了処理をする。
		asd.Engine.Terminate();
		}
	}
}
