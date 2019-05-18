using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	// 衝突判定を持つTextureObject2D
	public class CollidableObject : asd.TextureObject2D
	{
		// 半径
		public float Radius = 0.0f;

		// 引数に指定したCollidableObjectと自分が衝突しているか、を返す。
		public bool IsCollide(CollidableObject o)
		{
			// 二点間の距離 が お互いの半径の和 より小さい場合にはtrueを返す。
			return (o.Position - Position).Length < Radius + o.Radius;
		}

		// 衝突時の処理を行うメソッドを実装する。
		public virtual void OnCollide(CollidableObject obj)
		{

		}
	}
}