using ArenaDeBatalha.GameLogic;
using System.Drawing;
using System.Windows.Forms;

namespace ArenaDeBatalha.GUI
{
    public partial class FormPrincipal : Form
    {

        Bitmap telaBuffer { get; set; }
        Graphics telaDePintura { get; set; }
        TelaDeFundo telaDeFundo { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();
            this.telaBuffer = new Bitmap(Media.cosmos.Width, Media.cosmos.Height);
            this.telaDePintura = Graphics.FromImage(this.telaBuffer);
            this.telaDeFundo = new TelaDeFundo(this.telaBuffer.Size, this.telaDePintura);
        }
    }
}
