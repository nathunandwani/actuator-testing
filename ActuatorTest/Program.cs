using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidSharp;
using System.Threading;

namespace ActuatorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ActuatorController controller = new ActuatorController();
            if (controller.Connect()) 
            {
                Console.WriteLine("Opening actuator 1");
                controller.Actuator1Action(true);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[0]);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Closing actuator 1");
                controller.Actuator1Action(false);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[0]);

                Console.WriteLine("Opening actuator 2");
                controller.Actuator2Action(true);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[1]);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Closing actuator 2");
                controller.Actuator2Action(false);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[1]);

                Console.WriteLine("Opening actuator 3");
                controller.Actuator3Action(true);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[2]);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Closing actuator 3");
                controller.Actuator3Action(false);
                Console.WriteLine("DELAY: 3 SECONDS");
                Thread.Sleep(3000);
                Console.WriteLine("Current position: " + controller.getPositions()[2]);
            }

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
