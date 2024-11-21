using System.ComponentModel.DataAnnotations;

namespace FurniRoomStore.Models
{
    public class Product
    {
        public int Id { get; set; } // Уникальный идентификатор продукта


        public required string Name { get; set; } = string.Empty;  // Название продукта


        public decimal Price { get; set; } // Цена продукта

       
        public string? Description { get; set; } // Описание продукта

     
        public int StockQuantity { get; set; } // Количество на складе


        public required string Category { get; set; } = string.Empty;  // Категория продукта


        public string Type { get; set; } // Вид мебели

       
        public string ManufacturingTechnology { get; set; } // Технология изготовления

       
        public string Construction { get; set; } // Конструктив

      
        public string Material { get; set; } // Основной материал
    }
}
