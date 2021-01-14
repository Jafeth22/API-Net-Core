using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_ConnectionDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_ConnectionDB.Context
{
    public class AppDbContext : DbContext
    {
        //No permitirá pasar la información del acá (AppDbContext) hacia la clase base AppDbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Debe llevar el mismo nombre para que el EntityFramework no podrá establecer conexión con la tabla
        public DbSet<Marcas> Marcas { get; set; }
    }
}
