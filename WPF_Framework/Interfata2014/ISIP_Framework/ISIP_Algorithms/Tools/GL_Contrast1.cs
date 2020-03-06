using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class GL_Contrast1
    {
        public static Image<Gray, byte> Contrast1_Method(Image<Gray, byte> InputImage)
        {
            int[] LookUp_Table = new int[256];
            double a = 127.5;
            double b = Math.PI / 255;
            double c = 127.5;
            double fi = -Math.PI / 2;
            for(int g=0;g<=255;g++)
            {
                LookUp_Table[g] = (int)(a * Math.Sin(b * g + fi) + c);
            }
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, x, 0] = (byte)LookUp_Table[(int)InputImage.Data[y, x, 0]];
                }
            }
            return Result;
        }
    }
}
