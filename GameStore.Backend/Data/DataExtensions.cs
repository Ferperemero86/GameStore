using Microsoft.EntityFrameworkCore;

namespace GameStore.Backend.Data;

public static class DataExtenseions
{
  public static void MigrateDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext  = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

    dbContext.Database.Migrate();
  }
}
