using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	// タイトル画面を表すシーン
	class TitleScene : asd.Scene
	{
		protected override void OnRegistered()
		{
			// 2Dを表示するレイヤーのインスタンスを生成する。
			asd.Layer2D layer = new asd.Layer2D();

			// シーンにレイヤーのインスタンスを追加する。
			AddLayer(layer);

			// 背景画像を表示するオブジェクトのインスタンスを生成する。
			asd.TextureObject2D background = new asd.TextureObject2D();
			background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Title.png");

			// レイヤーにオブジェクトのインスタンスを追加する。
			layer.AddObject(background);
		}

		protected override void OnUpdated()
		{
			// もし、Zキーを押したら{}内の処理を行う。
			if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
			{
				asd.Engine.ChangeSceneWithTransition(new GameScene(), new asd.TransitionFade(1.0f, 1.0f));
			}
		}
	}
}
