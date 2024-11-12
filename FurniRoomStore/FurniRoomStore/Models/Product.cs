namespace FurniRoomStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Type { get; set; }  // Вид мебели
        public string ManufacturingTechnology { get; set; }  // Технология изготовления
        public string Construction { get; set; }  // Конструктив
        public string Material { get; set; }  // Основной материал
        public decimal PriceInUAH { get; set; }  // Стоимость в гривне
        public decimal PriceInEUR { get; set; }  // Стоимость в евро
        public decimal PriceInUSD { get; set; }  // Стоимость в долларах США
        public string PhotoPath { get; set; }  // Путь к фото продукта
        public byte[] ImageData { get; set; }
    }
}
