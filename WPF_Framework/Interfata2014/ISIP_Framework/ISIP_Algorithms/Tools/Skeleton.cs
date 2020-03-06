using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ISIP_Algorithms.Tools
{
    public class Skeleton
    {
        public static Image<Gray, byte> CreateObjects()
        {
            Image<Gray, byte> Intermediate = new Image<Gray, byte>(640, 680);
            int x0 = 160;
            int y0 = 240;
            int r = 50;
            for (int y = 190; y <= 290; y++)
            {
                for (int x = 110; x <= 210; x++)
                {
                    if ((x - x0) * (x - x0) + (y - y0) * (y - y0) <= r * r)
                    {
                        Intermediate.Data[y, x, 0] = 255;
                    }
                }
            }
            for (int y = 190; y <= 489; y++)
            {
                for (int x = 430; x <= 529; x++)
                {
                    Intermediate.Data[y, x, 0] = 255;
                }
            }
            return Intermediate;
        }
        public static Image<Gray, byte> ZhangSuen_Method(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = InputImage.Clone();
            Image<Gray, byte> temp = InputImage.Clone();
            bool ok = false;
            do
            {
                temp = Result.Clone();
                ok = false;
                for (int y = 1; y < temp.Height - 1; y++)
                {
                    for (int x = 1; x < temp.Width - 1; x++)
                    {
                        if (temp.Data[y, x, 0] == 255)
                        {
                            int B = 0;
                            if (temp.Data[y, x + 1, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x + 1, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x + 1, 0] == 0)
                                B++;
                            if (B >= 2 && B <= 6)
                            {
                                int A = 0;
                                if (temp.Data[y, x + 1, 0] == 255 && temp.Data[y - 1, x + 1, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x + 1, 0] == 255 && temp.Data[y - 1, x, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x, 0] == 255 && temp.Data[y - 1, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x - 1, 0] == 255 && temp.Data[y, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y, x - 1, 0] == 255 && temp.Data[y + 1, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x - 1, 0] == 255 && temp.Data[y + 1, x, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x, 0] == 255 && temp.Data[y + 1, x + 1, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x + 1, 0] == 255 && temp.Data[y, x + 1, 0] == 0)
                                    A++;
                                if (A == 1)
                                {
                                    if (temp.Data[y - 1, x, 0] == 0 || temp.Data[y, x + 1, 0] == 0 || temp.Data[y + 1, x, 0] == 0)
                                    {
                                        if (temp.Data[y, x + 1, 0] == 0 || temp.Data[y + 1, x, 0] == 0 || temp.Data[y, x - 1, 0] == 0)
                                        {
                                            Result.Data[y, x, 0] = 0;
                                            ok = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                temp = Result.Clone();
                for (int y = 1; y < temp.Height - 1; y++)
                {
                    for (int x = 1; x < temp.Width - 1; x++)
                    {
                        if (temp.Data[y, x, 0] == 255)
                        {
                            int B = 0;
                            if (temp.Data[y, x + 1, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x + 1, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x, 0] == 0)
                                B++;
                            if (temp.Data[y - 1, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x - 1, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x, 0] == 0)
                                B++;
                            if (temp.Data[y + 1, x + 1, 0] == 0)
                                B++;
                            if (B >= 2 && B <= 6)
                            {
                                int A = 0;
                                if (temp.Data[y, x + 1, 0] == 255 && temp.Data[y - 1, x + 1, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x + 1, 0] == 255 && temp.Data[y - 1, x, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x, 0] == 255 && temp.Data[y - 1, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y - 1, x - 1, 0] == 255 && temp.Data[y, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y, x - 1, 0] == 255 && temp.Data[y + 1, x - 1, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x - 1, 0] == 255 && temp.Data[y + 1, x, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x, 0] == 255 && temp.Data[y + 1, x + 1, 0] == 0)
                                    A++;
                                if (temp.Data[y + 1, x + 1, 0] == 255 && temp.Data[y, x + 1, 0] == 0)
                                    A++;
                                if (A == 1)
                                {
                                    if (temp.Data[y - 1, x, 0] == 0 || temp.Data[y, x + 1, 0] == 0 || temp.Data[y, x - 1, 0] == 0)
                                    {
                                        if (temp.Data[y - 1, x, 0] == 0 || temp.Data[y + 1, x, 0] == 0 || temp.Data[y, x - 1, 0] == 0)
                                        {
                                            Result.Data[y, x, 0] = 0;
                                            ok = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } while (ok == true );
            return Result;
        }
    }
}
