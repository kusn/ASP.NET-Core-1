using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.Identity;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InSQL
{
    public class SqlDataService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManadger;

        public SqlDataService(WebStoreDB db, UserManager<User> UserManadger)
        {
            this._db = db;
            _UserManadger = UserManadger;
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetUserOrders(string user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GreateOrder(string userName, CartViewModel Cart, OrderViewModel orderViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
