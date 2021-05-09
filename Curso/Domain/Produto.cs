using Curso.Ef.Core.ValueObjects;
using System.Collections.Generic;

namespace Curso.Ef.Core.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public bool Ativo { get; set; }
        public StatusPedido Status { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Items { get; set; }
    }
}