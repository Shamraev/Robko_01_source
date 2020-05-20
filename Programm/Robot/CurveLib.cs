using static System.Math;
using VecLib;
using static VecLib.VecLibMethods;
using ArrayHelp;

namespace CurveLib
{
    public static class CurveLibMethods
    {
        /// <summary>
        /// Получить центр дуги cp по двум точкам (a и b), знаковому радиусу R: R>0 дуга < PI, R<0 дуга > PI
        /// и направлению дуги direction: true - против ЧС
        /// </summary>
        public static bool GetCP_FromArc(Vector2d a, Vector2d b, double R, bool direction, out Vector2d cp)
        {
            bool res = false;
            cp = new Vector2d(0, 0);
            if (R == 0) return res;

            int directionInt;
            if (direction)
                directionInt = 1;
            else
                directionInt = -1;

            Vector2d d = b - a; //хорда дуги
            if (d.isZero()) d = 2 * Abs(R) * X2dAxes; //Полная окружность

            Vector2d h;
            if (directionInt * R < 0) h = d.NormR();
            else h = d.NormL();
            h.Norm();
            double H_LengthSqr = Pow(R, 2) - Pow((d.Length() / 2), 2);
            if (H_LengthSqr < 0) return res;
            h = (Sqrt(H_LengthSqr)) * h;//выставляем длину вектора h   

            cp = a + 0.5 * d + h;
            res = true;
            return res;
        }
    }
    /// <summary>
    /// Структура - отрезок
    /// </summary>
    public struct Cut
    {
        private ArrayHelper<Vector3d> _endPoint;

        /// <summary>
        /// Точки начала и конца сегмента; [true] - начало, [false] - конец, 
        /// </summary>
        public ArrayHelper<Vector3d> EndPoint { get { return _endPoint; } set { } }

        private double _length;
        public double Length { get { return _length; } set { } }

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
    /// Структура - дуга
    /// </summary>
    public struct Arc
    {
        private ArrayHelper<Vector2d> _endPoint;

        /// <summary>
        /// Точки начала и конца сегмента; [true] - начало, [false] - конец, 
        /// </summary>
        public ArrayHelper<Vector2d> EndPoint { get { return _endPoint; } set { } }

        private Vector2d _CP;
        /// <summary>
        /// Центр дуги
        /// </summary>
        public Vector2d CP { get { return _CP; } set { } }

        private bool _Direction;
        /// <summary>
        /// Направление дуги - против часовой стрелки
        /// </summary>
        public bool Direction { get { return _Direction; } set { } }
        private int _DirectionInt;//>0 - против часовой стрелки

        private double _R;
        /// <summary>
        /// Радиус дуги
        /// </summary>
        public double R { get { return _R; } set { } }

        private double _Theta;
        /// <summary>
        /// Занковый центральный угол дуги; >0 - против часовой стрелки
        /// </summary>
        public double Theta { get { return _Theta; } set { } }

        /// <summary>
        /// Новый ноль от нчала дуги ??
        /// </summary>
        private double _ThetaZero;

        private bool _IsClosed;
        public bool IsClosed { get { return _IsClosed; } set { } }

        private double _length;
        public double Length { get { return _length; } set { } }

        private Vector2d _vDirect;//вектор направление отрезка в полную длину
        public Vector2d VDirect { get { return _vDirect; } set { } }

        private Vector2d _vDirectNorm;//вектор направление отрезка нормированный
        public Vector2d VDirectNorm { get { return _vDirectNorm; } set { } }


        /// <summary>
        /// Дуга в плоскости (x, y). Точка начала дуги - a, точка конца дуги - b
        /// </summary>
        /// <param name="a">начало дуги</param>
        /// <param name="b">конец дуги</param>
        public Arc(Vector2d a, Vector2d b, Vector2d cp, bool direction)
        {
            //init
            _endPoint = new ArrayHelper<Vector2d>(2);
            _CP = new Vector2d(0, 0);
            _Direction = false;
            _R = 0;
            _Theta = 0;
            _ThetaZero = 0;
            _length = 0;
            _vDirect = new Vector2d(0, 0);
            _vDirectNorm = new Vector2d(0, 0);
            _IsClosed = false;


            _endPoint[true] = a;
            _endPoint[false] = b;

            _CP = cp;

            _Direction = direction;

            if (_Direction) _DirectionInt = 1;
            else _DirectionInt = -1;

            _R = (_endPoint[true] - _CP).Length();//?? точно ли до точки b будет тот же радиус??
            if (_R == 0) return;

            _Theta = Angle_V1_v2(_endPoint[true] - _CP, _endPoint[false] - _CP);
            if (_DirectionInt * Sign(_Theta) < 0) _Theta = 2 * PI - Abs(_Theta);
            _Theta = _DirectionInt * Abs(_Theta);

            _ThetaZero = Angle_V1_v2(X2dAxes, _endPoint[true] - _CP);

            if (Equal_V1_V2(_endPoint[true], _endPoint[false]))
            {
                _Theta = 2 * PI * _DirectionInt;
                _IsClosed = true;
            }
            else _IsClosed = false;            

            _length = _R * Abs(_Theta);

            _vDirect = _endPoint[false] - _endPoint[true];
            _vDirectNorm = _vDirect;
            _vDirectNorm.Norm();

        }
        /// <summary>
        /// Возвращает точку дуги по параметру t: 0 - начало, 1 - конец
        /// </summary>
        public Vector2d GetPointParam(double t)
        {
            double tt = t;
            if (tt < 0)
                tt = 0;
            if (tt > 1)
                tt = 1;

            if (tt == 0) return _endPoint[true];
            if (tt == 1) return _endPoint[false];//для исключения потери точности при нормировании вектора _VDirectNorm

            double t_PI = tt * _Theta;//параметр в значениях PI
            t_PI = t_PI + _ThetaZero;

            return (new Vector2d(_R * Cos(t_PI) + _CP.x, _R * Sin(t_PI) + _CP.y));
        }
        /// <summary>
        /// Возвращает точку дуги на расстоянии h от начала;
        /// </summary>
        public Vector2d GetPointLen(double h)
        {
            double hh = h;
            if (hh < 0)
                hh = 0;
            if (hh > _length)
                hh = _length;

            if (hh == 0) return _endPoint[true];
            if (hh == _length) return _endPoint[false];//для исключения потери точности при нормировании вектора _VDirectNorm

            return GetPointParam(hh / _length);
        }
    }

}
