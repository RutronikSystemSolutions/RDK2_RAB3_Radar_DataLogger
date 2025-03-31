using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RAB3RadarDistanceMeasurementGUI
{
    public class RadarProcessor
    {
        private double startFrequency = 60000000000;
        private double endFrequency = 61500000000;
        private double samplingRate = 2000000;
        private int samplesPerChirp = 256;
        private int chirpsPerFrame = 32;

        private SignalWindow window;
        private double[] timeBuffer;

        public RadarProcessor()
        {
            timeBuffer = new double[samplesPerChirp];
            window = new SignalWindow(SignalWindow.Type.TypeBlackmanHarris, samplesPerChirp);
        }

        public System.Numerics.Complex[] GetAverageRangeFFT(UInt16[] frame)
        {
            int spectrumLen = (samplesPerChirp / 2) + 1;

            // Init retval
            System.Numerics.Complex[] spectrum = new System.Numerics.Complex[spectrumLen];
            for (int i = 0; i < spectrum.Length; ++i) spectrum[i] = 0;

            for(int chirpIndex = 0; chirpIndex < chirpsPerFrame; ++chirpIndex)
            {
                // Copy samples to time buffer
                int startIndex = chirpIndex * samplesPerChirp;
                for (int sampleIndex = 0; sampleIndex < samplesPerChirp; ++sampleIndex)
                {
                    timeBuffer[sampleIndex] = frame[startIndex + sampleIndex];
                }

                // Time buffer now contains the samples of one antenna during a chirp
                // Scale between 0 and 1
                ArrayUtils.scaleInPlace(timeBuffer, 1.0 / 4096.0);

                // Compute the average
                double average = ArrayUtils.getAverage(timeBuffer);

                // Offset
                ArrayUtils.offsetInPlace(timeBuffer, -average);

                // Apply windows
                window.applyInPlace(timeBuffer);

                // Compute real FFT
                // Size of spectrum is (SamplesPerChirp / 2) + 1
                System.Numerics.Complex[] chirpSpectrum = FftSharp.FFT.ForwardReal(timeBuffer);

                // Add to retval (averaging)
                for(int binIndex = 0; binIndex < chirpSpectrum.Length; ++binIndex)
                {
                    spectrum[binIndex] += chirpSpectrum[binIndex];
                }
            }

            // Compute average value
            for (int binIndex = 0; binIndex < spectrum.Length; ++binIndex)
            {
                spectrum[binIndex] = spectrum[binIndex] / chirpsPerFrame;
            }

            return spectrum;
        }

        public double[] ComputeAsDBFS(System.Numerics.Complex[] spectrum)
        {
            double[] retval = new double[spectrum.Length];
            for (int i = 0; i < retval.Length; ++i)
            {
                retval[i] = (spectrum[i].Magnitude * 2) / window.getSum();
                retval[i] = 20 * Math.Log10(retval[i] / 1);
            }
            return retval;
        }

        public double GetDetectedRange(double[] dbfs, double threshold)
        {
            if (dbfs == null) return double.NaN;

            int index = 0;
            double max = dbfs[0];

            for(int i = 0; i < dbfs.Length; ++i)
            {
                if (dbfs[i] > max)
                {
                    max = dbfs[i];
                    index = i;
                }
            }

            if (max < threshold) return double.NaN;

            double bandWidth = endFrequency - startFrequency;
            double celerity = 299792458;
            double fftLen = dbfs.Length;
            double slope = bandWidth / (samplesPerChirp * (1 / samplingRate));

            double fractionFs = index / ((fftLen - 1) * 2);
            double freq = fractionFs * samplingRate;
            double rangeMeters = (celerity * freq) / (2 * slope);

            return rangeMeters;
        }
    }
}
