using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Sample
{
    public class OrderService
    {
        public Order CreateOrder(int userId)
        {
            decimal total = 1m;
            var x = 2;
            var y = 99;

            if (x == 1)
            {
                total = -50m;
            }
            else if (x == 2)
            {
                if (y == 2)
                {
                    total = 1m;
                }
                else if (y == 1)
                {
                    total = 5m;
                }
                else if (y == 98 || y == 99)
                {
                    total = total - 10m;
                }
            }
            else
            {
                total = 1m;
            }

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
