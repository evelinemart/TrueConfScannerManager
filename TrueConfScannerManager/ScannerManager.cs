using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrueConfScannerManager
{
    [Flags]
    internal enum TwainStateFlag
    {

        /// <summary>
        /// The DSM open.
        /// </summary>
        DSMOpen = 0x1,

        /// <summary>
        /// The ds open.
        /// </summary>
        DSOpen = 0x2,

        /// <summary>
        /// The ds enabled.
        /// </summary>
        DSEnabled = 0x4,

        /// <summary>
        /// The ds ready.
        /// </summary>
        DSReady = 0x08
    }

    public enum ScannerMessages
    {
        Undefined = -1,
        Null = 0,
        FinishScanning = 1,
        CloseScannerRequest = 2,
        CloseScannerOk = 3,
        DeviceEvent = 4
    }

    public class ScannerManager : IMessageFilter
    {
        private TwIdentity appId;
        private List<TwIdentity> sources;
        private static readonly string _twainDsm = Path.ChangeExtension(Path.Combine(Environment.SystemDirectory, "twaindsm"), ".dll");
        private readonly bool isTwain2Enable = IntPtr.Size != 4 && File.Exists(_twainDsm);
        private TwainNativeMethods tw;
        private IntPtr hwnd;
        private TwEvent evtmsg;
        private WINMSG winmsg;
        private TwainStateFlag twainState;
        public int CurrentScanner { get; set; }
        public int DefaultScanner
        {
            get
            {
                TwIdentity identity = new TwIdentity();
                if (tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, identity) == TwRC.Success)
                {
                    return sources.IndexOf(identity);
                }
                else
                    throw new Exception(GetTwainStatus());
            }
            set
            {
                if (value < 0 || value >= sources.Count)
                    throw new Exception("Index was out of bounds of the scanner list");
                if (tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.Set, sources[value]) != TwRC.Success)
                    throw new Exception(GetTwainStatus());
            }
        }

        public event EventHandler OnFinishScanning;

        public ScannerManager(IntPtr hwnd)
        {
            this.hwnd = hwnd;
            tw = isTwain2Enable ? new Twain64NativeMethods() : (TwainNativeMethods)new Twain32NativeMethods();
            CreateAppId();
            CurrentScanner = -1;
            evtmsg = new TwEvent();
            evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winmsg));
            OpenDSM();
        }

        ~ScannerManager()
        {
            Marshal.FreeHGlobal(evtmsg.EventPtr);
        }

        public string[] GetAllScanners()
        {
            List<string> sourcesNames = new List<string>();
            foreach (TwIdentity source in sources)
            {
                sourcesNames.Add(source.ProductName);
            }
            return sourcesNames.ToArray();
        }

        public void SelectScannerByName(string scannerName)
        {
            if (!sources.Exists(c => c.ProductName == scannerName))
                throw new Exception("Scanner with this name does not exist");
            else
                CurrentScanner = sources.IndexOf(sources.Find(c => c.ProductName == scannerName));

        }

        public void Scan()
        {
            if (OpenDSM() == TwRC.Success)
            {
                if (CurrentScanner < 0)
                    throw new Exception("No scanner selected");
                if (OpenDS() == TwRC.Success)
                {
                    if (EnableDS() != TwRC.Success)
                    {
                        CloseDS();
                        throw new Exception(GetTwainStatus());
                    }
                }
            }
        }

        public string[] SaveScanImages(string imagePath)
        {
            ArrayList scans = TransferPictures();
            string[] files = new string[scans.Count];
            CloseDSM();
            string fileName = Path.GetFileNameWithoutExtension(imagePath);
            for (int i = 0; i < scans.Count; i++)
            {
                files[i] = imagePath.Replace(fileName, fileName + i);
                ImageHelper.SaveDIBAs(files[i], (IntPtr)scans[i]);
            }
            return files;
        }

        private void CreateAppId()
        {
            if (this.appId == null)
            {
                Assembly _asm = typeof(ScannerManager).Assembly;
                AssemblyName _asm_name = new AssemblyName(_asm.FullName);
                Version _version = new Version(((AssemblyFileVersionAttribute)_asm.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0]).Version);

                this.appId = new TwIdentity()
                {
                    Id = 0,
                    Version = new TwVersion()
                    {
                        MajorNum = (ushort)_version.Major,
                        MinorNum = (ushort)_version.Minor,
                        Language = TwLanguage.RUSSIAN,
                        Country = TwCountry.BELARUS,
                        Info = _asm_name.Version.ToString()
                    },
                    ProtocolMajor = (ushort)(isTwain2Enable ? 2 : 1),
                    ProtocolMinor = (ushort)(isTwain2Enable ? 3 : 9),
                    SupportedGroups = TwDG.Image | TwDG.Control | (isTwain2Enable ? TwDG.APP2 : 0),
                    Manufacturer = ((AssemblyCompanyAttribute)_asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company,
                    ProductFamily = "TWAIN Class Library",
                    ProductName = ((AssemblyProductAttribute)_asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product
                };
            }
        }

        private TwRC OpenDSM()
        {
            CloseDSM();
            TwRC rc = tw.DsmParent(appId, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwnd);
            if (rc == TwRC.Success)
            {
                twainState |= TwainStateFlag.DSMOpen;
                GetAllSorces();
                CurrentScanner = DefaultScanner;
            }
            else
            {
                throw new Exception(GetTwainStatus());
            }
            return rc;
        }

        private void GetAllSorces()
        {
            sources = new List<TwIdentity>();
            TwIdentity _item = new TwIdentity();
            for (TwRC _rc = tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetFirst, _item); _rc != TwRC.Success;)
            {
                if (_rc == TwRC.EndOfList)
                {
                    return;
                }
                throw new Exception(GetTwainStatus());
            }
            sources.Add(_item);
            while (true)
            {
                _item = new TwIdentity();
                TwRC _rc = tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetNext, _item);
                if (_rc == TwRC.Success)
                {
                    sources.Add(_item);
                    continue;
                }
                if (_rc == TwRC.EndOfList)
                {
                    break;
                }
                throw new Exception(GetTwainStatus());
            }
            if(sources.Count < 0)
            {
                CloseDSM();
                throw new Exception("Cannot find any scanner");
            }
        }

        private TwRC OpenDS()
        {
            CloseDS();
            TwRC rc = tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, sources[CurrentScanner]);
            if (rc == TwRC.Success)
            {
                twainState |= TwainStateFlag.DSOpen;
            }
            else
                throw new Exception(GetTwainStatus());
            return rc;
        }

        private TwRC EnableDS()
        {
            TwRC rc = SetCapability();
            if (rc == TwRC.Success)
            {
                TwUserInterface guif = new TwUserInterface()
                {
                    ShowUI = false,
                    ModalUI = true,
                    ParentHand = hwnd
                };
                rc = tw.DsUserinterface(appId, sources[CurrentScanner], TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
                if (rc == TwRC.Success)
                    twainState |= TwainStateFlag.DSEnabled;
                else
                    throw new Exception(GetTwainStatus());
            }
            return rc;
        }

        private TwRC SetCapability()
        {
            TwCapability cap = new TwCapability(TwCap.XferCount, 1, TwType.Int16);
            TwRC rc = tw.DsCapability(appId, sources[CurrentScanner], TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
            if (rc != TwRC.Success)
            {
                CloseDS();
                throw new Exception(GetTwainStatus());
            }
            else
                twainState |= TwainStateFlag.DSReady;
            return rc;
        }

        private ArrayList TransferPictures()
        {
            ArrayList pics = new ArrayList();
            if (CurrentScanner == -1)
                return pics;

            TwRC rc;
            TwPendingXfers pendingxfer = new TwPendingXfers();

            do
            {
                pendingxfer.Count = 0;
                IntPtr hbitmap = IntPtr.Zero;

                TwImageInfo imageinfo = new TwImageInfo();
                rc = tw.DsImageInfo(appId, sources[CurrentScanner], TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, imageinfo);
                if (rc != TwRC.Success)
                {
                    CloseDS();
                    return pics;
                }

                rc = tw.DsImagexfer(appId, sources[CurrentScanner], TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                if (rc != TwRC.XferDone)
                {
                    CloseDS();
                    return pics;
                }

                rc = tw.DsPendingxfers(appId, sources[CurrentScanner], TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pendingxfer);
                if (rc != TwRC.Success)
                {
                    CloseDS();
                    return pics;
                }

                pics.Add(hbitmap);
            }
            while (pendingxfer.Count != 0);

            rc = tw.DsPendingxfers(appId, sources[CurrentScanner], TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pendingxfer);
            return pics;
        }

        private string GetTwainStatus()
        {
            TwStatus status = new TwStatus();
            TwRC _rc = tw.DsmStatus(appId, IntPtr.Zero, TwDG.Control, TwDAT.Status, TwMSG.Get, status);
            switch (status.ConditionCode)
            {
                case TwCC.Success:
                    return "It worked!";
                case TwCC.Bummer:
                    return "Failure due to unknown causes.";
                case TwCC.LowMemory:
                    return "Not enough memory to perform operation.";
                case TwCC.NoDS:
                    return "No Data Source.";
                case TwCC.MaxConnections:
                    return "DS is connected to max possible applications.";
                case TwCC.OperationError:
                    return "DS or DSM reported error, application shouldn't.";
                case TwCC.BadCap:
                    return "Unknown capability.";
                case TwCC.BadProtocol:
                    return "Unrecognized MSG DG DAT combination.";
                case TwCC.BadValue:
                    return "Data parameter out of range.";
                case TwCC.SeqError:
                    return "DG DAT MSG out of expected sequence.";
                case TwCC.BadDest:
                    return "Unknown destination Application/Source in DSM_Entry.";
                case TwCC.CapUnsupported:
                    return "Capability not supported by source.";
                case TwCC.CapBadOperation:
                    return "Operation not supported by capability.";
                case TwCC.CapSeqError:
                    return "Capability has dependancy on other capability.";
                /* Added 1.8 */
                case TwCC.Denied:
                    return "File System operation is denied (file is protected).";
                case TwCC.FileExists:
                    return "Operation failed because file already exists.";
                case TwCC.FileNotFound:
                    return "File not found.";
                case TwCC.NotEmpty:
                    return "Operation failed because directory is not empty.";
                case TwCC.PaperJam:
                    return "The feeder is jammed.";
                case TwCC.PaperDoubleFeed:
                    return "The feeder detected multiple pages.";
                case TwCC.FileWriteError:
                    return "Error writing the file (meant for things like disk full conditions).";
                case TwCC.CheckDeviceOnline:
                    return "The device went offline prior to or during this operation.";
                default:
                    return "Unknown error.";
            }

        }

        public ScannerMessages PassMessage(ref Message m)
        {
            if (CurrentScanner == -1)
                return ScannerMessages.Undefined;
            Marshal.StructureToPtr(new WINMSG { hwnd = m.HWnd, message = m.Msg, wParam = m.WParam, lParam = m.LParam }, evtmsg.EventPtr, true);
            evtmsg.Message = TwMSG.Null;
            TwRC rc = tw.DsEvent(appId, sources[CurrentScanner], TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg);
            if (rc == TwRC.NotDSEvent)
                return ScannerMessages.Undefined;
            switch (evtmsg.Message)
            {
                case TwMSG.XFerReady:
                    return ScannerMessages.FinishScanning;
                case TwMSG.CloseDSReq:
                    CloseDS();
                    return ScannerMessages.CloseScannerRequest;
                case TwMSG.CloseDSOK:
                    return ScannerMessages.CloseScannerOk;
                case TwMSG.DeviceEvent:
                    return ScannerMessages.DeviceEvent;
                default:
                    return ScannerMessages.Null;
            }
        }

        private void DisableDS()
        {
            if ((twainState & TwainStateFlag.DSEnabled) != 0)
            {
                TwUserInterface guif = new TwUserInterface()
                {
                    ParentHand = hwnd,
                    ShowUI = false
                };
                if (tw.DsUserinterface(appId, sources[CurrentScanner], TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif) == TwRC.Success)
                {
                    twainState &= ~TwainStateFlag.DSEnabled;
                }
                else
                    throw new Exception(GetTwainStatus());
            }
        }

        private void CloseDS()
        {
            DisableDS();
            if ((twainState & TwainStateFlag.DSOpen) != 0)
            {
                if (tw.DsmIdentity(appId, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, sources[CurrentScanner]) == TwRC.Success)
                    twainState &= ~TwainStateFlag.DSOpen;
                else
                    throw new Exception(GetTwainStatus());
            }
        }

        private void CloseDSM()
        {
            CloseDS();
            if ((twainState & TwainStateFlag.DSMOpen) != 0)
                if (tw.DsmParent(appId, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd) == TwRC.Success)
                    twainState &= ~TwainStateFlag.DSMOpen;
                else
                    throw new Exception(GetTwainStatus());
            appId.Id = 0;
        }

        public bool PreFilterMessage(ref Message m)
        {
            throw new NotImplementedException();
        }
    }
}


