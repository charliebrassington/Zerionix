namespace Zerionix.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService();
            var order = orderService.CreateOrder(1);
        }
    }
}