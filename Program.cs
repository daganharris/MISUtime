
using System.Diagnostics;

namespace MISUtime
{
    internal class Program
    {
        private static string _semanticVersion = "1.0.0";
        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                ColorPrinter.Print("&red ERROR: &white Please provide a timestamp and an option.\n" +
                                    "&yellow Help: &white MISUtime -h\n");
                return;
            }
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
                    ColorPrinter.Print("&yellow MISUtime v" + _semanticVersion + " &white - Convert Earth and MISU timestamps in terminal.\n" +
                                        "&white By &yellow daganharris &white on GitHub\n" +
                                        "&yellow Usage: &white MISUtime [options] [timestamp]\n" +
                                        "&yellow Options:\n" +
                                        "&cyan  --earth-input&white , &cyan -e &white - Convert Earth ISO(!!) timestamp to MISU timestamp\n" +
                                        "&cyan  --misu-input&white , &cyan -m &white - Convert MISU timestamp to Earth timestamp\n" +
                                        "&cyan  --help&white , &cyan -h &white - Show this help message" +
                                        "&cyan  --now&white , &cyan -n &white - Shows current time in both formats" +
                                        "&cyan  --clock&white , &cyan -c &white - Shows current time in both formats and updates constantly");
                    break;
                case "--now":
                case "-n":
                    CurrentTime();
                    break;
                case "--clock":
                case "-c":
                    await Clock();
                    break;
                default:
                    ColorPrinter.Print("&red ERROR: &white Argument unrecognised.\n" +
                                       "&yellow Help: &white MISUtime -h\n");
                    break;
            }
        }
        static async Task Clock()
        {
            while (true)
            {
                Console.Clear();
                CurrentTime();
                ColorPrinter.Print("&white Press &cyan CTRL &white + &cyan C &white to exit.\n");
                await Task.Delay(500);
            } 
        }
        static void CurrentTime()
        {
            DateTime now = DateTime.Now;
            ColorPrinter.Print("&yellow MISUtime v" + _semanticVersion);
            MISUtimestamp MISUtimestamp = new MISUtimestamp(now);
            ColorPrinter.Print("&white Earth Timestamp: &green " + now + "\n" +
                               "&white MISU Timestamp: &magenta " + MISUtimestamp.Timestamp.ToString("F8"));
        }
        static void EarthInput(string[] args)
        {
            ColorPrinter.Print("&yellow MISUtime v" + _semanticVersion);
            try
            {
                MISUtimestamp MISUtimestamp = new MISUtimestamp(DateTime.Parse(args[1]));
                ColorPrinter.Print("&white Earth Timestamp: &green " + args[1] + "\n" +
                                    "&white MISU Timestamp: &magenta " + MISUtimestamp.Timestamp.ToString("F8"));
            }
            catch (FormatException)
            {
                ColorPrinter.Print("&red ERROR: &white Invalid date format.\n &white Please use ISO format &cyan (YYYY-MM-DDTHH:MM:SSZ).");
            }
            catch (ArgumentOutOfRangeException)
            {
                ColorPrinter.Print("&red ERROR: &white Date is out of range for MISU timestamp conversion.");
            }
            catch (Exception ex)
            {
                ColorPrinter.Print($"&red An unexpected error occurred:\n &white {ex.Message}");
            }
        }
        static void MISUInput(string[] args)
        {
            ColorPrinter.Print("&yellow MISUtime v" + _semanticVersion);
            try
            {
                MISUtimestamp MISUtimestamp = new MISUtimestamp(Convert.ToDouble(args[1]));
                ColorPrinter.Print("&white MISU Timestamp: &green " + args[1] + "\n" +
                                    "&white Earth Timestamp: &magenta " + MISUtimestamp.ToDateTime(MISUtimestamp));
            }
            catch (FormatException)
            {
                ColorPrinter.Print("&red ERROR: &white Invalid date format.\n &white Please use MISU format &cyan (YYYY.MWDHHMMSS).");
            }
            catch (OverflowException)
            {
                ColorPrinter.Print("&red ERROR: &white Date is out of range for Earth timestamp conversion.");
            }
            catch (Exception ex)
            {
                ColorPrinter.Print($"&red An unexpected error occurred:\n &white {ex.Message}");
            }
        }
    }
}
