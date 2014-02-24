using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vJoyInterfaceWrap;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace DemoFeederAlpha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbl.Text += "HELLO";
        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        static int GetIntegerFromBinaryString(string binary, int bitCount)
        {
            if (binary.Length == bitCount && binary[0] == '1')
                return Convert.ToInt32(binary.PadLeft(32, '1'), 2);
            else
                return Convert.ToInt32(binary, 2);
        }

        public static void StartFeeding()
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            vJoy joystick;
            vJoy.JoystickState iReport;
            iReport = new vJoy.JoystickState();
            uint id = 15;
            joystick = new vJoy();
            joystick.AcquireVJD(id);
            joystick.ResetAll();

            int localPort = 6000
             , bufferSize = 9;
            //Buffer size is 9 beacause 2 extra bytes are recieved..
            IPAddress localAddress = IPAddress.Any;
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;
            Socket serverSocket = null;
            lbl.Text += "Reacing try block";
            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(localAddress, localPort);
                Socket clientSocket;
                byte[] receiveBuffer = new byte[bufferSize];
                int rc;
                // Create the server socket
                serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);
                // Bind the socket to the local interface specified
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(1);
                // Wait for a client connection
                clientSocket = serverSocket.Accept();
                lbl.Text += "\n" + "Server Started";
                while (true)
                {
                    clientSocket.Receive(receiveBuffer);
                    lbl.Text += "Heap size reciever" + receiveBuffer.Length + "\n";
                    iReport = new vJoy.JoystickState();
                    //main parsing and feeding
                    iReport.bDevice = (byte)id;
                    iReport.AxisX = 128 * (int)receiveBuffer[3] * 2;
                    iReport.AxisY = 128 * (int)receiveBuffer[4] * 2;
                    iReport.AxisZ = 128 * (int)receiveBuffer[5] * 2;
                    iReport.AxisZRot = (int)receiveBuffer[6];

                    // Set buttons one by one
                    iReport.Buttons = (uint)(receiveBuffer[1] + 256 * (receiveBuffer[2] % 2)+ 512 * ((receiveBuffer[2] / 2) % 2)  );
                    int pov =(int)receiveBuffer[2]>>2;
                    if (pov==4)
                    {
                        pov = -1;
                    }
                    iReport.bHats = (uint)pov;
                    joystick.UpdateVJD(id, ref iReport);
                }
            }
            catch (SocketException err)
            { // Console.WriteLine("Server: Socket error occurred: {0}", err.Message);
            }
            finally { if (serverSocket != null)serverSocket.Close(); }


            //end of startfeeding


            //end of class
        }
    }
}