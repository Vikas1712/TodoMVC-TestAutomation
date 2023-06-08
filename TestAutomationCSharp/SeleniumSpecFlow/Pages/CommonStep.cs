using System.Diagnostics;

namespace SeleniumSpecFlow.Pages;

public static class CommonStep
{
    public static void KillProcessLocally(string processName)
    {
        var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processName));
        if (processes.Length == 0)
            Console.WriteLine($"Not found '{processName}' - nothing to kill");
        else
            foreach (var p in processes)
                try
                {
                    p.Kill();
                    Console.WriteLine($"'{processName}' - killed successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to kill '{processName}':");
                    Console.WriteLine($"Exception: {ex}");
                }
    }
}