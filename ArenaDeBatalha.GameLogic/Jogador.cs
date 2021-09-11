using System.Drawing;

namespace ArenaDeBatalha.GameLogic
{
    public class Jogador : ObjetoBase
    {
        public Jogador(Size limites, Graphics telaDePintura) : base(limites, telaDePintura)
        {
            this.Velocidade = 10;
            this.Som = Media.explosion_long;
            this.Esquerda = (this.Limites.Width / 2) - (this.Largura / 2);
            this.Topo = this.Limites.Height - this.Altura;
        }

        public override Bitmap ObterImagem()
        {
            return Media.nave;
        }

        public override void AtualizarObjeto()
        {
            base.AtualizarObjeto();
        }

        public override void MoverParaCima()
        {
            if(this.Topo > 0)
            {
                this.Topo -= this.Velocidade;
            }
        }

        public override void MoverParaBaixo()
        {
            if(this.Topo < (this.Limites.Height - this.Altura))
            {
                this.Topo += this.Velocidade;
            }
        }
    }
}
