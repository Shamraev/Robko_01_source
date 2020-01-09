using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;
using VecLib;

namespace InverseKinematics
{

    struct GlobalCoordts
    {
        public float[] ValueF;
        public byte[] ValueB;

        public GlobalCoordts(int n) : this()
        {
            this.ValueF = new float[n];
            this.ValueB = new byte[n * 4];
        }

    }
    class IKSolver
    {
        public double x, y, z;

        private byte gCoordCnt;
        protected byte GCoordCnt { get { return gCoordCnt; } set { gCoordCnt = value; } }

        private GlobalCoordts normQ;
        public GlobalCoordts NormQ { get { return normQ; } set { normQ = value; } }

        private double[] qRad;
        public double[] QRad { get { return qRad; } set { qRad = value; } }

        private double[] qDeg;
        public double[] QDeg { get { return qDeg; } set { qDeg = value; } }

        //virtual protected void SetOperationCoordts(double aX, double aY, double aZ)
        //{
        //    x = aX;
        //    y = aY;
        //    z = aZ;
        //}
        virtual public void SolveIK(double aX, double aY, double aZ)
        {
            //SetOperationCoordts(aX, aY, aZ); 
            this.x = aX;
            this.y = aY;
            this.z = aZ;
        }
        protected double GetAngelIntriangle(double l1, double l2, double l3)//l1, l2 - прилежащие стороны треугольника; l3 - противолежащая сторона от искомого угла
        {
            return Acos((l1 * l1 + l2 * l2 - l3 * l3) / (2 * l1 * l2));
        }
        protected void ConvertQFromRadToDeg()
        {
            if ((QRad.Length <= 0) || (QDeg.Length< QRad.Length)) return;
            QDeg = new double[QRad.Length];
            for (int i = 0; i < QRad.Length; i++)
            {
                QDeg[i] = Deg(QRad[i]);
            }
        }
        protected double Deg(double q)
        {
            return (q * (180 / PI));
        }
        protected void GetNormQ()
        {
            if ((QDeg.Length <= 0) || (NormQ.ValueF.Length < QDeg.Length)) return;
            for (int i = 0; i < QDeg.Length; i++)
            {
                NormQ.ValueF[i] = (float)Round(QDeg[i], 2);
            }

            Buffer.BlockCopy(NormQ.ValueF, 0, NormQ.ValueB, 0, NormQ.ValueB.Length);
        }

        protected double GetAngelSumSinCos(double A, double B, double C)
        {
            return Atan2(A, B) - Atan2(Sqrt(Pow(A, 2) + Pow(B, 2) - Pow(C, 2)), C);
        }
    }
    class IKSolver5DOF : IKSolver
    {           
        double d1, d2, d3, d4, d5;
        double r11, r12, r13, r21, r22, r23, r31, r32, r33;


        double C1 { get { return Cos(QRad[0]); } set { } }
        double S1 { get { return Sin(QRad[0]); } set { } }
        double C2 { get { return Cos(QRad[1]); } set { } }
        double S2 { get { return Sin(QRad[1]); } set { } }
        double C3 { get { return Cos(QRad[2]); } set { } }
        double S3 { get { return Sin(QRad[2]); } set { } }
        public override void SolveIK(double aX, double aY, double aZ)//добавить 2 координаты
        {
            base.SolveIK(aX, aY, aZ);

            QRad[0] = Atan2(-x, y);//!!-x

            //Задание ориантации схвата по умолчанию - параллельно XY
            //-------------------------            
            //Vy=-z
            r12 = 0; r22 = 0; r32 = -1;
            //Vz= по направлению d3, d4
            Vector2d vz = new Vector2d(-x, y);//!!-x
            vz.Norm();
            r13 = vz.x; r23 = vz.y; r33 = 0;
            //Vx= NormRVz
            Vector2d Vx = vz.NormR();//NormR??
            r11 = Vx.x; r21 = Vx.y; r31 = 0;
            //-------------------------


            double a1 = (d5 * C1 * r23) - (d5 * S1 * r13) - C1 * y + S1 * x;
            double a2 = d5 * r33 + d1 + d2 - z;

            QRad[1] = GetAngelSumSinCos((-2 * a1 * d3), (2 * a2 * d3), (d4 * d4 - d3 * d3 - a1 * a1 - a2 * a2));
            QRad[2] = Atan2(((C2 * a1 + S2 * a2) / (d4)), ((-C2 * a2 + S2 * a1 - d3) / (d4)));
            QRad[3] = GetAngelSumSinCos(-S3, C3, (C2 * r33 - C1 * S2 * r23 + S1 * S2 * r13));
            QRad[4] = Atan2(-(C1 * r12 + S1 * r22), (C1 * r11 + S1 * r21));
            ConvertQFromRadToDeg();
            GetNormQ();
        }        
        public IKSolver5DOF(double d1, double d2, double d3, double d4, double d5) : this()
        {
            this.d1 = d1;
            this.d2 = d2;
            this.d3 = d3;
            this.d4 = d4;
            this.d5 = d5;
            GCoordCnt = 5;//количество обобщенных координат
            NormQ = new GlobalCoordts(GCoordCnt);//вида 56,75            
            QRad = new double[GCoordCnt];
            QDeg = new double[GCoordCnt];
        }

        public IKSolver5DOF()
        {
        }
    }
    class IKSolver3DOF : IKSolver
    {
        double d1, d2, d3, d4, d5;
        public override void SolveIK(double aX, double aY, double aZ)
        {
            base.SolveIK(aX, aY, aZ);

            QRad[0] = Atan2(x, y);
            double ll = Sqrt(x * x + y * y) - d5;
            double l = Sqrt(Pow(z - d2, 2) + Pow(ll, 2));
            double a1 = Atan2(z - d2, ll);
            double a2 = GetAngelIntriangle(l, d3, d4);
            QRad[1] = PI / 2 - a1 - a2;
            double a3 = GetAngelIntriangle(d4, d3, l);
            QRad[2] = PI - a3;
            //--------совместить нули нормально
            //-----------
            QRad[2] = PI - QRad[2] - QRad[1];
            QRad[2] = PI / 2 - QRad[2];
            //-----------------
            ConvertQFromRadToDeg();
            GetNormQ();
        }
        public IKSolver3DOF(double d1, double d2, double d3, double d4, double d5) : this()
        {
            this.d1 = d1;
            this.d2 = d2;
            this.d3 = d3;
            this.d4 = d4;
            this.d5 = d5;
            GCoordCnt = 3;//количество обобщенных координат
            NormQ = new GlobalCoordts(GCoordCnt);//вида 56,75            
            QRad = new double[GCoordCnt];
            QDeg = new double[GCoordCnt];
        }

        public IKSolver3DOF()
        {
        }
    }
}
