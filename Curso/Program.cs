using Curso.Ef.Core.Data;
using Curso.Ef.Core.Domain;
using Curso.Ef.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Ef.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //Execultando Migrate em dev
            //using var db = new ApplicationContext();
            /// db.Database.Migrate();
            //var existerMigracaoPedente = db.Database.GetPendingMigrations().Any();

            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            ColsultarPeidoCarregamentoAdiamtado();
            Console.ReadKey();
        }

        private static void ColsultarPeidoCarregamentoAdiamtado()
        {
            using var db = new ApplicationContext();
            var pedidos = db.Pedidos
                                    .Include(p=> p.Itens) //1 nivel
                                        .ThenInclude(p =>p.Produto) //2 nivel
                                    .ToList();
            Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto=0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456987",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };


            using var db = new ApplicationContext();
            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s) {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste Em Massa",
                CodigoBarras = "12345698789",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            //var cliente = new Cliente
            //{
            //    Nome = "Farley Rufino",
            //    Cep = "99999000",
            //    Cidade = "Governador Valadares",
            //    Estado = "Mg",
            //    Telefone = "991057769"

            //};

            var listaClientes = new[]
            {
                new Cliente
                {
                Nome = "Farley Rufino",
                Cep = "99999000",
                Cidade = "Governador Valadares",
                Estado = "Mg",
                Telefone = "991057769"

                },

                new Cliente
                {
                Nome = "Farley Rufino Dois",
                Cep = "99999000",
                Cidade = "Governador Valadares",
                Estado = "Mg",
                Telefone = "991057769"

                }
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto,cliente);
            db.Set<Cliente>().AddRange(listaClientes);
            //db.Clientes.AddRange(listaClientes);
            var registros = db.SaveChanges();

            Console.WriteLine($"Total de Registro(s) {registros}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();

            //var consultaporSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            //var consultaporMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();

            var consultaporMetodo = db.Clientes.OrderBy(p=> p.Id).Where(p => p.Id > 0).ToList();

            foreach (var cliente in consultaporMetodo)
            {
                Console.WriteLine($"Consultando Cliente:{cliente.Id}");
                db.Clientes.Find(cliente.Id);
            }
        }
    }
}
