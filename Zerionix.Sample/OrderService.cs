using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Sample
{
    public class OrderService
    {
        public Order CreateOrder(int userId)
        {
            var total = -50m;
            var totalB = total;
            var totalC = totalB;
            var totalD = total;
            var totalE = totalC;

            return new Order
            {
                OrderID = 1,
                UserID = userId,
                Total = totalE
            };
        }
    }
}
