using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;

namespace Monstruletul_mancacios
{
    public partial class Form1 : Form
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion

        #region .. code for Flickering ..

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion
        SoundPlayer sunet = new SoundPlayer(Properties.Resources.ra);

        public Form1()
        {
            InitializeComponent();
            background.Controls.Add(player);
            player.BackColor = Color.Transparent;
            background.Controls.Add(candy);
            candy.BackColor = Color.Transparent;
            background.Controls.Add(candy1);
            candy1.BackColor = Color.Transparent;


        }
        int puncte = 0, vieti = 3,viteza=5;
        bool stop = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //miscare bomboana
            foreach (Control x in background.Controls)
            {
                if ((string)x.Tag == "candy")
                {
                    if (x.Top <= background.Height)
                    {
                        x.Top = x.Top + viteza;
                    }
                    else
                    {
                        Random nr = new Random();
                        int ya = nr.Next(80, 120);//coordonata pe verticala
                        int xa = nr.Next(425);//coordonata pe orizontala a bomboanei
                        x.Top = -ya;
                        x.Left = xa;
                    }
                }
                
            }
            lbl_vieti.Text = Convert.ToString(vieti);
            foreach (Control x in background.Controls)
            {

                if ((string)x.Tag == "candy")
                {
                    if (wood.Bounds.IntersectsWith(x.Bounds))
                    {
                        lbl_vieti.Text = Convert.ToString(vieti);
                        vieti--;
                        Random nr = new Random();
                        int ya = nr.Next(80, 120);//coordonata pe verticala
                        int xa = nr.Next(425);//coordonata pe orizontala a bomboanei
                        x.Top = -ya;
                        x.Left = xa;
                    }
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        puncte++;
                        lbl_puncte.Text = Convert.ToString(puncte);
                        if (puncte % 3 == 0)
                            viteza += 3;
                        sunet.Play();

                        Random nr = new Random();
                        int ya = nr.Next(80, 120);//coordonata pe verticala
                        int xa = nr.Next(425);//coordonata pe orizontala a bomboanei
                        x.Top = -ya;
                        x.Left = xa;
                    }
                }
                if (vieti == 0)
                {
                    
                    stop = false;
                    timer1.Enabled = false;
                    this.Hide();
                    Final f = new Final();
                    
                    Program.scor = puncte;
                    if (Program.scor > Program.max_scor)
                        Program.max_scor = Program.scor;
                    f.scor =Program.scor;
                    f.label1.Text = Convert.ToString(f.scor);
                    f.record.Text = Convert.ToString(Program.max_scor);
                    f.ShowDialog();
                }
            }


        }

        private void background_MouseMove(object sender, MouseEventArgs e)
        {
            if (stop == true)
            {
                Point position = e.Location;
                double pX = position.X;
                player.Left = (int)pX - (player.Width / 2);
            }
        }
    }
}
