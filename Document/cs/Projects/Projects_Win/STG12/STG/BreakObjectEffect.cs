using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	// オブジェクトが破壊された時に表示されるエフェクト
	public class BreakObjectEffect : asd.TextureObject2D
	{
		// エフェクトの1コマあたりのテクスチャの1辺あたりの長さ
		const int TextureSize = 128;

		// 横方向のコマ数
		const int TextureXCount = 4;

		// 縦方向のコマ数
		const int TextureYCount = 4;

		// 毎フレーム1増加し続けるカウンタ変数
		protected int count;

		// コンストラクタ(初期位置を引数として受け取る。)
		public BreakObjectEffect(asd.Vector2DF pos)
			: base()
		{
			// インスタンスの位置を設定する。
			Position = pos;

			// 画像の中心位置を設定する。
			CenterPosition = new asd.Vector2DF(TextureSize / 2, TextureSize / 2);

			//　画像を読み込み、敵のインスタンスに画像を設定する。
			Texture = asd.Engine.Graphics.CreateTexture2D("Resources/BreakObject.png");

			// 設定された画像で実際に表示する範囲を設定する。
			Src = new asd.RectF(0, 0, TextureSize, TextureSize);

			// アルファブレンドの方法を加算に変更する。
			AlphaBlend = asd.AlphaBlendMode.Add;
		}

		protected override void OnUpdate()
		{
			// 表示するアニメーションの位置を計算する。
			int x = count % TextureXCount;
			int y = count / TextureXCount;

			// 設定された画像で実際に表示する範囲を設定する。
			Src = new asd.RectF(x * TextureSize, y * TextureSize, TextureSize, TextureSize);

			// アニメーションが再生し終わったら削除する。
			if (count == TextureXCount * TextureYCount)
			{
				Dispose();
			}

			++count;
		}
	}
}
