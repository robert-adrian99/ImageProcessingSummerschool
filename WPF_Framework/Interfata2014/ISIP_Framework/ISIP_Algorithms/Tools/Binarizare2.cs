using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class Binarizare2
    {
        public static Image<Gray, byte> Binarizare2_Method(Image<Gray, byte> InputImage, int t1,int t2)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);

            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    if (InputImage.Data[y, x, 0] >= Math.Min(t1, t2) && InputImage.Data[y, x, 0] <= Math.Max(t1, t2)) ;
                    {
                        Result.Data[y, x, 0] = 255;
                    }
                }
            }
            return Result;
        }
    }
}
