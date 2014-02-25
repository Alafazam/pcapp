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
        static feeder feeder1;
        public Form1()
        {
            
            InitializeComponent();
             lbl.Text += "\nHELLO";
             feeder1 = new feeder();
             UpdateMyStatus(feeder1.getStatus());
        }

       
        public  void updateLog(String AddThisToLog){
            this.lbl.Text += "\n" + AddThisToLog;
        } 

        private void btn1_Click(object sender, EventArgs e)
        {
            if (!feeder1.started)
            {   
            feeder1.started = true;
            feeder1.feederThread.Start();
            updateLog("Feeder Started");
            }else
            updateLog("Feeder can not be Started again,please restart ");
           
           
        }

        private void UpdateMyStatus (String status){
        
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
            updateLog("Feeder Already Not Runnig");
            
        }

        
       
    }

    public class feeder{
            vJoy joystick;
            uint id = 15;
            int localPort = 6000
            , bufferSize = 9;//Buffer size is 9 beacause 2 extra bytes are recieved..
            IPAddress localAddress = IPAddress.Any;
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;
            Socket serverSocket = null;
            IPEndPoint localEndPoint;
            
        public bool started=false;  
        public  Thread feederThread;
            
        public feeder() {    
            localEndPoint = new IPEndPoint(localAddress, localPort);
            // Create the server socket
            serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);
            feederThread = new Thread(this.StartFeeding);
            joystick = new vJoy();
        }

           
        public string getStatus()
        {
            VjdStat status = this.joystick.GetVJDStatus(this.id);
            return "" + status;

        }

        public void StartFeeding()
        {   
            
            vJoy.JoystickState iReport;
            iReport = new vJoy.JoystickState();
            joystick.AcquireVJD(id);
            joystick.ResetAll();
           //updateLog("Reacing try block");
            Socket clientSocket;
            byte[] receiveBuffer = new byte[bufferSize];
            // Bind the socket to the local interface specified
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(1);
            // Wait for a client connection
            clientSocket = serverSocket.Accept();
                

                // lbl.Text += "\n" + "Server Started";
               while (true)
                {
                    clientSocket.Receive(receiveBuffer);
                    // lbl.Text += "Heap size reciever" + receiveBuffer.Length + "\n";
                    iReport = new vJoy.JoystickState();
                    //main parsing and feeding
                    iReport.bDevice = (byte)id;
                    iReport.AxisX = 128 * (int)receiveBuffer[3] * 2;
                    iReport.AxisY = 128 * (int)receiveBuffer[4] * 2;
                    iReport.AxisZ = 128 * (int)receiveBuffer[5] * 2;
                    iReport.AxisZRot = (int)receiveBuffer[6];

                    // Set buttons one by one
                    iReport.Buttons = (uint)(receiveBuffer[1] + 256 * (receiveBuffer[2] % 2) + 512 * ((receiveBuffer[2] / 2) % 2));
                    int pov = (int)receiveBuffer[2] >> 2;
                    if (pov == 4) { pov = -1; }
                    iReport.bHats = (uint)pov;
                    joystick.UpdateVJD(id, ref iReport);
                }
            
            
            


            //end of startfeeding
        }

        public void LogDetails() {

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