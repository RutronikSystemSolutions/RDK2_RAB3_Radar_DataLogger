namespace RAB3RadarDistanceMeasurementGUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new Label();
            comPortComboBox = new ComboBox();
            connectButton = new Button();
            connectionStateTextBox = new TextBox();
            spectrumView = new SpectrumView();
            mainSplitContainer = new SplitContainer();
            menuStrip = new MenuStrip();
            configurationToolStripMenuItem = new ToolStripMenuItem();
            distanceView = new DistanceView();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "COM port:";
            // 
            // comPortComboBox
            // 
            comPortComboBox.FormattingEnabled = true;
            comPortComboBox.Location = new Point(95, 36);
            comPortComboBox.Name = "comPortComboBox";
            comPortComboBox.Size = new Size(151, 28);
            comPortComboBox.TabIndex = 1;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(252, 35);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(94, 29);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // connectionStateTextBox
            // 
            connectionStateTextBox.Location = new Point(352, 36);
            connectionStateTextBox.Name = "connectionStateTextBox";
            connectionStateTextBox.ReadOnly = true;
            connectionStateTextBox.Size = new Size(125, 27);
            connectionStateTextBox.TabIndex = 3;
            connectionStateTextBox.Text = "Not connected";
            // 
            // spectrumView
            // 
            spectrumView.BorderStyle = BorderStyle.FixedSingle;
            spectrumView.Dock = DockStyle.Fill;
            spectrumView.Location = new Point(0, 0);
            spectrumView.Name = "spectrumView";
            spectrumView.Size = new Size(791, 472);
            spectrumView.TabIndex = 4;
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainSplitContainer.Location = new Point(12, 70);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(spectrumView);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(distanceView);
            mainSplitContainer.Size = new Size(1088, 472);
            mainSplitContainer.SplitterDistance = 791;
            mainSplitContainer.TabIndex = 5;
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(255, 128, 255);
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { configurationToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1112, 28);
            menuStrip.TabIndex = 6;
            menuStrip.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.Size = new Size(114, 24);
            configurationToolStripMenuItem.Text = "Configuration";
            configurationToolStripMenuItem.Click += configurationToolStripMenuItem_Click;
            // 
            // distanceView
            // 
            distanceView.Dock = DockStyle.Fill;
            distanceView.Location = new Point(0, 0);
            distanceView.Name = "distanceView";
            distanceView.Size = new Size(293, 472);
            distanceView.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 554);
            Controls.Add(mainSplitContainer);
            Controls.Add(connectionStateTextBox);
            Controls.Add(connectButton);
            Controls.Add(comPortComboBox);
            Controls.Add(label1);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RAB3 Radar Distance Measurement v1.0";
            Load += MainForm_Load;
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comPortComboBox;
        private Button connectButton;
        private TextBox connectionStateTextBox;
        private SpectrumView spectrumView;
        private SplitContainer mainSplitContainer;
        private MenuStrip menuStrip;
        private ToolStripMenuItem configurationToolStripMenuItem;
        private DistanceView distanceView;
    }
}
