namespace Lab3_3sem_UI;
using System.Diagnostics;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        //prepare to use:
        if (!File.Exists("Solution.exe") == true) {
            Process preparation = new Process();
            preparation.StartInfo.UseShellExecute = false;

            // Перехватываем вывод
            preparation.StartInfo.RedirectStandardOutput = true;
            preparation.StartInfo.RedirectStandardInput = true;
            preparation.StartInfo.CreateNoWindow = true;
            // Запускаемое приложение
            preparation.StartInfo.FileName = "CMD.exe";
            //p.StartInfo.FileName = "example.exe";

            // Передаем необходимые аргументы
            // p.Arguments = "example.txt";
            preparation.Start();
            preparation.StandardInput.WriteLine("g++ Solution.cpp -o Solution.exe");
            preparation.StandardInput.WriteLine("exit");
            preparation.WaitForExit();
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}