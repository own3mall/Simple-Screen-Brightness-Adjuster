using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleScreenBright.Classes;
using System.Management;
using System.Text.RegularExpressions;
using SimpleScreenBright.Helpers;
using System.IO;

namespace SimpleScreenBright
{
    public partial class SimpleScreenBright : Form
    {
        public System.Drawing.Size originalSize = new Size(0, 0);
        public Dictionary<IntPtr, List<IntPtr>> Ptrs = new Dictionary<IntPtr, List<IntPtr>>();
        public List<MonitorSetting> CurrentSettings = new List<MonitorSetting>();
        public BrightnessControl brightnessControl;
        Regex digitsOnly = new Regex(@"[^\d]");
        string handleKeyword = "Handle";
        string physicalPtrPlace = "Sub";
        string monitorKeyword = "Monitor";
        string deviceKeyword = "Device";
        string sliderKeyword = "Slider";
        string textBoxKeyword = "TB";
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;

        public SimpleScreenBright()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SimpleScreenBright_Load(object sender, EventArgs e)
        {
            DetectMonitors();
            SetAutoScrollOnTabPages();
        }

        private void SetAutoScrollOnTabPages()
        {
            foreach (TabPage control in this.monitorBrightnessTabControl.TabPages)
            {
                control.AutoScroll = true;
            }
        }

        private void DetectMonitors()
        {
            IntPtr hWnd = this.Handle;
            Ptrs.Clear();
            brightnessControl = new BrightnessControl();

            for (int i = 0; i < Screen.AllScreens.Count(); i++)
            {
                var handle = brightnessControl.GetScreenPtr(Screen.AllScreens[i]);

                var deviceName = Screen.AllScreens[i].DeviceName;

                var updatedPtrs = brightnessControl.SetupMonitors(handle);

                Ptrs.Add(handle, updatedPtrs.ToList());

                int p = 0;
                foreach (var monitor in updatedPtrs)
                {

                    if (!monitorBrightnessTabControl.TabPages.ContainsKey((i + 1).ToString()))
                    {
                        monitorBrightnessTabControl.TabPages.Add(monitorKeyword + " " + (i + 1).ToString());
                    }

                    var brightNess = brightnessControl.GetBrightnessCapabilities(monitor);

                    Label newLabel = new Label();
                    newLabel.Text = "Brightness:";
                    newLabel.Size = new System.Drawing.Size(300, 20);
                    newLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    newLabel.AutoSize = true;

                    TrackBar newBar = new TrackBar();
                    newBar.Name = sliderKeyword + monitorKeyword + (i + 1).ToString() + physicalPtrPlace + (monitor + 1).ToString() + deviceKeyword + deviceName + handleKeyword + handle.ToString();
                    newBar.Maximum = brightNess.maximum;
                    newBar.Minimum = brightNess.minimum;
                    newBar.Value = brightNess.current;
                    newBar.Width = 300;
                    newBar.Height = 20 * (p + 1);
                    ((System.ComponentModel.ISupportInitialize)(newBar)).BeginInit();
                    newBar.Location = new System.Drawing.Point(6, 25 * (p + 1));
                    newBar.Scroll += new System.EventHandler(AdjustBrightness_Scroll);
                    newBar.ValueChanged += new System.EventHandler(Trackbar_ValueChanged);

                    TextBox newTB = new TextBox();
                    newTB.Size = new System.Drawing.Size(50, 20);
                    newTB.Text = brightNess.current.ToString();
                    newTB.Location = new System.Drawing.Point(315, 25 * (p + 1));
                    newTB.Name = textBoxKeyword + monitorKeyword + (i + 1).ToString() + physicalPtrPlace + (monitor + 1).ToString() + deviceKeyword + deviceName + handleKeyword + handle.ToString();
                    newTB.TextChanged += new System.EventHandler(textBox_TextChanged);

                    monitorBrightnessTabControl.TabPages[i].Controls.Add(newLabel);
                    monitorBrightnessTabControl.TabPages[i].Controls.Add(newBar);
                    monitorBrightnessTabControl.TabPages[i].Controls.Add(newTB);

                    // Doesn't even remotely work
                    // string niceDisplayName = brightnessControl.GetFriendlyDisplayNameNONWorking(monitor);

                    p++;
                }
            }

            // Populate current settings
            CurrentSettings = GetAllBrightNessSettings();
        }

        

        private void AdjustBrightness_Scroll(object sender, EventArgs e)
        {
            HandleSliderValueChanged(sender, e);
        }

        private void Trackbar_ValueChanged(object sender, EventArgs e)
        {
            HandleSliderValueChanged(sender, e);
        }

        private void HandleSliderValueChanged(object sender, EventArgs e)
        {
            TrackBar sndr = (TrackBar)sender;

            // Get which handle we're playing 
            MonitorSetting currentSetting = getSettingsFromControlName(sndr.Name);

            brightnessControl.SetBrightness(Convert.ToInt16(sndr.Value), currentSetting.PhysicalHandle);

            Control control = FindControlRecursive(monitorBrightnessTabControl, textBoxKeyword + monitorKeyword + (currentSetting.MonitorNumber + 1).ToString() + physicalPtrPlace + (currentSetting.PhysicalHandle + 1).ToString() + deviceKeyword + currentSetting.DeviceName);
            if (control != null)
            {
                TextBox txtBox = control as TextBox;
                if (txtBox != null)
                {
                    txtBox.Text = sndr.Value.ToString();
                }
            }
        }

        private MonitorSetting getSettingsFromControlName(string name)
        {
            MonitorSetting setting = new MonitorSetting();

            string handle = name.Substring(name.LastIndexOf(handleKeyword) + handleKeyword.Length);
            int handleNum = Convert.ToInt32(digitsOnly.Replace(handle, ""));

            setting.Handle = new IntPtr(handleNum);

            string physicalPtr = name.Substring(name.LastIndexOf(physicalPtrPlace) + physicalPtrPlace.Length);
            string physicalPtrAdjusted = physicalPtr.Substring(0, physicalPtr.LastIndexOf(deviceKeyword));
            int location = Convert.ToInt32(digitsOnly.Replace(physicalPtrAdjusted, "")) - 1;

            setting.PhysicalHandle = new IntPtr(location);

            string monitor = name.Substring(name.LastIndexOf(monitorKeyword) + monitorKeyword.Length);
            string monitorNumber = monitor.Substring(0, monitor.LastIndexOf(physicalPtrPlace));
            int monitorNum = Convert.ToInt32(digitsOnly.Replace(monitorNumber, "")) - 1;

            setting.MonitorNumber = monitorNum;

            string device = name.Substring(name.LastIndexOf(deviceKeyword) + deviceKeyword.Length);
            device = device.Substring(0, device.LastIndexOf(handleKeyword));

            setting.DeviceName = device;

            Control control = FindControlRecursive(monitorBrightnessTabControl, sliderKeyword + monitorKeyword + (setting.MonitorNumber + 1).ToString() + physicalPtrPlace + (setting.PhysicalHandle + 1).ToString() + deviceKeyword + device);
            if (control != null)
            {
                TrackBar bar = control as TrackBar;
                if (bar != null)
                {
                    setting.Brightness = bar.Value;
                }
            }

            return setting;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox sndr = (TextBox)sender;
            MonitorSetting currentSetting = getSettingsFromControlName(sndr.Name);

            if (GenericHelper.IsNumeric(sndr.Text))
            {
                Control control = FindControlRecursive(monitorBrightnessTabControl, sliderKeyword + monitorKeyword + (currentSetting.MonitorNumber + 1).ToString() + physicalPtrPlace + (currentSetting.PhysicalHandle + 1).ToString() + deviceKeyword + currentSetting.DeviceName);
                if (control != null)
                {
                    TrackBar bar = control as TrackBar;
                    if (bar != null)
                    {
                        int val = Convert.ToInt32(sndr.Text);
                        if (val <= bar.Maximum && val >= bar.Minimum)
                        {
                            bar.Value = val;
                        }
                        else if (val < bar.Minimum)
                        {
                            bar.Value = bar.Minimum;
                            sndr.Text = bar.Minimum.ToString();
                        }
                        else if (val > bar.Maximum)
                        {
                            bar.Value = bar.Maximum;
                            sndr.Text = bar.Maximum.ToString();
                        }
                    }
                }
            }
        }

        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "simple screen bright profiles (*.ssbp)|*.ssbp|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.InitialDirectory = currentDir;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Can use dialog.FileName
                    using (Stream stream = saveFileDialog.OpenFile())
                    {
                        CurrentSettings = GetAllBrightNessSettings();
                        GenericHelper.WriteToBinaryFileStream(stream, CurrentSettings);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Error saving monitor settings to the selected file. " + E, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<MonitorSetting> GetAllBrightNessSettings()
        {
            List<MonitorSetting> settings = new List<MonitorSetting>();

            foreach (Control x in ReturnAllControlsRecursive(monitorBrightnessTabControl))
            {
                if (x is TrackBar)
                {
                    settings.Add(getSettingsFromControlName(((TrackBar)x).Name));
                }
            }

            return settings;
        }

        private void loadProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = currentDir;
            openFileDialog.Filter = "simple screen bright profiles (*.ssbp)|*.ssbp|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = openFileDialog.OpenFile())
                {
                    try
                    {
                        CurrentSettings = GenericHelper.ReadFromBinaryFileStream<List<MonitorSetting>>(stream);
                        
                        // Now apply the settings
                        foreach (var currentSetting in CurrentSettings)
                        {
                            Control control = FindControlRecursive(monitorBrightnessTabControl, sliderKeyword + monitorKeyword + (currentSetting.MonitorNumber + 1).ToString() + physicalPtrPlace + (currentSetting.PhysicalHandle + 1).ToString() + deviceKeyword + currentSetting.DeviceName.ToString());
                            if (control != null)
                            {
                                TrackBar bar = control as TrackBar;
                                if (bar != null)
                                {
                                    bar.Value = currentSetting.Brightness;
                                }
                            }
                        }
                    }
                    catch(Exception E)
                    {
                        MessageBox.Show("Failed to load settings file... Are you sure this is a valid profile file? " + E, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Control FindControlRecursive(Control root, string name)
        {
            if (root.Name.StartsWith(name)) return root;
            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, name);
                if (t != null) return t;
            }
            return null;
        }

        private List<Control> ReturnAllControlsRecursive(Control root)
        {
            List<Control> Controls = new List<Control>();

            foreach (Control c in root.Controls)
            {
                Controls.AddRange(ReturnAllControlsRecursive(c));
            }
            Controls.Add(root);

            return Controls;
        }

        private void redetectMonitorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // This doesn't work with multiple monitors for some reason... only the first physicalmonitor is destroyed successfully... the next one throws an error:
            // -1071241844 (ERROR_GRAPHICS_INVALID_PHYSICAL_MONITOR_HANDLE)
            /*
            DestroyMonitors();
            monitorBrightnessTabControl.TabPages.Clear();
            DetectMonitors();
            */

            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            this.Close(); //to turn off current app
        }

        private void DestroyMonitors()
        {
            List<NativeStructures.PHYSICAL_MONITOR> monsToDestroy = CurrentSettings.Select(p => new NativeStructures.PHYSICAL_MONITOR { hPhysicalMonitor = p.PhysicalHandle, szPhysicalMonitorDescription = p.DeviceName }).ToList();
            monsToDestroy = monsToDestroy.OrderByDescending(c => c.szPhysicalMonitorDescription).ToList();
            brightnessControl.DestroyMonitors(monsToDestroy.ToArray(), Convert.ToUInt32(monsToDestroy.Count()));            
        }
    }
}
