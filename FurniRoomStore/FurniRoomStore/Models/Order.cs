namespace FurniRoomStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }  // Связь с таблицей клиентов
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }  // Связь с таблицей товаров
        public decimal OrderPriceInUAH { get; set; }  // Стоимость заказа в гривнах
        public int Quantity { get; set; }  // Количество изделий
        public decimal TotalAmountInUAH { get; set; }  // Сумма заказа в гривнах
    }
}
