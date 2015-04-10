using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.UI.Screens
{
    public abstract class Screen
    {
        public abstract void Display();
        public void Run()
        {
            Console.Clear();
            this.Display();
        }
        public void GoTo(Screen nextScreen)
        {
            nextScreen.Run();
            this.Run();
        }
    }
}
