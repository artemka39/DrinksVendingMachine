using DrinksVendingMachine.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.DrinksVending
{
    public class DrinksQueue : IDrinksQueue
    {
        private Queue<Drink> drinks;
        public DrinksQueue()
        {
            drinks = new Queue<Drink>();
        }

        public Queue<Drink> GetDrinksQueue()
        {
            return drinks;
        }

        public void AddDrinkToQueue(Drink drink)
        {
            drinks.Enqueue(drink);
        }

        public Drink GetDrinkFromQueue()
        {
            return drinks.Dequeue();
        }
    }
}
