using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Library.WebApi.Services.DonutRepository {
    public interface IDonutRepository {

        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> AddCustomer(Customer user);

        Task<Customer?> UpdateCustomer(Customer user);
        Task<Customer?> DeleteCustomer(int id);

        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product?> AddProduct(Product product);
        Task<Product?> UpdateProduct(Product product);
        Task<Product?> DeleteProduct(int id);

        Task<List<Orders>> GetAllOpenOrders();
        Task<Orders?> AddOrder(Orders order);
        Task<Orders?> UpdateOrderStatus(int OrderId, string status);

        Task<List<Orders>> GetCustomerOpenOrders(int customerId);
        Task<Orders?> AddOrderItem(int orderId, OrderItems items);
    }

    public class DonutRepository: IDonutRepository {
        private readonly DonutDbContext _dbContext;

        public DonutRepository(DonutDbContext context) {
            _dbContext = context;
        }

        public async Task<List<Customer>> GetAllCustomers() {
            var customers =  await _dbContext.Customer.ToListAsync();
            return customers;
        }

        public async Task<Customer?> GetCustomerById(int id) {
            return await _dbContext.Customer.Where(u=>u.CustomerId ==id).FirstOrDefaultAsync();
        }

        public async Task<Customer?> AddCustomer(Customer customer) {
            await _dbContext.Customer.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateCustomer(Customer cust) {
            var customer = await _dbContext.Customer.Where(u => u.CustomerId == cust.CustomerId).FirstOrDefaultAsync();
            if (customer == null) {
                return null;
            }
            customer.CustomerName = cust.CustomerName;
            _dbContext.SaveChanges();
            return customer;
        }

        public async Task<Customer?> DeleteCustomer(int id) {
            var customer = await _dbContext.Customer.Where(u => u.CustomerId == id).FirstOrDefaultAsync();
            if (customer == null) {
                return null;
            }
            _dbContext.Customer.Remove(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
             
        }

        public async Task<List<Product>> GetAllProducts() {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id) {
            return await _dbContext.Product.Where(b => b.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<Product?> AddProduct(Product product) {
            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProduct(Product product) {
            var prod = await _dbContext.Product.Where(b => b.ProductId == product.ProductId).FirstOrDefaultAsync();
            if (prod == null) {
                return null;
            }
            prod.ProductName = product.ProductName;
            prod.Price = product.Price;
            await _dbContext.SaveChangesAsync();
            return prod;
        }

        public async Task<Product?> DeleteProduct(int id) {
            var prod = await _dbContext.Product.Where(b => b.ProductId == id).FirstOrDefaultAsync();
            if (prod == null) {
                return null;
            }
            _dbContext.Product.Remove(prod);
            await _dbContext.SaveChangesAsync();
            return prod;    
        }

        public async Task<List<Orders>> GetAllOpenOrders() {
            return await _dbContext.Orders.Include(o=>o.OrderItems).Where(o => o.OrderStatus == OrderStatus.Pending).ToListAsync();
        }

        public async Task<Orders?> AddOrder(Orders order) {
            order.OrderStatus = OrderStatus.Pending;
            order.UpdateTotal();

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
           
            return order;
           
        }
        /// <summary>
        /// Update the status of an order
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="status"></param>
        /// <returns>the updated order</returns>
        public async Task<Orders?> UpdateOrderStatus(int OrderId, string status) {
            var order = await _dbContext.Orders.Where(o => o.OrderId == OrderId).FirstOrDefaultAsync();
            if (order == null) {
                return null;
            }
            // if changing to completed or cancelled, then it must be pending
            if (status == OrderStatus.Completed || status == OrderStatus.Cancelled) {
                if (order.OrderStatus != OrderStatus.Pending) {
                    return null;
                }
            }
            // if changing to pendingcancelled, then it must be completed or cancelled
            if (status == OrderStatus.Pending) {
                if (order.OrderStatus == OrderStatus.Pending) {
                    return null;
                }
            }

            order.OrderStatus = status;
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<List<Orders>> GetCustomerOpenOrders(int customerId) {
            return await _dbContext.Orders.Include(o => o.OrderItems).Where(o => o.CustomerId == customerId && o.OrderStatus == OrderStatus.Pending).ToListAsync();
        }

        public async Task<Orders?> AddOrderItem(int orderId, OrderItems items) {
            var order = await _dbContext.Orders.Include(o=>o.OrderItems).Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
            // only allow updates if order is in pending status
            if (items == null || order == null || order.OrderStatus != OrderStatus.Pending) {
                return null;
            }
            items.OrderId = order.OrderId;
            order.OrderItems.Add(items);
            order.UpdateTotal();
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }

}
