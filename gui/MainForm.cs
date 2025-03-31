using System.IO.Ports;

namespace RAB3RadarDistanceMeasurementGUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// RDK2 object enabling to collect the radar data
        /// </summary>
        private RDK2 rdk2;

        /// <summary>
        /// Object enabling to perform computation
        /// </summary>
        private RadarProcessor processor;

        private MeasurementConfiguration configuration;

        public MainForm()
        {
            InitializeComponent();

            rdk2 = new RDK2();
            rdk2.OnNewConnectionState += Rdk2_OnNewConnectionState;
            rdk2.OnNewFrame += Rdk2_OnNewFrame;

            processor = new RadarProcessor();
            configuration = new MeasurementConfiguration();
        }

        private void Rdk2_OnNewFrame(object sender, ushort[] frame)
        {
            System.Numerics.Complex[] spectrum = processor.GetAverageRangeFFT(frame);
            double[] dbfs = processor.ComputeAsDBFS(spectrum);

            double minValue = ArrayUtils.getMin(dbfs);

            for(int i = 0; i < configuration.skipAtStart; ++i)
            {
                dbfs[i] = minValue;
            }

            spectrumView.setSpectrumDBFS(dbfs);

            distanceView.SetDistance(processor.GetDetectedRange(dbfs, configuration.threshold));
        }

        private void Rdk2_OnNewConnectionState(object sender, RDK2.ConnectionState state)
        {
            switch (state)
            {
                case RDK2.ConnectionState.Connected:
                    connectionStateTextBox.Text = "Connected";
                    connectionStateTextBox.BackColor = Color.Green;
                    connectButton.Enabled = false;
                    break;

                case RDK2.ConnectionState.Error:
                    connectionStateTextBox.Text = "Error";
                    connectionStateTextBox.BackColor = Color.Red;
                    connectButton.Enabled = true;
                    break;

                case RDK2.ConnectionState.Iddle:
                    connectionStateTextBox.Text = "Not connected";
                    connectionStateTextBox.BackColor = Color.Gray;
                    connectButton.Enabled = true;
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load the possible com ports
            string[] serialPorts = SerialPort.GetPortNames();
            comPortComboBox.DataSource = serialPorts;

            // Load settings
            configuration.threshold = Properties.Settings.Default.threshold;
            configuration.skipAtStart = Properties.Settings.Default.skipAtStart;

            spectrumView.setThreshold(configuration.threshold);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if ((comPortComboBox.SelectedIndex < 0) || (comPortComboBox.SelectedIndex >= comPortComboBox.Items.Count)) return;

            var selectedItem = comPortComboBox.Items[comPortComboBox.SelectedIndex];
            if (selectedItem != null)
            {
                string? portName = selectedItem.ToString();
                if (portName != null) rdk2.SetPortName(portName);
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationForm form = new ConfigurationForm(configuration.threshold, configuration.skipAtStart);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Copy
            configuration.skipAtStart = form.skipAtStart;
            configuration.threshold = form.threshold;

            spectrumView.setThreshold(configuration.threshold);

            // Store
            Properties.Settings.Default.skipAtStart = form.skipAtStart;
            Properties.Settings.Default.threshold = form.threshold;
            Properties.Settings.Default.Save();
        }
    }
}
