using System.Drawing;

namespace ArenaDeBatalha.GameLogic
{
    public class FimDeJogo : ObjetoBase
    {
        public FimDeJogo(Size limites, Graphics telaDePintura) : base(limites, telaDePintura)
        {
            this.Esquerda = (this.Limites.Width / 2) - (this.Largura / 2);
            this.Topo = (this.Limites.Width / 2) - (this.Largura / 2);
            this.Velocidade = 0;
        }

        public override Bitmap ObterImagem()
        {
            return Media.gameOver;
        }
    }
}
