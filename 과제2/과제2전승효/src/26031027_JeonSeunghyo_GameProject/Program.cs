using System;
using System.Windows.Forms;

namespace _26031027_JeonSeunghyo_GameProject
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new GameForm());
        }
    }
}
