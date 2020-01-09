using static System.Math;

namespace VecLib
{
    public struct Vector3d
    {
       public double x, y, z;
        public void Norm()
        {
            double length = this.Length();
            x = x / length;
            y = y / length;
            z = z / length;
        }
        public double Length()
        {
            return Sqrt(x*x+y*y+z*z);
        }
        public Vector3d(double x, double y, double z) : this()
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public struct Vector2d
    {
        public double x, y;
        public void Norm()
        {
            double length = this.Length();
            x = x / length;
            y = y / length;           
        }
        public Vector2d NormR()//повернуть направо на 90 градусов
        {
            Vector2d V;
            V.x = this.y;
            V.y = -this.x;
            return V;
        }
        public Vector2d NormL()//повернуть налево на 90 градусов
        {            
            Vector2d V;
            V.x = -this.y;
            V.y = this.x;
            return V;
        }
        public double Length()
        {
            return Sqrt(x * x + y * y);
        }
        public Vector2d(double x, double y) : this()
        {
            this.x = x;
            this.y = y;            
        }
    }

}