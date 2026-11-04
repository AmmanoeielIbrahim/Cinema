using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;

namespace Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }

        public DbSet<Payment> Payments;
        public DbSet<Ticket> Tickets;
        public DbSet<BookingSnack> BookingSnacks;
        public DbSet<SnacksItem> SnacksItems;
        public DbSet<Chair> Chairs;
    }
}
