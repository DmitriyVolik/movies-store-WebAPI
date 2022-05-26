using DAL.DB;
using Microsoft.EntityFrameworkCore;

namespace Tests.RepositoriesTests.Helpers;

public static class FakeDb
{
    public static DbContextOptions<Context> GetFakeDbOptions(string name)
    {
        return new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(name)
            .Options;
    }
}