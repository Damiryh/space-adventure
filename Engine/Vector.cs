using System;

namespace SpaceAdventure.Engine
{
	public class Vector
	{
		public Vector(double x, double y)
		{
			X = x;
			Y = y;
		}
		
		public Vector(double angle)
		{
			double rads = (Math.PI / 180) * angle;
			X = Math.Cos(rads);
			Y = Math.Sin(rads);
		}
		
		public Vector(Vector a, Vector b)
		{
			X = b.X - a.X;
			Y = b.Y - a.Y;
		}
		
		public double X { get; set; }
		public double Y { get; set; }
		
		public double Length()
		{
			return Math.Sqrt(X*X + Y*Y);
		}
		
		public Vector Norm()
		{
			return this / this.Length();
		}
		
		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.X + b.X, a.Y + b.Y);
		}
		
		public static Vector operator -(Vector a, Vector b)
		{
			return new Vector(a.X - b.X, a.Y - b.Y);
		}
		
		public static Vector operator *(double k, Vector a)
		{
			return new Vector(k * a.X, k * a.Y);
		}
		
		public static Vector operator *(Vector a, double k)
		{
			return k * a;
		}
		
		public static Vector operator /(Vector a, double k)
		{
			return new Vector(a.X / k, a.Y / k);
		}
		
		public static double operator *(Vector a, Vector b)
		{
			return a.X * b.X + a.Y * b.Y;
		}
	}
}
