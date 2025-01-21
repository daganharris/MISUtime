namespace MISUtime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MISUtimestamp m = new MISUtimestamp(new DateTime(1951, 2, 3));
            Console.WriteLine(m.Timestamp);
        }
    }//1943
    class MISUtimestamp
    {
        private const double DECIMAL_EPOCH = 95397359816;
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
        }
        public MISUtimestamp(DateTime eTime)
        {
            _eTime = eTime;
            double aggregatedDays = ((double)_eTime.Year * 365.2425) + _eTime.DayOfYear;

            double aggregatedSeconds = (_eTime.Hour * 60 * 60) + (_eTime.Minute * 60) + (aggregatedDays * 24 * 60 * 60);
            double dSeconds = (aggregatedSeconds * 0.864) + DECIMAL_EPOCH;

            _dtimestamp = dSeconds / (100 * 100 * 10 * 1000);
        }
        public MISUtimestamp(int year, int month, int day, int hour, int minute, int second)
        {
            _eTime = new DateTime(year, month, day, hour, minute, second);
            double aggregatedDays = ((double)_eTime.Year * 365.2425) + _eTime.DayOfYear;

            double aggregatedSeconds = (_eTime.Hour * 60 * 60) + (_eTime.Minute * 60) + (aggregatedDays * 24 * 60 * 60);

            double dSeconds = (aggregatedSeconds + DECIMAL_EPOCH) * 0.864;

            _dtimestamp = dSeconds / (100 * 100 * 10 * 1000);
        }
        public MISUtimestamp(int year, int day, int hour, int minute, int second)
        {
            _dtimestamp = year;
            _dtimestamp += day / 1000;
            _dtimestamp += hour / (1000 * 10);
            _dtimestamp += minute / (1000 * 10 * 100);
            _dtimestamp += minute / (1000 * 10 * 100 * 100);
        }
        public static DateTime ToDateTime(MISUtimestamp mTime)
        {
            throw new NotImplementedException("To Do!");
            // to do
            //int dSeconds = mTime.Second + (mTime.Minute * 100) + (mTime.Hour * 100 * 100) + (mTime.Day * 100 * 100 * 10) + (mTime.Year * 100 * 100 * 10 * 1000);

            //double aggregateSeconds = (dSeconds / 0.864); // - DECIMAL_EPOCH;
            //Console.WriteLine(aggregateSeconds);
            //int eYear = (int)Math.Floor(aggregateSeconds / 365.2425);
            //aggregateSeconds -= eYear;

            //int eDay = (int)Math.Floor(aggregateSeconds / (24 * 60 * 60));
            //aggregateSeconds -= eDay;

            //int[] eDaysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };           
            //int eMonth = 0;

            //if (DateTime.IsLeapYear(eYear))
            //{
            //    eDaysInMonth[1] += 1; // 29th of Febuary
            //}

            //foreach (var eDaysInThisMonth in eDaysInMonth)
            //{
            //    if (eDay >= eDaysInThisMonth)
            //    {
            //        eDay -= eDaysInThisMonth;
            //        eMonth++;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //int eHour = (int)Math.Floor(aggregateSeconds / (60 * 60));
            //aggregateSeconds -= eHour;

            //int eMinute = (int)Math.Floor(aggregateSeconds / (60));
            //aggregateSeconds -= eMinute;

            //int eSecond = (int)Math.Floor(aggregateSeconds);

            //return new DateTime(eYear, eMonth, eDay, eHour, eMinute, eSecond);
        }
    }
}
