using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class Binarizare
    {
        public static Image<Gray, byte> Binarizare_Method(Image<Gray, byte> InputImage, int t)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    if (InputImage.Data[y, x, 0] >= t)
                    {
                        Result.Data[y, x, 0] = 255;
                    }
                }
            }
            return Result;
        }
    }
}
