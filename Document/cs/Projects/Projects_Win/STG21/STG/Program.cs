using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	class Laser : asd.EffectObject2D
	{
		public Laser()
		{
			Effect = asd.Engine.Graphics.CreateEffect("effect.efk");
		}
	}

	class Player : asd.TextureObject2D
	{
		Laser laser = null;

		public Player(asd.Vector2DF position)
		{
			Position = position;
			Src = new asd.RectF(0, 0, 16, 16);
			CenterPosition = new asd.Vector2DF(8, 8);
		}

		protected override void OnStart()
		{
			laser = new Laser();
			Layer.AddObject(laser);
		}

		protected override void OnUpdate()
		{
			// レーザーの位置を同期する。
			laser.Position = Position;

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
				// エフェクトとエフェクトオブジェクト2Dの位置を同期するように設定する。
				laser.SyncEffects = true;

				// エフェクトの角度を設定する。
				laser.EffectRotation = 90;
				laser.Angle = 90;
				
				// エフェクトの拡大率を設定する。
				laser.Scale = new asd.Vector2DF(10, 10);

				// レーザーのエフェクトを再生する。
				laser.Play();
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
			Player player = new Player(new asd.Vector2DF(320,240));

			// エンジンにプレイヤーのインスタンスを追加する。
			asd.Engine.AddObject2D(player);

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
