using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUtime
{
    class MISUtimestamp
    {
        public const long DECIMAL_EPOCH = 76603264427;
        private const double _calibration = 0.00032502;
        private readonly double _dtimestamp;
        private readonly DateTime _eTime;

        public double Timestamp
        {
            get
            {
                return Math.Round(_dtimestamp, 8);
            }
        }

        public int Year
        {
            get
            {
                return (int)Math.Floor(_dtimestamp);
            }
        }
        public int Day
        {
            get
            {
                double fractional = _dtimestamp - Math.Floor(_dtimestamp);
                long dddhhmmss = (long)Math.Round(fractional * 100000000);
                return (int)(dddhhmmss / 100000);
            }
        }
        public int Hour
        {
            get
            {
                double fractional = _dtimestamp - Math.Floor(_dtimestamp);
                long dddhhmmss = (long)Math.Round(fractional * 100000000);
                return (int)((dddhhmmss % 100000) / 10000);
            }
        }
        public int Minute
        {
            get
            {
                double fractional = _dtimestamp - Math.Floor(_dtimestamp);
                long dddhhmmss = (long)Math.Round(fractional * 100000000);
                return (int)((dddhhmmss % 10000) / 100);
            }
        }
        public int Second
        {
            get
            {
                double fractional = _dtimestamp - Math.Floor(_dtimestamp);
                long dddhhmmss = (long)Math.Round(fractional * 100000000);
                return (int)(dddhhmmss % 100);
            }
        }
        public MISUtimestamp(double dtimestamp)
        {
            _dtimestamp = dtimestamp;
            _eTime = ToDateTime(this);
        }

        public MISUtimestamp(DateTime eTime)
        {
            long ticks = eTime.Ticks;
            double totalRegularSeconds = (double)ticks / TimeSpan.TicksPerSecond;
            double totalMisuSeconds = (totalRegularSeconds / 0.864) + DECIMAL_EPOCH;

            long totalMisuDays = (long)(totalMisuSeconds / 100000);
            double remainingMisuSeconds = totalMisuSeconds % 100000;

            int hour = (int)(remainingMisuSeconds / 10000);
            remainingMisuSeconds %= 10000;
            int minute = (int)(remainingMisuSeconds / 100);
            int second = (int)(remainingMisuSeconds % 100);

            int year = (int)(totalMisuDays / 1000) + 1;
            int dayOfYear = (int)(totalMisuDays % 1000);

            double fractionalPart = dayOfYear * 100000.0 + hour * 10000.0 + minute * 100.0 + second;
            fractionalPart /= 100000000.0; // 1e8

            _dtimestamp = year + fractionalPart + _calibration;
            _eTime = eTime;
        }

        public static DateTime ToDateTime(MISUtimestamp mTime)
        {
            double dt = mTime.Timestamp;
            int year = (int)dt;
            double fractionalPart = dt - year;

            long dddhhmmss = (long)Math.Round((fractionalPart - _calibration) * 100000000);
            int dayOfYear = (int)(dddhhmmss / 100000);
            int hour = (int)((dddhhmmss % 100000) / 10000);
            int minute = (int)((dddhhmmss % 10000) / 100);
            int second = (int)(dddhhmmss % 100);

            long totalMisuDays = (year - 1) * 1000L + dayOfYear;
            long totalMisuSeconds = totalMisuDays * 100000L + hour * 10000L + minute * 100L + second;

            double totalRegularSeconds = (totalMisuSeconds - DECIMAL_EPOCH) * 0.864;
            long ticks = (long)(totalRegularSeconds * TimeSpan.TicksPerSecond);

            return new DateTime(ticks, DateTimeKind.Utc);
        }
    }
}
