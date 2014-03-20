
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
        int sensivity_factor;
        public Form1()
        {

            InitializeComponent();
            tb1.Text += "Hello User \r\n";
            feeder1 = new feeder();
            UpdateMyStatus(feeder1.getStatus());
            String[] asd = new String[9];

            asd = feeder1.LogDetails();
            tb1.Text += "X Axis " + asd[0] + "\r\n";
            tb1.Text += "Y Axis " + asd[1] + "\r\n";
            tb1.Text += "Z Axis " + asd[2] + "\r\n";
            tb1.Text += "RX Axis " + asd[3] + "\r\n";
            tb1.Text += "RZ Axis " + asd[4] + "\r\n";
            tb1.Text += "No of Buttons " + asd[5] + "\r\n";
            tb1.Text += "No of Constant POV " + asd[6] + "\r\n";
            tb1.Text += "No of Discrete POV " + asd[7] + "\r\n";
            tb1.Text += "Press Start feeding to Start " +  "\r\n";

        }


        public void updateLog(String AddThisToLog)
        {
            this.tb1.Text += "\r\n" + AddThisToLog;
        }


        private void btn1_Click(object sender, EventArgs e)
        {
            if (!feeder1._keepRunning)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(feeder1.InitFeeding));
                tb1.Text += "Feeding Started" +" \r\n";
            }
            else
            {
                tb1.Text +="Feeder alreading feeding \r\n";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (feeder1._keepRunning)
            {
                feeder1.StopFeeding();
                tb1.Text += "\r\n" + "Feeding stoped ";
            }

        }


        private void UpdateMyStatus(String status)
        {

            switch (status)
            {
                case "VJD_STAT_OWN":
                    tb1.Text += "vJoy Device  is already owned by this feeder \r\n";
                    break;
                case "VJD_STAT_FREE":
                    tb1.Text += "vJoy Device  is free and can be started \r\n";
                    break;
                case "VJD_STAT_BUSY":
                    tb1.Text += "vJoy Device  is already owned by another feeder\r\nCannot continue\r\n";
                    return;
                case "VJD_STAT_MISS":
                    tb1.Text += "vJoy Device  is not installed or disabled\r\nCannot continue\r\n";
                    return;
                default:
                    tb1.Text += "vJoy Device general error\r\nCannot continue\r\n";
                    return;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            feeder1.StopFeeding();
            feeder1.applyChanges(sensivity_factor);
            tb1.Text += "Changes Applied please restart feeding";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GameLad Pc App 1.0 \nWritten by:Alaf Azam Khan & Ankit Verma\n'Hope you have fun'\nFeedbacks are Welcome", "About");
        }

       






    }







    public class feeder
    {
        vJoy joystick;
        uint id = 15;
        int localPort = 6000, bufferSize = 7;
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
        public float conversionFactor;
        private int sensivityFactor = 2;

        public feeder()
        {
            localEndPoint = new IPEndPoint(localAddress, localPort);
            // Create the server socket 
            serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);
            // Bind the socket to the local interface specified
            serverSocket.Bind(localEndPoint);
            //create the joystick handler
            joystick = new vJoy();
            //set value for max_of_Axis
            joystick.GetVJDAxisMax(id, HID_USAGES.HID_USAGE_X, ref maxval);
            conversionFactor = maxval / 255;

        }

        public void applyChanges(int s)
        {
            sensivityFactor = s;
        }


        public string getStatus()
        {
            VjdStat status = this.joystick.GetVJDStatus(this.id);
            return "" + status;

        }


        public void InitFeeding(object state)
        {
            iReport = new vJoy.JoystickState();
            //Acquire Vjoystick Driver
            joystick.AcquireVJD(id);
            //reset vjoy driver
            joystick.ResetAll();
            this.StartFeeding(new object());
        }

        public void StartFeeding(object state)
        {
            byte[] receiveBuffer = new byte[bufferSize];
            _keepRunning = true;
            // Wait for a client connection
            this.serverSocket.Listen(1);

            try
            {

                clientSocket = serverSocket.Accept();
                while (_keepRunning)
                {
                    clientSocket.Receive(receiveBuffer);
                    //creating Ireport for feeding
                    iReport = new vJoy.JoystickState();
                    //main parsing and feeding
                    iReport.bDevice = (byte)id;
                    iReport.AxisX = (int)(conversionFactor * ((int)receiveBuffer[3] / sensivityFactor * sensivityFactor + sensivityFactor - 1));
                    iReport.AxisY = (int)(conversionFactor * ((int)receiveBuffer[4] / sensivityFactor * sensivityFactor + sensivityFactor - 1));
                    iReport.AxisZ = (int)(conversionFactor * ((int)receiveBuffer[5] / sensivityFactor * sensivityFactor + sensivityFactor - 1));
                    iReport.AxisZRot = (int)(conversionFactor * ((int)receiveBuffer[6] / sensivityFactor * sensivityFactor + sensivityFactor - 1));
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
                    this.InitFeeding(new object());
                }

                // Console.WriteLine("Server: Socket error occurred: {0}", err.Message);
            }
            finally
            {

                //serverSocket.Dispose();
                clientSocket.Dispose();


            }


            //end of startfeeding
        }


        public String[] LogDetails()
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
            String[] asd;
            asd = new String[9];
            asd[0] = "" + AxisX;
            asd[1] = "" + AxisY;
            asd[2] = "" + AxisZ;
            asd[3] = "" + AxisRX;
            asd[4] = "" + AxisRZ;
            asd[5] = "" + nButtons;
            asd[6] = "" + ContPovNumber;
            asd[7] = "" + DiscPovNumber;

            return asd;
        }

        public void StopFeeding()
        {
            _keepRunning = false;
        }




    }
}