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
        DispatcherTimer intervaloDeTempoDeJogo { get; set; }
        DispatcherTimer intervaloDeCriacaoDeInimigo { get; set; }
        Bitmap telaBuffer { get; set; }
        Graphics telaDePintura { get; set; }
        TelaDeFundo telaDeFundo { get; set; }
        List<ObjetoBase> objetosBase { get; set; }
        public Random random { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();

            this.ClientSize = Media.cosmos.Size;
            this.telaBuffer = new Bitmap(Media.cosmos.Width, Media.cosmos.Height);
            this.telaDePintura = Graphics.FromImage(this.telaBuffer);
            this.objetosBase = new List<ObjetoBase>();
            this.telaDeFundo = new TelaDeFundo(this.telaBuffer.Size, this.telaDePintura);
            
            this.intervaloDeTempoDeJogo = new DispatcherTimer(DispatcherPriority.Render);
            this.intervaloDeTempoDeJogo.Interval = TimeSpan.FromMilliseconds(16.66666);
            this.intervaloDeTempoDeJogo.Tick += LoopDoJogo;
            
            this.intervaloDeCriacaoDeInimigo = new DispatcherTimer(DispatcherPriority.Render);
            this.intervaloDeCriacaoDeInimigo.Interval = TimeSpan.FromMilliseconds(1000);
            this.intervaloDeCriacaoDeInimigo.Tick += CriarInimigo;

            this.objetosBase.Add(telaDeFundo);

            this.random = new Random();

            IniciarJogo();
        }

        public void IniciarJogo()
        {
            this.intervaloDeTempoDeJogo.Start();
            this.intervaloDeCriacaoDeInimigo.Start();
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

        public void CriarInimigo(object sender, EventArgs e)
        {
            Point posicaoDoInimigo = new Point(this.random.Next(10, this.telaBuffer.Width - 74), -62);
            Inimigo inimigo = new Inimigo(this.telaBuffer.Size, this.telaDePintura, posicaoDoInimigo);
            this.objetosBase.Add(inimigo);
        }
    }
}
