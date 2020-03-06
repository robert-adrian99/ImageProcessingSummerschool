using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ISIP_FrameworkHelpers;

namespace ISIP_FrameworkGUI.Windows
{
    /// <summary>
    /// Interaction logic for Canvas.xaml
    /// </summary>
    public partial class Canvas : Window
    {
        public Canvas()
        {
            InitializeComponent();
        }
        public void drawGraph()
        {
            List<Point> puncte = new List<Point>();
            int k;
            float sigma;
            Point y1 = new Point(GraphCanvas.ActualWidth / 2, 10);
            Point y2 = new Point(GraphCanvas.ActualWidth / 2, GraphCanvas.ActualHeight - 10);
            Point x1 = new Point(10, GraphCanvas.ActualHeight / 2);
            Point x2 = new Point(GraphCanvas.ActualWidth - 10, GraphCanvas.ActualHeight / 2);
            DrawHelper.DrawAndGetLine(GraphCanvas, y1, y2, Brushes.Black);
            DrawHelper.DrawAndGetLine(GraphCanvas, x1, x2, Brushes.Black);
            DrawHelper.DrawText(GraphCanvas, "x", new Point(GraphCanvas.ActualWidth - 15, GraphCanvas.ActualHeight / 2), 12, Colors.Black);
            DrawHelper.DrawText(GraphCanvas, "f(x)", new Point(GraphCanvas.ActualWidth / 2 - 20, 9), 12, Colors.Black);
            try
            {
                System.Exception error=new System.Exception();
                k = int.Parse(K.Text);
                sigma = float.Parse(Sigma.Text);
                if (k <= 0 || sigma <= 0)
                    throw error;
                for(int x=0;x<=200;x++)
                {
                    double y = 100 - 100 * Math.Exp(-(x - k) * (x - k) / (2 * sigma * sigma));
                    Point p = new Point(x + GraphCanvas.ActualWidth / 2, GraphCanvas.ActualHeight / 2 - y);
                    puncte.Add(p);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            DrawHelper.DrawAndGetPolyline(GraphCanvas, puncte, Brushes.Red);
        }

        private void DrawClick(object sender, RoutedEventArgs e)
        {
            GraphCanvas.Children.Clear();
            drawGraph();
        }
    }
}
