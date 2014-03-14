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
            if (feeder1._firstRun)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(feeder1.InitFeeding));
                lbl.Text += "\nfirst run";
            }
            else
            {
                if (!feeder1._keepRunning)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(feeder1.InitFeeding));
                    lbl.Text += "\n Feeding restarted";
                }
                else
                {
                    lbl.Text += "\nfeeder alreading feeding";
                }
            }
        
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (feeder1._keepRunning)
            {
                feeder1.StopFeeding();
                lbl.Text += "\n" + "feeding stoped ";
            }
            
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
        public Thread feederThread;
        Socket clientSocket;
        long maxval = 0;
        public volatile bool _keepRunning = false;
        public volatile bool _firstRun = true;
       


        public feeder()
        {
            localEndPoint = new IPEndPoint(localAddress, localPort);
            // Create the server socket
            
            joystick = new vJoy();
            joystick.GetVJDAxisMax(id, HID_USAGES.HID_USAGE_X, ref maxval);
        }


        public string getStatus()
        {
            VjdStat status = this.joystick.GetVJDStatus(this.id);
            return "" + status;

        }


        public void InitFeeding(object state)
        {
            iReport = new vJoy.JoystickState();
            joystick.AcquireVJD(id);
            joystick.ResetAll();

          
            _firstRun = false;

            this.StartFeeding(new object());
        }



        public void StartFeeding(object state)
        {//start the server object
            serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);

            // Bind the socket to the local interface specified
            serverSocket.Bind(localEndPoint);
            
            _keepRunning = true;
            byte[] receiveBuffer = new byte[bufferSize];
            // Wait for a client connection
        this.serverSocket.Listen(1);

            
        try
        {
            
            clientSocket = serverSocket.Accept();
            this.LogDetails();
            //lbl.Text += "\n" + "Server Started";
            while (_keepRunning)
            {
                clientSocket.Receive(receiveBuffer);
                // lbl.Text += "Heap size reciever" + receiveBuffer.Length + "\n";
                iReport = new vJoy.JoystickState();
                //main parsing and feeding
                iReport.bDevice = (byte)id;
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

        }
        catch (SocketException err)
        {
            if (err.Message == "An existing connection was forcibly closed by the remote host")
            {
                serverSocket.Dispose();
                this.InitFeeding(new object() );
            }
            
            // Console.WriteLine("Server: Socket error occurred: {0}", err.Message);
        }
        finally {

            serverSocket.Dispose();
            clientSocket.Dispose();

        }


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
            _keepRunning = false;
        }




    }
}