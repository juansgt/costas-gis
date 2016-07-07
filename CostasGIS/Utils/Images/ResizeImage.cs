using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Utils.Images
{
    public static class ResizeImage
    {
        #region Imagen
        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }

        public enum Dim
        {
            Width,
            Height
        }

        public static Image Scale(Image image, int MaxSize)
        {

            try
            {
                if (image.Width > MaxSize || image.Height > MaxSize)
                {
                    Size size = getSize(image.Size, MaxSize);

                    Bitmap bitmap = new Bitmap(image, size.Width, size.Height);

                    MemoryStream stream = new MemoryStream();

                    saveJpeg(stream, bitmap, 95);

                    Image imageResized = System.Drawing.Image.FromStream(stream);

                    return imageResized;
                }
                else
                {
                    return image;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static Image Scale(Image image, int Width, int Height)
        {

            try
            {
                Bitmap bitmap = new Bitmap(image, Width, Height);

                MemoryStream stream = new MemoryStream();

                saveJpeg(stream, bitmap, 95);

                Image imageResized = System.Drawing.Image.FromStream(stream);

                return imageResized;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static Image ScaleToCrop(Image image, int widthDestino, int heightDestino)
        {

            try
            {
                double proporcionOrigen = Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height);
                double proporcionDestino = Convert.ToDouble(widthDestino) / Convert.ToDouble(heightDestino);

                Size size = new Size();

                if (proporcionOrigen < proporcionDestino)
                {
                    size = getSize(image.Size, widthDestino);
                }
                else
                {
                    size = getMinSize(image.Size, heightDestino);
                }

                Bitmap bitmap = new Bitmap(image, size.Width, size.Height);

                MemoryStream stream = new MemoryStream();

                saveJpeg(stream, bitmap, 95);

                Image imageResized = System.Drawing.Image.FromStream(stream);

                return imageResized;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static Image Rotate(Image image, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Image returnBitmap = new Bitmap(image.Width, image.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(image, new Point(0, 0));
            return returnBitmap;
        }

        public static MemoryStream Merge(Stream imagenOrigen, Stream imagenSuperpuesta, double anchorelativo, double altorelativo, double posrelativaX, double posrelativaY, bool tam_relativo, bool pos_relativa)
        {
            // Convierte a image el fichero original y el que vamos a superponer
            System.Drawing.Image original = System.Drawing.Image.FromStream(imagenOrigen);
            System.Drawing.Image superpuesta = System.Drawing.Image.FromStream(imagenSuperpuesta);

            // Decidimos las dimensiones en base a si son relativas o absolutas
            double posicionX;
            double posicionY;
            double ancho;
            double alto;

            if (tam_relativo)
            {
                ancho = anchorelativo * original.Width;
                alto = altorelativo * original.Height;
            }
            else
            {
                ancho = anchorelativo;
                alto = altorelativo;
            }

            if (pos_relativa)
            {
                posicionX = posrelativaX * (original.Width - anchorelativo);
                posicionY = posrelativaY * (original.Height - altorelativo);
            }
            else
            {
                posicionX = posrelativaX;
                posicionY = posrelativaY;
            }

            // A mezclar se ha dicho
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(imagenOrigen);
            Bitmap bmPhoto = new Bitmap(imgPhoto, original.Width, original.Height);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            grPhoto.DrawImage(superpuesta, new Rectangle((int)posicionX, (int)posicionY, (int)ancho, (int)alto), new Rectangle(0, 0, superpuesta.Width, superpuesta.Height), GraphicsUnit.Pixel);

            MemoryStream mm = new MemoryStream();

            saveJpeg(mm, bmPhoto, 95);

            // Cerramos todo lo cerrable

            original.Dispose();
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();

            return mm;
        }

        public static Image Crop(Image img, int Width, int Height, AnchorPosition Anchor)
        {
            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            if (img.Width < Width)
            {
                Width = img.Width;
            }

            if (img.Height < Height)
            {
                Height = img.Height;
            }

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)
                            (sourceHeight - (Height * nPercent));
                        break;
                    default:
                        destY = (int)
                            ((sourceHeight - (Height * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)
                          (sourceWidth - (Width * nPercent));
                        break;
                    default:
                        destX = (int)
                          ((sourceWidth - (Width * nPercent)) / 2);
                        break;
                }
            }

            if (destX >= 0 && destY >= 0)
            {
                Rectangle cropArea = new Rectangle(destX, destY, Width, Height);
                Bitmap bmpImage = new Bitmap(img);
                Bitmap bmpCrop = bmpImage.Clone(cropArea,
                bmpImage.PixelFormat);
                return (Image)(bmpCrop);
            }
            else
            {
                return img;
            }
        }

        public static void saveJpeg(Stream stream, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(stream, jpegCodec, encoderParams);
        }

        public static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Size getSize(Size size, int max)
        {
            Size result = new Size();

            result.Height = size.Height;
            result.Width = size.Width;

            if (size.Width > max || size.Height > max)
            {

                if (size.Width >= size.Height)
                {
                    result.Width = max;
                    result.Height = (max * size.Height) / size.Width;
                }
                else
                {
                    result.Height = max;
                    result.Width = (max * size.Width) / size.Height;
                }
            }

            return result;
        }

        public static Size getSize(Size size, int fix, Dim dim)
        {
            Size result = new Size();

            if (dim == Dim.Width)
            {
                result.Width = fix;
                result.Height = (fix * size.Height) / size.Width;
            }
            else
            {
                result.Height = fix;
                result.Width = (fix * size.Width) / size.Height;
            }

            return result;

        }

        public static Size getMinSize(Size size, int min)
        {
            Size result = new Size();

            result.Height = size.Height;
            result.Width = size.Width;

            if (size.Width > min && size.Height > min)
            {

                if (size.Width >= size.Height)
                {
                    result.Height = min;
                    result.Width = (min * size.Width) / size.Height;
                }
                else
                {
                    result.Width = min;
                    result.Height = (min * size.Height) / size.Width;
                }
            }

            return result;

        }

        public static Image byteArrayToImage(byte[] aByteImage)
        {
            MemoryStream mStream = new MemoryStream(aByteImage);
            Image image = Image.FromStream(mStream);

            return image;
        }

        public static byte[] imageToByteArray(Image image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] aByteImage = stream.GetBuffer();

            return aByteImage;
        }

        #endregion
    }
}