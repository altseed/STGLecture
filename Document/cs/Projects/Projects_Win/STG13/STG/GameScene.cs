using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	// ゲーム画面を表すシーン
	class GameScene : asd.Scene
	{
		// プレイヤーのインスタンス
		Player player = null;

		// シーンを変更中か?
		bool isSceneChanging = false;

		protected override void OnRegistered()
		{
            // レイヤーを生成する。
            asd.Layer2D layer = new asd.Layer2D();
            asd.Layer2D backgroundLayer = new asd.Layer2D();

            // レイヤーの描画優先度を設定する（デフォルトで0）
            backgroundLayer.DrawingPriority = -10;

            // シーンにレイヤーを追加する。
            AddLayer(layer);
            AddLayer(backgroundLayer);

            // MovingBackground オブジェクトを生成する。
            MovingBackground bg1 = new MovingBackground(new asd.Vector2DF(0.0f, 0.0f), "Resources/Game_Bg.png", 0.3f);
            // bg1 の上端に bg2 の下端をくっつける。
            MovingBackground bg2 = new MovingBackground(new asd.Vector2DF(0.0f, -bg1.Texture.Size.Y), "Resources/Game_Bg.png", 0.3f);

            // 赤い背景を生成する。
            MovingBackground bgRed1 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f), "Resources/Game_Bg_Red.png", 0.5f);
            MovingBackground bgRed2 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f - bgRed1.Texture.Size.Y), "Resources/Game_Bg_Red.png", 0.5f);

            // 黄色い背景を生成する。
            MovingBackground bgYellow1 = new MovingBackground(new asd.Vector2DF(-10.0f, 80.0f), "Resources/Game_Bg_Yellow.png", 1.0f);
            MovingBackground bgYellow2 = new MovingBackground(new asd.Vector2DF(-10.0f, 80.0f - bgRed1.Texture.Size.Y), "Resources/Game_Bg_Yellow.png", 1.0f);

            // 背景を背景レイヤーに追加する。
            backgroundLayer.AddObject(bg1);
            backgroundLayer.AddObject(bg2);
            backgroundLayer.AddObject(bgRed1);
            backgroundLayer.AddObject(bgRed2);
            backgroundLayer.AddObject(bgYellow1);
            backgroundLayer.AddObject(bgYellow2);

            // プレイヤーのインスタンスを生成し、GameScene に登録する。
            player = new Player();

            // レイヤーにプレイヤーのインスタンスを追加する。
            layer.AddObject(player);

            // エンジンに反復して移動する敵のインスタンスを生成する。
            layer.AddObject(new VortexShotEnemy(new asd.Vector2DF(320.0f, 100.0f), player));
		}

		protected override void OnUpdated()
		{
			// もしシーンが変更中でなく、プレイヤーが倒されていたら処理を行う。
			if(!isSceneChanging && !player.IsAlive)
			{
				// ゲームオーバー画面に遷移する。
				asd.Engine.ChangeSceneWithTransition(new GameOverScene(), new asd.TransitionFade(1.0f, 1.0f));

				// シーンを変更中にする。
				isSceneChanging = true;
			}
		}
	}
}
