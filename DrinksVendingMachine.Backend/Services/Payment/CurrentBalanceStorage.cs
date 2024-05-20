using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Payment
{
    public class CurrentBalanceStorage : ICurrentBalanceStorage
    {
        private int currentBalance;
        public CurrentBalanceStorage()
        {
            currentBalance = 0;
        }

        public int GetCurrentBalance()
        {
            return currentBalance;
        }

        public void IncreaceCurrenBalance(int value)
        {
            currentBalance += value;
        }

        public void DecreaseCurrentBalance(int value)
        {
            if (currentBalance < value)
            {
                throw new InvalidOperationException("Balance must be positive");
            }
            currentBalance -= value;
        }
    }
}
