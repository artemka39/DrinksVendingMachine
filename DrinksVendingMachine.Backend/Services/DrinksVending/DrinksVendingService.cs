using DrinksVendingMachine.Backend.Services.Payment;
using DrinksVendingMachine.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.DrinksVending
{
    public class DrinksVendingService : IDrinksVendingService
    {
        private readonly IPaymentService paymentService;
        private readonly IDrinksQueue drinksQueue;
        private readonly DrinksVendingMachineDbContext dbContext;
        public DrinksVendingService(IPaymentService paymentService, IDrinksQueue drinksQueue, DrinksVendingMachineDbContext dbContext)
        {
            this.paymentService = paymentService;
            this.drinksQueue = drinksQueue;
            this.dbContext = dbContext;
        }

        public async Task<int> TopUpBalance(int denomination)
        {
            return await paymentService.DepositCoin(denomination);
        }

        public async Task<List<Coin>> GetChange()
        {
            return await paymentService.GetChange();
        }

        public async Task<int> SelectDrink(Drink drink)
        {
            var drinkToSelect = await dbContext.Drinks.FindAsync(drink.Id);
            if (!paymentService.CheckBalance(drinkToSelect.Cost))
            {
                throw new Exception("Not enough money");
            }
            drinksQueue.AddDrinkToQueue(drink);
            return paymentService.PayForOrder(drink);
        }

        public async Task<List<Drink>> GetDrinks()
        {
            var drinks = await PrepareDrinks();
            return drinks;
        }

        public async Task<List<Drink>> GetDrinksList()
        {
            var drinks = await dbContext.Drinks.ToListAsync();
            return drinks;
        }

        private async Task<List<Drink>> PrepareDrinks()
        {
            var preparedDrinks = new List<Drink>();
            var drinksToPrepare = drinksQueue.GetDrinksQueue();
            for (int i = 0; i < drinksToPrepare.Count; i++)
            {
                var drinkFromQueue = drinksQueue.GetDrinkFromQueue();
                var drinkToGet = await GetDrinkFromStorage(drinkFromQueue);
                preparedDrinks.Add(drinkToGet);
            }
            return preparedDrinks;
        }

        private async Task<Drink> GetDrinkFromStorage(Drink drinkFromQueue)
        {
            var drinkToGet = await dbContext.Drinks.FindAsync(drinkFromQueue.Id);
            drinkToGet.Amount -= 1;
            dbContext.Drinks.Update(drinkToGet);
            await dbContext.SaveChangesAsync();
            return drinkToGet;
        }
    }
}
