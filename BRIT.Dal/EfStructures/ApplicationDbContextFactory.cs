using System;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BRIT.Dal.EfStructures
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = @"Data Source= GB0012;Initial Catalog=AZES;Integrated Security=False;Persist Security Info=True;User ID=name_of_server;Password=password_of_server; MultipleActiveResultSets= True; Trusted_Connection= False; Encrypt=True;TrustServerCertificate=True; Connect Timeout=22;";
            //var connectionString = @"host=localhost; port=5432; database=AZES; username=vadim; password=12345; sslmode=prefer; Pooling=false; Timeout=300; CommandTimeout=300;";
            //var connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\VGodzhik\\source\\repos\\Arbeitszeiterfassungssystem.Backend\\BRIT.Dal\\localAzesDb.mdf;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine);
            //optionsBuilder.UseNpgsql(connectionString);
            //optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            
            Console.WriteLine(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
            
        }
    }
}
