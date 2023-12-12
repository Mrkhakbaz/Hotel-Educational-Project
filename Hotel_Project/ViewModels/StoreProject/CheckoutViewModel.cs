using Microsoft.Build.Framework;

namespace Hotel_Project.ViewModels.StoreProject;

public class CheckoutViewModel
{
    public long OrderSum { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int PassCode { get; set; }
    [Required]
    public int Count { get; set; }
    public List<BasketDetailViewModel> BasketDetailViewModels { get; set; }
}