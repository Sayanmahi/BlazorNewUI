using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Popups;

namespace BlazorFastFood.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _foodDbContext;
        public OrderService(ApplicationDbContext foodDbContext)
        {
            _foodDbContext = foodDbContext;
        }
        public async Task<List<GetAllOrderDTO>> GetAllOrders()
        {
            var data = await _foodDbContext.Orders.ToListAsync();
            List<GetAllOrderDTO> d= new List<GetAllOrderDTO>();
            if(data!= null)
            {
                foreach(var i in data)
                {
                    var da = await _foodDbContext.Users.FindAsync(i.UserId);
                    var itemname= await _foodDbContext.Items.FindAsync(i.ItemId);
                    var dto = new GetAllOrderDTO()
                    {
                        Id = i.Id,
                        Qty = i.Qty,
                        Price = i.Price,
                        ItemName = itemname.ProdName,
                        date = i.date,
                        isdelivered = i.isdelivered,
                        UserName = da.Name,
                        PhoneNo = da.PhoneNumber,
                        ImageUrl=itemname.ImageUrl
                    };
                    d.Add(dto);
                }
            }
            return (d);
        }

        public async Task<List<GetAllOrderDTO>> GetOrdersNotDelivered()
        {
            var data = await _foodDbContext.Orders.Where(x => x.isdelivered == 1 || x.isdelivered==2).ToListAsync();
            List<GetAllOrderDTO> d = new List<GetAllOrderDTO>();
            if (data != null)
            {
                foreach (var i in data)
                {
                    var da = await _foodDbContext.Users.FindAsync(i.UserId);
                    var itemname = await _foodDbContext.Items.FindAsync(i.ItemId);
                    var dto = new GetAllOrderDTO()
                    {
                        Id = i.Id,
                        Qty = i.Qty,
                        ImageUrl=itemname.ImageUrl,
                        Price = i.Price,
                        ItemName = itemname.ProdName,
                        date = i.date,
                        isdelivered = i.isdelivered,
                        UserName = da.Name,
                        PhoneNo = da.PhoneNumber
                    };
                    d.Add(dto);
                }
            }
            return (d);
        }

        public async Task<List<MyOrder>> MyOrders(int cid)
        {
            var d = await _foodDbContext.Orders.Where(x => x.UserId == cid && x.isdelivered==1).OrderByDescending(x => x.date).ToListAsync();
            List<MyOrder> l = new List<MyOrder>();
            if (d != null)
            {
                foreach (var i in d)
                {
                    var dd = await _foodDbContext.Items.FirstOrDefaultAsync(x => x.Id == i.ItemId);
                    MyOrder n = new MyOrder()
                    {
                        Id = i.Id,
                        Qty = i.Qty,
                        Price = i.Price,
                        ItemName = dd.ProdName,
                        date = i.date,
                        isdelivered = i.isdelivered,
                        ImageUrl=dd.ImageUrl
                    };
                    l.Add(n);
                }
            }
                var d1 = await _foodDbContext.Orders.Where(x => x.UserId == cid && (x.isdelivered == 2 || x.isdelivered==0)).ToListAsync();
                if (d1 != null)
                {
                    foreach (var i in d1)
                    {
                        var dd = await _foodDbContext.Items.FirstOrDefaultAsync(x => x.Id == i.ItemId);
                        MyOrder n = new MyOrder()
                        {
                            Id = i.Id,
                            Qty = i.Qty,
                            Price = i.Price,
                            ItemName = dd.ProdName,
                            date = i.date,
                            isdelivered = i.isdelivered,
                            ImageUrl=dd.ImageUrl
                        };
                        l.Add(n);
                    }
                }
            return l;
        }

        public async Task<bool> OrderIsPreparing(int id)
        {
            var d = await _foodDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            d.isdelivered = 2;
            await _foodDbContext.SaveChangesAsync();
            return (true);
        }

        public async Task<bool> PlaceOrder(MyOrder orders)
        {
            try {
                Order o = new Order();
                o.isdelivered = 1;
                o.date = DateTime.Now;
                o.Qty=orders.Qty;
                o.Price=orders.Price;
                o.UserId = orders.uid;
                var d = await _foodDbContext.Items.FirstOrDefaultAsync(x => x.ProdName.Equals(orders.ItemName));
                o.ItemId = d.Id;
                    await _foodDbContext.AddAsync(o);
                    await _foodDbContext.SaveChangesAsync();
                var data= await _foodDbContext.Carts.FirstOrDefaultAsync(x=>x.Id==orders.Id);
                _foodDbContext.Carts.Remove(data);
                await _foodDbContext.SaveChangesAsync();
                return (true);
            }catch (Exception ex)
            {
                return (false);
            }
           
        }

        public async Task<List<MyOrder>> ShowMyUndeliveredOrders(int id)
        {
            var d = await _foodDbContext.Orders.Where(x => x.UserId == id && x.isdelivered == 1).ToListAsync();
            List<MyOrder> orders = new List<MyOrder>();
            if(d!=null)
            {
                foreach(var i in d)
                {
                    var c = await _foodDbContext.Items.FindAsync(i.ItemId);
                    var f = new MyOrder()
                    {
                        Id = i.Id,
                        Qty = i.Qty,
                        Price = i.Price,
                        ItemName = c.ProdName,
                        date = i.date,
                        isdelivered = i.isdelivered,
                        ImageUrl=c.ImageUrl
                    };
                    orders.Add(f);
                }
            }
            return (orders);
        }

        public async Task<bool> ValidateisDelieredOrder(int id)
        {
            var d = await _foodDbContext.Orders.FindAsync(id);
            if(d!=null)
            {
                d.isdelivered = 0;
                await _foodDbContext.SaveChangesAsync();
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public async Task<bool> PlaceListOrder(MyOrder order)
        {
            try
            {
                foreach (var i in order.cartids)
                {
                    var orders = await _foodDbContext.Carts.FirstOrDefaultAsync(x => x.Id == i);
                    Order o = new Order();
                    o.isdelivered = 1;
                    o.date = DateTime.Now;
                    o.Qty = orders.Qty;
                    o.Price = orders.Price;
                    o.UserId = order.uid;
                    //var d = await _foodDbContext.Items.FirstOrDefaultAsync(x => x.Id==orders.ItemId);
                    o.ItemId = orders.ItemId;
                    await _foodDbContext.AddAsync(o);
                    await _foodDbContext.SaveChangesAsync();
                    //var data = await _foodDbContext.Carts.FirstOrDefaultAsync(x => x.Id == orders.Id);
                    _foodDbContext.Carts.Remove(orders);
                    await _foodDbContext.SaveChangesAsync();
                }
                return (true);
            }
            catch (Exception ex)
            {
                return (false);
            }
        }
    }
}
