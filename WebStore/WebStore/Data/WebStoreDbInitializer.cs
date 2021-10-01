using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _Logger;

        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            _Logger.LogInformation("Запуск инициализвции БД");
            //var dbDeleted = await _db.Database.EnsureDeletedAsync();
            //var dbCreated = await _db.Database.EnsureCreatedAsync();

            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await _db.Database.GetAppliedMigrationsAsync();

            if (pendingMigrations.Any())
            {
                _Logger.LogInformation("Применение миграциЙ {0}", string.Join(",", pendingMigrations));
                await _db.Database.MigrateAsync();
            }

            await InitializeProductAsync();
        }

        private async Task InitializeProductAsync()
        {
            if(_db.Sections.Any())
            {
                _Logger.LogInformation("Инициализация БД информацией о товарах не требуется");
                return;
            }
            
            _Logger.LogInformation("Запись секций...");
            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Sections.AddRange(TestData.Sections);

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _Logger.LogInformation("Запись секций выполнена успешно");

            _Logger.LogInformation("Запись брендов...");
            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Brands.AddRange(TestData.Brands);
                
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _Logger.LogInformation("Запись брендов выполнена успешно");

            _Logger.LogInformation("Запись товаров...");
            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Products.AddRange(TestData.Products);

                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await _db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
                await _db.Database.CommitTransactionAsync();
            }
            _Logger.LogInformation("Запись товаров выполнена успешно");
        }
    }
}
