using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Todo.Models;

namespace Todo.Db
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoLista> todo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoLista>(entity =>
            {
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.HasIndex(e => e.email)
                      .IsUnique();
            });

            modelBuilder.Entity<TodoLista>()
                .HasOne(t => t.usuario)
                .WithMany(u => u.todoListas)
                .HasForeignKey(t => t.idUsuario);


            base.OnModelCreating(modelBuilder);
        }
    }
}