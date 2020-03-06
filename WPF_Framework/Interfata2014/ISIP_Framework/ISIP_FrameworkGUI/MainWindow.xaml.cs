using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using ISIP_UserControlLibrary;

using ISIP_Algorithms.Tools;
using ISIP_FrameworkHelpers;

namespace ISIP_FrameworkGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        //private Windows.Grafica dialog;
        Windows.Magnifyer MagnifWindow;
        Windows.GLine RowDisplay;
        bool Magif_SHOW = false;
        bool GL_ROW_SHOW = false;
        System.Windows.Point lastClick = new System.Windows.Point(0, 0);
        System.Windows.Point upClick = new System.Windows.Point(0, 0);

        public MainWindow()
        {
            InitializeComponent();
            mainControl.OriginalImageCanvas.MouseDown += new MouseButtonEventHandler(OriginalImageCanvas_MouseDown);
        }

        void OriginalImageCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastClick = Mouse.GetPosition(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllRectangles(mainControl.OriginalImageCanvas);
            DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
            DrawHelper.RemoveAllRectangles(mainControl.ProcessedImageCanvas);
            if (GL_ROW_ON.IsChecked)
            {
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(0, lastClick.Y),
                     new System.Windows.Point(mainControl.OriginalImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                if (mainControl.ProcessedGrayscaleImage != null)
                {
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(0, lastClick.Y),
                     new System.Windows.Point(mainControl.ProcessedImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                }
                if (mainControl.OriginalGrayscaleImage != null) RowDisplay.Redraw((int)lastClick.Y);

            }
            if (Magnifyer_ON.IsChecked)
            {
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(0, lastClick.Y),
                    new System.Windows.Point(mainControl.OriginalImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                DrawHelper.DrawAndGetLine(mainControl.OriginalImageCanvas, new System.Windows.Point(lastClick.X, 0),
                    new System.Windows.Point(lastClick.X, mainControl.OriginalImageCanvas.Height - 1), System.Windows.Media.Brushes.Red, 1);
                DrawHelper.DrawAndGetRectangle(mainControl.OriginalImageCanvas, new System.Windows.Point(lastClick.X - 4, lastClick.Y - 4),
                    new System.Windows.Point(lastClick.X + 4, lastClick.Y + 4), System.Windows.Media.Brushes.Red);
                if (mainControl.ProcessedGrayscaleImage != null)
                {
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(0, lastClick.Y),
                    new System.Windows.Point(mainControl.ProcessedImageCanvas.Width - 1, lastClick.Y), System.Windows.Media.Brushes.Red, 1);
                    DrawHelper.DrawAndGetLine(mainControl.ProcessedImageCanvas, new System.Windows.Point(lastClick.X, 0),
                        new System.Windows.Point(lastClick.X, mainControl.ProcessedImageCanvas.Height - 1), System.Windows.Media.Brushes.Red, 1);
                    DrawHelper.DrawAndGetRectangle(mainControl.ProcessedImageCanvas, new System.Windows.Point(lastClick.X - 4, lastClick.Y - 4),
                        new System.Windows.Point(lastClick.X + 4, lastClick.Y + 4), System.Windows.Media.Brushes.Red);
                }
                if (mainControl.OriginalGrayscaleImage != null) MagnifWindow.RedrawMagnifyer(lastClick);
            }
        }

        private void openGrayscaleImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Grayscale);
            Magnifyer_ON.IsEnabled = true;
            GL_ROW_ON.IsEnabled = true;

        }

        private void openColorImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainControl.LoadImageDialog(ImageType.Color);
            Magnifyer_ON.IsEnabled = true;
            GL_ROW_ON.IsEnabled = true;
        }

        private void saveProcessedImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!mainControl.SaveProcessedImageToDisk())
            {
                MessageBox.Show("Processed image not available!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void saveAsOriginalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.ProcessedGrayscaleImage != null)
            {
                mainControl.OriginalGrayscaleImage = mainControl.ProcessedGrayscaleImage;
            }
            else if (mainControl.ProcessedColorImage != null)
            {
                mainControl.OriginalColorImage = mainControl.ProcessedColorImage;
            }
        }

        private void Invert_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {

                mainControl.ProcessedGrayscaleImage = Tools.Invert(mainControl.OriginalGrayscaleImage);
            }

        }

        private void Magnifyer_ON_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (Magif_SHOW == true)
                {
                    Magif_SHOW = false;
                    MagnifWindow.Close();
                    DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllRectangles(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
                    DrawHelper.RemoveAllRectangles(mainControl.ProcessedImageCanvas);

                }
                else Magif_SHOW = true;
                if (Magif_SHOW == true)
                {
                    MagnifWindow = new Windows.Magnifyer(mainControl.OriginalGrayscaleImage, mainControl.ProcessedGrayscaleImage);
                    MagnifWindow.Show();
                    MagnifWindow.RedrawMagnifyer(lastClick);
                }
            }

        }

        private void GL_ROW_ON_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (GL_ROW_SHOW == true)
                {
                    GL_ROW_SHOW = false;
                    RowDisplay.Close();
                    DrawHelper.RemoveAllLines(mainControl.OriginalImageCanvas);
                    DrawHelper.RemoveAllLines(mainControl.ProcessedImageCanvas);
                }
                else GL_ROW_SHOW = true;

                if (GL_ROW_SHOW == true)
                {
                    RowDisplay = new Windows.GLine(mainControl.OriginalGrayscaleImage, mainControl.ProcessedGrayscaleImage);

                    RowDisplay.Show();
                    RowDisplay.Redraw((int)lastClick.Y);

                }
            }
        }

        private void BinarClick(object sender, RoutedEventArgs e)
        {
            UserInputDialog dlg = new UserInputDialog("Binar", new string[] { "t=" });
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (dlg.ShowDialog().Value == true)
                {
                    mainControl.ProcessedGrayscaleImage = Binarizare.Binarizare_Method(mainControl.OriginalGrayscaleImage, (int)dlg.Values[0]);
                }
            }
        }

        private void FlipClick(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = Flip.Flip_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void DrawGraphClick(object sender, RoutedEventArgs e)
        {
            Windows.Canvas wnd = new Windows.Canvas();
            wnd.Show();
        }

        private void BinarClick2(object sender, RoutedEventArgs e)
        {
            UserInputDialog dlg = new UserInputDialog("Binar", new string[] { "t1=", "t2=" });
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (dlg.ShowDialog().Value == true)
                {
                    mainControl.ProcessedGrayscaleImage = Binarizare2.Binarizare2_Method(mainControl.OriginalGrayscaleImage, (int)dlg.Values[0], (int)dlg.Values[1]);
                }
            }
        }

        private void Contrast1Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = GL_Contrast1.Contrast1_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void SensitiveClick(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = Sensitive.Sensitive_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void Neighbors4Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = Neighbors4.Neighbor_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void L_AdaptSobelClick(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = AdaptSobel.L_AdaptSobel_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void SkeletonClick(object sender, RoutedEventArgs e)
        {
            //mainControl.OriginalGrayscaleImage = Skeleton.CreateObjects();
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = Skeleton.ZhangSuen_Method(mainControl.OriginalGrayscaleImage);
            }
        }

        private void Sobel_Click(object sender, RoutedEventArgs e)
        {
            if (mainControl.OriginalGrayscaleImage != null)
            {
                mainControl.ProcessedGrayscaleImage = AdaptSobel.Sobel(mainControl.OriginalGrayscaleImage, 100);
            }
        }

        private void RotationClick(object sender, RoutedEventArgs e)
        {
            UserInputDialog dlg = new UserInputDialog("Degree", new string[] { "alfa=" });
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (dlg.ShowDialog().Value == true)
                {
                    mainControl.ProcessedGrayscaleImage = InterpolationRotation.Rotation_Method(mainControl.OriginalGrayscaleImage, (int)dlg.Values[0], (int)lastClick.X, (int)lastClick.Y);
                }
            }
        }

        private void HoughClick(object sender, RoutedEventArgs e)
        {
            UserInputDialog dlg = new UserInputDialog("Threshold", new string[] { "T=" });
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (dlg.ShowDialog().Value == true)
                {
                    mainControl.OriginalGrayscaleImage = AdaptSobel.Sobel(mainControl.OriginalGrayscaleImage, (int)dlg.Values[0]);
                    mainControl.ProcessedGrayscaleImage = Hough.HoughLines_Method(mainControl.OriginalGrayscaleImage, mainControl.OriginalImageCanvas);
                }
            }
        }

        private void CCL_Click(object sender, RoutedEventArgs e)
        {
            UserInputDialog dlg = new UserInputDialog("Threshold", new string[] { "T=" });
            if (mainControl.OriginalGrayscaleImage != null)
            {
                if (dlg.ShowDialog().Value == true)
                {
                    mainControl.OriginalGrayscaleImage = Binarizare.Binarizare_Method(mainControl.OriginalGrayscaleImage, (int)dlg.Values[0]);
                    mainControl.ProcessedGrayscaleImage = AppCCL.CCL_Method(mainControl.OriginalGrayscaleImage);
                }
            }
        }
    }
}
