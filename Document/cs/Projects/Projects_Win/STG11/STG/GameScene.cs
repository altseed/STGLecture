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
		protected override void OnRegistered()
		{
			// 2Dを表示するレイヤーのインスタンスを生成する。
			asd.Layer2D layer = new asd.Layer2D();

			// シーンにレイヤーのインスタンスを追加する。
			AddLayer(layer);

			// プレイヤーのインスタンスを生成する。
			Player player = new Player();

			// プレイヤーのインスタンスをレイヤーに追加する。
			layer.AddObject(player);

			// レイヤーに反復して移動する敵のインスタンスを生成する。
			layer.AddObject(new VortexShotEnemy(new asd.Vector2DF(320.0f, 100.0f), player));
		}
	}
}
