using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend.Services.Payment
{
    public interface ICurrentBalanceStorage
    {
        int GetCurrentBalance();
        void IncreaceCurrenBalance(int value);
        void DecreaseCurrentBalance(int value);
    }
}
