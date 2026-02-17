using System.Drawing;
using System.Windows.Forms;

namespace UnityPatcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl_main = new TabControl();

            tabPage_patch = new TabPage();
            label_patchTitle = new Label();
            label_patchPackageLabel = new Label();
            comboBox_patchPackage = new ComboBox();
            button_patchLoadPackages = new Button();
            button_patchHelp = new Button();
            flowLayoutPanel_patchLog = new FlowLayoutPanel();
            button_patch = new Button();
            checkBox_deleteTempAfterPatch = new CheckBox();
            checkBox_installAfterPatch = new CheckBox();

            tabPage_mods = new TabPage();
            label_modsPackageLabel = new Label();
            comboBox_modsPackage = new ComboBox();
            button_modsLoadPackages = new Button();
            listBox_mods = new ListBox();
            button_modsRefresh = new Button();
            button_modsAdd = new Button();
            button_modsRemove = new Button();
            label_modsStatus = new Label();

            tabPage_library = new TabPage();
            label_libraryPackageLabel = new Label();
            textBox_libraryPackage = new TextBox();
            label_libraryStatus = new Label();
            button_libraryDownloadPush = new Button();
            label_libraryHint = new Label();

            tabPage_pull = new TabPage();
            label_pullPackageLabel = new Label();
            comboBox_pullPackage = new ComboBox();
            button_pullLoadPackages = new Button();
            button_pullApk = new Button();

            tabControl_main.SuspendLayout();
            tabPage_patch.SuspendLayout();
            tabPage_mods.SuspendLayout();
            tabPage_library.SuspendLayout();
            tabPage_pull.SuspendLayout();
            SuspendLayout();

            // tabControl_main
            tabControl_main.Controls.Add(tabPage_patch);
            tabControl_main.Controls.Add(tabPage_mods);
            tabControl_main.Controls.Add(tabPage_library);
            tabControl_main.Controls.Add(tabPage_pull);
            tabControl_main.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl_main.Location = new Point(-1, 0);
            tabControl_main.Name = "tabControl_main";
            tabControl_main.SelectedIndex = 0;
            tabControl_main.Size = new Size(230, 288);
            tabControl_main.TabIndex = 0;
            tabControl_main.SelectedIndexChanged += tabControl_main_SelectedIndexChanged;

            // ── PATCH TAB ──────────────────────────────────────────────────

            tabPage_patch.Controls.Add(label_patchTitle);
            tabPage_patch.Controls.Add(label_patchPackageLabel);
            tabPage_patch.Controls.Add(comboBox_patchPackage);
            tabPage_patch.Controls.Add(button_patchLoadPackages);
            tabPage_patch.Controls.Add(button_patchHelp);
            tabPage_patch.Controls.Add(flowLayoutPanel_patchLog);
            tabPage_patch.Controls.Add(button_patch);
            tabPage_patch.Controls.Add(checkBox_deleteTempAfterPatch);
            tabPage_patch.Controls.Add(checkBox_installAfterPatch);
            tabPage_patch.Location = new Point(4, 25);
            tabPage_patch.Name = "tabPage_patch";
            tabPage_patch.Padding = new Padding(3);
            tabPage_patch.Size = new Size(222, 259);
            tabPage_patch.TabIndex = 0;
            tabPage_patch.Text = "Patch";
            tabPage_patch.UseVisualStyleBackColor = true;

            label_patchTitle.AutoSize = true;
            label_patchTitle.Font = new Font("JetBrains Mono", 6, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_patchTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            label_patchTitle.Location = new Point(6, 214);
            label_patchTitle.Name = "label_patchTitle";
            label_patchTitle.Text = "Made by SilentErased; ModLoader by Livku2";

            label_patchPackageLabel.AutoSize = true;
            label_patchPackageLabel.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_patchPackageLabel.Location = new Point(75, 16);
            label_patchPackageLabel.Name = "label_patchPackageLabel";
            label_patchPackageLabel.Text = "Package";

            comboBox_patchPackage.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_patchPackage.FormattingEnabled = true;
            comboBox_patchPackage.Location = new Point(6, 36);
            comboBox_patchPackage.Name = "comboBox_patchPackage";
            comboBox_patchPackage.Size = new Size(180, 25);
            comboBox_patchPackage.TabIndex = 1;

            button_patchLoadPackages.FlatStyle = FlatStyle.Popup;
            button_patchLoadPackages.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_patchLoadPackages.Location = new Point(188, 36);
            button_patchLoadPackages.Name = "button_patchLoadPackages";
            button_patchLoadPackages.Size = new Size(28, 25);
            button_patchLoadPackages.Text = "↺";
            button_patchLoadPackages.UseVisualStyleBackColor = true;
            button_patchLoadPackages.Click += button_patchLoadPackages_Click;

            button_patchHelp.FlatStyle = FlatStyle.Popup;
            button_patchHelp.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_patchHelp.Location = new Point(188, 6);
            button_patchHelp.Name = "button_patchHelp";
            button_patchHelp.Size = new Size(28, 25);
            button_patchHelp.Text = "?";
            button_patchHelp.UseVisualStyleBackColor = true;
            button_patchHelp.Click += button_patchHelp_Click;

            flowLayoutPanel_patchLog.BackColor = Color.White;
            flowLayoutPanel_patchLog.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel_patchLog.Location = new Point(6, 67);
            flowLayoutPanel_patchLog.Name = "flowLayoutPanel_patchLog";
            flowLayoutPanel_patchLog.Size = new Size(210, 113);
            flowLayoutPanel_patchLog.TabIndex = 6;

            button_patch.FlatStyle = FlatStyle.Popup;
            button_patch.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_patch.Location = new Point(6, 186);
            button_patch.Name = "button_patch";
            button_patch.Size = new Size(210, 25);
            button_patch.Text = "Patch";
            button_patch.UseVisualStyleBackColor = true;
            button_patch.Click += button_patch_Click;

            checkBox_deleteTempAfterPatch.AutoSize = true;
            checkBox_deleteTempAfterPatch.Font = new Font("JetBrains Mono", 6, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox_deleteTempAfterPatch.Location = new Point(9, 227);
            checkBox_deleteTempAfterPatch.Name = "checkBox_deleteTempAfterPatch";
            checkBox_deleteTempAfterPatch.Text = "Delete Decompiled Folder After Patch";
            checkBox_deleteTempAfterPatch.UseVisualStyleBackColor = true;

            checkBox_installAfterPatch.AutoSize = true;
            checkBox_installAfterPatch.Font = new Font("JetBrains Mono", 6, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox_installAfterPatch.Location = new Point(9, 242);
            checkBox_installAfterPatch.Name = "checkBox_installAfterPatch";
            checkBox_installAfterPatch.Text = "Install after Patch";
            checkBox_installAfterPatch.UseVisualStyleBackColor = true;

            // ── MODS TAB ───────────────────────────────────────────────────

            tabPage_mods.Controls.Add(label_modsPackageLabel);
            tabPage_mods.Controls.Add(comboBox_modsPackage);
            tabPage_mods.Controls.Add(button_modsLoadPackages);
            tabPage_mods.Controls.Add(listBox_mods);
            tabPage_mods.Controls.Add(button_modsRefresh);
            tabPage_mods.Controls.Add(button_modsAdd);
            tabPage_mods.Controls.Add(button_modsRemove);
            tabPage_mods.Controls.Add(label_modsStatus);
            tabPage_mods.Location = new Point(4, 25);
            tabPage_mods.Name = "tabPage_mods";
            tabPage_mods.Padding = new Padding(3);
            tabPage_mods.Size = new Size(222, 259);
            tabPage_mods.TabIndex = 1;
            tabPage_mods.Text = "Mods";
            tabPage_mods.UseVisualStyleBackColor = true;

            label_modsPackageLabel.AutoSize = true;
            label_modsPackageLabel.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_modsPackageLabel.Location = new Point(75, 8);
            label_modsPackageLabel.Name = "label_modsPackageLabel";
            label_modsPackageLabel.Text = "Package";

            comboBox_modsPackage.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_modsPackage.FormattingEnabled = true;
            comboBox_modsPackage.Location = new Point(6, 27);
            comboBox_modsPackage.Name = "comboBox_modsPackage";
            comboBox_modsPackage.Size = new Size(180, 22);
            comboBox_modsPackage.TabIndex = 0;

            button_modsLoadPackages.FlatStyle = FlatStyle.Popup;
            button_modsLoadPackages.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_modsLoadPackages.Location = new Point(188, 27);
            button_modsLoadPackages.Name = "button_modsLoadPackages";
            button_modsLoadPackages.Size = new Size(28, 22);
            button_modsLoadPackages.Text = "↺";
            button_modsLoadPackages.UseVisualStyleBackColor = true;
            button_modsLoadPackages.Click += button_modsLoadPackages_Click;

            listBox_mods.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox_mods.FormattingEnabled = true;
            listBox_mods.Location = new Point(6, 55);
            listBox_mods.Name = "listBox_mods";
            listBox_mods.Size = new Size(210, 120);
            listBox_mods.TabIndex = 2;

            button_modsRefresh.FlatStyle = FlatStyle.Popup;
            button_modsRefresh.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_modsRefresh.Location = new Point(6, 181);
            button_modsRefresh.Name = "button_modsRefresh";
            button_modsRefresh.Size = new Size(68, 22);
            button_modsRefresh.Text = "Refresh";
            button_modsRefresh.UseVisualStyleBackColor = true;
            button_modsRefresh.Click += button_modsRefresh_Click;

            button_modsAdd.FlatStyle = FlatStyle.Popup;
            button_modsAdd.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_modsAdd.Location = new Point(78, 181);
            button_modsAdd.Name = "button_modsAdd";
            button_modsAdd.Size = new Size(68, 22);
            button_modsAdd.Text = "Add .so";
            button_modsAdd.UseVisualStyleBackColor = true;
            button_modsAdd.Click += button_modsAdd_Click;

            button_modsRemove.FlatStyle = FlatStyle.Popup;
            button_modsRemove.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_modsRemove.Location = new Point(150, 181);
            button_modsRemove.Name = "button_modsRemove";
            button_modsRemove.Size = new Size(66, 22);
            button_modsRemove.Text = "Remove";
            button_modsRemove.UseVisualStyleBackColor = true;
            button_modsRemove.Click += button_modsRemove_Click;

            label_modsStatus.AutoSize = true;
            label_modsStatus.Font = new Font("JetBrains Mono", 7, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_modsStatus.ForeColor = Color.Gray;
            label_modsStatus.Location = new Point(6, 210);
            label_modsStatus.Name = "label_modsStatus";
            label_modsStatus.Size = new Size(210, 40);
            label_modsStatus.Text = "Select a package and click Refresh.";

            // ── LIBRARY TAB ────────────────────────────────────────────────

            tabPage_library.Controls.Add(label_libraryPackageLabel);
            tabPage_library.Controls.Add(textBox_libraryPackage);
            tabPage_library.Controls.Add(label_libraryStatus);
            tabPage_library.Controls.Add(button_libraryDownloadPush);
            tabPage_library.Controls.Add(label_libraryHint);
            tabPage_library.Location = new Point(4, 25);
            tabPage_library.Name = "tabPage_library";
            tabPage_library.Padding = new Padding(3);
            tabPage_library.Size = new Size(222, 259);
            tabPage_library.TabIndex = 2;
            tabPage_library.Text = "Library";
            tabPage_library.UseVisualStyleBackColor = true;

            label_libraryPackageLabel.AutoSize = true;
            label_libraryPackageLabel.Font = new Font("JetBrains Mono", 7, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_libraryPackageLabel.Location = new Point(6, 6);
            label_libraryPackageLabel.Name = "label_libraryPackageLabel";
            label_libraryPackageLabel.Text = "Target Package:";

            textBox_libraryPackage.Font = new Font("JetBrains Mono", 8, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_libraryPackage.Location = new Point(6, 20);
            textBox_libraryPackage.Name = "textBox_libraryPackage";
            textBox_libraryPackage.PlaceholderText = "com.example.game";
            textBox_libraryPackage.Size = new Size(210, 22);
            textBox_libraryPackage.TabIndex = 0;

            label_libraryStatus.AutoSize = false;
            label_libraryStatus.Font = new Font("JetBrains Mono", 7, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_libraryStatus.ForeColor = Color.Gray;
            label_libraryStatus.Location = new Point(6, 222);
            label_libraryStatus.Name = "label_libraryStatus";
            label_libraryStatus.Size = new Size(210, 14);
            label_libraryStatus.Text = "Loading library list...";

            button_libraryDownloadPush.FlatStyle = FlatStyle.Popup;
            button_libraryDownloadPush.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_libraryDownloadPush.Location = new Point(6, 238);
            button_libraryDownloadPush.Name = "button_libraryDownloadPush";
            button_libraryDownloadPush.Size = new Size(210, 18);
            button_libraryDownloadPush.Text = "Download & Push to Device";
            button_libraryDownloadPush.UseVisualStyleBackColor = true;
            button_libraryDownloadPush.Click += button_libraryDownloadPush_Click;

            label_libraryHint.AutoSize = false;
            label_libraryHint.Font = new Font("JetBrains Mono", 6, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_libraryHint.ForeColor = Color.DarkGray;
            label_libraryHint.Location = new Point(6, 44);
            label_libraryHint.Name = "label_libraryHint";
            label_libraryHint.Size = new Size(210, 10);
            label_libraryHint.Text = "Unity version tabs below — select a lib then push.";

            // ── PULL APK TAB ───────────────────────────────────────────────

            tabPage_pull.Controls.Add(label_pullPackageLabel);
            tabPage_pull.Controls.Add(comboBox_pullPackage);
            tabPage_pull.Controls.Add(button_pullLoadPackages);
            tabPage_pull.Controls.Add(button_pullApk);
            tabPage_pull.Location = new Point(4, 25);
            tabPage_pull.Name = "tabPage_pull";
            tabPage_pull.Padding = new Padding(3);
            tabPage_pull.Size = new Size(222, 259);
            tabPage_pull.TabIndex = 3;
            tabPage_pull.Text = "Pull APK";
            tabPage_pull.UseVisualStyleBackColor = true;

            label_pullPackageLabel.AutoSize = true;
            label_pullPackageLabel.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_pullPackageLabel.Location = new Point(75, 55);
            label_pullPackageLabel.Name = "label_pullPackageLabel";
            label_pullPackageLabel.Text = "Package";

            comboBox_pullPackage.Font = new Font("JetBrains Mono", 9, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_pullPackage.FormattingEnabled = true;
            comboBox_pullPackage.Location = new Point(6, 75);
            comboBox_pullPackage.Name = "comboBox_pullPackage";
            comboBox_pullPackage.Size = new Size(180, 25);
            comboBox_pullPackage.TabIndex = 0;

            button_pullLoadPackages.FlatStyle = FlatStyle.Popup;
            button_pullLoadPackages.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_pullLoadPackages.Location = new Point(188, 75);
            button_pullLoadPackages.Name = "button_pullLoadPackages";
            button_pullLoadPackages.Size = new Size(28, 25);
            button_pullLoadPackages.Text = "↺";
            button_pullLoadPackages.UseVisualStyleBackColor = true;
            button_pullLoadPackages.Click += button_pullLoadPackages_Click;

            button_pullApk.FlatStyle = FlatStyle.Popup;
            button_pullApk.Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_pullApk.Location = new Point(6, 106);
            button_pullApk.Name = "button_pullApk";
            button_pullApk.Size = new Size(210, 25);
            button_pullApk.Text = "Pull";
            button_pullApk.UseVisualStyleBackColor = true;
            button_pullApk.Click += button_pullApk_Click;

            // ── FORM ───────────────────────────────────────────────────────

            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(225, 282);
            Controls.Add(tabControl_main);
            Font = new Font("JetBrains Mono", 12, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Easy Patcher";
            Load += Form1_Load;

            tabControl_main.ResumeLayout(false);
            tabPage_patch.ResumeLayout(false);
            tabPage_patch.PerformLayout();
            tabPage_mods.ResumeLayout(false);
            tabPage_mods.PerformLayout();
            tabPage_library.ResumeLayout(false);
            tabPage_library.PerformLayout();
            tabPage_pull.ResumeLayout(false);
            tabPage_pull.PerformLayout();
            ResumeLayout(false);
        }

        private TabControl tabControl_main;

        private TabPage tabPage_patch;
        private Label label_patchTitle;
        private Label label_patchPackageLabel;
        private ComboBox comboBox_patchPackage;
        private Button button_patchLoadPackages;
        private Button button_patchHelp;
        private FlowLayoutPanel flowLayoutPanel_patchLog;
        private Button button_patch;
        private CheckBox checkBox_deleteTempAfterPatch;
        private CheckBox checkBox_installAfterPatch;

        private TabPage tabPage_mods;
        private Label label_modsPackageLabel;
        private ComboBox comboBox_modsPackage;
        private Button button_modsLoadPackages;
        private ListBox listBox_mods;
        private Button button_modsRefresh;
        private Button button_modsAdd;
        private Button button_modsRemove;
        private Label label_modsStatus;

        private TabPage tabPage_library;
        private Label label_libraryPackageLabel;
        private TextBox textBox_libraryPackage;
        private Label label_libraryStatus;
        private Button button_libraryDownloadPush;
        private Label label_libraryHint;

        private TabPage tabPage_pull;
        private Label label_pullPackageLabel;
        private ComboBox comboBox_pullPackage;
        private Button button_pullLoadPackages;
        private Button button_pullApk;
    }
}
