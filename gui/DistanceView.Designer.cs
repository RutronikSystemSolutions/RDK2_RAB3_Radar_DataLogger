namespace RAB3RadarDistanceMeasurementGUI
{
    partial class DistanceView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            distanceLabel = new Label();
            SuspendLayout();
            // 
            // distanceLabel
            // 
            distanceLabel.BorderStyle = BorderStyle.FixedSingle;
            distanceLabel.Dock = DockStyle.Fill;
            distanceLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            distanceLabel.Location = new Point(0, 0);
            distanceLabel.Name = "distanceLabel";
            distanceLabel.Size = new Size(167, 221);
            distanceLabel.TabIndex = 0;
            distanceLabel.Text = "--- m";
            distanceLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DistanceView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(distanceLabel);
            Name = "DistanceView";
            Size = new Size(167, 221);
            ResumeLayout(false);
        }

        #endregion

        private Label distanceLabel;
    }
}
