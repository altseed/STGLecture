using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class StraightMovingEnemy : Enemy
    {
        // 速度ベクトル
        private asd.Vector2DF moveVector;

        // コンストラクタ(敵の初期位置、移動ベクトルならびにプレイヤーへの参照を引数として受け取る。)
        public StraightMovingEnemy(asd.Vector2DF pos, asd.Vector2DF movevector, Player player)
            : base(pos, player)
        {
            // 敵の移動速度ベクトルを設定する。
            moveVector = movevector;
        }

        protected override void OnUpdate()
        {
            // 速度ベクトル分移動する。
            Position += moveVector;

            DisposeFromGame();

            // カウンタ変数が60の倍数の時
            if (count % 60 == 0)
            {
                // 自分の位置から自機の位置へ対するベクトルを計算。
                asd.Vector2DF dir = player.Position - Position;

                // ベクトルの長さを1.5に変更。
                asd.Vector2DF moveVelocity = dir.Normal * 1.5f;

                // 弾を発射する。
                asd.Engine.AddObject2D(new StraightMovingEnemyBullet(Position, moveVelocity));

                // moveVelocityを時計方向に10.0度回転させたベクトルmoveVelocity2を作成。
                asd.Vector2DF moveVelocity2 = moveVelocity;
                moveVelocity2.Degree += 10.0f;

                // moveVelocity2を速度ベクトルとして弾を発射する。
                asd.Engine.AddObject2D(new StraightMovingEnemyBullet(Position, moveVelocity2));

                // moveVelocityを反時計方向に10.0度回転させたベクトルmoveVelocity3を作成。
                asd.Vector2DF moveVelocity3 = moveVelocity;
                moveVelocity3.Degree -= 10.0f;

                // moveVelocity3を速度ベクトルとして弾を発射する。
                asd.Engine.AddObject2D(new StraightMovingEnemyBullet(Position, moveVelocity3));
            }

            // カウンタの増加機能を使いまわすため基底(Enemy)クラスのOnUpdateを呼び出す。
            base.OnUpdate();
        }
    }
}