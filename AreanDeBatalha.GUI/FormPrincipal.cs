using ArenaDeBatalha.GameLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace ArenaDeBatalha.GUI
{
    public partial class FormPrincipal : Form
    {
        private bool _podeAtirar;
        DispatcherTimer intervaloDeTempoDeJogo { get; set; }
        DispatcherTimer intervaloDeCriacaoDeInimigo { get; set; }
        Bitmap telaBuffer { get; set; }
        Graphics telaDePintura { get; set; }
        TelaDeFundo telaDeFundo { get; set; }
        List<ObjetoBase> objetosBase { get; set; }
        public Random random { get; set; }
        Jogador jogador { get; set; }
        FimDeJogo fimDeJogo { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();

            this.ClientSize = Media.cosmos.Size;
            this.telaBuffer = new Bitmap(Media.cosmos.Width, Media.cosmos.Height);
            this.telaDePintura = Graphics.FromImage(this.telaBuffer);
            this.objetosBase = new List<ObjetoBase>();
            this.telaDeFundo = new TelaDeFundo(this.telaBuffer.Size, this.telaDePintura);

            this.jogador = new Jogador(this.telaBuffer.Size, this.telaDePintura);
            this.fimDeJogo = new FimDeJogo(this.telaBuffer.Size, this.telaDePintura);
            
            this.intervaloDeTempoDeJogo = new DispatcherTimer(DispatcherPriority.Render);
            this.intervaloDeTempoDeJogo.Interval = TimeSpan.FromMilliseconds(16.66666);
            this.intervaloDeTempoDeJogo.Tick += LoopDoJogo;
            
            this.intervaloDeCriacaoDeInimigo = new DispatcherTimer(DispatcherPriority.Render);
            this.intervaloDeCriacaoDeInimigo.Interval = TimeSpan.FromMilliseconds(1000);
            this.intervaloDeCriacaoDeInimigo.Tick += CriarInimigo;

            this.objetosBase.Add(telaDeFundo);
            this.objetosBase.Add(jogador);

            this.random = new Random();

            IniciarJogo();
        }

        public void IniciarJogo()
        {
            this.objetosBase.Clear();
            this.objetosBase.Add(telaDeFundo);
            this.objetosBase.Add(jogador);
            this.jogador.CriarPosicaoInicial();
            this.jogador.Ativo = true;
            this.intervaloDeTempoDeJogo.Start();
            this.intervaloDeCriacaoDeInimigo.Start();
            _podeAtirar = true;
        }

        public void FimDeJogo()
        {
            this.objetosBase.Clear();
            this.intervaloDeTempoDeJogo.Stop();
            this.intervaloDeCriacaoDeInimigo.Stop();
            this.objetosBase.Add(telaDeFundo);
            this.objetosBase.Add(fimDeJogo);
            this.telaDeFundo.AtualizarObjeto();
            this.fimDeJogo.AtualizarObjeto();
            Invalidate();
        }

        public void LoopDoJogo(object sender, EventArgs e)
        {
            this.objetosBase.RemoveAll(x => !x.Ativo);

            this.ProcessarControles();

            foreach(ObjetoBase objeto in this.objetosBase)
            {
                objeto.AtualizarObjeto();

                if (objeto.EstaForaDaTela())
                {
                    objeto.DestruirObjeto();
                }

                if(objeto is Inimigo)
                {
                    if (objeto.EstaColidindoComOutroObjeto(jogador))
                    {
                        
                        jogador.TocarSom();
                        jogador.DestruirObjeto();
                        FimDeJogo();
                        return;
                    }

                    foreach(ObjetoBase tiro in this.objetosBase.Where(x => x is Tiro))
                    {
                        if (objeto.EstaColidindoComOutroObjeto(tiro))
                        {
                            objeto.DestruirObjeto();
                            tiro.DestruirObjeto();
                        }
                    }
                }
            }

            this.Invalidate();
            //Debug.WriteLine(this.objetosBase.Count); Observa a quantidade de objetos criados e verifica se estão sendo destruídos
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

        private void ProcessarControles()
        {
            if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left)) jogador.MoverParaEsquerda();
            if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right)) jogador.MoverParaDireita();
            if (Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up)) jogador.MoverParaCima();
            if (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down)) jogador.MoverParaBaixo();

            if (Keyboard.IsKeyDown(Key.Space) && _podeAtirar)
            {
                this.objetosBase.Insert(1, jogador.Atirar());
                this._podeAtirar = false;
            }
            if (Keyboard.IsKeyUp(Key.Space)) _podeAtirar = true;
        }

        private void FormPrincipal_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.R)
            {
                IniciarJogo();
            }
            
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
