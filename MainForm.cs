using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceAdventure.Engine;

namespace SpaceAdventure
{	
	public partial class MainForm : Form
	{
		Canvas canvas;
		Camera camera;
		Rocket rocket;
		double fuel;
		Ball[] balls;
		
		bool game;
		
		bool up, down, left, right, space;
		
		public MainForm()
		{
			InitializeComponent();
			canvas = new Canvas(CanvasFrame);
			camera = new Camera(0, 0, CanvasFrame.Width, CanvasFrame.Height);
			rocket = new Rocket(0, 0);
			camera.SetPosition(0.0, 0.0);
			fuel = 32000;
			//rocket.Thrust(20);
			
			balls = new Ball[50];
			
			Random rand = new Random(1000);
			
			for (int i = 0; i < balls.Length; i++)
			{
				balls[i] = new Ball(rand.Next(-10000, 10000), rand.Next(-10000, 10000), rand.Next(40, 200));
			}
			
			game = true;
			ticker.Start();
		}
		
		void TickerTick(object sender, EventArgs e)
		{
			if (!game) ticker.Stop();
			
			if (up && (fuel > 0))
			{
				fuel -= 0.5;
				rocket.Thrust(0.5);
			}
			
			if (left && (fuel > 0))
			{
				fuel -= 0.05;
				rocket.TurnLeft(5);
			}
			
			if (right && (fuel > 0))
			{
				fuel -= 0.05;
				rocket.TurnRight(5);
			}
			
			if (down && (fuel > 0))
			{
				fuel -= 0.1;
				rocket.Stop(0.2);
			}
			
			canvas.Gr.Clear(Color.Black);
			
			camera.MoveTo(rocket.X, rocket.Y);
			EvalGravity();
			rocket.Update();
			
			DrawRocket();
			DrawBalls();
			
			canvas.Gr.DrawString(
				"Speed: " + Math.Round(rocket.Speed*500).ToString() + " Px/sec",
				new Font("Courier New", 14, FontStyle.Bold),
				Brushes.LightGray,
				10, 10
			);
			
			canvas.Gr.DrawString(
				"Fuel: " + Math.Round(fuel*10).ToString() + " Cub",
				new Font("Courier New", 14, FontStyle.Bold),
				Brushes.LightGray,
				10, 30
			);
			
			canvas.Redraw();
		}
		
		public void DrawRocket()
		{
			Vector rel = camera.Convert(rocket.X, rocket.Y);
			
			Vector left = rel + 20*(new Vector(rocket.angle - 150));
			Vector right = rel + 20*(new Vector(rocket.angle + 150));
			Vector center = rel - 15*rocket.Direction;
			
			PointF[] points= new PointF[4];
			points[0] = new PointF((float)rel.X, (float)rel.Y);
			points[1] = new PointF((float)left.X, (float)left.Y);
			points[2] = new PointF((float)center.X, (float)center.Y);
			points[3] = new PointF((float)right.X, (float)right.Y);
			
			// Draw engine fire
			if (up)
			{
				PointF[] fire = new PointF[3];
				center = rel - 40*rocket.Direction;
				left = rel + 15*(new Vector(rocket.angle - 155));
				right = rel + 15*(new Vector(rocket.angle + 155));
				
				fire[0] = new PointF((float)left.X, (float)left.Y);
				fire[1] = new PointF((float)center.X, (float)center.Y);
				fire[2] = new PointF((float)right.X, (float)right.Y);
				
				canvas.Gr.FillPolygon(Brushes.Red, fire);
			}
			
			
			// Draw stopping barrier around rocket
			if (down)
			{
				canvas.Gr.FillEllipse(Brushes.Violet, new RectangleF(
					(float)(rel.X - 10*rocket.Direction.X - 20),
					(float)(rel.Y - 10*rocket.Direction.Y - 20),
					40,
					40
				));
			}
			
			// Draw rocket
			canvas.Gr.FillPolygon(Brushes.LightGray, points);
			
		}
		
		public void DrawBalls()
		{
			for (int i = 0; i < balls.Length; i++)
			{
				Vector rel = camera.Convert(balls[i].Position);
				canvas.Gr.FillEllipse(
					Brushes.Yellow,
					new RectangleF(
						(float)(rel.X-balls[i].Radius),
						(float)(rel.Y-balls[i].Radius),
						(float)(balls[i].Radius*2),
						(float)(balls[i].Radius*2)
					)
				);
			}
		}
		
		void EvalGravity()
		{
			double minLen = 20000;
			int ballIndex = -1;
			
			for (int i = 0; i < balls.Length; i++)
			{
				double len = (new Vector(rocket.Position, balls[i].Position)).Length();
				if (len < minLen)
				{
					ballIndex = i;
					minLen = len;
				}
			}
			
			if (ballIndex != -1)
			{
				Vector vec = new Vector(rocket.Position, balls[ballIndex].Position);
				Vector pt1 = camera.Convert(rocket.X, rocket.Y) + 50 * vec.Norm();
				Vector pt2 = pt1 + 40 * vec.Norm();
				
				string distanse = Math.Round(vec.Length()*10).ToString();
				
				Vector pt3 = pt2 + (distanse.Length * 5) * vec.Norm();
				
				canvas.Gr.DrawLine(Pens.Yellow, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y);
				canvas.Gr.DrawString(
					distanse,
					new Font("Courier New", 8),
					Brushes.Yellow,
					(float)pt3.X - (distanse.Length/2)*5, (float)pt3.Y
				);
				
				if (space && (vec.Length() <= balls[ballIndex].Radius + 200))
				{
					canvas.Gr.DrawString(
						"Заправка",
						new Font("Courier New", 8),
						Brushes.DarkGreen,
						(float)pt3.X - (distanse.Length/2)*5, (float)pt3.Y + 10
					);
					
					fuel += 20;
				}
				if (fuel > 99999) fuel = 99999;
				
				
				if (vec.Length() <= balls[ballIndex].Radius)
				{
					game = false;
					ticker.Stop();
				}
				
				rocket.Gravity(balls[ballIndex].Weight/(vec.Length()*vec.Length()), vec.Norm());
			}
			
		}
		
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up : up = true; break;
				case Keys.Down : down = true; break;
				case Keys.Left : left = true; break;
				case Keys.Right : right = true; break;
				case Keys.Space : space = true; break;
			}
		}
		
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up : up = false; break;
				case Keys.Down : down = false; break;
				case Keys.Left : left = false; break;
				case Keys.Right : right = false; break;
				case Keys.Space : space = false; break;
			}
		}
		
		void MainFormResizeEnd(object sender, EventArgs e)
		{
			canvas.Update();
			camera = new Camera(camera.X, camera.Y, CanvasFrame.Width, CanvasFrame.Height);
			canvas.Redraw();
			ticker.Start();
		}
		
		void MainFormResizeBegin(object sender, EventArgs e)
		{
			ticker.Stop();
		}
		
		void MainFormLeave(object sender, EventArgs e)
		{
			ticker.Stop();
		}
		
		
		
		void MainFormEnter(object sender, EventArgs e)
		{
			ticker.Start();
		}
	}
}
