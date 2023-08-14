using Microsoft.EntityFrameworkCore;
using TMS.Api.Exceptions;
using TMS.Api.Models;

namespace TMS.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PracticaContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new PracticaContext();
        }

        public int Add(Order @order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order @order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }

        public  IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public IEnumerable<object> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetById(int id)
        {
            //var @order =await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            var @order = await _dbContext.Orders.Where(o => o.OrderId == id).Include(o => o.TicketCategory).Include(o => o.TicketCategory.Event).FirstOrDefaultAsync();
            if (order == null)
                throw new EntityNotFoundException(id, nameof(Order));

            return @order;
        }

        public void Update(Order @order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }

    
}
