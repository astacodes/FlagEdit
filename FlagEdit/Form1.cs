using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace FlagEdit
{
    public partial class FlagEdit : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Point mouseLocation;

        Dictionary<string, object> Freecam = new Dictionary<string, object> {
            { "DFFlagDebugSimulateHangAtStartup", "True" }
        };

        Dictionary<string, object> NoClipFlags = new Dictionary<string, object> {
            { "FFlagDebugSimDefaultPrimalSolver", "True" },
            { "DFIntMaximumFreefallMoveTimeInTenths", "99999" },
            { "DFIntDebugSimPrimalStiffness", "0" }
        };

        Dictionary<string, object> NoAdFlags = new Dictionary<string, object> {
            { "FFlagAdServiceEnabled", "False" }
        };

        Dictionary<string, object> LowGravityFlags = new Dictionary<string, object> {
            { "FFlagDebugSimDefaultPrimalSolver", "True" },
            { "DFIntDebugSimPrimalLineSearch", "3" }
        };

        Dictionary<string, object> NoAnimationFlags = new Dictionary<string, object> {
            { "DFIntReplicatorAnimationTrackLimitPerAnimator", "-1" }
        };

        Dictionary<string, object> NoTelemetryFlags = new Dictionary<string, object> {
            { "FFlagDebugDisableTelemetryEphemeralCounter", "True" },
            { "FFlagDebugDisableTelemetryEphemeralStat", "True" },
            { "FFlagDebugDisableTelemetryEventIngest", "True" },
            { "FFlagDebugDisableTelemetryPoint", "True" },
            { "FFlagDebugDisableTelemetryV2Counter", "True" },
            { "FFlagDebugDisableTelemetryV2Event", "True" },
            { "FFlagDebugDisableTelemetryV2Stat", "True" }
        };

        Dictionary<string, object> DisableTouchEventsFlags = new Dictionary<string, object> {
            { "DFIntTouchSenderMaxBandwidthBps", "-1" }
        };

        Dictionary<string, object> DisableRemoteEventsFlags = new Dictionary<string, object> {
            { "DFIntRemoteEventSingleInvocationSizeLimit", "1" }
        };

        Dictionary<string, object> CicleUnderAvatarFlags = new Dictionary<string, object> {
            { "FFlagDebugAvatarChatVisualization", "True" },
            { "FFlagEnableInGameMenuChromeABTest2", "False" }
        };

        Dictionary<string, object> NetworkOwnershipFlags = new Dictionary<string, object> {
            { "DFIntMinClientSimulationRadius", "2147000000" },
            { "DFIntMinimalSimRadiusBuffer", "2147000000" },
            { "DFIntMaxClientSimulationRadius", "2147000000" }
        };

        Dictionary<string, object> BoxEsp = new Dictionary<string, object> {
            { "DFFlagDebugDrawBroadPhaseAABBs", "True" }
        };

        Dictionary<string, object> ForceDirectX10 = new Dictionary<string, object> {
            { "FFlagDebugGraphicsDisableDirect3D11", "False" },
            { "FFlagDebugGraphicsPreferD3D11", "False" },
            { "FFlagDebugGraphicsPreferD3D11FL10", "True" },
            { "FFlagGraphicsEnableD3D10Compute", "True" }
        };

        Dictionary<string, object> FpsBoost = new Dictionary<string, object> {
            { "FFlagDebugGraphicsPreferD3D11FL10", "True" },
            { "FFlagGameBasicSettingsFramerateCap5", "False" },
            { "DFIntTaskSchedulerTargetFps", "5588562" },
            { "FFlagTaskSchedulerLimitTargetFpsTo2402", "False" },
            { "DFIntMaxFrameBufferSize", "4" },
            { "FIntDebugForceMSAASamples", "0" },
            { "DFFlagDebugPerfMode", "True" },
            { "FFlagFixGraphicsQuality", "True" },
            { "DFFlagDisableDPIScale", "True" },
            { "FFlagHandleAltEnterFullscreenManually", "False" },
            { "DFFlagDebugRenderForceTechnologyVoxel", "True" },
            { "DFFlagVoxelizerDisableTerrainSIMD", "True" },
            { "DFFlagDebugSkipMeshVoxelizer", "True" },
            { "FIntRenderShadowIntensity", "0" },
            { "DFIntCullFactorPixelThresholdShadowMapHighQuality", "2147483647" },
            { "DFIntCullFactorPixelThresholdShadowMapLowQuality", "2147483647" },
            { "FIntRenderLocalLightUpdatesMax", "1" },
            { "FIntRenderLocalLightUpdatesMin", "1" },
            { "FFlagDebugRenderingSetDeterministic", "True" },
            { "FIntTerrainArraySliceSize", "8" },
            { "FIntFRMMinGrassDistance", "0" },
            { "FIntFRMMaxGrassDistance", "0" },
            { "FIntRenderGrassDetailStrands", "0" }
        };

        Dictionary<string, object> GraySky = new Dictionary<string, object> {
            { "FFlagDebugSkyGray", "True" }
        };

        Dictionary<string, object> HitboxExpander = new Dictionary<string, object> {
            { "FFlagDebugSimIntegrationStabilityTesting", "500" }
        };

        Dictionary<string, object> NoPlayerShadow = new Dictionary<string, object> {
            { "FIntRenderShadowIntensity", 0 }
        };

        Dictionary<string, object> NoClip = new Dictionary<string, object> {
            { "DFIntAssemblyExtentsExpansionStudHundredth", "-150" }
        };

        Dictionary<string, object> NoClip2 = new Dictionary<string, object> {
            { "DFFlagAssemblyExtentsExpansionStudHundredth", "-50" },
            { "FIntPGSPenetrationMarginMax", "199999999" },
            { "FIntPGSPenetrationMarginMin", "100000000" }
        };

        Dictionary<string, object> NoClip3 = new Dictionary<string, object> {
            { "FIntPGSPenetrationMarginMax", "2147483647" },
            { "FIntPGSPenetrationMarginMin", "2147483647" }
        };

        Dictionary<string, object> SpinWhileMoving = new Dictionary<string, object> {
            { "FFlagDebugSimDefaultPrimalSolver", "True" },
            { "FIntDebugSimPrimalGSLumpAlpha", "-2147483647" },
            { "DFIntDebugSimPrimalPreconditioner", "1100" },
            { "DFIntDebugSimPrimalPreconditionerMinExp", "1000" },
            { "DFIntDebugSimPrimalNewtonIts", "2" },
            { "DFIntDebugSimPrimalWarmstartVelocity", "102" },
            { "DFIntDebugSimPrimalWarmstartForce", "-800" },
            { "DFIntDebugSimPrimalToleranceInv", "1" }
        };

        Dictionary<string, object> UnCappedFps = new Dictionary<string, object> {
            { "DFIntTaskSchedulerTargetFps", "9999" }
        };

        private Timer timer;

        public FlagEdit()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            base.OnFormClosing(e);
        }

        public string GetRobloxFolder()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string robloxPath = Path.Combine(localAppData, "Roblox", "Versions");

            foreach (var directory in Directory.GetDirectories(robloxPath))
            {
                if (File.Exists(Path.Combine(directory, "RobloxPlayerBeta.exe")))
                {
                    return directory;
                }
            }

            Console.WriteLine("Roblox installation not found.");
            return null;
        }

        public string CreateClientSettings()
        {
            string folder = GetRobloxFolder();
            if (folder == null)
            {
                Console.WriteLine("Roblox installation not found.");
                return null;
            }

            string settingsFolder = Path.Combine(folder, "ClientSettings");
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
                Console.WriteLine($"Created ClientSettings folder at: {settingsFolder}");
            }

            return settingsFolder;
        }

        public void EnsureJsonExists()
        {
            string settingsFolder = CreateClientSettings();
            if (settingsFolder != null)
            {
                string jsonPath = Path.Combine(settingsFolder, "ClientAppSettings.json");
                if (!File.Exists(jsonPath))
                {
                    File.WriteAllText(jsonPath, JsonConvert.SerializeObject(new Dictionary<string, object>(), Formatting.Indented));
                    Console.WriteLine($"Initialized JSON at {jsonPath}. Restart Roblox to see changes.");
                }
            }
        }

        public void SaveJson(Dictionary<string, object> data)
        {
            string settingsFolder = CreateClientSettings();
            if (settingsFolder != null)
            {
                string jsonPath = Path.Combine(settingsFolder, "ClientAppSettings.json");
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(data, Formatting.Indented));
                Console.WriteLine($"Saved JSON to {jsonPath}. Restart Roblox to see changes.");
            }
        }

        public Dictionary<string, object> LoadJson()
        {
            string settingsFolder = CreateClientSettings();
            if (settingsFolder == null) return new Dictionary<string, object>();

            string jsonPath = Path.Combine(settingsFolder, "ClientAppSettings.json");
            if (File.Exists(jsonPath))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(jsonPath));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading JSON: {e.Message}");
                    EnsureJsonExists();
                }
            }
            return new Dictionary<string, object>();
        }

        public void ToggleFFlag(string flagName, Dictionary<string, object> flagData)
        {
            var data = LoadJson();
            bool flagEnabled = flagData.All(flag => data.ContainsKey(flag.Key));

            if (flagEnabled)
            {
                Console.WriteLine($"Disabling {flagName}...");
                foreach (var flag in flagData.Keys)
                {
                    if (data.ContainsKey(flag))
                    {
                        data.Remove(flag);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Enabling {flagName}...");
                foreach (var flag in flagData)
                {
                    data[flag.Key] = flag.Value;
                }
            }

            SaveJson(data);
        }

        public void SetHipHeight(int hipHeightValue)
        {
            var data = LoadJson();
            data["DFIntMaxAltitudePDStickHipHeightPercent"] = hipHeightValue.ToString();
            SaveJson(data);
            Console.WriteLine($"Set Hip Height to {hipHeightValue}.");
        }

        public void SetZoomDistance(int zoomDistance)
        {
            var data = LoadJson();
            data["FIntCameraMaxZoomDistance"] = zoomDistance.ToString();
            SaveJson(data);
            Console.WriteLine($"Set Max Zoom Distance to {zoomDistance}.");
        }

        public void SetFlags(Dictionary<string, object> flagData)
        {
            var data = LoadJson();
            foreach (var flag in flagData)
            {
                data[flag.Key] = flag.Value;
            }
            SaveJson(data);
        }

        public void AddCustomFflag(string customFflag, string customValue)
        {
            if (!string.IsNullOrEmpty(customFflag) && !string.IsNullOrEmpty(customValue))
            {
                var data = LoadJson();
                data[customFflag] = customValue;
                SaveJson(data);
                Console.WriteLine($"Added custom FFlag: {customFflag} = {customValue}");
            }
            else
            {
                Console.WriteLine("Please enter both FFlag and value.");
            }
        }

        public void ModifyCustomFflag(string customFflag, string customValue)
        {
            if (!string.IsNullOrEmpty(customFflag) && !string.IsNullOrEmpty(customValue))
            {
                var data = LoadJson();
                if (data.ContainsKey(customFflag))
                {
                    data[customFflag] = customValue;
                    SaveJson(data);
                    Console.WriteLine($"Modified custom FFlag: {customFflag} = {customValue}");
                }
                else
                {
                    Console.WriteLine($"FFlag {customFflag} does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Please enter both FFlag and value.");
            }
        }

        public void DeleteCustomFflag(string customFflag)
        {
            if (!string.IsNullOrEmpty(customFflag))
            {
                var data = LoadJson();
                if (data.ContainsKey(customFflag))
                {
                    data.Remove(customFflag);
                    SaveJson(data);
                    Console.WriteLine($"Deleted custom FFlag: {customFflag}");
                }
                else
                {
                    Console.WriteLine($"FFlag {customFflag} does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Please enter the FFlag to delete.");
            }
        }

        public string LoadData()
        {
            string settingsFolder = CreateClientSettings();
            if (settingsFolder == null) return "{}";

            string jsonPath = Path.Combine(settingsFolder, "ClientAppSettings.json");
            if (File.Exists(jsonPath))
            {
                return File.ReadAllText(jsonPath);
            }
            return "Unable to load FFlags";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            textBox3.Text = LoadData();
        }

        public void ClearJson()
        {
            SaveJson(new Dictionary<string, object>());
            Console.WriteLine("Cleared entire ClientAppSettings.json file.");
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            return;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("Noclip", NoClipFlags); // works
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("NoAds", NoAdFlags);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("LowGravity", LowGravityFlags); // works
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> NoKnockback = new Dictionary<string, object> {
                { "DFIntDebugSimPrimalPreconditionerMinExp", "1" },
                { "DFIntDebugSimPrimalNewtonIts", "2" },
                { "DFIntDebugSimPrimalWarmstartForce", "0" },
                { "FFlagDebugSimDefaultPrimalSolver", "True" },
                { "DFIntDebugSimPrimalWarmstartVelocity", "0" },
                { "DFIntDebugSimPrimalToleranceInv", "1" },
                { "DFIntDebugSimPrimalPreconditioner", "1" }
            };

            ToggleFFlag("NoKnockback", NoKnockback);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("NoTelemetry", NoTelemetryFlags); // unknown
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("DisableTouchEvents", DisableTouchEventsFlags); // unknown
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("DisableRemoteEvents", DisableRemoteEventsFlags); // unknown
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("CircleUnderAvatar", CicleUnderAvatarFlags); // works
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("NetworkOwnership", NetworkOwnershipFlags); // unknown
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearJson();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x = 0;
            Int32.TryParse(textBox1.Text, out x);

            if (x != 0)
            {
                SetHipHeight(x);
            } else
            {
                Dictionary<string, object> Data = new Dictionary<string, object> {
                    { "DFIntMaxAltitudePDStickHipHeightPercent", "0" },
                };

                ToggleFFlag("HipHeight", Data);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x = 0;
            Int32.TryParse(textBox1.Text, out x);

            if (x != 0)
            {
                SetZoomDistance(x);
            }
            else
            {
                Dictionary<string, object> Data = new Dictionary<string, object> {
                    { "FIntCameraMaxZoomDistance", "0" },
                };

                ToggleFFlag("ZoomDistance", Data);
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("ESP", BoxEsp);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("FPSBoost", FpsBoost); // unknown
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> Fling = new Dictionary<string, object> {
                { "DFIntDebugSimPrimalNewtonIts", "2" },
                { "DFIntDebugSimPrimalPreconditioner", "1100" },
                { "DFIntDebugSimPrimalPreconditionerMinExp", "1000" },
                { "DFIntDebugSimPrimalToleranceInv", "1" },
                { "DFIntDebugSimPrimalWarmstartForce", "-800" },
                { "DFIntDebugSimPrimalWarmstartVelocity", "102" },
                { "FFlagDebugSimDefaultPrimalSolver", "True" },
                { "FIntDebugSimPrimalGSLumpAlpha", "-2147483647" }
            };

            ToggleFFlag("Fling", Fling);
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> Speed = new Dictionary<string, object> {
                { "DFIntDebugSimPrimalWarmstartForce", "-285" },
                { "DFIntDebugSimPrimalWarmstartVelocity", "750" },
                { "FIntDebugSimPrimalGSLumpAlpha", "-2147483647" },
                { "FFlagDebugSimDefaultPrimalSolver", "True" },
                { "DFIntDebugSimPrimalPreconditioner", "100" },
                { "DFIntDebugSimPrimalPreconditionerMinExp", "1000" },
                { "DFIntDebugSimPrimalNewtonIts", "1" },
                { "DFIntDebugSimPrimalToleranceInv", "10" },
                { "DFFlagSimHumanoidTimestepModelUpdate", "True" },
                { "FFlagSimAdaptiveTimesteppingDefault2", "True" },
                { "DFIntDebugSimPrimalLineSearch", "100" }
            };

            ToggleFFlag("Speed", Speed);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("Freecam", Freecam); // untested
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("NoFPSCap", UnCappedFps); // unknown
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("NoShadow", NoPlayerShadow); // works
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("GraySky", GraySky); // works
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("Hitboxes", HitboxExpander); // unknown
        } 

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            ToggleFFlag("SpinOnMove", SpinWhileMoving); // semi working
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
