using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class AppCCL
    {
        static int min(int value1, int value2, int value3, int value4)
        {
            int minimum = 50;
            if (value1 < minimum && value1 != 0)
                minimum = value1;
            if (value2 < minimum && value2 != 0)
                minimum = value2;
            if (value3 < minimum && value3 != 0)
                minimum = value3;
            if (value4 < minimum && value4 != 0)
                minimum = value4;
            return minimum;
        }

        public static Image<Gray, byte> CCL_Method(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);
            Image<Gray, int> Intermediate = new Image<Gray, int>(InputImage.Size);
            int[] vector = new int[500];
            //List<List<int>> l = new List<List<int>>();

            for (int i = 1; i < 500; i++)
            {
                vector[i] = i;
            }
            int index = 1;
            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    if (InputImage.Data[y, x, 0] == 255)
                    {
                        if (Intermediate.Data[y - 1, x + 1, 0] == 0 && Intermediate.Data[y - 1, x, 0] == 0 && Intermediate.Data[y - 1, x - 1, 0] == 0 && Intermediate.Data[y, x - 1, 0] == 0)
                        {
                            Intermediate.Data[y, x, 0] = index++;
                        }
                        else
                        {
                            if (Intermediate.Data[y - 1, x + 1, 0] != 0 || Intermediate.Data[y - 1, x, 0] != 0 || Intermediate.Data[y - 1, x - 1, 0] != 0 || Intermediate.Data[y, x - 1, 0] != 0)
                            {
                                Intermediate.Data[y, x, 0] = min(Intermediate.Data[y - 1, x + 1, 0], Intermediate.Data[y - 1, x, 0], Intermediate.Data[y - 1, x - 1, 0], Intermediate.Data[y, x - 1, 0]);
                                if (Intermediate.Data[y - 1, x + 1, 0] != 0 && Intermediate.Data[y - 1, x + 1, 0] != Intermediate.Data[y, x, 0])
                                    vector[Intermediate.Data[y - 1, x + 1, 0]] = Intermediate.Data[y, x, 0];
                                if (Intermediate.Data[y - 1, x, 0] != 0 && Intermediate.Data[y - 1, x, 0] != Intermediate.Data[y, x, 0])
                                    vector[Intermediate.Data[y - 1, x, 0]] = Intermediate.Data[y, x, 0];
                                if (Intermediate.Data[y - 1, x - 1, 0] != 0 && Intermediate.Data[y - 1, x - 1, 0] != Intermediate.Data[y, x, 0])
                                    vector[Intermediate.Data[y - 1, x - 1, 0]] = Intermediate.Data[y, x, 0];
                                if (Intermediate.Data[y, x - 1, 0] != 0 && Intermediate.Data[y, x - 1, 0] != Intermediate.Data[y, x, 0])
                                    vector[Intermediate.Data[y, x - 1, 0]] = Intermediate.Data[y, x, 0];
                            }
                        }
                    }
                }
            }
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Intermediate.Data[y, x, 0] = vector[Intermediate.Data[y, x, 0]];
                }
            }
            for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    Result.Data[y, x, 0] =  (byte)(255.5/index * Intermediate.Data[y, x, 0]);
                }
            }
            return Result;
        }
    }
}
