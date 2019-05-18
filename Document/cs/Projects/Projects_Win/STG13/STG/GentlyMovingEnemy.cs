using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	public class GentlyMovingEnemy : Enemy
	{
		// 移動の際の基準となる位置。
		private asd.Vector2DF basePosition;

		public GentlyMovingEnemy(asd.Vector2DF pos, Player player)
			: base(pos, player)
		{
			//　移動の際の基準となる位置を初期化する。
			basePosition = pos;
		}

		protected override void OnUpdate()
		{
			// basePositionに(X,Y) = ( sin(count*2) , 0 )のベクトルをX,Y成分それぞれ50.0倍した結果を新たな位置とする。
			Position = basePosition + 50.0f * new asd.Vector2DF((float)Math.Sin((count * 2) * Math.PI / 180.0f), 0);

			// 240フレームに一回分裂する弾を撃つ。(分裂するのは発射してから45フレーム経過後)
			if (count % 240 == 0)
			{
				SplitShot(45);
			}

			// カウンタの増加機能を使いまわすため基底(Enemy)クラスのOnUpdateを呼び出す。
			base.OnUpdate();
		}
	}
}