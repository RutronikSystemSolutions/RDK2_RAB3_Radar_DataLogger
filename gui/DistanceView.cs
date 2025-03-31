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
    public partial class DistanceView : UserControl
    {
        public DistanceView()
        {
            InitializeComponent();
        }

        public void SetDistance(double distance)
        {
            if (double.IsNaN(distance))
            {
                distanceLabel.Text = "? m";
            }
            else
            {
                distanceLabel.Text = string.Format("{0:0.00} m", distance);
            }
        }
    }
}
