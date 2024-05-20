using DrinksVendingMachine.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private DrinksVendingMachineDbContext dbContext;
        private ICurrentBalanceStorage currentBalanceStorage;
        public PaymentService(DrinksVendingMachineDbContext dbContext, ICurrentBalanceStorage currentBalanceStorage)
        {
            this.dbContext = dbContext;
            this.currentBalanceStorage = currentBalanceStorage;
        }

        public bool CheckBalance(int drinkCost)
        {
            return (drinkCost > currentBalanceStorage.GetCurrentBalance())? false : true;
        }

        public async Task<int> DepositCoin(int denomination)
        {
            var coinToDeposit = await dbContext.Coins.FirstOrDefaultAsync(coin => coin.Denomination == denomination);
            if (coinToDeposit == null)
            {
                throw new ArgumentException("Invalid coin");
            }
            if (!coinToDeposit.Acceptance)
            {
                throw new Exception("Coin is not acceptable");
            }
            coinToDeposit.Count++;
            dbContext.Coins.Update(coinToDeposit);
            await dbContext.SaveChangesAsync();
            currentBalanceStorage.IncreaceCurrenBalance(coinToDeposit.Denomination);
            return currentBalanceStorage.GetCurrentBalance();
        }

        public int PayForOrder(Drink drink)
        {
            currentBalanceStorage.DecreaseCurrentBalance(drink.Cost);
            return currentBalanceStorage.GetCurrentBalance();
        }

        public async Task<List<Coin>> GetChange()
        {
            var currentBalance = currentBalanceStorage.GetCurrentBalance();
            if (currentBalance > 0)
            {
                return await GetCoins(currentBalance);
            }
            throw new Exception("Nothing to return");
        }

        private async Task<List<Coin>> GetCoins(int currentBalance)
        {
            var coinsToChange = new List<Coin>();
            var coins = new Dictionary<int, Coin>
            {
                { 10, await dbContext.Coins.FirstOrDefaultAsync(coin => coin.Denomination == 10) },
                { 5, await dbContext.Coins.FirstOrDefaultAsync(coin => coin.Denomination == 5) },
                { 2, await dbContext.Coins.FirstOrDefaultAsync(coin => coin.Denomination == 2) },
                { 1, await dbContext.Coins.FirstOrDefaultAsync(coin => coin.Denomination == 1) }
            };
            foreach (var coin in coins)
            {
                while (currentBalance >= coin.Key && coin.Value.Count > 0)
                {
                    currentBalanceStorage.DecreaseCurrentBalance(coin.Key);
                    currentBalance = currentBalanceStorage.GetCurrentBalance();
                    coinsToChange.Add(await GetCoin(coin.Value));
                }
            }
            return coinsToChange;
        }

        private async Task<Coin> GetCoin(Coin coin)
        {
            if (coin.Count == 0)
            {
                throw new Exception("There are no more coins with this denomination");
            }
            coin.Count--;
            dbContext.Coins.Update(coin);
            await dbContext.SaveChangesAsync();
            return coin;
        }
    }
}
