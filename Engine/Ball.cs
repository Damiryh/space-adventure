using System;

namespace SpaceAdventure.Engine
{
	public class Ball
	{
		Vector pos;
		double radius;
		double weight;
		
		public Ball(double x, double y, double radius)
		{
			pos = new Vector(x, y);
			this.radius = radius;
			this.weight = Math.Pow(radius, 2) * (4*Math.PI/3) * 0.05;
		}
		
		public Vector Position
		{
			get { return pos; }
		}
		
		public double X
		{
			get { return pos.X; }
		}
		
		public double Y
		{
			get { return pos.Y; }
		}
		
		public double Radius
		{
			get { return radius; }
		}
		
		public double Weight
		{
			get { return weight; }
		}
	}
}
