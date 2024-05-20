using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Common.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public int Denomination { get; set; }
        public bool Acceptance { get; set; }
        public int Count { get; set; }
    }
}
