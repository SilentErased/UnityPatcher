using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityPatcher
{
    public partial class Form1 : Form
    {
        private const string ModLoaderDownloadUrl = "https://github.com/Livku2/ModLoader/releases/latest/download/libModLoader.so";
        private static string ApktoolJarPath => Path.Combine(AppContext.BaseDirectory, "apktool.jar");
        private static string UberApkSignerPath => Path.Combine(AppContext.BaseDirectory, "uber-apk-signer.jar");
        private static string Il2CppDumperPath => Path.Combine(AppContext.BaseDirectory, "Il2CppDumper", "Il2CppDumper.exe");
        private static string TempDecompileFolder => Path.Combine(AppContext.BaseDirectory, "Temp");
        private static string RecompiledFolder => Path.Combine(AppContext.BaseDirectory, "Recompiled");
        private static string PulledAppsFolder => Path.Combine(AppContext.BaseDirectory, "PulledAPPS");
        private static string DumpsFolder => Path.Combine(AppContext.BaseDirectory, "Dumps");
        private string _selectedPackage = null;
        private TabControl libraryVersionTabControl;
        private bool libraryLoaded = false;

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            tabControl_main.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl_main.DrawItem += TabControl_Main_DrawItem;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DisableRoundedCorners(this.Handle);
            CheckFont();
            EnsureDirectoriesExist();
            EnsureDependenciesExist();
            BuildLibraryTab();
            RefreshHomePackageList();
        }

        private void CheckFont()
        {
            string fontName = "JetBrains Mono";
            using Font testFont = new Font(fontName, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            if (testFont.Name != fontName)
            {
                if (MessageBox.Show("You need to install JetBrains Mono Font!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    Process.Start(new ProcessStartInfo { FileName = "https://fonts.google.com/specimen/JetBrains+Mono", UseShellExecute = true });
            }
        }

        private void EnsureDirectoriesExist()
        {
            Directory.CreateDirectory(PulledAppsFolder);
            Directory.CreateDirectory(DumpsFolder);

            flowLayoutPanel_patchLog.AutoScroll = true;
            flowLayoutPanel_patchLog.WrapContents = false;
            flowLayoutPanel_patchLog.FlowDirection = FlowDirection.TopDown;
        }

        private void EnsureDependenciesExist()
        {
            if (!File.Exists(ApktoolJarPath))
            {
                LogToConsole("Downloading apktool.jar...");
                Dependices.DownloadApktool();
            }
            if (!File.Exists(UberApkSignerPath))
            {
                LogToConsole("Downloading uber-apk-signer.jar...");
                Dependices.DownloadUberApkSigner();
            }
            if (!File.Exists(Il2CppDumperPath))
            {
                LogToConsole("Il2CppDumper missing. It will download when you use the Dumper.");
            }
        }

        private void TabControl_Main_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            TabPage page = tc.TabPages[e.Index];
            Rectangle rect = tc.GetTabRect(e.Index);
            e.Graphics.FillRectangle(new SolidBrush(tc.BackColor), rect);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Brush textBrush = (e.State == DrawItemState.Selected) ? Brushes.Black : Brushes.Gray;
            e.Graphics.DrawString(page.Text, tc.Font, textBrush, rect, sf);
        }

        private void LogToConsole(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LogToConsole(message)));
                return;
            }

            Label logLabel = new Label
            {
                Text = message,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                BackColor = Color.White,
                ForeColor = Color.Gray,
                Font = new Font("JetBrains Mono", 7, FontStyle.Regular),
                AutoSize = true,
                MaximumSize = new Size(flowLayoutPanel_patchLog.Width - 10, 0),
                AutoEllipsis = false
            };
            flowLayoutPanel_patchLog.Controls.Add(logLabel);
            flowLayoutPanel_patchLog.ScrollControlIntoView(logLabel);
        }
        private void button_homeRefresh_Click(object sender, EventArgs e)
        {
            RefreshHomePackageList();
        }

        private void RefreshHomePackageList()
        {
            listBox_homePackages.Items.Clear();
            label_homeStatus.Text = "Loading packages...";

            try
            {
                List<string> packages = Adb.GetPackageList();
                foreach (string pkg in packages)
                {
                    listBox_homePackages.Items.Add(pkg);
                }
                label_homeStatus.Text = $"Found {packages.Count} packages.";
            }
            catch (Exception ex)
            {
                label_homeStatus.Text = "Error loading packages.";
                MessageBox.Show("ADB Error: " + ex.Message);
            }
        }

        private void listBox_homePackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_homePackages.SelectedItem == null) return;

            _selectedPackage = listBox_homePackages.SelectedItem.ToString();
            label_patchSelected.Text = "Target: " + _selectedPackage;
            label_modsSelected.Text = "Target: " + _selectedPackage;
            label_libSelected.Text = "Target: " + _selectedPackage;
            label_pullSelected.Text = "Target: " + _selectedPackage;
            label_dumpSelected.Text = "Target: " + _selectedPackage;

            CheckPackageStatus(_selectedPackage);
        }

        private async void CheckPackageStatus(string packageName)
        {
            label_homeGameInfo.Text = "Checking status...";

            await Task.Run(() =>
            {
                bool hasNativeMods = false;
                try
                {
                    string result = CmdUtility.DoCmdResult($"/c adb shell ls /sdcard/Android/data/{packageName}/nativemods");
                    if (!string.IsNullOrWhiteSpace(result) && !result.Contains("No such file"))
                    {
                        hasNativeMods = true;
                    }
                }
                catch { }

                string status = hasNativeMods ? "PATCHED (ModLoader active)" : "NOT PATCHED (or never launched)";

                BeginInvoke(new Action(() =>
                {
                    label_homeGameInfo.Text = $"Package: {packageName}\nStatus: {status}\nUnity: Unknown (requires dump)";
                }));
            });
        }
        private async void button_patch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage))
            {
                MessageBox.Show("Select a game in the Home tab first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button_patch.Enabled = false;
            flowLayoutPanel_patchLog.Controls.Clear();

            string selectedPackage = _selectedPackage;
            Action<string> log = LogToConsole;

            log($"Starting patch for: {selectedPackage}");
            log("Finding APK path...");

            string apkPath = Adb.GetBaseApk(selectedPackage);

            if (string.IsNullOrEmpty(apkPath))
            {
                log("Package APK path not found.");
                button_patch.Enabled = true;
                return;
            }

            await Task.Run(() =>
            {
                log($"Remote Path: {apkPath}");
                log("Pulling APK...");
                Adb.PullFile(apkPath);
                log("Pulled: TOPATCH.apk");

                string apkFile = Path.Combine(AppContext.BaseDirectory, "TOPATCH.apk");
                string recompiledApk = Path.Combine(RecompiledFolder, Path.GetFileName(apkFile));
                string signedFolder = Path.Combine(RecompiledFolder, "signed");

                // Cleanup
                if (Directory.Exists(signedFolder)) Directory.Delete(signedFolder, true);
                if (Directory.Exists(TempDecompileFolder)) Directory.Delete(TempDecompileFolder, true);
                if (Directory.Exists(RecompiledFolder)) Directory.Delete(RecompiledFolder, true);
                Directory.CreateDirectory(TempDecompileFolder);
                Directory.CreateDirectory(RecompiledFolder);
                Directory.CreateDirectory(signedFolder);

                log("Decompiling...");
                CmdUtility.DoCmd($"/c java -jar \"{ApktoolJarPath}\" d \"{apkFile}\" -f -o \"{TempDecompileFolder}\"");

                log("Downloading ModLoader...");
                string libDir = Path.Combine(TempDecompileFolder, "lib", "arm64-v8a");
                if (!Directory.Exists(libDir)) Directory.CreateDirectory(libDir);

                using (WebClient wc = new WebClient())
                {
                    byte[] modLoaderBytes = wc.DownloadData(ModLoaderDownloadUrl);
                    File.WriteAllBytes(Path.Combine(libDir, "libModLoader.so"), modLoaderBytes);
                }
                log("Installed libModLoader.so");

                log("Injecting smali code...");
                string injectResult = InjectModLoaderIntoSmali(TempDecompileFolder);

                if (injectResult.StartsWith("err"))
                {
                    string errorMessage = injectResult switch
                    {
                        "err1" => "UnityPlayer smali file not found.",
                        "err2" => "onCreate method not found.",
                        _ => $"Injection failed: {injectResult}"
                    };
                    log(errorMessage);
                    BeginInvoke(new Action(() => MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    return;
                }

                log("Recompiling APK...");
                CmdUtility.DoCmd($"/c java -jar \"{ApktoolJarPath}\" b \"{TempDecompileFolder}\" -f -o \"{recompiledApk}\"");

                log("Signing APK...");
                CmdUtility.DoCmd($"/c java -jar \"{UberApkSignerPath}\" -a \"{recompiledApk}\" --out \"{signedFolder}\"");
                log("Signed successfully.");

                if (checkBox_deleteTempAfterPatch.Checked)
                {
                    Directory.Delete(TempDecompileFolder, true);
                }

                string apkBaseName = Path.GetFileNameWithoutExtension(recompiledApk);
                string signedApkPath = Path.Combine(RecompiledFolder, "signed", apkBaseName + "-aligned-debugSigned.apk");

                if (checkBox_installAfterPatch.Checked)
                {
                    if (File.Exists(signedApkPath))
                    {
                        log("Uninstalling old app...");
                        CmdUtility.DoCmd($"/c adb uninstall {selectedPackage}");
                        log("Installing patched APK...");
                        CmdUtility.DoCmd($"/c adb install \"{signedApkPath}\"");
                        log("Installation Complete!");
                    }
                    else
                    {
                        log("Error: Signed APK not found.");
                    }
                }
                else
                {
                    BeginInvoke(new Action(() => Process.Start("explorer.exe", Path.GetDirectoryName(signedApkPath))));
                    log("Done. Folder opened.");
                }
            });

            button_patch.Enabled = true;
        }

        private static string InjectModLoaderIntoSmali(string tempFolder)
        {
            string[] smaliFileNames = { "UnityPlayerActivity.smali", "UnityPlayerGameActivity.smali" };
            string smaliFilePath = null;

            // Search recursively in smali folders (smali, smali_classes2, etc)
            foreach (var dir in Directory.GetDirectories(tempFolder, "smali*"))
            {
                foreach (string fileName in smaliFileNames)
                {
                    string candidate = Path.Combine(dir, "com", "unity3d", "player", fileName);
                    if (File.Exists(candidate))
                    {
                        smaliFilePath = candidate;
                        goto Found;
                    }
                }
            }
        Found:

            if (smaliFilePath == null) return "err1";

            string classReference = "Lcom/unity3d/player/" + Path.GetFileNameWithoutExtension(smaliFilePath) + ";";
            List<string> smaliLines = File.ReadAllLines(smaliFilePath).ToList();

            int onCreateLine = -1;
            int localsLine = -1;

            for (int i = 0; i < smaliLines.Count; i++)
            {
                string trimmed = smaliLines[i].Trim();
                if (onCreateLine == -1 && trimmed.StartsWith(".method protected onCreate"))
                    onCreateLine = i;
                if (onCreateLine != -1 && localsLine == -1 && trimmed.StartsWith(".locals"))
                {
                    localsLine = i;
                    break;
                }
            }

            if (onCreateLine == -1 || localsLine == -1) return "err2";

            string helperMethod = $@"
.method private static loadModLoaderLibrary()V
    .locals 1
    const-string v0, ""ModLoader""
    invoke-static {{v0}}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V
    return-void
.end method
";
            IEnumerable<string> helperMethodLines = helperMethod
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Skip(1)
                .Append("");

            int helperLineCount = helperMethodLines.Count();
            string insertResult = FileInserter.InsertMultipleLinesToFile(smaliFilePath, helperMethodLines, onCreateLine + 1);
            if (insertResult != "success") return $"err3: {insertResult}";

            localsLine += helperLineCount;
            File.AppendAllLines(smaliFilePath, helperMethodLines);
            string callInject = $"    invoke-static {{}}, {classReference}->loadModLoaderLibrary()V";
            string callResult = FileInserter.InsertLineToFile(smaliFilePath, callInject, localsLine + 2); // +2 to be safe after .locals X

            return "success";
        }

        private void button_patchHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "1. Select Game in 'Home' tab.\n" +
                "2. Click 'Patch'.\n" +
                "3. Launch game once to create folders.\n" +
                "4. Use 'Mods' tab to add .so files.\n",
                "Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button_modsRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage)) return;

            listBox_mods.Items.Clear();
            label_modsStatus.Text = "Loading mods...";

            List<string> mods = Adb.GetInstalledMods(_selectedPackage);
            if (mods.Count == 0)
            {
                label_modsStatus.Text = "No mods found.";
            }
            else
            {
                foreach (string mod in mods) listBox_mods.Items.Add(mod);
                label_modsStatus.Text = $"{mods.Count} mod(s) found.";
            }
        }

        private void button_modsAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage))
            {
                MessageBox.Show("Select a game first!"); return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select mod (.so file)",
                Filter = "Native Libraries (*.so)|*.so"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                label_modsStatus.Text = "Pushing...";
                Adb.CreateNativeModsFolder(_selectedPackage);
                Adb.PushMod(dialog.FileName, _selectedPackage);
                button_modsRefresh_Click(sender, e);
            }
        }

        private void button_modsRemove_Click(object sender, EventArgs e)
        {
            if (listBox_mods.SelectedItem == null || string.IsNullOrEmpty(_selectedPackage)) return;

            string mod = listBox_mods.SelectedItem.ToString();
            if (MessageBox.Show($"Delete {mod}?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Adb.RemoveMod(mod, _selectedPackage);
                button_modsRefresh_Click(sender, e);
            }
        }
        private void BuildLibraryTab()
        {
            libraryVersionTabControl = new TabControl
            {
                Location = new Point(6, 40),
                Size = new Size(210, 150),
                Font = new Font("JetBrains Mono", 8, FontStyle.Regular)
            };
            tabPage_library.Controls.Add(libraryVersionTabControl);
        }

        private async void tabControl_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If entering Library tab
            if (tabControl_main.SelectedTab == tabPage_library && !libraryLoaded)
            {
                libraryLoaded = true;
                label_libStatus.Text = "Loading Unity versions...";
                try
                {
                    List<string> versions = await LibraryManager.GetUnityVersions();
                    libraryVersionTabControl.TabPages.Clear();
                    foreach (string version in versions)
                    {
                        TabPage t = new TabPage(version) { Tag = version };
                        ListView lv = new ListView
                        {
                            Dock = DockStyle.Fill,
                            View = View.Details,
                            FullRowSelect = true,
                            GridLines = true,
                            Font = new Font("JetBrains Mono", 7)
                        };
                        lv.Columns.Add("Name", 140);
                        lv.Columns.Add("Size", 40);
                        t.Controls.Add(lv);
                        libraryVersionTabControl.TabPages.Add(t);
                    }
                    libraryVersionTabControl.SelectedIndexChanged += (s, ev) => LoadLibsForTab(libraryVersionTabControl.SelectedTab);
                    if (libraryVersionTabControl.TabPages.Count > 0) LoadLibsForTab(libraryVersionTabControl.TabPages[0]);
                    label_libStatus.Text = "Ready.";
                }
                catch { label_libStatus.Text = "Failed to load repo."; }
            }
        }

        private async void LoadLibsForTab(TabPage tab)
        {
            if (tab == null) return;
            string ver = tab.Tag as string;
            ListView lv = tab.Controls.OfType<ListView>().FirstOrDefault();
            if (lv == null || lv.Items.Count > 0) return;

            label_libStatus.Text = "Fetching libs...";
            var libs = await LibraryManager.GetLibrariesForVersion(ver);
            lv.Items.Clear();
            foreach (var l in libs)
            {
                var item = new ListViewItem(l.Name);
                item.SubItems.Add(l.SizeFormatted);
                item.Tag = l;
                lv.Items.Add(item);
            }
            label_libStatus.Text = "Ready.";
        }

        private async void button_libDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage)) { MessageBox.Show("Select a game first!"); return; }

            TabPage tab = libraryVersionTabControl.SelectedTab;
            if (tab == null) return;
            ListView lv = tab.Controls.OfType<ListView>().FirstOrDefault();
            if (lv?.SelectedItems.Count == 0) { MessageBox.Show("Select a library!"); return; }

            LibraryEntry lib = lv.SelectedItems[0].Tag as LibraryEntry;

            button_libDownload.Enabled = false;
            label_libStatus.Text = $"Downloading {lib.Name}...";

            try
            {
                byte[] data = await LibraryManager.DownloadLibrary(lib.DownloadUrl);
                string temp = Path.Combine(Path.GetTempPath(), lib.Name);
                await File.WriteAllBytesAsync(temp, data);

                label_libStatus.Text = "Pushing...";
                await Task.Run(() => {
                    Adb.CreateNativeModsFolder(_selectedPackage);
                    Adb.PushMod(temp, _selectedPackage);
                });

                File.Delete(temp);
                label_libStatus.Text = "Success!";
                MessageBox.Show($"{lib.Name} installed to {_selectedPackage}");
            }
            catch (Exception ex)
            {
                label_libStatus.Text = "Error.";
                MessageBox.Show(ex.Message);
            }
            button_libDownload.Enabled = true;
        }
        private void button_pull_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage)) { MessageBox.Show("Select a game first!"); return; }

            button_pull.Enabled = false;
            string apkPath = Adb.GetBaseApk(_selectedPackage);
            if (string.IsNullOrEmpty(apkPath)) { MessageBox.Show("APK not found."); button_pull.Enabled = true; return; }

            string outPath = Path.Combine(PulledAppsFolder, _selectedPackage + ".apk");
            Adb.PullFile(apkPath, outPath);

            Process.Start("explorer.exe", PulledAppsFolder);
            button_pull.Enabled = true;
        }
        private async void button_dump_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPackage)) { MessageBox.Show("Select a game first!"); return; }

            // Check if Dumper exists, if not download
            if (!File.Exists(Il2CppDumperPath))
            {
                label_dumpStatus.Text = "Downloading Il2CppDumper...";
                await Dependices.DownloadIl2CppDumper();
                if (!File.Exists(Il2CppDumperPath))
                {
                    MessageBox.Show("Failed to download Dumper.");
                    return;
                }
            }

            button_dump.Enabled = false;
            label_dumpStatus.Text = "Pulling files...";

            await Task.Run(() =>
            {
                string packageDumpDir = Path.Combine(DumpsFolder, _selectedPackage);
                Directory.CreateDirectory(packageDumpDir);
                string findLibCmd = $"shell find /data/app/ -name libil2cpp.so | grep {_selectedPackage}";
                string libPath = CmdUtility.DoCmdResult("/c adb " + findLibCmd).Trim().Split('\n').FirstOrDefault();

                string findMetaCmd = $"shell find /data/app/ -name global-metadata.dat | grep {_selectedPackage}";
                string metaPath = CmdUtility.DoCmdResult("/c adb " + findMetaCmd).Trim().Split('\n').FirstOrDefault();
                if (string.IsNullOrEmpty(libPath) || string.IsNullOrEmpty(metaPath))
                {
                    BeginInvoke(new Action(() => MessageBox.Show("Could not locate libil2cpp.so or global-metadata.dat on device.\nMake sure the game is installed and running.")));
                    return;
                }

                string localLib = Path.Combine(packageDumpDir, "libil2cpp.so");
                string localMeta = Path.Combine(packageDumpDir, "global-metadata.dat");

                Adb.PullFile(libPath, localLib);
                Adb.PullFile(metaPath, localMeta);

                BeginInvoke(new Action(() => label_dumpStatus.Text = "Running Dumper..."));

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Il2CppDumperPath,
                    Arguments = $"\"{localLib}\" \"{localMeta}\" \"{packageDumpDir}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(psi)?.WaitForExit();

                BeginInvoke(new Action(() =>
                {
                    label_dumpStatus.Text = "Dump Complete!";
                    Process.Start("explorer.exe", packageDumpDir);
                }));
            });

            button_dump.Enabled = true;
        }
        const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        enum DWM_WINDOW_CORNER_PREFERENCE { DWMWCP_DONOTROUND = 1 }
        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private static void DisableRoundedCorners(IntPtr handle)
        {
            int pref = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(int));
        }
    }
}