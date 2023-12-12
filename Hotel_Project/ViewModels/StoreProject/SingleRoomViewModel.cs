using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject;

public class SingleRoomViewModel
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public int Price { get; set; }
    public int BedCount { get; set; }
    public int Capacity { get; set; }
    public List<AdvantageRoom> AdvantageRooms { get; set; }
    public List<ReserveDate> ReserveDates { get; set; }
}