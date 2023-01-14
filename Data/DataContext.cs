using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;

namespace PaymentApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; } = null!;
    }
}
