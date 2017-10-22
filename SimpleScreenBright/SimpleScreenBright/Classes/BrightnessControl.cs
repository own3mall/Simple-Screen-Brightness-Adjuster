using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using SimpleScreenBright;
using System.Windows.Forms; 

namespace SimpleScreenBright.Classes
{
    public class BrightnessControl
    {

        private uint pdwNumberOfPhysicalMonitors;
        private NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray;

        public BrightnessControl()
        {

        }

        public IntPtr[] SetupMonitors(IntPtr hMonitor)
        {
            bool numberOfPhysicalMonitorsFromHmonitor = NativeCalls.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref pdwNumberOfPhysicalMonitors);
            int lastWin32Error = Marshal.GetLastWin32Error();

            pPhysicalMonitorArray = new NativeStructures.PHYSICAL_MONITOR[pdwNumberOfPhysicalMonitors];
            bool physicalMonitorsFromHmonitor = NativeCalls.GetPhysicalMonitorsFromHMONITOR(hMonitor, pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
            lastWin32Error = Marshal.GetLastWin32Error();

            return pPhysicalMonitorArray.ToList().Select(c => c.hPhysicalMonitor).ToArray();
        }

        public IntPtr GetScreenPtr(Screen screen)
        {
            var pnt = new System.Drawing.Point(screen.Bounds.Left + 1, screen.Bounds.Top + 1);
            return NativeCalls.MonitorFromPoint(pnt, 2/*MONITOR_DEFAULTTONEAREST*/);
        }

        public void GetMonitorCapabilities(IntPtr monitorNumber)
        {
            uint pdwMonitorCapabilities = 0u;
            uint pdwSupportedColorTemperatures = 0u;
            var monitorCapabilities = NativeCalls.GetMonitorCapabilities(monitorNumber, ref pdwMonitorCapabilities, ref pdwSupportedColorTemperatures);
            Debug.WriteLine(pdwMonitorCapabilities);
            Debug.WriteLine(pdwSupportedColorTemperatures);
            int lastWin32Error = Marshal.GetLastWin32Error();
            NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE type = NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE.MC_SHADOW_MASK_CATHODE_RAY_TUBE;
            var monitorTechnologyType = NativeCalls.GetMonitorTechnologyType(monitorNumber, ref type);
            Debug.WriteLine(type);
            lastWin32Error = Marshal.GetLastWin32Error();
        }

        public bool SetBrightness(short brightness, IntPtr monitorNumber)
        {
            var brightnessWasSet = NativeCalls.SetMonitorBrightness(monitorNumber, (short)brightness);
            if (brightnessWasSet)
                Debug.WriteLine("Brightness set to " + (short)brightness);
            int lastWin32Error = Marshal.GetLastWin32Error();
            return brightnessWasSet;
        }

        public BrightnessInfo GetBrightnessCapabilities(IntPtr monitorNumber)
        {
            short current = -1, minimum = -1, maximum = -1;

            bool getBrightness = NativeCalls.GetMonitorBrightness(monitorNumber,ref minimum,ref current,ref maximum);
            int lastWin32Error = Marshal.GetLastWin32Error();
            return new BrightnessInfo { minimum = minimum, maximum = maximum, current = current};
        }

        public void DestroyMonitors(NativeStructures.PHYSICAL_MONITOR[] monitors, uint number)
        {
            var destroyPhysicalMonitors = NativeCalls.DestroyPhysicalMonitors(number, monitors);
            int lastWin32Error = Marshal.GetLastWin32Error();
        }

        // Doens't work.
        /*
        public string GetFriendlyDisplayNameNONWorking(IntPtr monitor)
        {
            string displayName = NativeCalls.EnumDisplayMonitors(monitor, IntPtr.Zero, NativeCode.MonitorEnumProc, IntPtr.Zero);
            return displayName;
        }
        */
        
    }
}
