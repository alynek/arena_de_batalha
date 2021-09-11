using ArenaDeBatalha.GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ArenaDeBatalha.GUI
{
    public partial class FormPrincipal : Form
    {
        DispatcherTimer tempoDoJogo { get; set; }
        Bitmap telaBuffer { get; set; }
        Graphics telaDePintura { get; set; }
        TelaDeFundo telaDeFundo { get; set; }
        List<ObjetoBase> objetosBase { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();

            this.ClientSize = Media.cosmos.Size;
            this.telaBuffer = new Bitmap(Media.cosmos.Width, Media.cosmos.Height);
            this.telaDePintura = Graphics.FromImage(this.telaBuffer);
            this.objetosBase = new List<ObjetoBase>();
            this.telaDeFundo = new TelaDeFundo(this.telaBuffer.Size, this.telaDePintura);
            
            this.tempoDoJogo = new DispatcherTimer(DispatcherPriority.Render);
            this.tempoDoJogo.Interval = TimeSpan.FromMilliseconds(16.66666);
            this.tempoDoJogo.Tick += LoopDoJogo;

            this.objetosBase.Add(telaDeFundo);

            IniciarJogo();
        }

        public void IniciarJogo()
        {
            this.tempoDoJogo.Start();
        }

        public void LoopDoJogo(object sender, EventArgs e)
        {
            foreach(ObjetoBase objeto in this.objetosBase)
            {
                objeto.AtualizarObjeto();
                this.Invalidate();
            }
        }
        private void FormPrincipal_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(this.telaBuffer, 0, 0);
        }
    }
}
