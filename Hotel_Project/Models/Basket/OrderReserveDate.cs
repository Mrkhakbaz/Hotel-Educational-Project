using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hotel_Project.Models.Product;

namespace Hotel_Project.Models.Basket
{
    public class OrderReserveDate
    {
        [Key]
        public int Id { get; set; }
        public int ReserveId { get; set; }
        public int DetailId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        

        [ForeignKey(nameof(DetailId))]
        public OrderDetail OrderDetail { get; set; }
    }
}
