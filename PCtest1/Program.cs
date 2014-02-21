using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using vJoyInterfaceWrap;
namespace PCtest1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static public vJoy joystick;
        static public uint id = 15;
       

        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           
         

        }
    }
}
