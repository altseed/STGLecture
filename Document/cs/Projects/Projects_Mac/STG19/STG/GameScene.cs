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
        // 敵の出現するレイヤーを持つ
        asd.Layer2D gameLayer;

		// UIを表示するレイヤーを持つ
		asd.Layer2D uiLayer;

        // ステージの最後にボスが出る
        Boss boss;

        // ステージ数を管理
        int stage;

        // ゲームの経過時間を管理
        int count;

		// スコアを管理
		public int Score = 0;

        // ステージごとに敵を入れておくキュー
        Queue<Enemy>[] enemyQueues = new Queue<Enemy>[3];

        // プレイヤーのインスタンス
        Player player = null;

        // シーンを変更中か?
        bool isSceneChanging = false;

		// BGM
		asd.SoundSource bgm;

		// 再生中のBGMを扱うためのID。
		int? playingBgmId;

        protected override void OnRegistered()
        {
            // レイヤーを生成する。
            gameLayer = new asd.Layer2D();
            asd.Layer2D backgroundLayer = new asd.Layer2D();
			uiLayer = new asd.Layer2D();

            // レイヤーの描画優先度を設定する（デフォルトで0）
            backgroundLayer.DrawingPriority = -10;

            // シーンにレイヤーを追加する。
            AddLayer(gameLayer);
            AddLayer(backgroundLayer);
			AddLayer(uiLayer);

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
            gameLayer.AddObject(player);

			var score = new Score();
			uiLayer.AddObject(score);

            // stage を初期化する
            initAllStage();

			// BGMを読み込む。
			bgm = asd.Engine.Sound.CreateSoundSource("Resources/Bgm.ogg", false);

			// BGMがループするように設定する。
			bgm.IsLoopingMode = true;

			// BGMは流れていないのでIDはnull
			playingBgmId = null;
        }

        protected override void OnUpdated()
        {
            // もしシーンが変更中でなく、プレイヤーが倒されていたら処理を行う。
            if (!isSceneChanging && !player.IsAlive)
            {
				if (playingBgmId.HasValue)
				{
					asd.Engine.Sound.FadeOut(playingBgmId.Value, 1.0f);
					playingBgmId = null;
				}

                // ゲームオーバー画面に遷移する。
                asd.Engine.ChangeSceneWithTransition(new GameOverScene(), new asd.TransitionFade(1.0f, 1.0f));

                // シーンを変更中にする。
                isSceneChanging = true;
            }

            // stage を更新する 
            updateStage();

        }

        private void initAllStage()
        {
            // 最初のステージを0とする
            stage = 0;

            // ゲーム内のカウントを0にする
            count = 0;

            // ステージ0用の敵をセットする
            initStage0();

            // ステージ1用の敵をセットする
            initStage1();

            // ステージ2用の敵をセットする
            initStage2();
        }

        // ステージ0に出現する敵を設定する
        private void initStage0()
        {
            // enemyQueue[0]にQueue<Enemy>をインスタンス化する。このキューにステージ0の敵を入れておく。
            enemyQueues[0] = new Queue<Enemy>();

            // 敵を射出する速度 moveVelocity を設定しておく
            asd.Vector2DF moveVelocity = new asd.Vector2DF(1.0f, 0.0f);

            // 5回ループする
            for (int i = 0; i < 5; i++)
            {
                // 速度を60度の向きに
                moveVelocity.Degree = 60;

                // 左側に敵を出現させる
                enemyQueues[0].Enqueue(new StraightMovingEnemy(new asd.Vector2DF(100.0f, 0.0f), moveVelocity, player));

                // 速度を120度の向きに
                moveVelocity.Degree = 120;

                // 右側に敵を出現させる
                enemyQueues[0].Enqueue(new StraightMovingEnemy(new asd.Vector2DF(540.0f, 0.0f), moveVelocity, player));
            }

            // 10回ループする
            for (int i = 0; i < 10; i++)
            {
                // 速度を90度の向きに
                moveVelocity.Degree = 90;

                // 左側に敵を出現させる
                enemyQueues[0].Enqueue(new StraightMovingEnemy(new asd.Vector2DF(100.0f, 0.0f), moveVelocity, player));
            }

            // 10回ループする
            for (int i = 0; i < 10; i++)
            {
                // 速度を90度の向きに
                moveVelocity.Degree = 90;

                // 右側に敵を出現させる
                enemyQueues[0].Enqueue(new StraightMovingEnemy(new asd.Vector2DF(540.0f, 0.0f), moveVelocity, player));
            }
        }

        private void initStage1()
        {
            // ステージ0同様に設定していく
            enemyQueues[1] = new Queue<Enemy>();
        }
        private void initStage2()
        {
            // ステージ0同様に設定していく
            enemyQueues[2] = new Queue<Enemy>();
        }

        private void updateStage()
        {
			// 60カウントのときに
			if(count == 60)
			{
				// BGMを再生。再生のIDを保持しておく
				playingBgmId = asd.Engine.Sound.Play(bgm);
			}

            // ステージに対応するキューが空でないならば
            if (enemyQueues[stage].Count > 0)
            {
                // countが144の倍数の時
                // (調整用に一定の時間(count>100)を置く)
                if (count % 144 == 0 && count > 100)
                {
                    // 敵を出現させる
                    gameLayer.AddObject(enemyQueues[stage].Dequeue());
                }
            }

            // そのステージの敵が出現し終わったら
            else
            {
                // ボスが出現してないときに
                if (boss == null)
                {
                    // ボスをステージに出現させる
                    boss = new Boss(new asd.Vector2DF(320.0f, 0.0f), player);

                    // ボスをレイヤーに追加する
                    gameLayer.AddObject(boss);
                }

                else if (!boss.IsAlive && stage < 2)
                {
                    // ボスを初期化しておいて
                    boss = null;

                    // ステージを先に進める
                    ++stage;
                    
                    // ステージが進んだら count を 0 に戻す。
                    count = 0;

					// BGMが再生中ならば
					if (playingBgmId.HasValue)
					{
						// BGMをフェードアウトする。
						asd.Engine.Sound.FadeOut(playingBgmId.Value, 0.5f);
						// BGMが鳴っていないのでIDはnull
						playingBgmId = null;
					}
                }
            }
            // onUpdate ごとに count を進める
            ++count;
        }
    }
}
