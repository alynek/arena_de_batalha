using System.Drawing;

namespace ArenaDeBatalha.GameLogic
{
    public class Tiro : ObjetoBase
    {
        public Tiro(Size limites, Graphics telaDePintura, Point posicao):base(limites, telaDePintura)
        {
            this.Velocidade = 20;
            this.Som = Media.Missile;
            this.Esquerda = posicao.X;
            this.Topo = posicao.Y;
            this.TocarSom();
        }

        public override Bitmap ObterImagem()
        {
            return Media.laser;
        }

        public override void AtualizarObjeto()
        {
            this.MoverParaCima();
            base.AtualizarObjeto();
        }
    }
}
