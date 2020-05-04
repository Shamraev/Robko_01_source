using VecLib;
using ArrayHelp;

namespace CurveLib
{
    /// <summary>
    /// Класс - участок кривой
    /// </summary>
    public class Segment
    {
        protected ArrayHelper<Vector3d> _endPoint;

        /// <summary>
        /// Точки начала и конца сегмента; [true] - начало, [false] - конец, 
        /// </summary>
        public ArrayHelper<Vector3d> EndPoint { get { return _endPoint; } set { } }

        protected double _length;

        public double Length()
        {
            return _length;
        }        
    }

    /// <summary>
    /// Класс - отрезок
    /// </summary>
    public class Cut: Segment
    {     
        private Vector3d _vDirect;//вектор направление отрезка в полную длину
        public Vector3d VDirect { get { return _vDirect; } set { } }

        private Vector3d _vDirectNorm;//вектор направление отрезка нормированный
        public Vector3d VDirectNorm { get { return _vDirectNorm; } set { } }

        /// <summary>
        /// Точка начала отрезка - a, точка конца отрезка - b
        /// </summary>
        /// <param name="a">начало отрезка</param>
        /// <param name="b">конец отрезка</param>
        public Cut(Vector3d a, Vector3d b)
        {
            _endPoint = new ArrayHelper<Vector3d>(2);

            _endPoint[true] = a;
            _endPoint[false] = b;

            _vDirect = _endPoint[false] - _endPoint[true];
            _vDirectNorm = _vDirect;
            _vDirectNorm.Norm();

            _length = _vDirect.Length();

        }

        /// <summary>
        /// Возвращает точку отрезка по параметру t: 0 - начало, 1 - конец
        /// </summary>
        public Vector3d GetPointParam(double t)
        {
            double tt = t;
            if (tt < 0)
                tt = 0;
            if (tt > 1)
                tt = 1;

            if (tt == 0) return _endPoint[true];
            if (tt == 1) return _endPoint[false];//для исключения потери точности при нормировании вектора _VDirectNorm

            return (_endPoint[true] + tt * _vDirect);
        }
        /// <summary>
        /// Возвращает точку отрезка на расстоянии h от начала;
        /// </summary>
        public Vector3d GetPointLen(double h)
        {
            double hh = h;
            if (hh < 0)
                hh = 0;
            if (hh > _length)
                hh = _length;

            if (hh == 0) return _endPoint[true];
            if (hh == _length) return _endPoint[false];//для исключения потери точности при нормировании вектора _VDirectNorm

            return (_endPoint[true] + hh * _vDirectNorm);
        }
    }

    /// <summary>
    /// Класс - дуга
    /// </summary>
    public class Arc: Segment
    {

    }

}
