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
			// 2Dを表示するレイヤーのインスタンスを生成する。
			asd.Layer2D layer = new asd.Layer2D();

			// シーンにレイヤーのインスタンスを追加する。
			AddLayer(layer);

			// プレイヤーのインスタンスを生成する。
			player = new Player();

			// プレイヤーのインスタンスをレイヤーに追加する。
			layer.AddObject(player);

			// レイヤーに反復して移動する敵のインスタンスを生成する。
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
