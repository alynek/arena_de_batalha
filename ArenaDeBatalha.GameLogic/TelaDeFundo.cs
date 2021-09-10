using System.Drawing;

namespace ArenaDeBatalha.GameLogic
{
    public class TelaDeFundo : ObjetoBase
    {
        public TelaDeFundo(Size limites, Graphics tela) : base(limites, tela)
        {
            Esquerda= 0;
            Topo = 0;
            Velocidade = 0;
        }

        public override Bitmap ObterImagem()
        {
            return Media.cosmos;
        }
    }
}
