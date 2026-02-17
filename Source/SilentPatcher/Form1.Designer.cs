using System.Drawing;
using System.Windows.Forms;

namespace UnityPatcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl_main = new TabControl();
            tabPage_home = new TabPage();
            label_homeStatus = new Label();
            label_homeTitle = new Label();
            listBox_homePackages = new ListBox();
            button_homeRefresh = new Button();
            label_homeGameInfo = new Label();
            tabPage_patch = new TabPage();
            label_patchSelected = new Label();
            button_patchHelp = new Button();
            flowLayoutPanel_patchLog = new FlowLayoutPanel();
            button_patch = new Button();
            checkBox_deleteTempAfterPatch = new CheckBox();
            checkBox_installAfterPatch = new CheckBox();
            tabPage_mods = new TabPage();
            label_modsSelected = new Label();
            listBox_mods = new ListBox();
            button_modsRefresh = new Button();
            button_modsAdd = new Button();
            button_modsRemove = new Button();
            label_modsStatus = new Label();
            tabPage_library = new TabPage();
            label_libSelected = new Label();
            button_libDownload = new Button();
            label_libStatus = new Label();
            tabPage_dumper = new TabPage();
            label_dumpSelected = new Label();
            button_dump = new Button();
            label_dumpStatus = new Label();
            tabPage_pull = new TabPage();
            label_pullSelected = new Label();
            button_pull = new Button();
            tabControl_main.SuspendLayout();
            tabPage_home.SuspendLayout();
            tabPage_patch.SuspendLayout();
            tabPage_mods.SuspendLayout();
            tabPage_library.SuspendLayout();
            tabPage_dumper.SuspendLayout();
            tabPage_pull.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl_main
            // 
            tabControl_main.Controls.Add(tabPage_home);
            tabControl_main.Controls.Add(tabPage_patch);
            tabControl_main.Controls.Add(tabPage_mods);
            tabControl_main.Controls.Add(tabPage_library);
            tabControl_main.Controls.Add(tabPage_dumper);
            tabControl_main.Controls.Add(tabPage_pull);
            tabControl_main.Font = new Font("JetBrains Mono", 8F);
            tabControl_main.Location = new Point(-2, 0);
            tabControl_main.Name = "tabControl_main";
            tabControl_main.SelectedIndex = 0;
            tabControl_main.Size = new Size(230, 300);
            tabControl_main.TabIndex = 0;
            tabControl_main.SelectedIndexChanged += tabControl_main_SelectedIndexChanged;
            // 
            // tabPage_home
            // 
            tabPage_home.Controls.Add(label_homeStatus);
            tabPage_home.Controls.Add(label_homeTitle);
            tabPage_home.Controls.Add(listBox_homePackages);
            tabPage_home.Controls.Add(button_homeRefresh);
            tabPage_home.Controls.Add(label_homeGameInfo);
            tabPage_home.Location = new Point(4, 23);
            tabPage_home.Name = "tabPage_home";
            tabPage_home.Size = new Size(222, 273);
            tabPage_home.TabIndex = 0;
            tabPage_home.Text = "Home";
            // 
            // label_homeStatus
            // 
            label_homeStatus.AutoSize = true;
            label_homeStatus.Location = new Point(39, 158);
            label_homeStatus.Name = "label_homeStatus";
            label_homeStatus.Size = new Size(140, 14);
            label_homeStatus.TabIndex = 4;
            label_homeStatus.Text = "Select Application:";
            // 
            // label_homeTitle
            // 
            label_homeTitle.AutoSize = true;
            label_homeTitle.Location = new Point(6, 6);
            label_homeTitle.Name = "label_homeTitle";
            label_homeTitle.Size = new Size(140, 14);
            label_homeTitle.TabIndex = 0;
            label_homeTitle.Text = "Select Application:";
            // 
            // listBox_homePackages
            // 
            listBox_homePackages.Location = new Point(6, 25);
            listBox_homePackages.Name = "listBox_homePackages";
            listBox_homePackages.Size = new Size(210, 130);
            listBox_homePackages.TabIndex = 1;
            listBox_homePackages.SelectedIndexChanged += listBox_homePackages_SelectedIndexChanged;
            // 
            // button_homeRefresh
            // 
            button_homeRefresh.FlatStyle = FlatStyle.Popup;
            button_homeRefresh.Location = new Point(6, 175);
            button_homeRefresh.Name = "button_homeRefresh";
            button_homeRefresh.Size = new Size(210, 20);
            button_homeRefresh.TabIndex = 2;
            button_homeRefresh.Text = "Refresh List";
            button_homeRefresh.Click += button_homeRefresh_Click;
            // 
            // label_homeGameInfo
            // 
            label_homeGameInfo.Font = new Font("JetBrains Mono", 7F);
            label_homeGameInfo.ForeColor = Color.DimGray;
            label_homeGameInfo.Location = new Point(6, 205);
            label_homeGameInfo.Name = "label_homeGameInfo";
            label_homeGameInfo.Size = new Size(210, 60);
            label_homeGameInfo.TabIndex = 3;
            label_homeGameInfo.Text = "No package selected.";
            // 
            // tabPage_patch
            // 
            tabPage_patch.Controls.Add(label_patchSelected);
            tabPage_patch.Controls.Add(button_patchHelp);
            tabPage_patch.Controls.Add(flowLayoutPanel_patchLog);
            tabPage_patch.Controls.Add(button_patch);
            tabPage_patch.Controls.Add(checkBox_deleteTempAfterPatch);
            tabPage_patch.Controls.Add(checkBox_installAfterPatch);
            tabPage_patch.Location = new Point(4, 23);
            tabPage_patch.Name = "tabPage_patch";
            tabPage_patch.Size = new Size(222, 273);
            tabPage_patch.TabIndex = 1;
            tabPage_patch.Text = "Patch";
            // 
            // label_patchSelected
            // 
            label_patchSelected.Font = new Font("JetBrains Mono", 7F, FontStyle.Bold);
            label_patchSelected.Location = new Point(6, 10);
            label_patchSelected.Name = "label_patchSelected";
            label_patchSelected.Size = new Size(180, 15);
            label_patchSelected.TabIndex = 0;
            label_patchSelected.Text = "Target: None";
            // 
            // button_patchHelp
            // 
            button_patchHelp.FlatStyle = FlatStyle.Popup;
            button_patchHelp.Location = new Point(190, 5);
            button_patchHelp.Name = "button_patchHelp";
            button_patchHelp.Size = new Size(25, 25);
            button_patchHelp.TabIndex = 1;
            button_patchHelp.Text = "?";
            button_patchHelp.Click += button_patchHelp_Click;
            // 
            // flowLayoutPanel_patchLog
            // 
            flowLayoutPanel_patchLog.BackColor = Color.White;
            flowLayoutPanel_patchLog.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel_patchLog.Location = new Point(6, 35);
            flowLayoutPanel_patchLog.Name = "flowLayoutPanel_patchLog";
            flowLayoutPanel_patchLog.Size = new Size(210, 145);
            flowLayoutPanel_patchLog.TabIndex = 2;
            // 
            // button_patch
            // 
            button_patch.FlatStyle = FlatStyle.Popup;
            button_patch.Location = new Point(6, 185);
            button_patch.Name = "button_patch";
            button_patch.Size = new Size(210, 30);
            button_patch.TabIndex = 3;
            button_patch.Text = "Patch Game";
            button_patch.Click += button_patch_Click;
            // 
            // checkBox_deleteTempAfterPatch
            // 
            checkBox_deleteTempAfterPatch.AutoSize = true;
            checkBox_deleteTempAfterPatch.Location = new Point(6, 220);
            checkBox_deleteTempAfterPatch.Name = "checkBox_deleteTempAfterPatch";
            checkBox_deleteTempAfterPatch.Size = new Size(96, 18);
            checkBox_deleteTempAfterPatch.TabIndex = 4;
            checkBox_deleteTempAfterPatch.Text = "Clean Temp";
            // 
            // checkBox_installAfterPatch
            // 
            checkBox_installAfterPatch.AutoSize = true;
            checkBox_installAfterPatch.Location = new Point(6, 240);
            checkBox_installAfterPatch.Name = "checkBox_installAfterPatch";
            checkBox_installAfterPatch.Size = new Size(110, 18);
            checkBox_installAfterPatch.TabIndex = 5;
            checkBox_installAfterPatch.Text = "Auto Install";
            // 
            // tabPage_mods
            // 
            tabPage_mods.Controls.Add(label_modsSelected);
            tabPage_mods.Controls.Add(listBox_mods);
            tabPage_mods.Controls.Add(button_modsRefresh);
            tabPage_mods.Controls.Add(button_modsAdd);
            tabPage_mods.Controls.Add(button_modsRemove);
            tabPage_mods.Controls.Add(label_modsStatus);
            tabPage_mods.Location = new Point(4, 23);
            tabPage_mods.Name = "tabPage_mods";
            tabPage_mods.Size = new Size(222, 273);
            tabPage_mods.TabIndex = 2;
            tabPage_mods.Text = "Mods";
            // 
            // label_modsSelected
            // 
            label_modsSelected.Font = new Font("JetBrains Mono", 7F, FontStyle.Bold);
            label_modsSelected.Location = new Point(6, 10);
            label_modsSelected.Name = "label_modsSelected";
            label_modsSelected.Size = new Size(210, 15);
            label_modsSelected.TabIndex = 0;
            label_modsSelected.Text = "Target: None";
            // 
            // listBox_mods
            // 
            listBox_mods.Location = new Point(6, 30);
            listBox_mods.Name = "listBox_mods";
            listBox_mods.Size = new Size(210, 144);
            listBox_mods.TabIndex = 1;
            // 
            // button_modsRefresh
            // 
            button_modsRefresh.FlatStyle = FlatStyle.Popup;
            button_modsRefresh.Location = new Point(6, 185);
            button_modsRefresh.Name = "button_modsRefresh";
            button_modsRefresh.Size = new Size(68, 25);
            button_modsRefresh.TabIndex = 2;
            button_modsRefresh.Text = "Refresh";
            button_modsRefresh.Click += button_modsRefresh_Click;
            // 
            // button_modsAdd
            // 
            button_modsAdd.FlatStyle = FlatStyle.Popup;
            button_modsAdd.Location = new Point(78, 185);
            button_modsAdd.Name = "button_modsAdd";
            button_modsAdd.Size = new Size(68, 25);
            button_modsAdd.TabIndex = 3;
            button_modsAdd.Text = "Add .so";
            button_modsAdd.Click += button_modsAdd_Click;
            // 
            // button_modsRemove
            // 
            button_modsRemove.FlatStyle = FlatStyle.Popup;
            button_modsRemove.Location = new Point(150, 185);
            button_modsRemove.Name = "button_modsRemove";
            button_modsRemove.Size = new Size(66, 25);
            button_modsRemove.TabIndex = 4;
            button_modsRemove.Text = "Remove";
            button_modsRemove.Click += button_modsRemove_Click;
            // 
            // label_modsStatus
            // 
            label_modsStatus.ForeColor = Color.Gray;
            label_modsStatus.Location = new Point(6, 215);
            label_modsStatus.Name = "label_modsStatus";
            label_modsStatus.Size = new Size(210, 40);
            label_modsStatus.TabIndex = 5;
            label_modsStatus.Text = "Ready.";
            // 
            // tabPage_library
            // 
            tabPage_library.Controls.Add(label_libSelected);
            tabPage_library.Controls.Add(button_libDownload);
            tabPage_library.Controls.Add(label_libStatus);
            tabPage_library.Location = new Point(4, 23);
            tabPage_library.Name = "tabPage_library";
            tabPage_library.Size = new Size(222, 273);
            tabPage_library.TabIndex = 3;
            tabPage_library.Text = "Lib";
            // 
            // label_libSelected
            // 
            label_libSelected.Font = new Font("JetBrains Mono", 7F, FontStyle.Bold);
            label_libSelected.Location = new Point(6, 10);
            label_libSelected.Name = "label_libSelected";
            label_libSelected.Size = new Size(210, 15);
            label_libSelected.TabIndex = 0;
            label_libSelected.Text = "Target: None";
            // 
            // button_libDownload
            // 
            button_libDownload.FlatStyle = FlatStyle.Popup;
            button_libDownload.Location = new Point(6, 220);
            button_libDownload.Name = "button_libDownload";
            button_libDownload.Size = new Size(210, 30);
            button_libDownload.TabIndex = 1;
            button_libDownload.Text = "Download & Push";
            button_libDownload.Click += button_libDownload_Click;
            // 
            // label_libStatus
            // 
            label_libStatus.ForeColor = Color.Gray;
            label_libStatus.Location = new Point(6, 200);
            label_libStatus.Name = "label_libStatus";
            label_libStatus.Size = new Size(210, 20);
            label_libStatus.TabIndex = 2;
            label_libStatus.Text = "Select library...";
            // 
            // tabPage_dumper
            // 
            tabPage_dumper.Controls.Add(label_dumpSelected);
            tabPage_dumper.Controls.Add(button_dump);
            tabPage_dumper.Controls.Add(label_dumpStatus);
            tabPage_dumper.Location = new Point(4, 23);
            tabPage_dumper.Name = "tabPage_dumper";
            tabPage_dumper.Size = new Size(222, 273);
            tabPage_dumper.TabIndex = 4;
            tabPage_dumper.Text = "Dump";
            // 
            // label_dumpSelected
            // 
            label_dumpSelected.Font = new Font("JetBrains Mono", 7F, FontStyle.Bold);
            label_dumpSelected.Location = new Point(6, 10);
            label_dumpSelected.Name = "label_dumpSelected";
            label_dumpSelected.Size = new Size(210, 15);
            label_dumpSelected.TabIndex = 0;
            label_dumpSelected.Text = "Target: None";
            // 
            // button_dump
            // 
            button_dump.FlatStyle = FlatStyle.Popup;
            button_dump.Location = new Point(6, 100);
            button_dump.Name = "button_dump";
            button_dump.Size = new Size(210, 40);
            button_dump.TabIndex = 1;
            button_dump.Text = "Dump Game (Il2Cpp)";
            button_dump.Click += button_dump_Click;
            // 
            // label_dumpStatus
            // 
            label_dumpStatus.Font = new Font("JetBrains Mono", 7F);
            label_dumpStatus.ForeColor = Color.Gray;
            label_dumpStatus.Location = new Point(6, 150);
            label_dumpStatus.Name = "label_dumpStatus";
            label_dumpStatus.Size = new Size(210, 60);
            label_dumpStatus.TabIndex = 2;
            label_dumpStatus.Text = "Requires Root/ADB access to /data/app/";
            // 
            // tabPage_pull
            // 
            tabPage_pull.Controls.Add(label_pullSelected);
            tabPage_pull.Controls.Add(button_pull);
            tabPage_pull.Location = new Point(4, 23);
            tabPage_pull.Name = "tabPage_pull";
            tabPage_pull.Size = new Size(222, 273);
            tabPage_pull.TabIndex = 5;
            tabPage_pull.Text = "Pull";
            // 
            // label_pullSelected
            // 
            label_pullSelected.Font = new Font("JetBrains Mono", 7F, FontStyle.Bold);
            label_pullSelected.Location = new Point(6, 10);
            label_pullSelected.Name = "label_pullSelected";
            label_pullSelected.Size = new Size(210, 15);
            label_pullSelected.TabIndex = 0;
            label_pullSelected.Text = "Target: None";
            // 
            // button_pull
            // 
            button_pull.FlatStyle = FlatStyle.Popup;
            button_pull.Location = new Point(6, 50);
            button_pull.Name = "button_pull";
            button_pull.Size = new Size(210, 30);
            button_pull.TabIndex = 1;
            button_pull.Text = "Pull APK to PC";
            button_pull.Click += button_pull_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(225, 290);
            Controls.Add(tabControl_main);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Easy Patcher";
            Load += Form1_Load;
            tabControl_main.ResumeLayout(false);
            tabPage_home.ResumeLayout(false);
            tabPage_home.PerformLayout();
            tabPage_patch.ResumeLayout(false);
            tabPage_patch.PerformLayout();
            tabPage_mods.ResumeLayout(false);
            tabPage_library.ResumeLayout(false);
            tabPage_dumper.ResumeLayout(false);
            tabPage_pull.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TabControl tabControl_main;


        private TabPage tabPage_home;
        private ListBox listBox_homePackages;
        private Button button_homeRefresh;
        private Label label_homeTitle;
        private Label label_homeGameInfo;


        private TabPage tabPage_patch;
        private Label label_patchSelected;
        private Button button_patchHelp;
        private FlowLayoutPanel flowLayoutPanel_patchLog;
        private Button button_patch;
        private CheckBox checkBox_deleteTempAfterPatch;
        private CheckBox checkBox_installAfterPatch;


        private TabPage tabPage_mods;
        private Label label_modsSelected;
        private ListBox listBox_mods;
        private Button button_modsRefresh;
        private Button button_modsAdd;
        private Button button_modsRemove;
        private Label label_modsStatus;


        private TabPage tabPage_library;
        private Label label_libSelected;
        private Label label_libStatus;
        private Button button_libDownload;


        private TabPage tabPage_pull;
        private Label label_pullSelected;
        private Button button_pull;

        // Dumper
        private TabPage tabPage_dumper;
        private Label label_dumpSelected;
        private Button button_dump;
        private Label label_dumpStatus;
        private Label label_homeStatus;
    }
}