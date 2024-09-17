using System.Diagnostics;

namespace Domain.Models.ValueObjects.SiteData
{
    public static class SiteDataService
    {
        public static void BuildHugoSite(string userId)
        {
            Environment.SetEnvironmentVariable("HUGO_JSON_DATA_PATH", $"../../../../../StaticData/site-constructor/{userId}/data.json", EnvironmentVariableTarget.Process);
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "hugo",
                    Arguments = "-D",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = "./site-creator/sample"
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;

                    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                    process.ErrorDataReceived += (sender, e) => Console.WriteLine("ERROR: " + e.Data);

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("Сайт успешно собран.");
                    }
                    else
                    {
                        Console.WriteLine("Произошла ошибка при сборке сайта.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
