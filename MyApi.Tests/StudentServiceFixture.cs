using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SQLitePCL;


public class StudentServiceFixture
{
    public AppDbContext CreateContext()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        // var options = new DbContextOptionsBuilder<AppDbContext>()
        //     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //     .Options;
        connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}