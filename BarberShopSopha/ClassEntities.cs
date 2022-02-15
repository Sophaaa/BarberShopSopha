using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberShopSopha.EF;

namespace BarberShopSopha
{
    internal class ClassEntities
    {
        public static BarberShopEntities context { get; set; } = new BarberShopEntities();

    }
}
