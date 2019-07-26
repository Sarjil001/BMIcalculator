using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Author's Name : SARJIL RAVAL
/// Student Number: 301043757
/// Date Last Modified: 2019-07-25
/// Program Description: This program works as a BMI Calculator and calculates your BMI as 
///                      per the values entered. It starts with a splash screen and after 3 sec
///                      it will open the Calculator.
/// </summary>

namespace BMIcalculator
{
    static class Program
    {
        //Form Properties
        public static SplashScreen startform {get; set;}
        public static BMICalculator mainform { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            startform = new SplashScreen();
            mainform = new BMICalculator();
            Application.Run(new SplashScreen());
        }
    }
}
