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

        virtual protected void SetOperationCoordts(double aX, double aY, double aZ)
        {
            x = aX;
            y = aY;
            z = aZ;
        }
        virtual public void SolveIK(double aX, double aY, double aZ)
        {
            SetOperationCoordts(aX, aY, aZ);

        }

    }
    class RobkoIKSolver : IKSolver
    {
        static byte GCoordCnt = 5;//количество обобщенных координат
        public GlobalCoordts NormQ = new GlobalCoordts(GCoordCnt);//вида 56,75
        double[] q = new double[GCoordCnt];
        double[] qDeg = new double[GCoordCnt];
        double d1, d2, d3, d4, d5;
        double r11, r12, r13, r21, r22, r23, r31, r32, r33;

        
        double C1 { get { return Cos(q[0]); } set { } }
        double S1 { get { return Sin(q[0]); } set { } }
        double C2 { get { return Cos(q[1]); } set {} }
        double S2 { get { return Sin(q[1]); } set {} }
        double C3 { get { return Cos(q[2]); } set {} }
        double S3 { get { return Sin(q[2]); } set {} }
        public override void SolveIK(double aX, double aY, double aZ)
        {
            base.SolveIK(aX, aY, aZ);

            q[0] = Atan2(-x, y);//!!-x

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

            q[1] = GetAngelSumSinCos((-2 * a1 * d3), (2 * a2 * d3), (d4 * d4 - d3 * d3 - a1 * a1 - a2 * a2));
            q[2] = Atan2(((C2 * a1 + S2 * a2) / (d4)), ((-C2 * a2 + S2 * a1 - d3) / (d4)));
            q[3] = GetAngelSumSinCos(-S3, C3, (C2 * r33 - C1 * S2 * r23 + S1 * S2 * r13));
            q[4] = Atan2(-(C1 * r12 + S1 * r22), (C1 * r11 + S1 * r21));
            ConvertQFromRadToDeg();
            GetNormQ();
        }
        private void GetNormQ()
        {
            for (int i =0; i< qDeg.Length; i++)
            {
                NormQ.ValueF[i] = (float)Round(qDeg[i], 2);
            }       
            
            Buffer.BlockCopy(NormQ.ValueF, 0, NormQ.ValueB, 0, NormQ.ValueB.Length);
        }
        private void ConvertQFromRadToDeg()
        {
            for (int i = 0; i < q.Length; i++)
            {
                qDeg[i] = Deg(q[i]);
            }
        }
        private double Deg(double q)
        {
            return (q * (180 / PI));
        }
        public double GetAngelSumSinCos(double A, double B, double C)
        {
            return Atan2(A, B) - Atan2(Sqrt(Pow(A, 2) + Pow(B, 2) - Pow(C, 2)), C);
        }
        public RobkoIKSolver(double d1, double d2, double d3, double d4, double d5) : this()
        {
            this.d1 = d1;
            this.d2 = d2;
            this.d3 = d3;
            this.d4 = d4;
            this.d5 = d5;
        }

        public RobkoIKSolver()
        {
        }
    }
}
