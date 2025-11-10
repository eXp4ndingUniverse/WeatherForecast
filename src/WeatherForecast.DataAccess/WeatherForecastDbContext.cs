using WeatherForecast.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.DataAccess;

public class WeatherForecastDbContext : DbContext
{
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<FavoriteCityEntity> FavoriteCities { get; set; }
    public DbSet<SearchHistoryEntity> SearchHistory { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WeatherDataEntity> WeatherData { get; set; }
    public DbSet<WeatherForecastEntity> WeatherForecast { get; set; }

    // Конструктор для миграций
    public WeatherForecastDbContext() { }
    
    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WeatherForecastDb;Username=postgres;Password=P@ssw0rd");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация Countries
        modelBuilder.Entity<CountryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CountryName).IsUnique();
            entity.Property(e => e.CountryName).HasMaxLength(20).IsRequired();
        });

        // Конфигурация Cities
        modelBuilder.Entity<CityEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CityName).HasMaxLength(20);
            entity.Property(e => e.LatitudeLongitude).HasMaxLength(20).IsRequired();
            
            // Внешний ключ на Countries
            entity.HasOne(c => c.Country)
                  .WithMany(co => co.Cities)
                  .HasForeignKey(c => c.CountryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Конфигурация Users
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            
            // CHECK CONSTRAINT CH_Users_RegistrationDate
            entity.HasCheckConstraint("CH_Users_RegistrationDate", "\"RegistrationDate\" <= CURRENT_DATE");
        
            // CHECK CONSTRAINT CH_Users_E_mail
            entity.HasCheckConstraint("CH_Users_E_mail", "\"Email\" LIKE '%@%.%'");
            
            // Настроить enum для Role
            entity.Property(e => e.Role)
                .HasConversion<int>(); // Сохранять в БД как int
        });

        // Конфигурация FavoriteCities (многие-ко-многим)
        modelBuilder.Entity<FavoriteCityEntity>(entity =>
        {
            entity.HasKey(fc => new { fc.UserId, fc.CityId });
            
            // Внешние ключи
            entity.HasOne(fc => fc.User)
                  .WithMany(u => u.FavoriteCities)
                  .HasForeignKey(fc => fc.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(fc => fc.City)
                  .WithMany(c => c.FavoriteCities)
                  .HasForeignKey(fc => fc.CityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация SearchHistory
        modelBuilder.Entity<SearchHistoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TimeSearch).IsRequired();
            
            // CHECK CONSTRAINT CH_SearchHistory_TimeSearch
            entity.HasCheckConstraint("CH_SearchHistory_TimeSearch", "\"TimeSearch\" <= CURRENT_DATE");
            
            // Внешние ключи
            entity.HasOne(sh => sh.User)
                  .WithMany(u => u.SearchHistories)
                  .HasForeignKey(sh => sh.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(sh => sh.City)
                  .WithMany(c => c.SearchHistories)
                  .HasForeignKey(sh => sh.CityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация WeatherForecast
        modelBuilder.Entity<WeatherForecastEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ForecastDay).IsRequired();
            entity.Property(e => e.TypeWeather).HasMaxLength(20);
            
            // Внешний ключ на Cities
            entity.HasOne(wf => wf.City)
                  .WithMany(c => c.WeatherForecasts)
                  .HasForeignKey(wf => wf.CityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация WeatherData
        modelBuilder.Entity<WeatherDataEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WindDirection).HasMaxLength(20);
            entity.Property(e => e.TimeData).IsRequired();
            
            // Внешние ключи
            entity.HasOne(wd => wd.City)
                  .WithMany(c => c.WeatherData)
                  .HasForeignKey(wd => wd.CityId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(wd => wd.WeatherForecast)
                  .WithMany(wf => wf.WeatherData)
                  .HasForeignKey(wd => wd.WeatherForecastId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Настройка значений по умолчанию для PostgreSQL
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.RegistrationDate)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<SearchHistoryEntity>()
            .Property(sh => sh.TimeSearch)
            .HasDefaultValueSql("NOW()");
    }
}