using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Helpers;
using System.Net;

namespace TCR.Lib.Utility
{
    public class ImageUtils
    {
        public static byte[] ResizeToMaxSize(int maxWidth, int maxHeight, byte[] imagedata)
        {
            //always call resize even if size the same to avoid locs and to create jpgs from any format
            using (Image img = LoadFromByteArray(imagedata))
            {
                
                Size size = new Size();
                size.Width = img.Width;
                size.Height = img.Height;
                if (img.Width > maxWidth || img.Height > maxHeight)
                {
                  
                    if (maxWidth > maxHeight)
                    {
                        size.Width = maxWidth;
                        size.Height = maxWidth;
                    }
                    else
                    {
                        size.Width = maxHeight;
                        size.Height = maxHeight;
                    }
                   
                }
                using (Image resized = ResizeImage(img, size))
                {
                    return ImageToByteArray(resized);
                }
            }
        }

        private static byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                img.Save(memStream, ImageFormat.Jpeg);
                byte[] result = new byte[memStream.Length];
                memStream.Position = 0;
                memStream.Read(result, 0, result.Length);
                return result;
            }
        }

        public static Image LoadFromByteArray(byte[] jpg)
        {
            Image result;
            using (MemoryStream memStream = new MemoryStream(jpg))
            {
                 result = Image.FromStream(memStream);
            }
            return result;
        }

        private static Image ResizeImage(Image image, Size size)
        {
            int newWidth;
            int newHeight;
            int originalWidth = image.Width;
            int originalHeight = image.Height;
            float percentWidth = (float)size.Width / (float)originalWidth;
            float percentHeight = (float)size.Height / (float)originalHeight;
            float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
            newWidth = (int)(originalWidth * percent);
            newHeight = (int)(originalHeight * percent);

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.Clear(Color.White);
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static byte[] CreateThumbNail(byte[] imageData, int maxWidth,bool keepFixedWidth)
        {
            try
            {
                var image = new WebImage(imageData);

                if (image.Width <= maxWidth || keepFixedWidth)
                    image.Resize(maxWidth, ((maxWidth * image.Height) / image.Width));
                byte[] result = image.GetBytes("image/jpeg");
                return result;
            }
            catch
            {
                return imageData;
                //log some error here as the file is invalid - return original imageData back
            }
        }

        public static bool WebImageSizeAtLeast(string path, int minWidth, int minHeight)
        {
            WebRequest request = WebRequest.Create(path);
            using (WebResponse response = request.GetResponse())
            {
                using (var respStream = response.GetResponseStream())
                {
                    var webImage = new WebImage(respStream);
                    if (webImage.Width >= minWidth && webImage.Height >= minHeight)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
