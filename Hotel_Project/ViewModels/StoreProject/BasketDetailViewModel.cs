using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject;

public class BasketDetailViewModel
{
    public int Id { get; set; }
    public string HotelName { get; set; }
    public string RoomName { get; set; }
    public List<ReserveDate> ReserveDates { get; set; }
    public int BasePrice { get; set; }
    public int TotalPrice { get; set; }
}