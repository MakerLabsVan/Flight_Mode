using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using imcpkg;

namespace Example
{
	public partial class Flight : Form
	{
		int[] m_encp = new int[Global.MAX_NAXIS];
		int flight_level = 95000;
		int max_height = 180000;
		int min_height = 10000;
		double acc = 10;
		double svel = 10;
		double tvel = 40;
		Dictionary<string, bool> keysPressed = new Dictionary<string, bool>();
		public Flight()
		{
			InitializeComponent();
			Flightalert.Text += "===========================" + Environment.NewLine;
			Flightalert.Text += "Welcome to the Flight Mode!" + Environment.NewLine;
			Flightalert.Text += "Press Z to raise the platform" + Environment.NewLine;
			Flightalert.Text += "Press X to lower the platform" + Environment.NewLine;
			Flightalert.Text += "Press W to push down" + Environment.NewLine;
			Flightalert.Text += "Press S to pull up" + Environment.NewLine;
			Flightalert.Text += "Press A to turn left" + Environment.NewLine;
			Flightalert.Text += "Press D to turn right" + Environment.NewLine;
			Flightalert.Text += "You can also use mouse to control" + Environment.NewLine + Environment.NewLine;
			Flightalert.Text += "If the control is reversed" + Environment.NewLine;
			Flightalert.Text += "Please turn off both two computers" + Environment.NewLine;
			Flightalert.Text += "Then push the stop button" + Environment.NewLine;
			Flightalert.Text += "Wait for at least 10s" + Environment.NewLine;
			Flightalert.Text += "Then do the start steps again" + Environment.NewLine + Environment.NewLine;
			Flightalert.Text += "Hope you enjoy the flight!" + Environment.NewLine;
			Flightalert.Text += "===========================" + Environment.NewLine;
			keysPressed.Add("w", false);
			keysPressed.Add("s", false);
			keysPressed.Add("a", false);
			keysPressed.Add("d", false);
			keysPressed.Add("q", false);
			keysPressed.Add("e", false);
			keysPressed.Add("z", false);
			keysPressed.Add("x", false);


		}

		private bool In_Bounds(int pos)
		{
			return pos > min_height && pos < max_height;
		}

		private void Stop_All_Motors()
		{
			IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 0);
			IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 1);
			IMC_Pkg.PKG_IMC_P2Pstop(Global.g_handle, 2);
		}

		private void Set_All_Acceleration(double acc)
		{
			IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 0);
			IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 1);
			IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 2);
		}

		private void Move_Motor(int final_pos, double start_velocity, double target_velocity, int motor)
		{
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, motor);
		}

		private void Move_All_Motors(int final_pos, double start_velocity, double target_velocity)
		{
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 0);
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 1);
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, final_pos, start_velocity, target_velocity, 0, 2);
		}

		private void Stop_Motor(int motor)
		{
			IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, 0, 0, motor);
		}

		private void Move_To_Flight_Level(double start_velocity, double target_velocity)
		{
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 0);
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 1);
			IMC_Pkg.PKG_IMC_MoveAbs(Global.g_handle, flight_level, start_velocity, target_velocity, 0, 2);
		}

		private void Flight_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X) || (e.KeyCode == Keys.Q) || (e.KeyCode == Keys.E))
			{
				switch (e.KeyCode)
				{
					case Keys.W:
						keysPressed["w"] = true;
						break;
					case Keys.S:
						keysPressed["s"] = true;
						break;
					case Keys.A:
						keysPressed["a"] = true;
						break;
					case Keys.D:
						keysPressed["d"] = true;
						break;
					case Keys.Z:
						keysPressed["z"] = true;
						break;
					case Keys.X:
						keysPressed["x"] = true;
						break;
					case Keys.Q:
						keysPressed["q"] = true;
						break;
					case Keys.E:
						keysPressed["e"] = true;
						break;

				}
			}
		}

		private void Flight_KeyUp(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.W) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.S) || (e.KeyCode == Keys.D) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.X) || (e.KeyCode == Keys.Q) || (e.KeyCode == Keys.E))
			{
				switch (e.KeyCode)
				{
					case Keys.W:
						keysPressed["w"] = false;
						break;
					case Keys.S:
						keysPressed["s"] = false;
						break;
					case Keys.A:
						keysPressed["a"] = false;
						break;
					case Keys.D:
						keysPressed["d"] = false;
						break;
					case Keys.Z:
						keysPressed["z"] = false;
						break;
					case Keys.X:
						keysPressed["x"] = false;
						break;
					case Keys.Q:
						keysPressed["q"] = false;
						break;
					case Keys.E:
						keysPressed["e"] = false;
						break;
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			IMC_Pkg.PKG_IMC_GetEncp(Global.g_handle, m_encp, Global.g_naxis);
			Set_All_Acceleration(acc);
			for (int i = 0; i < 3; i++)
			{
				if (m_encp[i] >= max_height)
				{
					Stop_Motor(i);
					Flightalert.Text += "Axis Max Limit Triggered!" + Environment.NewLine;
				}
				if (m_encp[i] <= min_height)
				{
					Stop_Motor(i);
					Flightalert.Text += "Axis Min Limit Triggered!" + Environment.NewLine;
				}

			}
			//W only
			if (keysPressed["w"] && !keysPressed["a"] && !keysPressed["d"] && !keysPressed["s"])
			{
				Move_Motor(min_height, svel, tvel, 0);
				Move_Motor(max_height, svel, tvel, 1);
				Move_Motor(max_height, svel, tvel, 2);
			}
			//S only
			else if (!keysPressed["w"] && !keysPressed["a"] && !keysPressed["d"] && keysPressed["s"])
			{
				Move_Motor(max_height, svel, tvel, 0);
				Move_Motor(min_height, svel, tvel, 1);
				Move_Motor(min_height, svel, tvel, 2);
			}
			//A only
			else if (!keysPressed["w"] && keysPressed["a"] && !keysPressed["d"] && !keysPressed["s"])
			{
				Move_Motor(flight_level, svel, tvel, 0);
				Move_Motor(min_height, svel, tvel, 1);
				Move_Motor(max_height, svel, tvel, 2);
			}
			//D only
			else if (!keysPressed["w"] && !keysPressed["a"] && keysPressed["d"] && !keysPressed["s"])
			{
				Move_Motor(flight_level, svel, tvel, 0);
				Move_Motor(max_height, svel, tvel, 1);
				Move_Motor(min_height, svel, tvel, 2);
			}

			//Z only
			else if (keysPressed["z"] && !keysPressed["x"])
			{
				Move_All_Motors(max_height, svel, tvel);
			}
			//X only
			else if (keysPressed["x"] && !keysPressed["z"])
			{
				Move_All_Motors(min_height, svel, tvel);
			}
			//W & A
			else if (keysPressed["w"] && keysPressed["a"] && !keysPressed["d"] && !keysPressed["s"])
			{
				Move_Motor(min_height, svel, tvel, 0);
				Move_Motor(min_height, svel, tvel, 1);
				Move_Motor(max_height, svel, tvel, 2);
			}
			//W & D
			else if (keysPressed["w"] && !keysPressed["a"] && keysPressed["d"] && !keysPressed["s"])
			{
				Move_Motor(min_height, svel, tvel, 0);
				Move_Motor(max_height, svel, tvel, 1);
				Move_Motor(min_height, svel, tvel, 2);
			}
			//S & A
			else if (!keysPressed["w"] && keysPressed["a"] && !keysPressed["d"] && keysPressed["s"])
			{
				Move_Motor(max_height, svel, tvel, 0);
				Move_Motor(min_height, svel, tvel, 1);
				Move_Motor(max_height, svel, tvel, 2);
			}
			//S & D
			else if (!keysPressed["w"] && !keysPressed["a"] && keysPressed["d"] && keysPressed["s"])
			{
				Move_Motor(max_height, svel, tvel, 0);
				Move_Motor(max_height, svel, tvel, 1);
				Move_Motor(min_height, svel, tvel, 2);
			}
			//No keys
			else
			{
				Move_To_Flight_Level(svel, tvel);
			}

			//Q
			if (keysPressed["q"] && !keysPressed["e"])
			{
				IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
				IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, -svel * 0.6, -tvel * 0.6, 3); //Reduce rotation speed
			}
			//E
			else if (keysPressed["e"] && !keysPressed["q"])
			{
				IMC_Pkg.PKG_IMC_SetAccel(Global.g_handle, acc, acc, 3);
				IMC_Pkg.PKG_IMC_MoveVel(Global.g_handle, svel * 0.6, tvel * 0.6, 3);
			}
			else
			{
				Stop_Motor(3);
			}
		}

		//Acceleration input
		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode == Keys.Back)) || (e.KeyCode == Keys.Decimal))
			{
				label1.Focus();
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			string tString = textBox1.Text;
			tString = tString.Trim();
			try
			{
				acc = Convert.ToDouble(tString);
				Console.WriteLine("Converted '{0}' to {1}.", tString, acc);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to convert '{0}' to a Double.", tString);
				acc = 10;
			}
			catch (OverflowException)
			{
				Console.WriteLine("'{0}' is outside the range of a Double.", tString);
				acc = 10;
			}
			//If it get's here it's a valid number
		}

		//Starting velocity input
		private void textBox2_KeyDown(object sender, KeyEventArgs e)
		{
			if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Decimal)))
			{
				label2.Focus();
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			string tString = textBox2.Text;
			tString = tString.Trim();
			try
			{
				svel = Convert.ToDouble(tString);
				Console.WriteLine("Converted '{0}' to {1}.", tString, svel);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to convert '{0}' to a Double.", tString);
				svel = 10;
			}
			catch (OverflowException)
			{
				Console.WriteLine("'{0}' is outside the range of a Double.", tString);
				svel = 10;
			}
			//If it get's here it's a valid number
		}

		//Target velocity input
		private void textBox3_KeyDown(object sender, KeyEventArgs e)
		{
			if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Decimal)))
			{
				label4.Focus();
			}
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
			string tString = textBox3.Text;
			tString = tString.Trim();
			try
			{
				tvel = Convert.ToDouble(tString);
				if (tvel > 100)
				{
					tvel = 100;
					textBox3.Text = "100";
				}
				Console.WriteLine("Converted '{0}' to {1}.", tString, tvel);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to convert '{0}' to a Double.", tString);
				tvel = 40;
			}
			catch (OverflowException)
			{
				Console.WriteLine("'{0}' is outside the range of a Double.", tString);
				tvel = 40;
			}
			//If it get's here it's a valid number
		}

		//Mouse button controls
		private void button1_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["w"] = true;
		}

		private void button2_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["s"] = true;
		}

		private void button3_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["a"] = true;
		}

		private void button4_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["d"] = true;
		}

		private void button5_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["z"] = true;
		}

		private void button6_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["x"] = true;
		}

		private void button7_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["q"] = true;
		}

		private void button8_MouseDown(object sender, MouseEventArgs e)
		{
			keysPressed["e"] = true;
		}

		private void button1_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["w"] = false;
		}

		private void button2_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["s"] = false;
		}

		private void button3_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["a"] = false;
		}

		private void button4_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["d"] = false;
		}

		private void button5_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["z"] = false;
		}

		private void button6_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["x"] = false;
		}

		private void button7_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["q"] = false;
		}

		private void button8_MouseUp(object sender, MouseEventArgs e)
		{
			keysPressed["e"] = false;
		}
	}
}

