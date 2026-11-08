using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persistence.Repositories
{
    public class DataSeeder(CinemaDbContext _dbContext, UserManager<User> _userManager,
        RoleManager<IdentityRole<Guid>> _roleManager) : IDataSeeder
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private static readonly string SeedFolder =
            Path.Combine(AppContext.BaseDirectory, "DataSeedFiles");

        public async Task DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                    _dbContext.Database.Migrate();
                await Seeding<Hall>("halls.json");
                await Seeding<Movie>("movies.json");
                await Seeding<Chair>("chairs.json");
                await Seeding<SnacksItem>("snacks.json");
                await Seeding<Show>("shows.json");
                await SeedingUsers();
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<List<TEntity>> ReadAsync<TEntity>(string fileName)
        {
            var path = Path.Combine(SeedFolder, fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Seed {nameof(TEntity)} not found: {path}");

            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<List<TEntity>>(stream, JsonOptions) ?? [];
        }

        private async Task Seeding<TEntity>(string path) where TEntity : class
        {
            if (await _dbContext.Set<TEntity>().AnyAsync()) return;
            var times = await ReadAsync<TEntity>(path);
            if (times is null || times.Count == 0) return;
            await _dbContext.AddRangeAsync(times);
        }

        private async Task SeedingRoles()
        {
            if (await _roleManager.Roles.AnyAsync()) return;
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        }

        private async Task SeedingUsers()
        {
            await SeedingRoles();
            if (await _userManager.Users.AnyAsync()) return;

            var user = new User
            {
                BirthDate = new DateOnly(2005, 2, 1),
                Email = "AmmanoeielUser@gmail.com",
                FirstName = "Ammanoeiel",
                LastName = "User",
                Gender = Domain.Enums.Gender.Male,
                PhoneNumber = "01203867609",
                UserName = "Manuel.User",
            };
            var admin = new User
            {
                BirthDate = new DateOnly(2005, 2, 1),
                Email = "AmmanoeielAdmin@gmail.com",
                FirstName = "Ammanoeiel",
                LastName = "Admin",
                Gender = Domain.Enums.Gender.Male,
                PhoneNumber = "01203867609",
                UserName = "Manuel.Admin",
            };
            await _userManager.CreateAsync(user, "P@ssw0rd");
            await _userManager.CreateAsync(admin, "P@ssw0rd");

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}