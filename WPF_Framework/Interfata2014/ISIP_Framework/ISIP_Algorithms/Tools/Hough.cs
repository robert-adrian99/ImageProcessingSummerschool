using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using Emgu.CV;
using Emgu.CV.Structure;
using ISIP_FrameworkHelpers;

namespace ISIP_Algorithms.Tools
{
    public class Hough
    {
        public static Image<Gray, byte> HoughLines_Method(Image<Gray, byte> InputImage, Canvas canvas)
        {
            Image<Gray, int> Hough = new Image<Gray, int>(271, (int)Math.Sqrt(InputImage.Width * InputImage.Width + InputImage.Height * InputImage.Height));
            Image<Gray, byte> Representation = new Image<Gray, byte>(271, (int)Math.Sqrt(InputImage.Width * InputImage.Width + InputImage.Height * InputImage.Height));
            int max = 0;
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    double r;
                    if (InputImage.Data[y, x, 0] == 255)
                    {
                        for (int alfa = -90; alfa <=180; alfa++)
                        {

                            r = Math.Cos(alfa * Math.PI / 180) * x + Math.Sin(alfa * Math.PI / 180) * y;
                            if (r >= 0)
                            {
                                Hough.Data[(int)r, alfa + 90, 0]++;
                                if (Hough.Data[(int)r, alfa + 90, 0] > max)
                                    max = Hough.Data[(int)r, alfa + 90, 0];
                            }
                        }
                    }
                }
            }
            for (int y = 0; y < Hough.Height; y++)
            {
                for (int x = 0; x < Hough.Width; x++)
                {
                    Representation.Data[y, x, 0] = (byte)((255.0 / max) * Hough.Data[y, x, 0]);
                }
            }
            for (int y = 50; y < Representation.Height-50; y++)
            {
                for (int x = 50; x < Representation.Width-50; x++)
                {
                    max = 0;
                    for (int i = -50; i <= 50; i++)
                    {
                        for(int j=-50;j<=50;j++)
                        {
                            if (Representation.Data[y + i, x + j, 0] > max)
                                max = Representation.Data[y + i, x + j, 0];
                        }
                    }
                    for (int i = -50; i <= 50; i++)
                    {
                        for (int j = -50; j <= 50; j++)
                        {
                            if (Representation.Data[y + i, x + j, 0] < max)
                                Representation.Data[y + i, x + j, 0] = 0;
                        }
                    }
                }
            }
            for (int y = 0; y < Representation.Height; y++)
            {
                for (int x = 0; x < Representation.Width; x++)
                {
                    if (Representation.Data[y, x, 0] > 130)
                    {
                        int x1 = 0;
                        int x2 = InputImage.Width - 1;
                        int y1;
                        int y2;
                        if (x - 90 == 0 || x - 90 == 180)
                        {
                            y1 = y;
                            y2 = y;
                        }
                        else
                        {
                            y1 = (int)(y / Math.Sin(Math.PI / 180 * (x - 90)));
                            y2 = (int)((y - x2 * Math.Cos(Math.PI / 180 * (x - 90))) / Math.Sin(Math.PI / 180 * (x - 90)));
                        }
                        Point p1 = new Point(x1, y1);
                        Point p2 = new Point(x2, y2);
                        DrawHelper.DrawAndGetLine(canvas, p1, p2, Brushes.Blue);
                    }
                }
            }
            return Representation;
        }
    }
}
