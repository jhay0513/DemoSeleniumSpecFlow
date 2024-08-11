using System.Text.Json;

namespace DemoSeleniumSpecFlow.Support
{
    public static class Util
    {
        public static string GetProjectPath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            return projectDirectory;
        }

        public static string GetAppSetting(string key)
        {
            string appsettingPath = Path.Combine(GetProjectPath(), "appsettings.json");
            var setting = JsonDocument.Parse(File.ReadAllText(appsettingPath));
            var settingValue = setting.RootElement.GetProperty(key);

            return settingValue.ToString();
        }
    }
}
