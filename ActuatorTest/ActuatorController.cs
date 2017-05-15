using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidSharp;
using System.Threading;
namespace ActuatorTest
{
    public class ActuatorController
    {
        HidDevice usb_controller;
        HidStream stream;
        HidDeviceLoader loader;

        public bool connected = false;

        public bool overrideToStopActuator1 = false;
        public bool overrideToStopActuator2 = false;
        public bool overrideToStopActuator3 = false;

        private int actuator1_limit_open = 40;
        private int actuator2_limit_open = 40;
        private int actuator3_limit_open = 55;

        private int actuator1_limit_close = 210;
        private int actuator2_limit_close = 210;
        private int actuator3_limit_close = 210;

        private int max_error_counter = 20;

        private byte[] actuatorCommand = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        private enum ActuatorCommand 
        {
            OPEN,
            CLOSE,
            STOP
        }

        public ActuatorController() 
        {

        }

        public bool Connect()
        {
            try
            {
                loader = new HidDeviceLoader();
                usb_controller = loader.GetDevices(0x03eb, 0x204f).First();
                connected = usb_controller.TryOpen(out stream);
                if (connected)
                {
                    Console.WriteLine("Connected...");
                    int[] positions = getPositions();
                    Console.WriteLine("Current position: " + positions[0]);
                }
                return connected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when connecting to actuators: " + ex.Message);
                return false;
            }
        }

        public void Actuator1Action(bool open) 
        {
            int errorCounter = 0;
            while (true) 
            {
                int[] positions = getPositions();
                if (open)
                {
                    if (positions[0] >= actuator1_limit_open)
                    {
                        if (overrideToStopActuator1)
                        {
                            actuatorCommander(1, 1, 2, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(1, 1, 2, ActuatorCommand.OPEN))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[0]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
                else 
                {
                    if (positions[0] <= actuator1_limit_close)
                    {
                        if (overrideToStopActuator1)
                        {
                            actuatorCommander(1, 1, 2, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(1, 1, 2, ActuatorCommand.CLOSE))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[0]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
            }
        }

        public void Actuator2Action(bool open) 
        {
            int errorCounter = 0;
            while (true)
            {
                int[] positions = getPositions();
                if (open)
                {
                    if (positions[1] >= actuator2_limit_open)
                    {
                        if (overrideToStopActuator2) 
                        {
                            actuatorCommander(2, 3, 4, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(2, 3, 4, ActuatorCommand.OPEN))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[1]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
                else
                {
                    if (positions[1] <= actuator2_limit_close)
                    {
                        if (overrideToStopActuator2) 
                        {
                            actuatorCommander(2, 3, 4, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(2, 3, 4, ActuatorCommand.CLOSE))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[1]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
            }
        }

        public void Actuator3Action(bool open) 
        {
            int errorCounter = 0;
            while (true)
            {
                int[] positions = getPositions();
                if (open)
                {
                    if (positions[2] >= actuator3_limit_open)
                    {
                        if (overrideToStopActuator3) 
                        {
                            actuatorCommander(3, 5, 6, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(3, 5, 6, ActuatorCommand.OPEN))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[2]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
                else
                {
                    if (positions[2] <= actuator3_limit_close)
                    {
                        if (overrideToStopActuator3) 
                        {
                            actuatorCommander(3, 5, 6, ActuatorCommand.STOP);
                            break;
                        }
                        if (!actuatorCommander(3, 5, 6, ActuatorCommand.CLOSE))
                        {
                            errorCounter++;
                        }
                        Thread.Sleep(100);
                        Console.WriteLine("Position: " + positions[2]);
                        if (errorCounter >= max_error_counter)
                        {
                            errorCounter = 0;
                            break;
                        }
                    }
                    else break;
                }
            }
        }

        public int[] getPositions() 
        {
            int[] positions = new int[3] { 0, 0, 0 };
            try 
            {
                byte[] data = stream.Read();
                positions[0] = data[7];
                positions[1] = data[8];
                positions[2] = data[9];
                connected = true;
            }
            catch (Exception ex)
            {
                connected = false;
                Console.WriteLine("Error when getting actuator positions: " + ex.Message);
            }
            return positions;
        }

        private bool actuatorCommander(int actuator, int index1, int index2, ActuatorCommand cmd) 
        {
            try
            {
                switch (cmd) 
                {
                    case ActuatorCommand.OPEN:
                        actuatorCommand[index1] = 1;
                        actuatorCommand[index2] = 0;
                        break;
                    case ActuatorCommand.CLOSE:
                        actuatorCommand[index1] = 0;
                        actuatorCommand[index2] = 1;
                        break;
                    case ActuatorCommand.STOP:
                        actuatorCommand[index1] = 0;
                        actuatorCommand[index2] = 0;
                        break;
                }
                stream.Write(actuatorCommand);
                return true;
            }
            catch (Exception ex) 
            {
                switch (cmd) 
                {
                    case ActuatorCommand.OPEN:
                        Console.WriteLine("Error when opening ACTUATOR #" + actuator.ToString() + ": " + ex.Message);
                        break;
                    case ActuatorCommand.CLOSE:
                        Console.WriteLine("Error when closing ACTUATOR #" + actuator.ToString() + ": " + ex.Message);
                        break;
                    case ActuatorCommand.STOP:
                        Console.WriteLine("Error when stopping ACTUATOR #" + actuator.ToString() + ": " + ex.Message);
                        break;
                }
                return false;
            }
        }
    }
}
