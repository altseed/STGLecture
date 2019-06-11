using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class RushingEnemy : Enemy
    {
        // 移動の際の速度ベクトル。
        private asd.Vector2DF moveVelocity;

        public RushingEnemy(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            // 速度ベクトルベクトルを初期化。
            moveVelocity = new asd.Vector2DF();
        }

        protected override void OnUpdate()
        {
            // カウンタ変数を120で割った値を求める。
            int mod = count % 120;

            // mod = 0のとき
            if (mod == 0)
            {
                // その時点での自機に向かう速度ベクトルを求める。
                moveVelocity = (player.Position - Position).Normal * 5.0f;
            }
            else if (mod <= 60) // 1 <= mod <= 60 のとき
            {
                //　毎フレーム、速度ベクトル分移動する。
                Position += moveVelocity;
            }

            // カウンタの増加機能を使いまわすため基底(Enemy)クラスのOnUpdateを呼び出す。
            base.OnUpdate();
        }
    }
}