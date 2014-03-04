using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using vJoyInterfaceWrap;
using System.Net;
using System.Net.Sockets;

namespace DemoFeederAlpha
{
    public partial class Form1 : Form
    {
        feeder feeder1;
        public Form1()
        {

            InitializeComponent();
            lbl.Text += "\nHELLO";
            feeder1 = new feeder();
            UpdateMyStatus(feeder1.getStatus());
        }


        public void updateLog(String AddThisToLog)
        {
            this.lbl.Text += "\n" + AddThisToLog;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (!feeder1.started)
            {
                feeder1.started = true;
                feeder1.feederThread.Start();
                updateLog("Feeder Started");
            }
            else
                updateLog("Feeder can not be Started again,please restart ");


        }

        private void UpdateMyStatus(String status)
        {

            switch (status)
            {
                case "VJD_STAT_OWN":
                    lbl.Text += "vJoy Device  is already owned by this feeder\n";
                    break;
                case "VJD_STAT_FREE":
                    lbl.Text += "\nvJoy Device  is free and can be started";
                    break;
                case "VJD_STAT_BUSY":
                    lbl.Text += "vJoy Device  is already owned by another feeder\nCannot continue\n";
                    return;
                case "VJD_STAT_MISS":
                    lbl.Text += "vJoy Device  is not installed or disabled\nCannot continue\n";
                    return;
                default:
                    lbl.Text += "vJoy Device general error\nCannot continue\n";
                    return;
            };
        }


        private void btn2_Click(object sender, EventArgs e)
        {
            if (feeder1.started)
            {

                feeder1.StopFeeding();
                updateLog("Feeder Stopped");
            }
            else
                updateLog("Feeder Already Not Running");

        }



    }







    public class feeder
    {
        vJoy joystick;
        uint id = 15;
        int localPort = 6000
        , bufferSize = 7;
        IPAddress localAddress = IPAddress.Any;
        SocketType sockType = SocketType.Stream;
        ProtocolType sockProtocol = ProtocolType.Tcp;
        Socket serverSocket = null;
        IPEndPoint localEndPoint;
        vJoy.JoystickState iReport;
        public bool started = false;
        public Thread feederThread;
        Socket clientSocket;
        long maxval = 0;

        public feeder()
        {
            localEndPoint = new IPEndPoint(localAddress, localPort);
            // Create the server socket
            serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);
            feederThread = new Thread(this.StartFeeding);
            joystick = new vJoy();
            joystick.GetVJDAxisMax(id, HID_USAGES.HID_USAGE_X, ref maxval);
        }


        public string getStatus()
        {
            VjdStat status = this.joystick.GetVJDStatus(this.id);
            return "" + status;

        }

        public void StartFeeding()
        {


            iReport = new vJoy.JoystickState();
            joystick.AcquireVJD(id);
            joystick.ResetAll();
            //updateLog("Reacing try block");

            byte[] receiveBuffer = new byte[bufferSize];
            // Bind the socket to the local interface specified
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(1);
            // Wait for a client connection
            clientSocket = serverSocket.Accept();

      //      try
        //    {
                // lbl.Text += "\n" + "Server Started";
                while (true)
                {
                    clientSocket.Receive(receiveBuffer);
                    // lbl.Text += "Heap size reciever" + receiveBuffer.Length + "\n";
                    iReport = new vJoy.JoystickState();
                    //main parsing and feeding
                    iReport.bDevice = (byte)id;
                    //
                    iReport.AxisX = (int)((maxval / 255) * (int)receiveBuffer[3]);
                    iReport.AxisY = (int)((maxval / 255) * (int)receiveBuffer[4]);
                    iReport.AxisZ = (int)((maxval / 255) * (int)receiveBuffer[5]);
                    iReport.AxisZRot = (int)((maxval / 255) * (int)receiveBuffer[6]);

                    // Set buttons one by one
                    iReport.Buttons = (uint)(receiveBuffer[1] + 256 * (receiveBuffer[2] % 2) + 512 * ((receiveBuffer[2] / 2) % 2));
                    int pov = (int)receiveBuffer[2] >> 2;
                    //if (pov == 4)
                        iReport.bHats = 0xFFFFFFFF;
                    //else
                      //  iReport.bHats = (uint)pov;
                    joystick.UpdateVJD(id, ref iReport);
                }

            //}
          //  catch (SocketException err)
            //{
                
                // Console.WriteLine("Server: Socket error occurred: {0}", err.Message);
            //}
            //finally
            //{
                // Close the socket if necessary
              //  if (serverSocket != null)
                //{
                    //Console.WriteLine("Server: Closing using Close()...");
                 //   serverSocket.Close();
                //}
            



            //end of startfeeding
        }

        public void LogDetails()
        {

            bool AxisX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X);
            bool AxisY = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y);
            bool AxisZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z);
            bool AxisRX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RX);
            bool AxisRZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RZ);
            // Get the number of buttons and POV Hat switchessupported by this vJoy device
            int nButtons = joystick.GetVJDButtonNumber(id);
            int ContPovNumber = joystick.GetVJDContPovNumber(id);
            int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);


        }
        public void StopFeeding()
        {
            serverSocket.Close();
            feederThread.Abort();

        }




    }
}