using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Data.Sql;

namespace HelixControl
{
    public class MyControlEventArgs : EventArgs
    {
        //...  
    }

    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();
        }

        public Brush BackgroundBrush
        {
            get { return _Background; }
            set
            {
                _Background = value;
                plot.Background = value;
            }
        }

        public Brush AxisBrush
        {
            get { return _AxisBrush; }
            set
            {
                _AxisBrush = value;
                plot.AxisBrush = (SolidColorBrush)value;
            }
        }

        public Brush MarkerBrush
        {
            get { return _MarkerBrush; }
            set
            {
                _MarkerBrush = value;
                plot.MarkerBrush = (SolidColorBrush)value;
            }
        }

        public Color GraphColor
        {
            get { return _graphcolor; }
            set
            {
                _graphcolor = value;
            }
        }

        public Color PauseColor
        {
            get { return _pausecolor; }
            set
            {
                _pausecolor = value;
            }
        }

        public string Satellite
        {
            get { return _satellite; }
            set
            {
                _satellite = value;
            }
        }

        private struct Point3DPlus
        {
            public Point3DPlus(Point3D point, Color color, double thickness)
            {
                this.point = point;
                this.color = color;
                this.thickness = thickness;
            }

            public Point3D point;
            public Color color;
            public double thickness;
        }

        private List<Point3DPlus> points = new List<Point3DPlus>();
        private Stopwatch stopwatch = Stopwatch.StartNew();

        public delegate void MyControlEventHandler(object sender, MyControlEventArgs args);
        // public event MyControlEventHandler OnButtonClick;
        public Brush _Background;
        public Brush _AxisBrush;
        public Brush _MarkerBrush;
        public Color _graphcolor;
        public Color _pausecolor;
        private string _satellite;
        public Boolean isPlotReady = false;

        public Color color;

        public void Init() //object sender, EventArgs e)
        {
            _Background = Brushes.White;
            _AxisBrush = Brushes.Gray;
            _MarkerBrush = Brushes.Red;
            _graphcolor = Colors.LightGreen;
            _pausecolor = Colors.Black;
            _satellite = "USI";

            isPlotReady = true;

            color = Colors.Blue;
            plot.BoundingBox = new Rect3D(-500, 0, 0, 1000, 500, 400);
        }

        public void AddPoint(double x, double y, double z)
        {
            var point = new Point3DPlus(new Point3D(x, y, z), color, 1.5);
            AddPoint(point);
        }

        private void AddPoint(Point3DPlus point)
        {
            plot.AddPoint(point.point, point.color, point.thickness);
        }

        public void Clear()
        {
            if (plot!=null)
                plot.Clear();
        }
        private void PlotData()
        {
            if (points.Count == 1)
            {
                Point3DPlus point;
                lock (points)
                {
                    point = points[0];
                    points.Clear();
                }

                plot.AddPoint(point.point, point.color, point.thickness);
            }
            else
            {
                Point3DPlus[] pointsArray;
                lock (points)
                {
                    pointsArray = points.ToArray();
                    points.Clear();
                }

                foreach (Point3DPlus point in pointsArray)
                    plot.AddPoint(point.point, point.color, point.thickness);
            }
        }

        public void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            plot.ZoomExtents(500);  // zoom to extents
            //plot.ResetCamera();  // orient and zoom
        }
        public void ResetCamera()
        {
            plot.ResetCamera();  // orient and zoom
        }
        public void MoveTo(double x, double y, double z)
        {
            var point = new Point3DPlus(new Point3D(x, y, z), color, 0);
            AddPoint(point);
        }

    }
}
