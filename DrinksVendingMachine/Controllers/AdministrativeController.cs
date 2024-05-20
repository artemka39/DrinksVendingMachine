using DrinksVendingMachine.Backend.Services.Administrative;
using DrinksVendingMachine.Backend.Services.DrinksVending;
using DrinksVendingMachine.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinksVendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrativeController : Controller
    {
        private readonly IAdministrativeService administrativeService;
        public AdministrativeController(IAdministrativeService administrativeService)
        {
            this.administrativeService = administrativeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var drinks = await administrativeService.GetDrinksList();
                var coins = await administrativeService.GetCoinsList();
                return Ok((object)new
                {
                    Drinks = drinks,
                    Coins = coins
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddDrink")]
        public async Task<IActionResult> AddDrink(Drink drink)
        {
            try
            {
                await administrativeService.AddDrink(drink);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteDrink")]
        public async Task<IActionResult> DeleteDrink(Drink drink)
        {
            try
            {
                await administrativeService.DeleteDrink(drink);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPatch]
        [Route("ChangeDrink")]
        public async Task<IActionResult> ChangeDrink(Drink drink)
        {
            try
            {
                await administrativeService.ChangeDrink(drink);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("ImportDrink")]
        public async Task<IActionResult> ImportDrink(string drinkSource)
        {
            try
            {
                await administrativeService.ImportDrink(drinkSource);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("ChangeCoinsCount")]
        public async Task<IActionResult> ChangeCoinsCount(Coin coin, int coinsCount)
        {
            try
            {
                await administrativeService.ChangeCoinsCount(coin, coinsCount);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("DisableCoinAcceptance")]
        public async Task<IActionResult> DisableCoinAcceptance(Coin coin)
        {
            try
            {
                await administrativeService.DisableCoinAcceptance(coin);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("EnableCoinAcceptance")]
        public async Task<IActionResult> EnableCoinAcceptance(Coin coin)
        {
            try
            {
                await administrativeService.EnableCoinAcceptance(coin);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
