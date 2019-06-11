using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class VortexShotEnemy : Enemy
    {
        public VortexShotEnemy(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {

        }

        protected override void OnUpdate()
        {
            //渦ショット
            if (count % 4 == 0)
            {
                VortexShot(count * 8);
            }

            //カウンタの増加機能を使いまわすため基底(Enemy)クラスのOnUpdateを呼び出す。
            base.OnUpdate();
        }
    }
}