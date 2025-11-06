using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace UnityPatcher
{
    public partial class Form1 : Form
    {
        private void ConsoleMSG(string msg)
        {
            Label zov = new Label();
            zov.Text = msg;
            zov.Padding = Padding.Empty;
            zov.Margin = Padding.Empty;
            zov.BackColor = Color.White;
            zov.ForeColor = Color.Gray;
            zov.Font = new Font("JetBrains Mono", 7, FontStyle.Regular);
            zov.AutoSize = true;
            zov.MaximumSize = new Size(0, 0);
            zov.AutoEllipsis = false;

            flowLayoutPanel1.Controls.Add(zov);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ScrollControlIntoView(zov);
        }

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"
How to use it:
1. Patch game
2. Launch the game once to create a folder at Android/data/packageName/nativemods
3. Close the game
4. From all native libs in the directory will be loaded
(https://github.com/Livku2/ModLoader/tree/master)
", "Guide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string package in Adb.GetPackageList())
            {
                comboBox1.Items.Add(package);
            }
        }
        // @ main shit \ patcher

        public static string zovzovzov(string tempFolder)
        {
            string[] vsovsovsozovzovzov = { "UnityPlayerActivity.smali", "UnityPlayerGameActivity.smali" };
            string shittomodify = null;
            foreach (var f in vsovsovsozovzovzov)
            {
                string p = Path.Combine(tempFolder, "smali", "com", "unity3d", "player", f);
                if (File.Exists(p)) { shittomodify = p; break; }
            }
            if (shittomodify == null) return "err1";
            string classRef = "Lcom/unity3d/player/" + Path.GetFileNameWithoutExtension(shittomodify) + ";";
            List<string> zovzovvsozelenskyputin = File.ReadAllLines(shittomodify).ToList();
            int oncreateline = -1;
            int localsinoncreateline = -1; ///zzzzzzzzzzzzzzzzzzzzzzovvvvvvvvvvvvvvvvvvvvvvvvvvvvv

            for (int i = 0; i < zovzovvsozelenskyputin.Count; i++)
            {
                string test = zovzovvsozelenskyputin[i].Trim();
                if (oncreateline == -1 && test.StartsWith(".method protected onCreate"))
                {
                    oncreateline = i;
                }
                if (oncreateline != -1 && localsinoncreateline == -1 && test.StartsWith(".locals"))
                {
                    localsinoncreateline = i;
                    break;
                }
            }

            if (oncreateline == -1 || localsinoncreateline == -1 || classRef == null) return "err2";
            string inject = $@"
.method private static fefejgjigrgigjrigjjir()V
    .locals 1
    const-string v0, ""ModLoader""
    invoke-static {{v0}}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V
    return-void
.end method
";
            IEnumerable<string> functiontoinsertinsmali = inject.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Skip(1).Append("");
            int tempLinesCount = functiontoinsertinsmali.Count();
            string pcallinsert = FileInserter.InsertMultipleLinesToFile(shittomodify, functiontoinsertinsmali, oncreateline + 1);
            if (pcallinsert != "success") return $"err3: {pcallinsert}";
            localsinoncreateline += tempLinesCount;
            string loadlibrary = $"    invoke-static {{}}, {classRef}->fefejgjigrgigjrigjjir()V";
            string pcallinsert2 = FileInserter.InsertLineToFile(shittomodify, loadlibrary, localsinoncreateline + 2);

            if (pcallinsert2 != "success") return $"err4: {pcallinsert2}";

            return "success";
        }
        public const string LatestModLoader = "https://github.com/Livku2/ModLoader/releases/latest/download/libModLoader.so";
        public static string APKToolPath => Path.Combine(AppContext.BaseDirectory, "apktool.jar");
        public static string TempFolder => Path.Combine(AppContext.BaseDirectory, "Temp");
        public static string Recompiled => Path.Combine(AppContext.BaseDirectory, "Recompiled");
        private async void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            flowLayoutPanel1.Controls.Clear();
            Action<string> uiLog = msg => BeginInvoke(new Action(() => ConsoleMSG(msg)));
            uiLog("Finding APK Path by Package");
            string packagepath = Adb.GetBaseApk(comboBox1.Text);
            string oldpackagename = comboBox1.Text;
            if (string.IsNullOrEmpty(packagepath))
            {
                uiLog("Package path not found");
                comboBox1.Enabled = true;
                return;
            }
            await Task.Run(() =>
            {
                uiLog(packagepath);
                uiLog("Pulling APK");
                Adb.PullFile(packagepath);
                uiLog("Pulled: TOPATCH.apk");
                string outputFile = Path.Combine(AppContext.BaseDirectory, "TOPATCH.apk");
                uiLog("Making temp folders.");
                string recompiledApk = Path.Combine(Recompiled, Path.GetFileName(outputFile));
                string signedPath = Path.Combine(Recompiled, "signed");
                if (Directory.Exists(signedPath))
                {
                    uiLog("Deleting Old Signed Dir (no idea how to call this)");
                    Directory.Delete(signedPath, true);
                }
                if (Directory.Exists(TempFolder))
                {
                    uiLog("Deleting Old TempFolder Dir (no idea how to call this)");
                    Directory.Delete(TempFolder, true);
                }
                if (Directory.Exists(Recompiled))
                {
                    uiLog("Deleting Old Recompiled Dir (no idea how to call this)");
                    Directory.Delete(Recompiled, true);
                }
                Directory.CreateDirectory(TempFolder);
                Directory.CreateDirectory(Recompiled);
                Directory.CreateDirectory(signedPath);
                uiLog("Decompiling APK: " + outputFile);
                CmdUtility.DoCmd("/c " + $"java -jar \"{APKToolPath}\" d \"{outputFile}\" -f -o \"{TempFolder}\"");
                uiLog("Decompiled APK! Downloading ModLoader");
                string libDir = $"{TempFolder}/lib/arm64-v8a";
                byte[] data = new WebClient().DownloadData("https://github.com/Livku2/ModLoader/releases/latest/download/libModLoader.so");
                File.WriteAllBytes(Path.Combine(libDir, "libModLoader.so"), data);
                uiLog("Installed libModLoader.so");
                uiLog("Trying to find Smali File for inject");
                string zovvsosigma = zovzovzov(TempFolder);
                if (zovvsosigma == "err1")
                {
                    uiLog("UnityPlayer smali file not found");
                    MessageBox.Show("ERROR! Check console (output)", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (zovvsosigma == "err2")
                {
                    uiLog("onCreate method not found");
                    MessageBox.Show("ERROR! Check console (output)", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (zovvsosigma == "err3")
                {
                    uiLog(".end method not found for onCreate");
                    MessageBox.Show("ERROR! Check console (output)", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (zovvsosigma == "err4")
                {
                    uiLog("loadLibrary call already exists");
                    MessageBox.Show("ERROR! Check console (output)", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                uiLog("Injected libModLoader.so! Recompiling");
                CmdUtility.DoCmd("/c " + $"java -jar \"{APKToolPath}\" b \"{TempFolder}\" -f -o \"{recompiledApk}\"");
                uiLog("Recompiled! Signing.");
                CmdUtility.DoCmd($"/c java -jar \"{Path.Combine(AppContext.BaseDirectory, "uber-apk-signer.jar")}\" -a \"{recompiledApk}\" --out \"{signedPath}\"");
                uiLog("Signed");
                if (checkBox1.Checked)
                {
                    uiLog("Deleting TempFolder");
                    Directory.Delete(TempFolder, true);
                }
                string apkname = Path.GetFileNameWithoutExtension(recompiledApk);
                string signedApk = Path.Combine(AppContext.BaseDirectory, "Recompiled", "signed", apkname + "-aligned-debugSigned.apk");
                if (checkBox2.Checked)
                {
                    if (File.Exists(signedApk))
                    {
                        uiLog("Deleting Old Package");
                        CmdUtility.DoCmd($"/c adb uninstall {oldpackagename}");
                        uiLog("Installing new Package");
                        CmdUtility.DoCmd($"/c adb install {signedApk}");
                        uiLog("Done");
                    }
                    else
                    {
                        uiLog("Signed apk not created!");
                        uiLog("Probally error while trying build apk.");
                        return;
                    }
                }
                else
                {
                    Process.Start("explorer.exe", Path.GetDirectoryName(signedApk));
                    uiLog("Done");
                }
            });
            comboBox1.Enabled = true;
        }

        const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;

        enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        static void DisableRoundedCorners(IntPtr hWnd)
        {
            int pref = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(int));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fontName = "JetBrains Mono";
            float fontSize = 12;

            using (Font fontTester = new Font(
                   fontName,
                   fontSize,
                   FontStyle.Regular,
                   GraphicsUnit.Pixel))
            {
                if (fontTester.Name != fontName)
                {
                    if (MessageBox.Show("You need to install JetBrains Mono Font!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        Process.Start(new ProcessStartInfo() { FileName = "https://fonts.google.com/specimen/JetBrains+Mono", UseShellExecute = true });
                    }
                    ;
                }
            }
            if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "PulledAPPS")))
            {
                Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "PulledAPPS"));
                MessageBox.Show("Created Pulled APPS Directory", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            DisableRoundedCorners(this.Handle);
            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "apktool.jar")))
            {
                comboBox1.Enabled = false;
                ConsoleMSG("Installing apktool.jar");
                Dependices.apktool();
                ConsoleMSG("Installed apktool.jar");
            }
            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "uber-apk-signer.jar")))
            {
                comboBox1.Enabled = false;
                ConsoleMSG("Installing uber-apk-signer.jar");
                Dependices.uberapksigner();
                ConsoleMSG("Installed uber-apk-signer.jar");
            }
            comboBox1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach (string package in Adb.GetPackageList())
            {
                comboBox2.Items.Add(package);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            button5.Enabled = false;
            string packagepath = Adb.GetBaseApk(comboBox2.Text);
            string newpath = Path.Combine(AppContext.BaseDirectory, "PulledAPPS", comboBox2.Text.Replace(".", "-") + ".apk");
            Adb.PullFile(packagepath, null, newpath);
            comboBox2.Enabled = true;
            button5.Enabled = true;
            if (MessageBox.Show("Pulled " + comboBox2.Text + "\n Open PulledAPPS Folder?", "Easy Patcher", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Process.Start("explorer.exe", Path.Combine(AppContext.BaseDirectory, "PulledAPPS"));
            }
        }
    }
}
