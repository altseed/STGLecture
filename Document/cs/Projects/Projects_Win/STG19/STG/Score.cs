using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
	class Score : asd.TextObject2D
	{
		public Score()
		{
			asd.Font font = asd.Engine.Graphics.CreateFont("Resources/Font.aff");
			Font = font;
		}

		protected override void OnUpdate()
		{
			var scene = (GameScene)Layer.Scene;
			Text = "Score : " + scene.Score.ToString();
		}
	}
}
