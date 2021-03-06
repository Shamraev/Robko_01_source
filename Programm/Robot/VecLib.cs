﻿using System;
using static System.Math;
using static VecLib.VecLibMethods;
using VecLib;
using System.Linq;

namespace VecLib
{
    public static class VecLibMethods
    {
        public static Vector2d Y2dAxes = new Vector2d(0, 1);
        public static Vector2d X2dAxes = new Vector2d(1, 0);

        public static Vector3d Vector3DZeros = new Vector3d(0, 0, 0);

        public static Plane PlaneZeros = new Plane(Vector3DZeros, Vector3DZeros, Vector3DZeros);


        /// <summary>
        /// Скалярное произведение векторов v1 и v2
        /// </summary>
        public static double VEC_MUL_Scalar(Vector3d v1, Vector3d v2)
        {
            return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z);
        }

        /// <summary>
        /// Скалярное произведение векторов v1 и v2
        /// </summary>
        public static double VEC_MUL_Scalar(Vector2d v1, Vector2d v2)
        {
            return (v1.x * v2.x + v1.y * v2.y);
        }
        /// <summary>
        /// Векторное произведение векторов v1 и v2; угол от v1 до v2: >=0 - против часовой стрелки, <0 - по часовой
        /// </summary>
        public static double VEC_MUL_Vector(Vector2d v1, Vector2d v2)
        {
            return (v1.x * v2.y - v1.y * v2.x);
        }
        /// <summary>
        /// Векторное произведение векторов v1 и v2; угол от v1 до v2: >=0 - против часовой стрелки, <0 - по часовой
        /// </summary>
        public static double VEC_MUL_Vector(Vector3d v1, Vector3d v2)//??
        {
            return (v1.x * v2.y - v1.y * v2.x);
        }

        /// <summary>
        /// Точка p находится на прямой между точками p1, p2, не включая их
        /// </summary>
        public static bool PBetweenP1P2(Vector3d p, Vector3d p1, Vector3d p2)
        {
            Vector3d v21 = p2 - p1;
            Vector3d v12 = p1 - p2;
            Vector3d v1 = p - p1;
            Vector3d v2 = p - p2;

            bool res = (VEC_MUL_Scalar(v21, v1) > 0) && (VEC_MUL_Scalar(v12, v2) > 0);

            return res;
        }
        /// <summary>
        /// Знаковый угол между двумя векторами в радианах; >=0 - против часовой стрелки, <0 - по часовой
        /// </summary>
        public static double Angle_V1_v2(Vector2d v1, Vector2d v2)
        {
            if (Equal_V1_V2(v1, v2)) return 0;//??
            if (v1.isNone() || v2.isNone()) return 0;//??

            double cs = Acos(VEC_MUL_Scalar(v1, v2) / (v1.Length() * v2.Length()));
            int sgn = Sign(VEC_MUL_Vector(v1, v2));
            if (sgn == 0) sgn = 1;
            return sgn * cs;
        }
        public static bool Equal_V1_V2(Vector3d v1, Vector3d v2, double precision = 1.0e-9)
        {
            return (Equal_Double(v1.x, v2.x, precision) && Equal_Double(v1.y, v2.y, precision) && Equal_Double(v1.z, v2.z, precision));
        }
        public static bool Equal_V1_V2(Vector2d v1, Vector2d v2, double precision = 1.0e-9)
        {
            return (Equal_Double(v1.x, v2.x, precision) && Equal_Double(v1.y, v2.y, precision));
        }

        public static bool Equal_Double(double a, double b, double precision = 1.0e-9)
        {
            return (Abs(a - b) <= precision);
        }

        public static Vector3d p3d(Vector2d v, double z = 0)
        {
            return new Vector3d(v.x, v.y, z);
        }
        public static Vector2d p2d(Vector3d v)
        {
            return new Vector2d(v.x, v.y);
        }
        public static bool NumbersFromStr(string nbrs, int count, out double[] NumbersDouble, int tol = 2)
        {
            bool res = false;

            String[] gh = nbrs.Split(',');//отделить числа запятой
            NumbersDouble = new double[gh.Length];
            if (gh.Length != count || gh.Contains(""))
                return false;

            //сделать double  с раделителем - ".", а не ","
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            for (int i = 0; i < gh.Length; i++)
            {
                try
                {
                    NumbersDouble[i] = Math.Round(Convert.ToDouble(gh[i]), tol);
                }
                catch
                {
                    return false;
                }

            }
            res = true;
            return res;
        }

        public static string NumbersToStr(double[] NumbersDouble)
        {
            string str = "";
            for (int i = 0; i < NumbersDouble.Length; i++)
            {
                if (i != NumbersDouble.Length - 1) str += Convert.ToString(NumbersDouble[i]) + ",";
                else str += Convert.ToString(NumbersDouble[i]);
            }
            return str;
        }

        public static bool VectorFromStr(string nbrs, out Vector3d V)
        {
            bool res = false;

            V = Vector3DZeros;
            double[] NumbersDouble;
            if (NumbersFromStr(nbrs, 3, out NumbersDouble))
            {
                V.x = NumbersDouble[0];
                V.y = NumbersDouble[1];
                V.z = NumbersDouble[2];
                res = true;
            }
            return res;
        }

        public static string VectorToStr(Vector3d V)
        {
            double[] nbrs = {V.x, V.y, V.z};
            return NumbersToStr(nbrs);
        }
        public static bool PlaneFromStr(string PlaneStr, out Plane pl)
        {
            bool res = false;
            pl = PlaneZeros;

            double[] ABCD;
            if (NumbersFromStr(PlaneStr, 4, out ABCD, 12))
            {
                pl = new Plane(ABCD[0], ABCD[1], ABCD[2], ABCD[3]);
                res = true;
            }
            return res;
        }
        public static string PlaneToStr(Plane pl)
        {
            double[] ABCD = {pl.A, pl.B, pl.C, pl.D};
            return NumbersToStr(ABCD);
        }
    }
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

        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return Add(v1, v2);
        }
        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return Subtract(v1, v2);
        }
        public static Vector3d operator *(double a, Vector3d v)
        {
            return Multiply(a, v);
        }
        public static bool operator !=(Vector3d v1, Vector3d v2)
        {
            return !Equal_V1_V2(v1, v2, 0);
        }
        public static bool operator ==(Vector3d v1, Vector3d v2)
        {
            return Equal_V1_V2(v1, v2, 0);
        }


        public static Vector3d Add(Vector3d v1, Vector3d v2)
        {
            Vector3d v;

            v.x = v1.x + v2.x;
            v.y = v1.y + v2.y;
            v.z = v1.z + v2.z;

            return v;
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

        public static Vector3d Multiply(double a, Vector3d v)
        {
            Vector3d vv;

            vv.x = v.x * a;
            vv.y = v.y * a;
            vv.z = v.z * a;

            return vv;
        }
        public bool isNone()
        {
            return Double.IsNaN(this.x) && Double.IsNaN(this.y) && Double.IsNaN(this.z);
        }
        public bool isZero()
        {
            return Equal_Double(this.x, 0, 0) && Equal_Double(this.y, 0, 0) && Equal_Double(this.z, 0, 0);
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
            if (length == 0) return;
            x = x / length;
            y = y / length;
        }
        /// <summary>
        /// Повернуть вектор направо на 90 градусов
        /// </summary>
        public Vector2d NormR()
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
        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            return Add(v1, v2);
        }
        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            return Subtract(v1, v2);
        }
        public static Vector2d operator *(double a, Vector2d v)
        {
            return Multiply(a, v);
        }
        public static bool operator !=(Vector2d v1, Vector2d v2)
        {
            return !Equal_V1_V2(v1, v2, 0);
        }
        public static bool operator ==(Vector2d v1, Vector2d v2)
        {
            return Equal_V1_V2(v1, v2, 0);
        }

        public static Vector2d Add(Vector2d v1, Vector2d v2)
        {
            Vector2d v;

            v.x = v1.x + v2.x;
            v.y = v1.y + v2.y;

            return v;
        }
        /// <summary>
        /// Вычитание векторов v1 - v2
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector2d Subtract(Vector2d v1, Vector2d v2)
        {
            Vector2d v;

            v.x = v1.x - v2.x;
            v.y = v1.y - v2.y;

            return v;
        }

        public static Vector2d Multiply(double a, Vector2d v)
        {
            Vector2d vv;

            vv.x = v.x * a;
            vv.y = v.y * a;

            return vv;
        }
        public bool isNone()
        {
            return Double.IsNaN(this.x) && Double.IsNaN(this.y);
        }
        public bool isZero()
        {
            return Equal_Double(this.x, 0, 0) && Equal_Double(this.y, 0, 0);
        }

        public Vector2d(double x, double y) : this()
        {
            this.x = x;
            this.y = y;
        }

    }

    public struct Plane
    {
        private double _A;
        public double A { get { return _A; } set { } }
        private double _B;
        public double B { get { return _B; } set { } }
        private double _C;
        public double C { get { return _C; } set { } }
        private double _D;
        public double D { get { return _D; } set { } }


        /// <summary>
        /// Плоскость по коэффициентам A, B, C, D
        /// </summary>
        public Plane(double A, double B, double C, double D) : this()
        {
            this._A = A;
            this._B = B;
            this._C = C;
            this._D = D;
        }
        /// <summary>
        /// Плоскость по трем точкам
        /// </summary>
        public Plane(Vector3d M0, Vector3d M1, Vector3d M2) : this()
        {
            this._A = (M1.y - M0.y) * (M2.z - M0.z) - (M2.y - M0.y) * (M1.z - M0.z);
            this._B = -((M1.x - M0.x) * (M2.z - M0.z) - (M2.x - M0.x) * (M1.z - M0.z));
            this._C = (M1.x - M0.x) * (M2.y - M0.y) - (M2.x - M0.x) * (M1.y - M0.y);
            this._D = -M0.x * A - M0.y * B - M0.z * C;
        }
    }


}