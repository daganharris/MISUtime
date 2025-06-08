using System.Runtime.CompilerServices;

namespace MISUtime
{
    internal class Program
    {
        static string SemanticVersion = "1.0.0";
        static void Main(string[] args)
        {
            //MISUtimestamp m = new MISUtimestamp(new DateTime(1951, 2, 3));
            switch (args[0])
            {
                case "--earth-input":
                case "-e":
                    EarthInput(args);
                    break;
                case "--misu-input":
                case "-m":
                    MISUInput(args);
                    break;
                case "--help":
                case "-h":
                    ColorPrinter.PrintColored("&yellow MISUtime v" + SemanticVersion + " &white - Convert Earth and MISU timestamps in terminal.\n" +
                                              "&white By &yellow daganharris &white on GitHub\n" +
                                              "&yellow Usage: &white MISUtime [options] [timestamp]\n" +
                                              "&yellow Options:\n" +
                                              "&cyan  --earth-input&white , &cyan -e &white - Convert Earth ISO(!!) timestamp to MISU timestamp\n" +
                                              "&cyan  --misu-input&white , &cyan -m &white - Convert MISU timestamp to Earth timestamp\n" +
                                              "&cyan  --help&white , &cyan -h &white - Show this help message");
                    break;
            }
        }
        static void EarthInput(string[] args)
        {
            ColorPrinter.PrintColored("&yellow MISUtime v" + SemanticVersion);
            try
            {
                MISUtimestamp MISUtimestamp = new MISUtimestamp(DateTime.Parse(args[1]));
                ColorPrinter.PrintColored("&white Earth Timestamp: &green " + args[1] + "\n" +
                                          "&white MISU Timestamp: &magenta " + MISUtimestamp.Timestamp);
            }
            catch (FormatException)
            {
                ColorPrinter.PrintColored("&red ERROR: &white Invalid date format.\n &white Please use ISO format &cyan (YYYY-MM-DDTHH:MM:SSZ).");
                return;
            }
            catch (ArgumentOutOfRangeException)
            {
                ColorPrinter.PrintColored("&red ERROR: &white Date is out of range for MISU timestamp conversion.");
                return;
            }
            catch (Exception ex)
            {
                ColorPrinter.PrintColored($"&red An unexpected error occurred:\n &white {ex.Message}");
                return;
            }
        }
        static void MISUInput(string[] args)
        {
            ColorPrinter.PrintColored("&yellow MISUtime v" + SemanticVersion);
            try
            {
                MISUtimestamp MISUtimestamp = new MISUtimestamp(Convert.ToDouble(args[1]));
                ColorPrinter.PrintColored("&white MISU Timestamp: &green " + args[1] + "\n" +
                                          "&white Earth Timestamp: &magenta " + MISUtimestamp.ToDateTime(MISUtimestamp));
            }
            catch (FormatException)
            {
                ColorPrinter.PrintColored("&red ERROR: &white Invalid date format.\n &white Please use MISU format &cyan (YYYY.MWDHHMMSS).");
                return;
            }
            catch (OverflowException)
            {
                ColorPrinter.PrintColored("&red ERROR: &white Date is out of range for Earth timestamp conversion.");
                return;
            }
            catch (Exception ex)
            {
                ColorPrinter.PrintColored($"&red An unexpected error occurred:\n &white {ex.Message}");
                return;
            }
        }
    }
}
