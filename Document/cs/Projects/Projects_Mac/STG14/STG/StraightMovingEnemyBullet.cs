using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	public class StraightMovingEnemyBullet : EnemyBullet
	{
		// 敵弾の速度ベクトル。
		private asd.Vector2DF moveVelocity;

		//コンストラクタ(敵の初期位置を引数として受け取る。)
		public StraightMovingEnemyBullet(asd.Vector2DF pos, asd.Vector2DF movevelocity)
			: base(pos)
		{
			//　敵弾の速度ベクトルを設定する。
			moveVelocity = movevelocity;
		}

		protected override void OnUpdate()
		{
			//　毎フレーム、速度ベクトル分移動する。
			Position += moveVelocity;

			DisposeFromGame();
		}
	}
}