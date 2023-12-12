using System.Security.Claims;
using Hotel_Project.Core;
using Hotel_Project.ViewModels.StoreProject;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Project.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _service;
        
        public StoreController(IStoreService service)
        {
            _service = service;
        }

        public IActionResult Shop()
        {
            return View(_service.ShowStore());
        }

        public IActionResult ShowSingleHotel(int id)
        {
            return View(_service.ShowSingleHotel(id));
        }

        public IActionResult ReserveRoom(int id)
        {
            return View(_service.ShowSingleRoom(id));
        }

        [HttpPost]
        public IActionResult CreateOrder(GetOrderVm viewModel)
        {
            viewModel.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response =_service.CreateUserOrder(viewModel);
            switch (response)
            {
                case 0:
                    return RedirectToAction("Index","Home");
                case 1:
                    return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Basket");
        }

        public IActionResult Basket()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_service.GetUserBasket(userId));
        }

        public IActionResult RemoveDetail(int id)
        {
            _service.RemoveOrderDetail(id);
            return RedirectToAction("Basket", "Store");
        }

        public IActionResult Checkout()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var viewModel = _service.GetUserCheckout(userId);
            if (viewModel != null)
            {
                return View(viewModel);
            }
            return RedirectToAction("Basket", "Store");
        }

        public IActionResult Payment(CheckoutViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Payment Method
            if (_service.OrderPayment(userId , viewModel) != 1)
            {
                return RedirectToAction("Checkout", "Store");
            }
            return RedirectToAction("UserOrders", "Account");
        }
    }
}
