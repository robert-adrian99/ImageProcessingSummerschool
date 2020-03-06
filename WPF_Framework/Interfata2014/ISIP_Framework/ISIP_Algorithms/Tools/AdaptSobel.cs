using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class AdaptSobel
    {
        public static Image<Gray,byte> Sobel(Image<Gray,byte> InputImage,int T)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
           
            int gx;
            int gy;

            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    gx = InputImage.Data[y - 1, x + 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y + 1, x - 1, 0] + 2 * InputImage.Data[y, x + 1, 0] - 2 * InputImage.Data[y, x - 1, 0];
                    gy = InputImage.Data[y + 1, x - 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y - 1, x + 1, 0] + 2 * InputImage.Data[y + 1, x, 0] - 2 * InputImage.Data[y - 1, x, 0];
                    double val = Math.Sqrt(gx * gx + gy * gy);
                    if (val> T)
                        Result.Data[y, x, 0] = 255;
                }
            }
            return Result;
        }

        public static Image<Gray, byte> L_AdaptSobel_Method(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            Image<Gray, int> Intermediate = new Image<Gray, int>(InputImage.Size);
            Image<Gray, byte> display = new Image<Gray, byte>(InputImage.Size);
            int Tmin = 15;
            int max = 0;
            int gx;
            int gy;
            
            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    gx = InputImage.Data[y - 1, x + 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y + 1, x - 1, 0] + 2 * InputImage.Data[y, x +1, 0] - 2 * InputImage.Data[y, x -1, 0];
                    gy = InputImage.Data[y + 1, x - 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y - 1, x + 1, 0] + 2 * InputImage.Data[y +1, x, 0] - 2 * InputImage.Data[y-1, x, 0];
                    Intermediate.Data[y, x, 0] = (int)Math.Sqrt(gx * gx + gy * gy);
                    if (Intermediate.Data[y, x, 0] > max)
                        max = Intermediate.Data[y, x, 0];
                }
            }
            double factor = 255 / (double)max;
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                   
                    display.Data[y, x, 0] = (byte)(Intermediate.Data[y, x, 0]* factor);
                }
            }
         //   return display;
            for (int y = 21; y < Intermediate.Height - 21; y++)
            {
                int gmax_local = 0;
                int gmin_local = 5*255;
                int T_adaptiv_local;
                for (int x = 21; x < Intermediate.Width - 21; x++)
                {
                    gmax_local = 0;
                    gmin_local = 5*255;
                    for (int index1 = -10; index1 <= 10; index1++)
                    {
                        for (int index2 = -10; index2 <= 10; index2++)
                        {
                            if (gmin_local > display.Data[y + index1, x + index2, 0])
                                gmin_local = display.Data[y + index1, x + index2, 0];
                            if (gmax_local < display.Data[y + index1, x + index2, 0])
                                gmax_local = display.Data[y + index1, x + index2, 0];
                        }
                    }
                    T_adaptiv_local = (int)(0.5 * gmax_local + (1 - 0.5) * gmin_local);
                    if (T_adaptiv_local < Tmin)
                        T_adaptiv_local = Tmin;
                    if (display.Data[y, x, 0] > T_adaptiv_local)
                        Result.Data[y, x, 0] = 255;
                }
            }
            return Result;
        }
    }
}
