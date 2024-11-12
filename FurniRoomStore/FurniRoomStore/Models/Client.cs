namespace FurniRoomStore.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }  // ФИО клиента
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AvatarPath { get; set; }  // Путь к аватару клиента
    }
}
