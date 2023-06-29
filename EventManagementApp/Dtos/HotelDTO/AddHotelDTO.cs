namespace EventManagementApp.Dtos.HotelDTO
{
    public class AddHotelDTO
    {
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelDescription { get; set; }
        public string HotelContactinfo { get; set; }
        public IFormFile? HotelImage { get; set; }
    }
}
