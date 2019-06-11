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
	
			// エンジンにプレイヤーのインスタンスを追加する。
			asd.Engine.AddObject2D(player);
			
			// プレイヤーの位置を変更する。
			player.Position = new asd.Vector2DF(320, 240);

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
