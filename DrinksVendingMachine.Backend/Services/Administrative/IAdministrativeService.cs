using DrinksVendingMachine.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Administrative
{
    public interface IAdministrativeService
    {
        Task AddDrink(Drink drink);
        Task DeleteDrink(Drink drink);
        Task ChangeDrink(Drink drink);
        Task ImportDrink(string drinkSource);
        Task ChangeCoinsCount(Coin coin, int coinsCount);
        Task EnableCoinAcceptance(Coin coin);
        Task DisableCoinAcceptance(Coin coin);
        Task<List<Drink>> GetDrinksList();
        Task<List<Coin>> GetCoinsList();
    }
}
