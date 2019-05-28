using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace TrueConfScannerManager
{
    internal interface TwainNativeMethods
    {
        TwRC DsmParent([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hwnd);

        TwRC DsmIdentity([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity twidentity);
        TwRC DsmStatus([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        TwRC DsUserinterface([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface twuserinterface);

        TwRC DsEvent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent twevent);

        TwRC DsStatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus twstatus);

        TwRC DsCapability([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability twcapability);

        TwRC DsImageInfo([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imageinfo);

        TwRC DsImagexfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);

        TwRC DsPendingxfers([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pendingxfer);
    }
    internal class NativeMethods
    {

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(int flags, int size);


        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);


        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern bool GlobalUnlock(IntPtr handle);


        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);


        [DllImport("kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
        internal static extern void ZeroMemory(IntPtr dest, IntPtr size);


        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern int GetMessagePos();

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern int GetMessageTime();


        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex);


        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateDC(string szdriver, string szdevice, string szoutput, IntPtr devmode);


        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo, IntPtr pixdat, ref IntPtr image);

        [DllImport("gdiplus.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int GdipSaveImageToFile(IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams);

        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipDisposeImage(IntPtr image);
    }
    internal class Twain64NativeMethods : TwainNativeMethods
    {
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryParent([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hwnd);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryIdentity([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity twidentity);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryStatus([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryUserinterface([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface twuserinterface);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryEvent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent twevent);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryStatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus twstatus);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryCapability([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability twcapability);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryImageInfo([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imageinfo);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryImagexfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twaindsm.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryPendingxfers([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pendingxfer);

        public TwRC DsmParent([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hwnd)
        {
            return DsmEntryParent(origin, zeropt, dg, dat, msg, ref hwnd);
        }

        public TwRC DsmIdentity([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity twidentity)
        {
            return DsmEntryIdentity(origin, zeropt, dg, dat, msg, twidentity);
        }

        public TwRC DsmStatus([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat)
        {
            return DsmEntryStatus(origin, zeropt, dg, dat, msg, dsmstat);
        }

        public TwRC DsUserinterface([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface twuserinterface)
        {
            return DsEntryUserinterface(origin, dest, dg, dat, msg, twuserinterface);
        }

        public TwRC DsEvent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent twevent)
        {
            return DsEntryEvent(origin, dest, dg, dat, msg, ref twevent);
        }

        public TwRC DsStatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus twstatus)
        {
            return DsEntryStatus(origin, dest, dg, dat, msg, twstatus);
        }

        public TwRC DsCapability([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability twcapability)
        {
            return DsEntryCapability(origin, dest, dg, dat, msg, twcapability);
        }

        public TwRC DsImageInfo(TwIdentity origin, TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwImageInfo imageinfo)
        {
            return DsEntryImageInfo(origin, dest, dg, dat, msg, imageinfo);
        }

        public TwRC DsImagexfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap)
        {
            return DsEntryImagexfer(origin, dest, dg, dat, msg, ref hbitmap);
        }

        public TwRC DsPendingxfers([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pendingxfer)
        {
            return DsEntryPendingxfers(origin, dest, dg, dat, msg, pendingxfer);
        }
    }
    internal class Twain32NativeMethods : TwainNativeMethods
    {
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryParent([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hwnd);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryIdentity([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity twidentity);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsmEntryStatus([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryUserinterface([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface twuserinterface);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryEvent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent twevent);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryStatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus twstatus);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryCapability([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability twcapability);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryImageInfo([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imgeinfo);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryImagexfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);


        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("twain_32.dll", EntryPoint = "DSM_Entry", CharSet = CharSet.Ansi)]
        internal static extern TwRC DsEntryPendingxfers([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pendingxfer);

        public TwRC DsmParent([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hwnd)
        {
            return DsmEntryParent(origin, zeropt, dg, dat, msg, ref hwnd);
        }

        public TwRC DsmIdentity([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity twidentity)
        {
            return DsmEntryIdentity(origin, zeropt, dg, dat, msg, twidentity);
        }

        public TwRC DsmStatus([In, Out] TwIdentity origin, IntPtr zeropt, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat)
        {
            return DsmEntryStatus(origin, zeropt, dg, dat, msg, dsmstat);
        }

        public TwRC DsUserinterface([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface twuserinterface)
        {
            return DsEntryUserinterface(origin, dest, dg, dat, msg, twuserinterface);
        }

        public TwRC DsEvent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent twevent)
        {
            return DsEntryEvent(origin, dest, dg, dat, msg, ref twevent);
        }

        public TwRC DsStatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus twstatus)
        {
            return DsEntryStatus(origin, dest, dg, dat, msg, twstatus);
        }

        public TwRC DsCapability([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability twcapability)
        {
            return DsEntryCapability(origin, dest, dg, dat, msg, twcapability);
        }

        public TwRC DsImageInfo(TwIdentity origin, TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwImageInfo imageinfo)
        {
            return DsEntryImageInfo(origin, dest, dg, dat, msg, imageinfo);
        }

        public TwRC DsImagexfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap)
        {
            return DsEntryImagexfer(origin, dest, dg, dat, msg, ref hbitmap);
        }

        public TwRC DsPendingxfers([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pendingxfer)
        {
            return DsEntryPendingxfers(origin, dest, dg, dat, msg, pendingxfer);
        }
    }
}
