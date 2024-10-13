using System.Diagnostics;

namespace Domain.Models.ValueObjects.SiteData
{
    public static class SiteDataService
    {
        public static void BuildHugoSite(string userId)
        {
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
                    WorkingDirectory = $"./site-creator/{userId}"
                };

                using (Process process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception("Произошла ошибка при сборке сайта.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
