using System;
using System.Threading;
using JHipsterNet.Boot.Ansi;

namespace JHipsterNet.Boot {

    public class BannerPrinter {

        public static void PrintBanner(int sleepMilli = 0) {
            Console.WriteLine($"  {AnsiColor.GREEN} ██████╗ ██████╗ ███╗   ███╗██████╗ ██╗   ██╗██╗     ███████╗████████╗██████╗  █████╗ {AnsiColor.MAGENTA}   ███╗   ██╗███████╗████████╗");
            Console.WriteLine($"  {AnsiColor.GREEN}██╔════╝██╔═══██╗████╗ ████║██╔══██╗██║   ██║██║     ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗{AnsiColor.MAGENTA}   ████╗  ██║██╔════╝╚══██╔══╝");
            Console.WriteLine($"  {AnsiColor.GREEN}██║     ██║   ██║██╔████╔██║██████╔╝██║   ██║██║     █████╗     ██║   ██████╔╝███████║{AnsiColor.MAGENTA}   ██╔██╗ ██║█████╗     ██║   ");
            Console.WriteLine($"  {AnsiColor.GREEN}██║     ██║   ██║██║╚██╔╝██║██╔═══╝ ██║   ██║██║     ██╔══╝     ██║   ██╔══██╗██╔══██║{AnsiColor.MAGENTA}   ██║╚██╗██║██╔══╝     ██║   ");
            Console.WriteLine($"  {AnsiColor.GREEN}╚██████╗╚██████╔╝██║ ╚═╝ ██║██║     ╚██████╔╝███████╗███████╗   ██║   ██║  ██║██║  ██║{AnsiColor.MAGENTA}██╗██║ ╚████║███████╗   ██║   ");
            Console.WriteLine($"  {AnsiColor.GREEN} ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝      ╚═════╝ ╚══════╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝{AnsiColor.MAGENTA}╚═╝╚═╝  ╚═══╝╚══════╝   ╚═╝   ");
            Console.WriteLine($"{AnsiColor.WHITE}█████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine($"{AnsiColor.BRIGHT_BLUE}:: JHipster.Net 🤓  :: Running ASP.Net Core 'The best version' ::");
            Console.WriteLine($":: COMPULETRA LTDA ::{AnsiColor.DEFAULT}");
            Thread.Sleep(sleepMilli);
        }
    }
}
