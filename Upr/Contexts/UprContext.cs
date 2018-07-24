using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Upr.Entities;

namespace Upr.Contexts
{
    public class UprContext : DbContext
    {
        public UprContext(DbContextOptions<UprContext> options) : base(options)
        {
            
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ModelManufacturerPair> ModelManufacturerPairs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public IQueryable<Transaction> GetTransactions()
        {
            return Transactions.FromSql("SELECT " +
                "NEWID() AS Id, " +
                "U._Fld7875 AS AgreementId, " +
                "IIF(T._Fld15653 > CONVERT(DATETIME2, '4001-01-01'), DATEADD(YEAR, -2000, T._Fld15653), NULL) as Date, " +
                "T._Fld13965 AS Sum, " +
                "T._Fld15657 AS Currency " +
                "FROM homnet_2.dbo._InfoRg14815(NOLOCK) AS H " +
                "JOIN upr_ulf_finance.dbo._InfoRg7874(NOLOCK) AS U " +
                "ON(CONVERT(INT, U._Fld7893) = CONVERT(INT, H._Fld14836) AND CONVERT(INT, U._Fld7893) > 0) OR LTRIM(RTRIM(H._Fld14820)) = LTRIM(RTRIM(U._Fld7877)) " +
                "JOIN homnet_stas.dbo._AccumRg13959(NOLOCK) AS T ON T._Fld15654 = H._Fld14841");
        }
    }
}
