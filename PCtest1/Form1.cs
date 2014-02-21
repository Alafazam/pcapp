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


namespace PCtest1
{
    public partial class Form1 : Form
    {
        static public vJoy joystick;
        static public vJoy.JoystickState iReport;
        static public uint id = 15;
        public Form1()
        {
            joystick = new vJoy();
                
            joystick.AcquireVJD(id);
            joystick.ResetAll();


            InitializeComponent();

            lbl.Text += "" + joystick.vJoyEnabled();
            // Get the state of the requested device
            VjdStat status = joystick.GetVJDStatus(id);
            lbl.Text += "\n" + status; 
        }

       

       private void btn_Click(object sender, EventArgs e)
        {
           // Get the driver attributes (Vendor ID, Product ID, Version Number)
            if (!joystick.vJoyEnabled())
            {
                lbl.Text += "\n"+"vJoy driver not enabled: Failed Getting vJoy attributes.\n";
                return;
            }
            else
                lbl.Text +="\n"+"Vendor: "+joystick.GetvJoyManufacturerString();
                lbl.Text +="\n"+"Product :"+joystick.GetvJoyProductString();
                lbl.Text +="\n"+"Version Number:"+joystick.GetvJoySerialNumberString();

            
           
           



            // Check which axes are supported
            bool AxisX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X);
            bool AxisY = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y);
            bool AxisZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z);
            bool AxisRX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RX);
            bool AxisRZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RZ);
            // Get the number of buttons and POV Hat switchessupported by this vJoy device
            int nButtons = joystick.GetVJDButtonNumber(id);
            int ContPovNumber = joystick.GetVJDContPovNumber(id);
            int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);

           

			lbl.Text +="\nvJoy Device  capabilities:";
            lbl.Text +="\n"+"Numner of buttons\t"+ nButtons;
            lbl.Text +="\n"+"Numner of Continuous POVs\t"+ContPovNumber;
            lbl.Text +="\n"+"Numner of Descrete POVs\t"+DiscPovNumber;
            lbl.Text +="\n"+"Axis X"+ (AxisX ? "Yes" : "No");
            lbl.Text +="\n"+"Axis Y"+ (AxisX ? "Yes" : "No");
            lbl.Text +="\n"+"Axis Z"+ (AxisX ? "Yes" : "No");
            lbl.Text +="\n"+"Axis Rx"+ (AxisRX ? "Yes" : "No");
            lbl.Text +="\n"+"Axis Rz"+ (AxisRZ ? "Yes" : "No");
  
        }

      

       

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb1.Checked)
            {
                joystick.SetBtn(true, id, 1);
            }
            else
                joystick.SetBtn(false, id, 1);
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            if (cb2.Checked)
            {
                joystick.SetBtn(true, id, 2);
            }
            else
                joystick.SetBtn(false, id, 2);

        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (cb3.Checked)
            {
                joystick.SetBtn(true, id, 3);
            }
            else
                joystick.SetBtn(false, id, 3);

        }
        private void cb4_CheckedChanged(object sender, EventArgs e)
        {
            if (cb4.Checked)
            {
                joystick.SetBtn(true, id, 4);
            }
            else
                joystick.SetBtn(false, id, 4);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tb1_TextChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(tb1.Text);

            if (val < 32760 ||joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X))
            {
                joystick.SetAxis(val, id, HID_USAGES.HID_USAGE_X);
            }
        }

        private void tb2_TextChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(tb2.Text);

            if (val < 32760 || joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y))
            {
                joystick.SetAxis(val, id, HID_USAGES.HID_USAGE_Y);
            }
        }

        private void tb3_TextChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(tb3.Text);

            if (val < 32760 || joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z))
            {
                joystick.SetAxis(val, id, HID_USAGES.HID_USAGE_Z);
            }
        }

        private void tb4_TextChanged(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(tb4.Text);

            if (val < 32760 || joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RZ))
            {
                joystick.SetAxis(val, id, HID_USAGES.HID_USAGE_RZ);
            }
        }

       
    }
}
