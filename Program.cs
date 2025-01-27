using System.Runtime.CompilerServices;

namespace MISUtime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MISUtimestamp m = new MISUtimestamp(new DateTime(1951, 2, 3));
            MISUtimestamp m = new MISUtimestamp(new DateTime(2025, 1, 21));
            Console.WriteLine(m.Timestamp);
            Console.WriteLine(MISUtimestamp.ToDateTime(m).ToString());
        }
    }//1943
    class MISUtimestamp
    {
        public const long DECIMAL_EPOCH = 76603264427;
        private readonly double _dtimestamp;
        private readonly DateTime _eTime;

        public double Timestamp
        {
            get
            {
                return double.Parse(_dtimestamp.ToString("0.########"));
            }
        }

        public int Year
        {
            get
            {
                return int.Parse(_dtimestamp.ToString("0.000000000").Substring(0, 4));
            }
        }
        public int Day
        {
            get
            {
                return int.Parse(_dtimestamp.ToString("0.000000000").Substring(5, 3));
            }
        }
        public int Hour
        {
            get
            {
                return int.Parse(_dtimestamp.ToString("0.000000000").Substring(8, 1));
            }
        }
        public int Minute
        {
            get
            {
                return int.Parse(_dtimestamp.ToString("0.000000000").Substring(9, 2));
            }
        }
        public int Second
        {
            get
            {
                return int.Parse(_dtimestamp.ToString("0.000000000").Substring(11, 2));
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

            _dtimestamp = year + fractionalPart;
            _eTime = eTime;
        }

        public static DateTime ToDateTime(MISUtimestamp mTime)
        {
            double dt = mTime.Timestamp;
            int year = (int)dt;
            double fractionalPart = dt - year;

            long dddhhmmss = (long)Math.Round(fractionalPart * 100000000);
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
