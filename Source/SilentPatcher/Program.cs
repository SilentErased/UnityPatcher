namespace SilentPatcher
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new UnityPatcher.Form1());
        }
    }
}
