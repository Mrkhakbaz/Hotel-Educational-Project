using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotel_Project.Models.Product;

namespace Hotel_Project.Models.Basket
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public List<OrderReserveDate> OrderReserveDates { get; set; }
        public int Price { get; set; }


        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public HotelRoom HotelRoom { get; set; }
    }
}
