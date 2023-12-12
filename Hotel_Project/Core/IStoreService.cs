using Hotel_Project.ViewModels.Account;
using Hotel_Project.ViewModels.StoreProject;

namespace Hotel_Project.Core
{
    public interface IStoreService
    {
        List<ShowStoreHotels> ShowStore();
        ShowSingleHotelVm ShowSingleHotel(long id);
        SingleRoomViewModel ShowSingleRoom(int id);
        int CreateUserOrder(GetOrderVm incomeModel);
        BasketViewModel GetUserBasket(int userId);
        int RemoveOrderDetail(int id);
        CheckoutViewModel GetUserCheckout(int userId);
        int OrderPayment(long id , CheckoutViewModel incomeModel);
        ICollection<UserOrdersViewModel> GetUserOrders(int id);
    }
}
