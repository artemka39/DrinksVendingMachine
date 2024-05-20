using DrinksVendingMachine.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Administrative
{
    public class AdministrativeService : IAdministrativeService
    {
        private readonly DrinksVendingMachineDbContext dbContext;
        public AdministrativeService(DrinksVendingMachineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Drink>> GetDrinksList()
        {
            var drinksList = await dbContext.Drinks.ToListAsync();
            return drinksList;
        }

        public async Task<List<Coin>> GetCoinsList()
        {
            var coinsList = await dbContext.Coins.ToListAsync();
            return coinsList;
        }

        public async Task AddDrink(Drink drink)
        {
            var existingDrink = await dbContext.Drinks.Where(d => d.Name.Equals(drink.Name)).FirstOrDefaultAsync();
            if (existingDrink != null)
            {
                throw new Exception("Drink already exists");
            }
            await dbContext.Drinks.AddAsync(drink);
            await dbContext.SaveChangesAsync();
        }

        public async Task ChangeDrink(Drink drink)
        {
            var drinkToChange = await dbContext.Drinks.FindAsync(drink.Id);
            if (drinkToChange == null)
            {
                throw new Exception("Drink not found");
            }
            dbContext.Entry(drinkToChange).CurrentValues.SetValues(drink);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDrink(Drink drink)
        {
            var drinkToDelete = await dbContext.Drinks.FindAsync(drink.Id);
            if (drinkToDelete == null)
            {
                throw new Exception("Drink not found");
            }
            dbContext.Drinks.Remove(drinkToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task ImportDrink(string drinkSource)
        {
            var importedData = await File.ReadAllTextAsync(drinkSource);
            var drink = JsonConvert.DeserializeObject<Drink>(importedData);
            await AddDrink(drink);
        }

        public async Task ChangeCoinsCount(Coin coin, int coinsCount)
        {
            var coinToChangeCount = await dbContext.Coins.FindAsync(coin.Id);
            coinToChangeCount.Count = coinsCount;
            dbContext.Coins.Update(coinToChangeCount);
            await dbContext.SaveChangesAsync();
        }

        public async Task EnableCoinAcceptance(Coin coin)
        {
            var coinToChangeAcceptance = await dbContext.Coins.FindAsync(coin.Id);
            coinToChangeAcceptance.Acceptance = true;
            dbContext.Coins.Update(coinToChangeAcceptance);
            await dbContext.SaveChangesAsync();
        }

        public async Task DisableCoinAcceptance(Coin coin)
        {
            var coinToChangeAcceptance = await dbContext.Coins.FindAsync(coin.Id);
            coinToChangeAcceptance.Acceptance = false;
            dbContext.Coins.Update(coinToChangeAcceptance);
            await dbContext.SaveChangesAsync();
        }
    }
}
