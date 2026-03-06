using Microsoft.EntityFrameworkCore;

namespace VoucherApp;

public class AppDbContext : DbContext
{
    public DbSet<Voucher> Vouchers { get; set; }

    public DbSet<VoucherUsage> UsedVouchers { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}