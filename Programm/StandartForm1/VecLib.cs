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
            return Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Вычитание векторов v1 - v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3d Subtract(Vector3d v1, Vector3d v2)
        {
            Vector3d v;

            v.x = v1.x - v2.x;
            v.y = v1.y - v2.y;
            v.z = v1.z - v2.z;

            return v;
        }
        public static Vector3d Add(Vector3d v1, Vector3d v2)
        {
            Vector3d v;

            v.x = v1.x + v2.x;
            v.y = v1.y + v2.y;
            v.z = v1.z + v2.z;

            return v;
        }
        public static Vector3d Multiply(Vector3d v, double a)
        {
            Vector3d vv;

            vv.x = v.x * a;
            vv.y = v.y * a;
            vv.z = v.z * a;

            return vv;
        }
        /// <summary>
        /// Точка p находится на прямой между точками p1, p2, не включая их
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool PBetweenP1P2(Vector3d p, Vector3d p1, Vector3d p2)
        {
            Vector3d v21 = Subtract(p2, p1);
            Vector3d v12 = Subtract(p1, p2);
            Vector3d v1 = Subtract(p, p1);
            Vector3d v2 = Subtract(p, p2);

            bool res = (VEC_MUL_Scalar(v21, v1) > 0) && (VEC_MUL_Scalar(v12, v2) > 0);

            return res;
        }

        /// <summary>
        /// Скалярное произведение векторов v1 и v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double VEC_MUL_Scalar(Vector3d v1, Vector3d v2)
        {
            return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
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