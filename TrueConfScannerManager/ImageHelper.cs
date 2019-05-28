using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace TrueConfScannerManager
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

    internal class ImageHelper
    {
        private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        private static bool GetCodecClsid(string filename, out Guid clsid)
        {
            clsid = Guid.Empty;
            string ext = Path.GetExtension(filename);
            if (ext == null)
                return false;
            ext = "*" + ext.ToUpper();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.IndexOf(ext) >= 0)
                {
                    clsid = codec.Clsid;
                    return true;
                }
            }
            return false;
        }

        private static IntPtr GetPixelInfo(IntPtr bmpptr)
        {
            BITMAPINFOHEADER bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);
            Rectangle bmprect = new Rectangle(0, 0, 0, 0);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        public static bool SaveDIBAs(string fileName, IntPtr bmptr)
        {
            NativeMethods.GlobalLock(bmptr);
            IntPtr pixdat = GetPixelInfo(bmptr);
            Guid clsid;
            if (!GetCodecClsid(fileName, out clsid))
            {
                throw new Exception("Unknown picture format for extension " + Path.GetExtension(fileName));
            }
            IntPtr img = IntPtr.Zero;
            int st = NativeMethods.GdipCreateBitmapFromGdiDib(bmptr, pixdat, ref img);
            if ((st != 0) || (img == IntPtr.Zero))
                return false;

            st = NativeMethods.GdipSaveImageToFile(img, fileName, ref clsid, IntPtr.Zero);
            NativeMethods.GdipDisposeImage(img);
            NativeMethods.GlobalFree(bmptr);
            return st == 0;
        }

    }
}
