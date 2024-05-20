using DrinksVendingMachine.Backend.Services.DrinksVending;
using DrinksVendingMachine.Common.Models;
using DrinksVendingMachine.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DrinksVendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinksVendingController : Controller
    {
        private readonly IDrinksVendingService drinksVendingService;
        public DrinksVendingController(IDrinksVendingService drinksVendingService)
        {
            this.drinksVendingService = drinksVendingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrinksList()
        {
            try
            {
                var drinks = await drinksVendingService.GetDrinksList();
                return Ok(drinks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("TopUpBalance")]
        public async Task<IActionResult> TopUpBalance([FromBody]DenominationRequest denomination)
        {
            try
            {
                var balance = await drinksVendingService.TopUpBalance(denomination.Value);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("SelectDrink")]
        public async Task<IActionResult> SelectDrink([FromBody]Drink drink)
        {
            try
            {
                var balance = await drinksVendingService.SelectDrink(drink);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDrinks")]
        public async Task<IActionResult> GetDrinks()
        {
            try
            {
                var drinks = await drinksVendingService.GetDrinks();
                return Ok(drinks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetChange")]
        public async Task<IActionResult> GetChange()
        {
            try
            {
                var change = await drinksVendingService.GetChange();
                return Ok(change);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
