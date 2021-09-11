using System.Drawing;

namespace ArenaDeBatalha.GameLogic
{
    public class Inimigo : ObjetoBase
    {
        public Inimigo(Size limites, Graphics telaDePintura, Point posicao) : base(limites, telaDePintura)
        {
            this.Esquerda = posicao.X;
            this.Topo = posicao.Y;
            this.Velocidade = 5;
            this.Som = Media.exploshion_short;
        }

        public override Bitmap ObterImagem()
        {
            return Media.inimigo;
        }

        public override void AtualizarObjeto()
        {
            this.MoverParaBaixo();
            base.AtualizarObjeto();
        }
    }
}
