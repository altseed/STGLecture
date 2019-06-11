using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class SplitEnemyBullet : StraightMovingEnemyBullet
    {
        // 1フレームごとに1増加していくカウンタ変数。
        private int count;

        // 分裂するときのカウンタの値を保存する変数。
        private int splitCount;

        public SplitEnemyBullet(asd.Vector2DF startPos, asd.Vector2DF moveVector, int splitcount)
            : base(startPos, moveVector)
        {
            // カウンタの初期値を0に設定。
            count = 0;

            // 分裂するときのカウンタの値を設定。
            splitCount = splitcount;

            // 分裂する弾独自のテクスチャを設定する。
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/SplitEnemyBullet.png");
        }

        protected override void OnUpdate()
        {
            // 基底クラス(EnemyBullet)のOnUpdateメソッド内にある毎フレーム速度ベクトル分移動する処理と
            // 画面外に出た時に消去する処理は使いまわせるので、使いまわす。
            base.OnUpdate();

            // カウンタの値が分裂時の値に達した時。
            if (splitCount == count)
            {
                // 全6方向に対して弾(EnemyBullet)を発射する。
                for (int i = 0; i < 6; ++i)
                {
                    asd.Vector2DF dir = new asd.Vector2DF(1, 0);
                    dir.Length = 2.0f;
                    dir.Degree = i * 60;
                    asd.Engine.AddObject2D(new StraightMovingEnemyBullet(Position, dir));
                }

                // これ自身は消去する。
                Dispose();
            }
            ++count;
        }
    }
}