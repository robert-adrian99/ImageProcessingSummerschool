using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class InterpolationRotation
    {
        public static Image<Gray, byte> Rotation_Method(Image<Gray, byte> InputImage,double alfa, int x0, int y0)
        {
            alfa = (Math.PI / 180 * alfa);
            //x0 = InputImage.Width / 2;
            //y0 = InputImage.Height / 2;
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
                for (int x = 0; x < InputImage.Width; x++)
                    Result.Data[y, x, 0] = 128;

            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    double x_calculated = (x - x0) * Math.Cos(alfa) + (y - y0) * Math.Sin(alfa) + x0;
                    double y_calculated = -(x - x0) * Math.Sin(alfa) + (y - y0) * Math.Cos(alfa) + y0;
                    int x_int = (int)x_calculated;
                    int y_int = (int)y_calculated;
                    double xd = x_calculated - x_int;
                    double yd = y_calculated - y_int;
                    if (xd >= 0 && x_int < InputImage.Width - 1 && yd >= 0 && y_int < InputImage.Height - 1)
                        Result.Data[y, x, 0] = (byte)((1 - xd) * (1 - yd) * InputImage.Data[y_int, x_int, 0] + xd * (1 - yd) * InputImage.Data[y_int, x_int + 1, 0] + (1 - xd) * yd * InputImage.Data[y_int + 1, x_int, 0] + xd * yd * InputImage.Data[y_int + 1, x_int + 1, 0]);
                }
            }
            return Result;
        }
    }
}
