using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class Neighbors4
    {
        public static Image<Gray, byte> Neighbor_Method(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    if (y == 0 || x == 0 || y == InputImage.Height - 1 || x == InputImage.Width - 1)
                    {
                        Result.Data[y, x, 0] = InputImage.Data[y, x, 0];
                    }
                    else
                    {
                        Result.Data[y, x, 0] = (byte)((InputImage.Data[y, x, 0] + InputImage.Data[y - 1, x, 0] + InputImage.Data[y, x - 1, 0] + InputImage.Data[y, x + 1, 0] + InputImage.Data[y + 1, x, 0]) / 5.0 + 0.5);
                    }
                }
            }
            return Result;
        }
    }
}
