using System.Drawing;
using System.IO;
using System.Media;

namespace ArenaDeBatalha.GameLogic
{
    public abstract class ObjetoBase
    {
        public bool Ativo { get; set; }
        public int Velocidade { get; set; }
        public int Esquerda { get; set; }
        public int Topo { get; set; }
        public int Largura { get { return this.Imagem.Width; } }
        public int Altura { get { return this.Imagem.Height; } }
        public Bitmap Imagem { get; set; }
        public Size Limites { get; set; }
        public Rectangle Retangulo {get; set;}
        public Stream Som { get; set; }
        public Graphics Tela { get; set; }
        private SoundPlayer _somDoJogador { get; set; }

        public ObjetoBase(Size limites, Graphics tela)
        {
            Limites = limites;
            Tela = tela;
            Ativo = true;
            _somDoJogador = new SoundPlayer();
            Imagem = ObterImagem();
            Retangulo = new Rectangle(Esquerda, Topo, Largura, Altura);
        }

        public abstract Bitmap ObterImagem();

        public virtual void AtualizarObjeto()
        {
            Retangulo = new Rectangle(Esquerda, Topo, Largura, Altura);
            Tela.DrawImage(Imagem, Retangulo);
        }

        public virtual void MoverParaEsquerda()
        {
            if(Esquerda > 0)
            {
                Esquerda -= Velocidade;
            }
        }

        public virtual void MoverParaDireita()
        {
            if(Esquerda < (Limites.Width - Largura))
            {
                Esquerda += Velocidade;
            }
        }

        public virtual void MoverParaBaixo()
        {
            Topo += Velocidade;
        }

        public virtual void MoverParaCima()
        {
            Topo -= Velocidade;
        }

        public bool EstaForaDaTela()
        {
            return (Topo > (Limites.Height + Altura))
                || (Topo < -Altura)
                || (Esquerda > (Limites.Width + Largura))
                || (Esquerda < -Largura);
        }

        public bool EstaColidindoComOutroObjeto(ObjetoBase objetoBase)
        {
            if (Retangulo.IntersectsWith(objetoBase.Retangulo))
            {
                TocarSom();
                return true;
            }

            return false;
        }

        public void TocarSom()
        {
            _somDoJogador.Stream = Som;
            _somDoJogador.Play();
        }

        public void DestruirObjeto()
        {
            Ativo = false;
        }
    }
}
