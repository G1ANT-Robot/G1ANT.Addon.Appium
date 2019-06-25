using System.Diagnostics;

namespace G1ANT.Addon.Appium
{
    public static class CmdHelper
    {
        public static void RunCommand(string pathToProgramm, string command)
        {
            Process.Start(pathToProgramm, command);
        }
    }
}
