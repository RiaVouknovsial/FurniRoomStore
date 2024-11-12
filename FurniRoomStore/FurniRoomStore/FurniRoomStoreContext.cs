using FurniRoomStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FurniRoomStore
{
    public class FurniRoomStoreContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FurniRoomStoreContext(DbContextOptions<FurniRoomStoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи между Order и Client
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // Определяем поведение при удалении клиента

            // Настройка связи между Order и Product
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Защита от удаления продукта с активными заказами

            // Пример ограничения длины строковых данных
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(c => c.FullName).HasMaxLength(255);
                entity.Property(c => c.Email).HasMaxLength(255);
                entity.Property(c => c.Phone).HasMaxLength(20);
                entity.Property(c => c.Country).HasMaxLength(100);
                entity.Property(c => c.City).HasMaxLength(100);
                entity.Property(c => c.AvatarPath).HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Type).HasMaxLength(50); // Ограничение для поля Type (Вид мебели)
                entity.Property(p => p.ManufacturingTechnology).HasMaxLength(100); // Технология изготовления
                entity.Property(p => p.Construction).HasMaxLength(100); // Конструктив
                entity.Property(p => p.Material).HasMaxLength(100); // Основной материал
                entity.Property(p => p.PhotoPath).HasMaxLength(500); // Путь к фото продукта
            });
        }
    }
}
