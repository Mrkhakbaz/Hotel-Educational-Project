using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject;

public class BasketViewModel
{
    public List<BasketDetailViewModel> BasketDetailViewModels { get; set; }
    public long OrderSum { get; set; }
}