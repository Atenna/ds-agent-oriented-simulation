using System.Drawing;
using System.Windows.Forms;

namespace ds_agent_oriented_simulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.pictureBox10, "Volume: 10, Speed: 60");
            toolTip1.SetToolTip(this.pictureBox9, "Volume: 20, Speed: 50");
            toolTip1.SetToolTip(this.pictureBox8, "Volume: 25, Speed: 45");
            toolTip1.SetToolTip(this.pictureBox6, "Volume: 5, Speed: 70");
            toolTip1.SetToolTip(this.pictureBox7, "Volume: 40, Speed: 30");
        }

        private void label5_MouseHover(object sender, System.EventArgs e)
        {
            label5.BackColor = Color.LightGray;
            ;
        }

        private void label5_MouseLeave(object sender, System.EventArgs e)
        {
            label5.BackColor = Color.White;
        }

        private void label6_MouseHover(object sender, System.EventArgs e)
        {
            label6.BackColor = Color.LightGray;
        }

        private void label6_MouseLeave(object sender, System.EventArgs e)
        {
            label6.BackColor = Color.White;
        }

        private void label4_MouseHover(object sender, System.EventArgs e)
        {
            label4.BackColor = Color.LightGray;
        }

        private void label4_MouseLeave(object sender, System.EventArgs e)
        {
            label4.BackColor = Color.White;
        }

        private void label3_MouseHover(object sender, System.EventArgs e)
        {
            label3.BackColor = Color.LightGray;
        }

        private void label3_MouseLeave(object sender, System.EventArgs e)
        {
            label3.BackColor = Color.White;
        }

        private void label6_Click(object sender, System.EventArgs e)
        {
            label2.Text = "1/2";
            checkBox6.Checked = true;
        }

        private void label5_Click(object sender, System.EventArgs e)
        {
            label2.Text = "0/2";
            checkBox6.Checked = false;
        }
    }
}
