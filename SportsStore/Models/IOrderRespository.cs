using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IOrderRespository
    {
        // chi chua method and always public
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
