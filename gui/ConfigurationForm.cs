using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAB3RadarDistanceMeasurementGUI
{
    public partial class ConfigurationForm : Form
    {
        public double threshold = -30;
        public int skipAtStart = 4;

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        public ConfigurationForm(double threshold, int skipAtStart)
        {
            InitializeComponent();
            this.thresholdTextBox.Text = threshold.ToString();
            this.skipAtStartTextBox.Text = skipAtStart.ToString();

            this.threshold = threshold;
            this.skipAtStart = skipAtStart;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Double.TryParse(thresholdTextBox.Text, out this.threshold) == false)
            {
                MessageBox.Show("Invalid threshold value...");
                return;
            }

            if (Int32.TryParse(skipAtStartTextBox.Text, out this.skipAtStart) == false)
            {
                MessageBox.Show("Invalid skip at start value...");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
