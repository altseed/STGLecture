using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class MovingBackground : asd.TextureObject2D
    {
        // vel という小数を定義しておく。
        private float vel;

        public MovingBackground(asd.Vector2DF pos, string texturePath, float moveVelocity)
            : base()
        {
            // 初期位置を設定する。
            Position = pos;

            // texturePath で指定したパスにある画像を読み込んで Texture に変換します。
            Texture = asd.Engine.Graphics.CreateTexture2D(texturePath);

            // moveVelocityを保持できるようにする。
            vel = moveVelocity;

            // Backgroundクラスのオブジェクトで加算合成ができるようにする
            AlphaBlend = asd.AlphaBlendMode.Add;

            // velの値に応じてDrawingPriorityを設定する
            DrawingPriority = (int)(vel * 100);
        }

        protected override void OnUpdate()
        {
            // 毎回、Y座標をvelの値だけ位置を動かす。
            Position += new asd.Vector2DF(0.0f, vel);

            // Y座標が画面外にはみ出たならば、
            if (Position.Y >= asd.Engine.WindowSize.Y)
            {
                // Y座標から画像の縦2枚分の大きさを引く。
                Position -= new asd.Vector2DF(0.0f, 2 * Texture.Size.Y);
            }
        }
    }
}
