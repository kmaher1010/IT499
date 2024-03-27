using System.Text.Json.Serialization;

namespace Library.WebApi.Services.DonutRepository {

    public class Customer {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
    public class Product {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

    }

    public static class OrderStatus {
        public const string Pending = "Pending";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
    }
    public class Orders {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public string? OrderStatus { get; set; }
        public List<OrderItems>? OrderItems { get; set; }

        public void UpdateTotal() {
            Total = OrderItems.Sum(i => i.UpdateTotal());
        }

    }
    public class OrderItems {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal UpdateTotal() {
            Total = Quantity * Price;
            return Total;
        }
        [JsonIgnore]
        public virtual Orders? Orders { get; set; }
    }


}
