using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace RAB3RadarDistanceMeasurementGUI
{
    public partial class SpectrumView : UserControl
    {
        private double startFrequency = 60000000000;
        private double endFrequency = 61500000000;
        private double samplingRate = 2000000;
        private int samplesPerChirp = 256;

        private double threshold = -30;

        /// <summary>
        /// X Axis
        /// </summary>
        LinearAxis xAxisSpectrum = new LinearAxis
        {
            MajorGridlineStyle = LineStyle.Dot,
            Position = AxisPosition.Bottom,
            AxislineStyle = LineStyle.Solid,
            AxislineColor = OxyColors.Gray,
            FontSize = 10,
            PositionAtZeroCrossing = true,
            IsPanEnabled = false,
            IsZoomEnabled = true,
            Unit = "Range (m)"
        };

        /// <summary>
        /// Y Axis for amplitude
        /// </summary>
        private LinearAxis yAxisSpectrum = new LinearAxis
        {
            MajorGridlineStyle = LineStyle.Dot,
            AxislineStyle = LineStyle.Solid,
            AxislineColor = OxyColors.Gray,
            FontSize = 10,
            Minimum = -120,
            Maximum = 0,
            TextColor = OxyColors.Gray,
            Position = AxisPosition.Left,
            IsPanEnabled = false,
            IsZoomEnabled = true,
            Unit = "Magnitude (dBFS)",
            Key = "Amp",
        };

        private LineAnnotation thresholdLine = new LineAnnotation()
        {
            StrokeThickness = 1,
            Color = OxyColors.Green,
            Type = LineAnnotationType.Horizontal,
            Text = "Threshold",
            TextColor = OxyColors.White
        };

        private LineSeries spectrumLineSeries = new LineSeries();

        public SpectrumView()
        {
            InitializeComponent();
            InitPlot();
        }

        private void InitPlot()
        {
            // Spectrum
            var timeModel = new PlotModel
            {
                PlotType = PlotType.XY,
                PlotAreaBorderThickness = new OxyThickness(0),
            };

            // Set the axes
            timeModel.Axes.Add(xAxisSpectrum);
            timeModel.Axes.Add(yAxisSpectrum);

            // Add series
            spectrumLineSeries.Title = "Spectrum";
            spectrumLineSeries.YAxisKey = yAxisSpectrum.Key;

            timeModel.Series.Add(spectrumLineSeries);

            thresholdLine.Y = threshold;
            timeModel.Annotations.Add(thresholdLine);

            plotView.Model = timeModel;
            plotView.InvalidatePlot(true);
        }

        public void setSpectrumDBFS(double[] spectrum)
        {
            double bandWidth = endFrequency - startFrequency;
            double celerity = 299792458;
            double fftLen = spectrum.Length;
            double slope = bandWidth / (samplesPerChirp * (1 / samplingRate));

            spectrumLineSeries.Points.Clear();
            for (int i = 0; i < spectrum.Length; ++i)
            {
                double fractionFs = i / ((fftLen - 1) * 2);
                double freq = fractionFs * samplingRate;
                double rangeMeters = (celerity * freq) / (2 * slope);
                spectrumLineSeries.Points.Add(new DataPoint(rangeMeters, spectrum[i]));
            }
            
            plotView.InvalidatePlot(true);
        }

        public void setThreshold(double threshold)
        {
            this.threshold = threshold;
            thresholdLine.Y = threshold;
            plotView.InvalidatePlot(true);
        }
    }
}
