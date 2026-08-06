using System;
using System.Windows.Forms;

namespace MobisHaims
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
            Application.Run(new ShellForm());
        }
    }
}
