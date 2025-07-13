using EninasHotel.Application.Common.Utility;
using EninasHotel.Application.Services.Interface;
using EninasHotel.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace EninasHotel.Web.Controllers
{
    [Authorize(Roles = SD.Role_User)]
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

        public async Task<IActionResult> SearchIndex(DateTime? fromDate, DateTime? toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var amounts = _amountService.GetQueryAmounts();

            if (fromDate.HasValue)
            {
                amounts = amounts.Where(a => a.TransactionDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                amounts = amounts.Where(a => a.TransactionDate <= toDate.Value);
            }

            var result = await amounts.OrderByDescending(a => a.TransactionDate).ToListAsync();

            var totalCashIn = await amounts.SumAsync(a => (decimal?)a.CashIn) ?? 0;
            var totalCashOut = await amounts.SumAsync(a => (decimal?)a.CashOut) ?? 0;
            var totalCharge = await amounts.SumAsync(a => (decimal?)a.Charge) ?? 0;
            var totalCommission = await amounts.SumAsync(a => (decimal?)a.Commission) ?? 0;

            ViewBag.TotalCashIn = totalCashIn;
            ViewBag.TotalCashOut = totalCashOut;
            ViewBag.TotalCharge = totalCharge;
            ViewBag.TotalCommission = totalCommission;

            return View(result);
        }

        public async Task<IActionResult> PrintList(DateTime? fromDate, DateTime? toDate)
        {
            var amounts = _amountService.GetQueryAmounts();

            if (fromDate.HasValue)
            {
                amounts = amounts.Where(a => a.TransactionDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                amounts = amounts.Where(a => a.TransactionDate <= toDate.Value);
            }

            var amountsList = await amounts.OrderByDescending(a => a.TransactionDate).ToListAsync();

            var totalCashIn = await amounts.SumAsync(a => a.CashIn);
            var totalCashOut = await amounts.SumAsync(a => a.CashOut);
            var totalCharge = await amounts.SumAsync(a => a.Charge);
            var totalCommission = await amounts.SumAsync(a => a.Commission);
            var viewModel = new AmountListViewModel
            {
                Amounts = amountsList,
                TotalCashIn = totalCashIn,
                TotalCashOut = totalCashOut,
                TotalCharge = totalCharge,
                TotalCommission = totalCommission,
                FromDate = fromDate,
                ToDate = toDate

            };

            return new ViewAsPdf("PrintList", viewModel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                FileName = "TransactionList.pdf",
            };
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
            if (obj != null && (obj.CashIn > 0 || obj.CashOut > 0))
            {
                if (ModelState.IsValid)
                {
                    _amountService.CreateAmount(obj);
                    TempData["success"] = "The amount has been created successfully.";
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