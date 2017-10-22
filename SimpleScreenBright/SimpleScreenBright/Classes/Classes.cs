using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleScreenBright.Classes
{
    public class Classes
    {

    }

    public class BrightnessInfo
    {
        public int minimum { get; set; }
        public int maximum { get; set; }
        public int current { get; set; }
    }

    [Serializable]
    public class MonitorSetting
    {
        public IntPtr Handle { get; set; }
        public IntPtr PhysicalHandle { get; set; }
        public int Brightness { get; set; }
        public int MonitorNumber { get; set; }
        public string DeviceName { get; set; }
    }
}
