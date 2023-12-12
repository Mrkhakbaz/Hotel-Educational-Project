using Hotel_Project.Data;
using Hotel_Project.Extention;
using Hotel_Project.ViewModels.StoreProject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Hotel_Project.Models.Basket;
using Hotel_Project.Models.Product;
using Hotel_Project.ViewModels.Account;

namespace Hotel_Project.Core
{
    public class StoreService : IStoreService
    {
        private readonly MyContext _context;

        public StoreService(MyContext context)
        {
            _context = context;
        }

        #region Store

        public List<ShowStoreHotels> ShowStore()
        {
            return _context.hotels.Select(h => new ShowStoreHotels()
            {
                Id = h.Id,
                Description = h.Description,
                ImageName = h.hotelGalleries.First().ImageName,
                Title = h.Title
            }).ToList();
        }

        public ShowSingleHotelVm ShowSingleHotel(long id)
        {
            var hotel = _context.hotels.Include(r => r.hotelRules).Include(g => g.hotelGalleries)
                .Include(x => x.hotelRooms).ThenInclude(r => r.reserveDates).SingleOrDefault(x => x.Id == id);
            return new ShowSingleHotelVm()
            {
                Id = hotel.Id,
                Description = hotel.Description,
                EntryTime = hotel.EntryTime,
                ExitTime = hotel.ExitTime,
                hotelGalleries = hotel.hotelGalleries,
                hotelRules = hotel.hotelRules,
                RoomCount = hotel.RoomCount,
                StageCount = hotel.StageCount,
                Title = hotel.Title,
                RoomListVm = hotel.hotelRooms.Select(x => new RoomListVm()
                {
                    Title = x.Title,
                    BedCount = x.BedCount,
                    Capacity = x.Capacity,
                    Count = x.Count,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    Id = x.Id,
                    RoomPrice = x.RoomPrice,
                    LastReserveDate = x.reserveDates.FirstOrDefault(x => x.ReserveTime.Date >= DateTime.Now.Date),
                    advantagesRoom = _context.advantageToRs.Where(a => a.RoomId == x.Id)
                        .Select(a => a.advantageRoom).ToList() ?? null,

                }).ToList()
            };
        }


        public SingleRoomViewModel ShowSingleRoom(int id)
        {
            var Room = _context.hotelRooms.Include(r=>r.reserveDates).SingleOrDefault(x => x.Id == id);
            if (Room != null)
            {
                return new SingleRoomViewModel()
                {
                    Title = Room.Title,
                    BedCount = Room.BedCount,
                    Capacity = Room.Capacity,
                    Description = Room.Description,
                    ImageName = Room.ImageName,
                    Id = Room.Id,
                    HotelId = Room.HotelId,
                    Price = Room.RoomPrice,
                    ReserveDates = Room.reserveDates.Where(x=>x.ReserveTime.Date >= DateTime.Now.Date).ToList(),
                    AdvantageRooms = _context.advantageToRs.Where(x=>x.RoomId == Room.Id).Select(x=>x.advantageRoom).ToList()
                };
            }
            return null;
        }

        #endregion

        #region Order And CheckOut

        public int CreateUserOrder(GetOrderVm incomeModel)
        {
            try
            {
                var room = _context.hotelRooms.SingleOrDefault(x => x.Id == incomeModel.RoomId);
                if (room == null)
                {
                    return 1;
                }

                var order = _context.Orders.Include(x=>x.OrderDetails).ThenInclude(x=>x.OrderReserveDates)
                    .SingleOrDefault(x => x.UserId == incomeModel.UserId && !x.IsFinilly);
                if (order == null)
                {
                    order = new Order()
                    {
                        CreateDate = DateTime.Now,
                        HotelId = room.HotelId,
                        UserId = incomeModel.UserId
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    var detail = new OrderDetail()
                    {
                        RoomId = room.Id,
                        OrderId = order.Id
                    };
                    _context.OrderDetails.Add(detail);
                    _context.SaveChanges();

                    var reserveDates = new List<OrderReserveDate>();
                    foreach (var item in incomeModel.Dates)
                    {
                        var date = _context.reserveDates.SingleOrDefault(x =>
                            !x.IsReserve && x.RoomId == room.Id && x.Id == item && x.Count > 0);
                        if (date != null)
                        {
                            date.Count -= 1;
                            reserveDates.Add(new OrderReserveDate()
                            {
                                DetailId = detail.Id,
                                ReserveId = date.Id,
                                Price = date.Price,
                                Count = 1
                            });
                            _context.Update(date);
                        }
                    }

                    detail.OrderReserveDates = reserveDates;
                    detail.Price = reserveDates.Sum(x => x.Price);
                    order.OrderSum = detail.Price;

                    _context.Update(detail);
                    _context.Update(order);
                    _context.SaveChanges();
                    return 2;

                }
                else
                {
                    var detail = order.OrderDetails.SingleOrDefault(x => x.RoomId == incomeModel.RoomId);
                    if (detail != null)
                    {
                        var reserveDates = new List<OrderReserveDate>();
                        foreach (var item in incomeModel.Dates)
                        {
                            var date = _context.reserveDates.SingleOrDefault(x =>
                                !x.IsReserve && x.RoomId == room.Id && x.Id == item && x.Count > 0);
                            if (date != null)
                            {
                                var reserve = detail.OrderReserveDates.SingleOrDefault(x => x.ReserveId == date.Id);
                                if (reserve != null)
                                {
                                    date.Count -= 1;
                                    order.OrderSum += date.Price;
                                    detail.Price += date.Price;
                                    reserve.Count += 1;
                                    reserve.Price *=2;
                                    _context.Update(date);
                                    _context.Update(detail);
                                    _context.Update(reserve);
                                    _context.Update(order);
                                }
                                else
                                {
                                    date.Count -= 1;
                                    detail.OrderReserveDates.Add(new OrderReserveDate()
                                    {
                                        DetailId = detail.Id,
                                        ReserveId = date.Id,
                                        Price = date.Price,
                                        Count = 1
                                    });
                                    order.OrderSum += date.Price;
                                    detail.Price += date.Price;
                                    _context.Update(date);
                                    _context.Update(detail);
                                    _context.Update(order);
                                }
                                _context.SaveChanges();
                            }
                        }
                        return 3;
                    }
                    else
                    {
                        var orderDetail = new OrderDetail()
                        {
                            RoomId = room.Id,
                            OrderId = order.Id
                        };
                        _context.OrderDetails.Add(orderDetail);
                        _context.SaveChanges();

                        var reserveDates = new List<OrderReserveDate>();
                        foreach (var item in incomeModel.Dates)
                        {
                            var date = _context.reserveDates.SingleOrDefault(x =>
                                !x.IsReserve && x.RoomId == room.Id && x.Id == item && x.Count > 0);
                            if (date != null)
                            {
                                date.Count -= 1;
                                reserveDates.Add(new OrderReserveDate()
                                {
                                    DetailId = orderDetail.Id,
                                    ReserveId = date.Id,
                                    Price = date.Price,
                                    Count = 1
                                });
                                _context.Update(date);
                            }
                        }

                        orderDetail.OrderReserveDates = reserveDates;
                        orderDetail.Price = reserveDates.Sum(x => x.Price);
                        order.OrderSum += orderDetail.Price;

                        _context.Update(orderDetail);
                        _context.Update(order);
                        _context.SaveChanges();
                        return 4;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public BasketViewModel GetUserBasket(int userId)
        {
            try
            {
                var order = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.OrderReserveDates).
                    Include(x=>x.OrderDetails).ThenInclude(x=>x.HotelRoom).ThenInclude(x=>x.hotel)
                    .SingleOrDefault(x => x.UserId == userId && !x.IsFinilly);
                if (order == null)
                {
                    return null;
                }

                return new BasketViewModel()
                {
                    OrderSum = order.OrderSum,
                    BasketDetailViewModels = order.OrderDetails.Select(x=> new BasketDetailViewModel
                    {
                        Id = x.Id,
                        BasePrice = x.HotelRoom.RoomPrice,
                        RoomName = x.HotelRoom.Title,
                        HotelName = x.HotelRoom.hotel.Title,
                        TotalPrice = x.Price,
                        ReserveDates = x.OrderReserveDates.Select(x=> new ReserveDate
                        {
                            ReserveTime = _context.reserveDates.SingleOrDefault(r=>r.Id == x.ReserveId).ReserveTime,
                            Price = x.Price,
                            Id = x.ReserveId
                        }).ToList()
                    }).ToList()
                };

            }
            catch (Exception)
            {
                return null;
            }
        }

        public CheckoutViewModel GetUserCheckout(int userId)
        {
            try
            {
                var order = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.OrderReserveDates).
                    Include(x=>x.OrderDetails).ThenInclude(x=>x.HotelRoom).ThenInclude(x=>x.hotel)
                    .SingleOrDefault(x => x.UserId == userId && !x.IsFinilly);
                if (order == null)
                {
                    return null;
                }

                return new CheckoutViewModel()
                {
                    OrderSum = order.OrderSum,
                    BasketDetailViewModels = order.OrderDetails.Select(x=> new BasketDetailViewModel
                    {
                        Id = x.Id,
                        BasePrice = x.HotelRoom.RoomPrice,
                        RoomName = x.HotelRoom.Title,
                        HotelName = x.HotelRoom.hotel.Title,
                        TotalPrice = x.Price,
                        ReserveDates = x.OrderReserveDates.Select(x=> new ReserveDate
                        {
                            ReserveTime = _context.reserveDates.SingleOrDefault(r=>r.Id == x.ReserveId).ReserveTime,
                            Price = x.Price,
                            Id = x.ReserveId
                        }).ToList()
                    }).ToList()
                };

            }
            catch (Exception)
            {
                return null;
            }
        }


        public int OrderPayment(long id, CheckoutViewModel incomeModel)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(x => x.UserId == id && !x.IsFinilly);
                order.IsFinilly = true;
                order.Count = incomeModel.Count;
                order.LastName = incomeModel.LastName;
                order.Name = incomeModel.Name;
                order.PassCode = incomeModel.PassCode;
                if (string.IsNullOrEmpty(incomeModel.Name) || string.IsNullOrEmpty(incomeModel.LastName) || incomeModel.PassCode == 0 || incomeModel.Count == 0)
                {
                    return 3;
                }
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 2;
            }
        }

        
        public int RemoveOrderDetail(int id)
        {
            var detail = _context.OrderDetails.Include(x=>x.OrderReserveDates).SingleOrDefault(x => x.Id == id);
            if (detail != null)
            {
                try
                {
                    foreach (var item in detail.OrderReserveDates)
                    {
                        var date = _context.reserveDates.SingleOrDefault(x => x.Id == item.ReserveId);
                        if (date != null)
                        {
                            date.Count += item.Count;
                        }
                    }

                    var order = _context.Orders.Include(d=>d.OrderDetails).SingleOrDefault(x => x.Id == detail.OrderId);
                    if (order.OrderDetails.Count() <= 1)
                    {
                        _context.Remove(order);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.Remove(detail);
                        _context.SaveChanges();
                    }

                    return 2;
                }
                catch (Exception)
                {
                    return 1;
                }
            }

            return 0;
        }

        public ICollection<UserOrdersViewModel> GetUserOrders(int id)
        {
            var order = _context.Orders.Where(x => x.UserId == id);
            return order.Select(x => new UserOrdersViewModel()
            {
                OrderSum = x.OrderSum,
                HotelName = _context.hotels.SingleOrDefault(h => h.Id == x.HotelId).Title,
                OrderId = x.Id
                
            }).ToList();
        }

        #endregion
    }
}
