using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceAdventure.Engine
{	
	public class Canvas
	{
		static BufferedGraphicsContext context;
		
		Control parent;
		BufferedGraphics buffer;
		public Graphics Gr { get; private set; }
		
		static Canvas()
		{
			context = BufferedGraphicsManager.Current;
		}
		
		public Canvas(Control control)
		{
			parent = control;
			Update();
		}
		
		public void Update()
		{
			var rect = new Rectangle(0, 0, parent.Width, parent.Height);
			buffer = context.Allocate(parent.CreateGraphics(), rect);
			Gr = buffer.Graphics;
		}
		
		public void Redraw()
		{
			buffer.Render();
		}
	}
}
