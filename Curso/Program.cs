using Curso.Ef.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso.Ef.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //Execultando Migrate em dev
            using var db = new ApplicationContext();
            db.Database.Migrate();
            
        }
    }
}
