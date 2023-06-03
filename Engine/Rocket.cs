using System;

namespace SpaceAdventure.Engine
{
	public class Rocket
	{
		Vector pos;
		Vector speed;
		
		public double X
		{
			get { return pos.X; }
		}
		
		public double Y
		{
			get { return pos.Y; }
		}
		
		public double Speed
		{
			get { return speed.Length(); }
		}
		
		public Vector Direction
		{
			get { return new Vector(angle).Norm(); }
		}
		
		public Vector Position
		{
			get { return pos; }
		}
		
		public double angle { get; private set; }
		
		public Rocket(double x, double y)
		{
			this.pos = new Vector(x, y);
			speed = new Vector(0, 0);
		}
		
		public void Gravity(double k, Vector force)
		{
			this.speed += force.Norm() * k;
		}
		
		public void TurnLeft(double angle)
		{
			this.angle -= angle;
		}
		
		public void TurnRight(double angle)
		{
			this.angle += angle;
		}
		
		public void Thrust(double power)
		{
			this.speed += power*(new Vector(angle)).Norm();
		}
		
		public void Stop(double power)
		{
			this.speed = (this.speed.Length() <= 1) ? new Vector(0, 0) : this.speed - this.speed.Norm()*power;
		}
		
		public void Update()
		{
			this.pos += this.speed;
		}
	}
}
