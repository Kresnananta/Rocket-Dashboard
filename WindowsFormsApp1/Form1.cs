using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string serialDataIn;
        sbyte indexOfA, indexOfB, indexOfC, indexOfD, indexOfE, indexOfF, indexOfG, indexOfH;
        string datasensor1, datasensor2, datasensor3, datasensor4, datasensor5, datasensor6, datasensor7, datasensor8;
        int timeCs, timeSec, timeMin;
        bool isActive;


        public Form1()
        {
            InitializeComponent();
        }

        private void EndResponsive()
        {
            //if(this.Width < 450)
            //{
            //    tableLayoutPanel2.ColumnStyles[1].Width = 350;
            //}
            //else if (this.Width < 1023){
            //    tableLayoutPanel2.ColumnStyles[1].Width = tableLayoutPanel2.Width - (chart1.Width + chart1.Margin.Right);
            //}
            //else
            //{
            //    tableLayoutPanel2.ColumnStyles[1].Width = chart1.Width;// - (chart1.Width + chart2.Width + chart1.Margin.Right + chart2.Margin.Right);
            //}

            //if (this.Height < 755)
            //{
            //    panel6.Height = 290;
            //}
            //else
            //{
            //    panel6.Height = panel5.Height;
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToPort.Enabled = true;
            closePort.Enabled = false;

            resetTime();
            isActive = false;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            EndResponsive();
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState== FormWindowState.Maximized) // not real time responsive but BEST PERFORMANCE mwehehe :)
            {
                EndResponsive() ;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
            //int temperature = Convert.ToInt32(datasensor5);
            //if (temperature <= 20)
            //{
            //    panel3.BackColor = Color.Blue;
            //}
            //else if (temperature > 20 && temperature < 45)
            //{
            //    panel3.BackColor = Color.Green;
            //}
            //else if (temperature >= 45 && temperature < 70)
            //{
            //    panel3.BackColor = Color.Yellow;
            //} else
            //{
            //    panel3.BackColor = Color.Red;
            //}
            


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ConnectToPort_Click(object sender, EventArgs e)
        {
            isActive= true;

            try
            {
                serialPort1.PortName = avaiablePortsBox.Text;
                serialPort1.BaudRate = Convert.ToInt32(baudRateBox.Text);
                serialPort1.Open();

                ConnectToPort.Enabled = false;
                closePort.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...!");
            }
        }

        private void avaiablePortsBox_DropDown(object sender, EventArgs e)
        {
            String[] portlists = SerialPort.GetPortNames();
            avaiablePortsBox.Items.Clear();
            avaiablePortsBox.Items.AddRange(portlists);
        }

        private void closePort_Click(object sender, EventArgs e)
        {
            isActive= false;

            try
            {
                serialPort1.Close();

                ConnectToPort.Enabled = true;
                closePort.Enabled = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }

            resetTime();
        }

        private void resetTime()
        {
            timeCs = 0;
            timeSec = 0;
            timeMin = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception error )
            {
                MessageBox.Show(error.Message, "Error");
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialDataIn = serialPort1.ReadLine();
            this.BeginInvoke(new EventHandler(ProcessData));
        }

        private void ProcessData(object sender, EventArgs e)
        {
            try
            {
                indexOfA = Convert.ToSByte(serialDataIn.IndexOf("A"));
                indexOfB = Convert.ToSByte(serialDataIn.IndexOf("B"));
                indexOfC = Convert.ToSByte(serialDataIn.IndexOf("C"));
                indexOfD = Convert.ToSByte(serialDataIn.IndexOf("D"));
                indexOfE = Convert.ToSByte(serialDataIn.IndexOf("E"));
                indexOfF = Convert.ToSByte(serialDataIn.IndexOf("F"));
                indexOfG = Convert.ToSByte(serialDataIn.IndexOf("G"));
                indexOfH = Convert.ToSByte(serialDataIn.IndexOf("H"));

                // prosesing ngilangin A,B,C,...
                datasensor1 = serialDataIn.Substring(0, indexOfA); //roll
                datasensor2 = serialDataIn.Substring(indexOfA + 1, (indexOfB - indexOfA) - 1); //pitch
                datasensor3 = serialDataIn.Substring(indexOfB + 1, (indexOfC - indexOfB) - 1); //yaw
                datasensor4 = serialDataIn.Substring(indexOfC + 1, (indexOfD - indexOfC) - 1); //height
                datasensor5 = serialDataIn.Substring(indexOfD + 1, (indexOfE - indexOfD) - 1); //temperature
                datasensor6 = serialDataIn.Substring(indexOfE + 1, (indexOfF - indexOfE) - 1); //servo_roll
                datasensor7 = serialDataIn.Substring(indexOfF + 1, (indexOfG - indexOfF) - 1); //servo_pitch
                datasensor8 = serialDataIn.Substring(indexOfG + 1, (indexOfH - indexOfG) - 1); //parachute_Open & Close

                // masukin ke data komponen
                rollBox.Text = datasensor1;
                pitchBox.Text = datasensor2;
                yawBox.Text = datasensor3;
                label5.Text = datasensor4;
                label4.Text = datasensor5;
                rollServoBox.Text = datasensor6;
                pitchServoBox.Text = datasensor7;


                chart1.Series[0].Points.AddY(datasensor6);
                chart1.Series[1].Points.AddY(datasensor7);
                chart3.Series[0].Points.AddY(datasensor4);

                //Parachute
                int chute = Convert.ToInt32(datasensor8);
                if (chute == 0)
                {
                    parachute_statusBox.Text = String.Format("Close");
                }
                else
                {
                    parachute_statusBox.Text = String.Format("Open");
                }


                // Ganti warna di temperature
                int temperature = Convert.ToInt32(datasensor5);
                if (temperature <= 30)
                {
                    panel3.BackColor = Color.Green;
                }
                else if (temperature > 30 && temperature <= 40 )
                {
                    panel3.BackColor = Color.Yellow;
                }
                else
                {
                    panel3.BackColor = Color.Red;
                }
                //High Low Temp
                if (temperature <= 30)
                {
                    label6.Text = String.Format("LOW");
                }
                else if (temperature > 30 && temperature <= 40)
                {
                    label6.Text = String.Format("NORMAL");
                }
                else
                {
                    label6.Text = String.Format("HIGH");
                }

                // Ganti warna di height
                int height = Convert.ToInt32(datasensor4);
                if (height <= 20)
                {
                    panel4.BackColor = Color.LightBlue;
                }
                else if (height > 20 && height < 50)
                {
                    panel4.BackColor = Color.DeepSkyBlue;
                }
                else if (height > 51 && height < 100)
                {
                    panel4.BackColor = Color.DodgerBlue;
                }
                else if (height >= 101 && height < 300)
                {
                    panel4.BackColor = Color.RoyalBlue;
                }
                else
                {
                    panel4.BackColor = Color.Teal;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                timeCs++;

                if (timeCs >= 100)
                {
                    timeSec++;
                    timeCs = 0;

                    if (timeSec >= 60)
                    {
                        timeMin++;
                        timeSec = 0;
                    }
                }
            }
            drawTime();
        }

        private void drawTime()
        {
            lblCs.Text = String.Format("{0:00}", timeCs);
            lblSec.Text = String.Format("{0:00}", timeSec);
            lblMin.Text = String.Format("{0:00}", timeMin);

        }
    }
}
/* Bang Udah Bang */