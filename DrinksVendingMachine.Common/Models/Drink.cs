using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Common.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
    }
}
