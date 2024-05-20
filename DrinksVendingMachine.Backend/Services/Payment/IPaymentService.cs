using DrinksVendingMachine.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Payment
{
    public interface IPaymentService
    {
        bool CheckBalance(int drinkCost);
        Task<int> DepositCoin(int denomination);
        int PayForOrder(Drink drink);
        Task<List<Coin>> GetChange();
    }
}
