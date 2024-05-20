using DrinksVendingMachine.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.DrinksVending
{
    public interface IDrinksQueue
    {
        Queue<Drink> GetDrinksQueue();
        void AddDrinkToQueue(Drink drink);
        Drink GetDrinkFromQueue();
    }
}
