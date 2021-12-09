using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Wm = System.Windows.Media;

namespace UiPlus
{
    public static class GraphicConversions
    {
        public static Eto.Drawing.Color ToEto(this Sd.Color color)
        {
            return new Eto.Drawing.Color((float)(color.R/255.0), (float)(color.G/255.0), (float)(color.B/255.0), (float)(color.A/255.0));
        }

        public static Sd.Color ToDrawingColor(this Wm.Color input)
        {
            return Sd.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        public static Wm.SolidColorBrush ToSolidColorBrush(this Wm.Color input)
        {
            return new Wm.SolidColorBrush(input);
        }

        public static Wm.SolidColorBrush ToSolidColorBrush(this Sd.Color input)
        {
            return new Wm.SolidColorBrush(input.ToMediaColor());
        }

        public static Wm.Color ToMediaColor(this Sd.Color input)
        {
            return Wm.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        public static Wm.Imaging.BitmapSource ToImageSource(this Sd.Bitmap input)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                input.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                Wm.Imaging.BitmapImage bitmapimage = new Wm.Imaging.BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = Wm.Imaging.BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        public static Sd.Bitmap ToDrawingBitmap(this Wm.Imaging.BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                Wm.Imaging.BitmapEncoder enc = new Wm.Imaging.BmpBitmapEncoder();
                enc.Frames.Add(Wm.Imaging.BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }


        public static Sd.Color Tween(this Sd.Color source, Sd.Color target, double parameter)
        {
            double A = source.A + (target.A - source.A) * parameter;
            double R = source.R + (target.R - source.R) * parameter;
            double G = source.G + (target.G - source.G) * parameter;
            double B = source.B + (target.B - source.B) * parameter;

            return Sd.Color.FromArgb((int)A, (int)R, (int)G, (int)B);
        }

    }
}
