using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class Flip
    {
        public static Image<Gray,byte> Flip_Method(Image<Gray,byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for(int y=0;y<InputImage.Height;y++)
            {
                for(int x=0;x<InputImage.Width;x++)
                {
                    Result.Data[y, x, 0] = (byte)InputImage.Data[InputImage.Height - y - 1, x, 0];
                }
            }
            return Result;
        }
    }
}
