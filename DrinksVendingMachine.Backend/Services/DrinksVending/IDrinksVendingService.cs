using DrinksVendingMachine.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.DrinksVending
{
    public interface IDrinksVendingService
    {
        Task<int> TopUpBalance(int denomination);
        Task<int> SelectDrink(Drink drink);
        Task<List<Drink>> GetDrinks();
        Task<List<Coin>> GetChange();
        Task<List<Drink>> GetDrinksList();
    }
}
