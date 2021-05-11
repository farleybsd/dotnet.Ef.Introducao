using Curso.Ef.Core.Data;
using Curso.Ef.Core.Domain;
using Curso.Ef.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
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

            InserirDados();
            Console.ReadKey();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras="123456987",
                Valor=10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo=true
            };

            using var db = new ApplicationContext();
            //db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total Registro(s) {registros}");
        }
    }
}
