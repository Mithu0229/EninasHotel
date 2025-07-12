using EninasHotel.Application.Services.Interface;
using EninasHotel.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EninasHotel.Web.Controllers
{
    [Authorize]
    public class AmountsController : Controller
    {
        private readonly IAmountService _amountService;

        public AmountsController(IAmountService amountService)
        {
            _amountService = amountService;
        }

        public IActionResult Index()
        {
            var amounts = _amountService.GetAllAmounts();
            return View(amounts);
        }

        public IActionResult Create()
        {
            var amount = new Amount
            {
                TransactionDate = DateTime.Today, // Default date = today
                CashIn = 0,
                CashOut = 0,
                Charge = 0,
                Commission = 0
            };

            return View(amount);
        }

        [HttpPost]
        public IActionResult Create(Amount obj)
        {
            if(obj != null && (obj.CashIn > 0 || obj.CashOut > 0))
            {
                if (ModelState.IsValid)
                {
                    _amountService.CreateAmount(obj);
                    TempData["success"] = "The villa has been created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
             TempData["error"] = "Please enter CashIn/CashOut.";
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            Amount? obj = _amountService.GetAmountById(id);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Amount obj)
        {
            if (ModelState.IsValid)
            {
                _amountService.UpdateAmount(obj);
                TempData["success"] = "The amount has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(Guid id)
        {
            Amount? obj = _amountService.GetAmountById(id);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Amount obj)
        {
            bool deleted = _amountService.DeleteAmount(obj.Id);
            if (deleted)
            {
                TempData["success"] = "The amount has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the amount.";
                return View();
            }
        }
    }
}