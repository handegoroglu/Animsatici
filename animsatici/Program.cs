using Microsoft.Win32;

namespace animsatici
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            string getValue = "";
            try
            {
                getValue = key.GetValue("Reminding").ToString();
            }
            catch (Exception)
            {
            }

            try
            {
                if (getValue.ToString() != "\"" + Application.ExecutablePath + "\"")
                {
                    key.SetValue("Reminding", "\"" + Application.ExecutablePath + "\"");
                }
            }
            catch (Exception)
            {
            }
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}