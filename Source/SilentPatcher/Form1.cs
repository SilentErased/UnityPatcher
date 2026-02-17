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
        private static string TempDecompileFolder => Path.Combine(AppContext.BaseDirectory, "Temp");
        private static string RecompiledFolder => Path.Combine(AppContext.BaseDirectory, "Recompiled");
        private static string PulledAppsFolder => Path.Combine(AppContext.BaseDirectory, "PulledAPPS");

        private TabControl libraryVersionTabControl;
        private bool libraryLoaded = false;

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void LogToConsole(string message)
        {
            Label logLabel = new Label
            {
                Text = message,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                BackColor = Color.White,
                ForeColor = Color.Gray,
                Font = new Font("JetBrains Mono", 7, FontStyle.Regular),
                AutoSize = true,
                MaximumSize = new Size(0, 0),
                AutoEllipsis = false
            };
            flowLayoutPanel_patchLog.Controls.Add(logLabel);
            flowLayoutPanel_patchLog.PerformLayout();
            flowLayoutPanel_patchLog.ScrollControlIntoView(logLabel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisableRoundedCorners(this.Handle);
            CheckFont();
            EnsureDirectoriesExist();
            EnsureDependenciesExist();
            BuildLibraryTab();
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
            if (!Directory.Exists(PulledAppsFolder))
            {
                Directory.CreateDirectory(PulledAppsFolder);
                MessageBox.Show("Created PulledAPPS folder.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            flowLayoutPanel_patchLog.AutoScroll = true;
            flowLayoutPanel_patchLog.WrapContents = false;
            flowLayoutPanel_patchLog.FlowDirection = FlowDirection.TopDown;
        }

        private void EnsureDependenciesExist()
        {
            if (!File.Exists(ApktoolJarPath))
            {
                comboBox_patchPackage.Enabled = false;
                LogToConsole("Downloading apktool.jar...");
                Dependices.DownloadApktool();
                LogToConsole("apktool.jar ready.");
            }
            if (!File.Exists(UberApkSignerPath))
            {
                comboBox_patchPackage.Enabled = false;
                LogToConsole("Downloading uber-apk-signer.jar...");
                Dependices.DownloadUberApkSigner();
                LogToConsole("uber-apk-signer.jar ready.");
            }
            comboBox_patchPackage.Enabled = true;
        }

        private void BuildLibraryTab()
        {
            libraryVersionTabControl = new TabControl
            {
                Location = new Point(6, 55),
                Size = new Size(210, 160),
                Font = new Font("JetBrains Mono", 8, FontStyle.Regular)
            };
            tabPage_library.Controls.Add(libraryVersionTabControl);
        }

        private async void tabControl_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_main.SelectedTab == tabPage_library && !libraryLoaded)
            {
                libraryLoaded = true;
                label_libraryStatus.Text = "Loading versions...";
                try
                {
                    List<string> versions = await LibraryManager.GetUnityVersions();
                    libraryVersionTabControl.TabPages.Clear();
                    foreach (string version in versions)
                    {
                        TabPage versionTab = new TabPage(version) { Tag = version };
                        ListView libListView = BuildLibraryListView();
                        libListView.Tag = version;
                        versionTab.Controls.Add(libListView);
                        libraryVersionTabControl.TabPages.Add(versionTab);
                    }
                    libraryVersionTabControl.SelectedIndexChanged += LibraryVersionTab_SelectedIndexChanged;
                    if (libraryVersionTabControl.TabPages.Count > 0)
                        await LoadLibrariesIntoTab(libraryVersionTabControl.TabPages[0]);
                    label_libraryStatus.Text = "Select a library and click Download & Push.";
                }
                catch (Exception ex)
                {
                    label_libraryStatus.Text = "Failed to load versions.";
                    MessageBox.Show("Error loading library versions:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private ListView BuildLibraryListView()
        {
            var listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("JetBrains Mono", 7, FontStyle.Regular)
            };
            listView.Columns.Add("Name", 140);
            listView.Columns.Add("Size", 50);
            return listView;
        }

        private async void LibraryVersionTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = libraryVersionTabControl.SelectedTab;
            if (selectedTab != null)
                await LoadLibrariesIntoTab(selectedTab);
        }

        private async Task LoadLibrariesIntoTab(TabPage tab)
        {
            string version = tab.Tag as string;
            ListView listView = tab.Controls.OfType<ListView>().FirstOrDefault();
            if (listView == null || listView.Items.Count > 0) return;

            label_libraryStatus.Text = $"Loading libs for Unity {version}...";
            try
            {
                List<LibraryEntry> libraries = await LibraryManager.GetLibrariesForVersion(version);
                listView.Items.Clear();
                foreach (LibraryEntry lib in libraries)
                {
                    var item = new ListViewItem(lib.Name);
                    item.SubItems.Add(lib.SizeFormatted);
                    item.Tag = lib;
                    listView.Items.Add(item);
                }
                label_libraryStatus.Text = libraries.Count > 0
                    ? $"{libraries.Count} libraries loaded."
                    : "No libraries found for this version.";
            }
            catch (Exception ex)
            {
                label_libraryStatus.Text = "Failed to load libraries.";
                MessageBox.Show("Error loading libraries:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button_libraryDownloadPush_Click(object sender, EventArgs e)
        {
            string packageName = textBox_libraryPackage.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                MessageBox.Show("Enter a package name first.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TabPage selectedVersionTab = libraryVersionTabControl.SelectedTab;
            if (selectedVersionTab == null) return;

            ListView listView = selectedVersionTab.Controls.OfType<ListView>().FirstOrDefault();
            if (listView?.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a library first.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LibraryEntry selectedLib = listView.SelectedItems[0].Tag as LibraryEntry;
            if (selectedLib == null) return;

            button_libraryDownloadPush.Enabled = false;
            label_libraryStatus.Text = $"Downloading {selectedLib.Name}...";

            try
            {
                byte[] libData = await LibraryManager.DownloadLibrary(selectedLib.DownloadUrl);
                string tempPath = Path.Combine(Path.GetTempPath(), selectedLib.Name);
                await File.WriteAllBytesAsync(tempPath, libData);

                label_libraryStatus.Text = "Pushing to device...";
                await Task.Run(() =>
                {
                    Adb.CreateNativeModsFolder(packageName);
                    Adb.PushMod(tempPath, packageName);
                });

                File.Delete(tempPath);
                label_libraryStatus.Text = $"Done! {selectedLib.Name} pushed to {packageName}.";

                MessageBox.Show(
                    $"{selectedLib.Name} pushed successfully!\n\nRemember: launch the game once to create the nativemods folder if this is the first time, then close and relaunch.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                label_libraryStatus.Text = "Failed.";
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_libraryDownloadPush.Enabled = true;
            }
        }

        private void button_modsRefresh_Click(object sender, EventArgs e)
        {
            string packageName = comboBox_modsPackage.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                MessageBox.Show("Select or type a package name.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listBox_mods.Items.Clear();
            label_modsStatus.Text = "Loading mods...";

            List<string> mods = Adb.GetInstalledMods(packageName);
            if (mods.Count == 0)
            {
                label_modsStatus.Text = "No mods found (or folder doesn't exist).";
                return;
            }

            foreach (string mod in mods)
                listBox_mods.Items.Add(mod);

            label_modsStatus.Text = $"{mods.Count} mod(s) found.";
        }

        private void button_modsLoadPackages_Click(object sender, EventArgs e)
        {
            comboBox_modsPackage.Items.Clear();
            foreach (string package in Adb.GetPackageList())
                comboBox_modsPackage.Items.Add(package);
        }

        private void button_modsAdd_Click(object sender, EventArgs e)
        {
            string packageName = comboBox_modsPackage.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                MessageBox.Show("Select a package first.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select mod (.so file)",
                Filter = "Native Libraries (*.so)|*.so|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            string filePath = dialog.FileName;
            label_modsStatus.Text = $"Pushing {Path.GetFileName(filePath)}...";

            Adb.CreateNativeModsFolder(packageName);
            Adb.PushMod(filePath, packageName);

            label_modsStatus.Text = "Pushed. Refreshing...";
            button_modsRefresh_Click(sender, e);
        }

        private void button_modsRemove_Click(object sender, EventArgs e)
        {
            string packageName = comboBox_modsPackage.Text.Trim();
            if (string.IsNullOrEmpty(packageName))
            {
                MessageBox.Show("Select a package first.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox_mods.SelectedItem == null)
            {
                MessageBox.Show("Select a mod to remove.", "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMod = listBox_mods.SelectedItem.ToString();
            if (MessageBox.Show($"Remove {selectedMod}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Adb.RemoveMod(selectedMod, packageName);
            label_modsStatus.Text = $"Removed {selectedMod}. Refreshing...";
            button_modsRefresh_Click(sender, e);
        }

        private void button_patchLoadPackages_Click(object sender, EventArgs e)
        {
            comboBox_patchPackage.Items.Clear();
            foreach (string package in Adb.GetPackageList())
                comboBox_patchPackage.Items.Add(package);
        }

        private void button_patchHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "How to use the patcher:\n" +
                "1. Patch the game\n" +
                "2. Launch the game once — it creates: Android/data/packageName/nativemods\n" +
                "3. Close the game\n" +
                "4. Drop your .so mods into the nativemods folder\n" +
                "5. ModLoader loads all native libs from that folder on startup\n\n" +
                "GitHub: https://github.com/Livku2/ModLoader",
                "Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void button_patch_Click(object sender, EventArgs e)
        {
            comboBox_patchPackage.Enabled = false;
            flowLayoutPanel_patchLog.Controls.Clear();
            string selectedPackage = comboBox_patchPackage.Text;
            Action<string> log = msg => BeginInvoke(new Action(() => LogToConsole(msg)));

            log("Finding APK path...");
            string apkPath = Adb.GetBaseApk(selectedPackage);

            if (string.IsNullOrEmpty(apkPath))
            {
                log("Package APK path not found.");
                comboBox_patchPackage.Enabled = true;
                return;
            }

            await Task.Run(() =>
            {
                log(apkPath);
                log("Pulling APK...");
                Adb.PullFile(apkPath);
                log("Pulled: TOPATCH.apk");

                string apkFile = Path.Combine(AppContext.BaseDirectory, "TOPATCH.apk");
                string recompiledApk = Path.Combine(RecompiledFolder, Path.GetFileName(apkFile));
                string signedFolder = Path.Combine(RecompiledFolder, "signed");

                log("Cleaning old temp folders...");
                if (Directory.Exists(signedFolder)) Directory.Delete(signedFolder, true);
                if (Directory.Exists(TempDecompileFolder)) Directory.Delete(TempDecompileFolder, true);
                if (Directory.Exists(RecompiledFolder)) Directory.Delete(RecompiledFolder, true);

                Directory.CreateDirectory(TempDecompileFolder);
                Directory.CreateDirectory(RecompiledFolder);
                Directory.CreateDirectory(signedFolder);

                log("Decompiling APK...");
                CmdUtility.DoCmd($"/c java -jar \"{ApktoolJarPath}\" d \"{apkFile}\" -f -o \"{TempDecompileFolder}\"");

                log("Downloading libModLoader.so...");
                string libDir = Path.Combine(TempDecompileFolder, "lib", "arm64-v8a");
                byte[] modLoaderBytes = new WebClient().DownloadData(ModLoaderDownloadUrl);
                File.WriteAllBytes(Path.Combine(libDir, "libModLoader.so"), modLoaderBytes);
                log("Installed libModLoader.so");

                log("Injecting smali...");
                string injectResult = InjectModLoaderIntoSmali(TempDecompileFolder);

                if (injectResult.StartsWith("err"))
                {
                    string errorMessage = injectResult switch
                    {
                        "err1" => "UnityPlayer smali file not found.",
                        "err2" => "onCreate method not found in smali.",
                        _ => $"Smali injection failed: {injectResult}"
                    };
                    log(errorMessage);
                    BeginInvoke(new Action(() =>
                        MessageBox.Show("ERROR! " + errorMessage, "Easy Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    return;
                }

                log("Smali injection successful. Recompiling...");
                CmdUtility.DoCmd($"/c java -jar \"{ApktoolJarPath}\" b \"{TempDecompileFolder}\" -f -o \"{recompiledApk}\"");

                log("Signing APK...");
                CmdUtility.DoCmd($"/c java -jar \"{UberApkSignerPath}\" -a \"{recompiledApk}\" --out \"{signedFolder}\"");
                log("Signed.");

                if (checkBox_deleteTempAfterPatch.Checked)
                {
                    log("Deleting temp folder...");
                    Directory.Delete(TempDecompileFolder, true);
                }

                string apkBaseName = Path.GetFileNameWithoutExtension(recompiledApk);
                string signedApkPath = Path.Combine(RecompiledFolder, "signed", apkBaseName + "-aligned-debugSigned.apk");

                if (checkBox_installAfterPatch.Checked)
                {
                    if (File.Exists(signedApkPath))
                    {
                        log("Uninstalling old package...");
                        CmdUtility.DoCmd($"/c adb uninstall {selectedPackage}");
                        log("Installing patched APK...");
                        CmdUtility.DoCmd($"/c adb install \"{signedApkPath}\"");
                        log("Done.");
                    }
                    else
                    {
                        log("Signed APK not found — build may have failed.");
                    }
                }
                else
                {
                    BeginInvoke(new Action(() => Process.Start("explorer.exe", Path.GetDirectoryName(signedApkPath))));
                    log("Done. Output folder opened.");
                }
            });

            comboBox_patchPackage.Enabled = true;
        }

        private static string InjectModLoaderIntoSmali(string tempFolder)
        {
            string[] smaliFileNames = { "UnityPlayerActivity.smali", "UnityPlayerGameActivity.smali" };
            string smaliFilePath = null;

            foreach (string fileName in smaliFileNames)
            {
                string candidate = Path.Combine(tempFolder, "smali", "com", "unity3d", "player", fileName);
                if (File.Exists(candidate)) { smaliFilePath = candidate; break; }
            }

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
            string callInject = $"    invoke-static {{}}, {classReference}->loadModLoaderLibrary()V";
            string callResult = FileInserter.InsertLineToFile(smaliFilePath, callInject, localsLine + 2);
            if (callResult != "success") return $"err4: {callResult}";

            return "success";
        }

        private void button_pullLoadPackages_Click(object sender, EventArgs e)
        {
            comboBox_pullPackage.Items.Clear();
            foreach (string package in Adb.GetPackageList())
                comboBox_pullPackage.Items.Add(package);
        }

        private void button_pullApk_Click(object sender, EventArgs e)
        {
            comboBox_pullPackage.Enabled = false;
            button_pullApk.Enabled = false;

            string packageName = comboBox_pullPackage.Text;
            string apkPath = Adb.GetBaseApk(packageName);
            string outputPath = Path.Combine(PulledAppsFolder, packageName.Replace(".", "-") + ".apk");

            Adb.PullFile(apkPath, outputPath);

            comboBox_pullPackage.Enabled = true;
            button_pullApk.Enabled = true;

            if (MessageBox.Show($"Pulled {packageName}\nOpen PulledAPPS folder?", "Easy Patcher", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                Process.Start("explorer.exe", PulledAppsFolder);
        }

        const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;

        enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DONOTROUND = 1
        }

        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private static void DisableRoundedCorners(IntPtr handle)
        {
            int pref = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(int));
        }
    }
}
