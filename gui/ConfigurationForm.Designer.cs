namespace RAB3RadarDistanceMeasurementGUI
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            thresholdLabel = new Label();
            thresholdTextBox = new TextBox();
            dBmLabel = new Label();
            skipAtStartLabel = new Label();
            skipAtStartTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // thresholdLabel
            // 
            thresholdLabel.AutoSize = true;
            thresholdLabel.Location = new Point(12, 15);
            thresholdLabel.Name = "thresholdLabel";
            thresholdLabel.Size = new Size(77, 20);
            thresholdLabel.TabIndex = 0;
            thresholdLabel.Text = "Threshold:";
            // 
            // thresholdTextBox
            // 
            thresholdTextBox.Location = new Point(105, 12);
            thresholdTextBox.Name = "thresholdTextBox";
            thresholdTextBox.Size = new Size(125, 27);
            thresholdTextBox.TabIndex = 1;
            thresholdTextBox.Text = "-30";
            // 
            // dBmLabel
            // 
            dBmLabel.AutoSize = true;
            dBmLabel.Location = new Point(236, 15);
            dBmLabel.Name = "dBmLabel";
            dBmLabel.Size = new Size(40, 20);
            dBmLabel.TabIndex = 2;
            dBmLabel.Text = "dBm";
            // 
            // skipAtStartLabel
            // 
            skipAtStartLabel.AutoSize = true;
            skipAtStartLabel.Location = new Point(12, 48);
            skipAtStartLabel.Name = "skipAtStartLabel";
            skipAtStartLabel.Size = new Size(87, 20);
            skipAtStartLabel.TabIndex = 3;
            skipAtStartLabel.Text = "Skip at start";
            // 
            // skipAtStartTextBox
            // 
            skipAtStartTextBox.Location = new Point(105, 45);
            skipAtStartTextBox.Name = "skipAtStartTextBox";
            skipAtStartTextBox.Size = new Size(125, 27);
            skipAtStartTextBox.TabIndex = 4;
            skipAtStartTextBox.Text = "4";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            okButton.Location = new Point(12, 144);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 5;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(360, 144);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 185);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(skipAtStartTextBox);
            Controls.Add(skipAtStartLabel);
            Controls.Add(dBmLabel);
            Controls.Add(thresholdTextBox);
            Controls.Add(thresholdLabel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConfigurationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Measurement Configuration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label thresholdLabel;
        private TextBox thresholdTextBox;
        private Label dBmLabel;
        private Label skipAtStartLabel;
        private TextBox skipAtStartTextBox;
        private Button okButton;
        private Button cancelButton;
    }
}