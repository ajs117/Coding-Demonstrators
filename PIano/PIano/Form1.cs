using System;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace PIano
{
    public partial class Form1 : Form
    {
        private BackgroundWorker _worker = null;

        public char[] Pi { get; set; }

        public Form1()
        {
            InitializeComponent();

            var piString = @"3.14159265358979323846264338327950288419716939937510
                        58209749445923078164062862089986280348253421170679
                        82148086513282306647093844609550582231725359408128
                        48111745028410270193852110555964462294895493038196
                        44288109756659334461284756482337867831652712019091
                        45648566923460348610454326648213393607260249141273
                        72458700660631558817488152092096282925409171536436
                        78925903600113305305488204665213841469519415116094
                        33057270365759591953092186117381932611793105118548
                        07446237996274956735188575272489122793818301194912
                        98336733624406566430860213949463952247371907021798
                        60943702770539217176293176752384674818467669405132
                        00056812714526356082778577134275778960917363717872
                        14684409012249534301465495853710507922796892589235
                        42019956112129021960864034418159813629774771309960
                        51870721134999999837297804995105973173281609631859
                        50244594553469083026425223082533446850352619311881
                        71010003137838752886587533208381420617177669147303
                        59825349042875546873115956286388235378759375195778
                        18577805321712268066130019278766111959092164201989";

            piString.Remove('.');

            Pi = piString.ToCharArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;

            _worker.DoWork += new DoWorkEventHandler((state, args) =>
            {
                int i = 0;

                do
                {
                    if (_worker.CancellationPending)
                    {
                        break;
                    }

                    switch (Pi[i])
                    {
                        case '1':
                            Player("Sounds/a1.wav");
                            break;

                        case '2':
                            Player("Sounds/a1s.wav");
                            break;

                        case '3':
                            Player("Sounds/b1.wav");
                            break;

                        case '4':
                            Player("Sounds/c1.wav");
                            break;

                        case '5':
                            Player("Sounds/c1s.wav");
                            break;

                        case '6':
                            Player("Sounds/c2.wav");
                            break;

                        case '7':
                            Player("Sounds/d1.wav");
                            break;

                        case '8':
                            Player("Sounds/e1.wav");
                            break;

                        case '9':
                            Player("Sounds/f1.wav");
                            break;

                        case '0':
                            Player("Sounds/g1.wav");
                            break;
                        default:
                            break;
                    }

                    i++;

                } while (true);
            });

            if (button1.Text == "Start")
            {
                _worker.RunWorkerAsync();
                button1.Text = "Stop";
            }
            else
            {
                _worker.CancelAsync();
                button1.Text = "Start";
            }

        }

        public void Player(string location)
        {
            var player = new SoundPlayer(location);

            player.PlaySync();
        }
    }
}