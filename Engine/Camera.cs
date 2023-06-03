using System;

namespace SpaceAdventure.Engine
{
	public class Camera
	{
		Vector pos;
		double top;
		double left;
		
		public Camera(double x, double y, double width, double height)
		{
			this.pos = new Vector(x, y);
			left = width / 2;
			top = height / 2; 
		}
		
		public double X
		{
			get { return pos.X; }
		}
		
		public double Y
		{
			get { return pos.Y; }
		}
		
		public void SetPosition(double x, double y)
		{
			pos = new Vector(x, y);
		}
		
		public void MoveTo(double x, double y)
		{
			Vector vec = ((new Vector(x, y)) - pos);
			Vector dir = vec.Norm();
			if (vec.Length() != 0) Move(dir*(vec/3).Length());
		}
		
		public void Move(Vector vec)
		{
			pos += vec;
		}
		
		public Vector Convert(Vector pos)
		{
			return (pos - this.pos) + (new Vector(left, top));
		}
		
		public Vector Convert(double x, double y)
		{
			return Convert(new Vector(x, y));
		}
	}
}
