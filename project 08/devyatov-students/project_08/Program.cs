using StudentManager.Data;
using StudentManager.Forms;
using System;
using System.Windows.Forms;

namespace StudentManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаем репозиторий и передаем в главную форму
            var repository = new StudentRepository();
            Application.Run(new MainForm(repository));
        }
    }
}
