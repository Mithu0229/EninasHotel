using EninasHotel.Application.Common.Interfaces;
using EninasHotel.Application.Common.Utility;
using EninasHotel.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EninasHotel.Application.Common.DTO;
using EninasHotel.Application.Services.Implementation;
using EninasHotel.Application.Services.Interface;

namespace EninasHotel.Web.Controllers
{
    public class DashboardACMController : Controller
    {
        private readonly IDashboardACMService _dashboardService;

        public DashboardACMController(IDashboardACMService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            return Json(await _dashboardService.GetTotalCashInRadialChartData());
        }

        public async Task<IActionResult> GetRegisteredUserChartData()
        {
            return Json(await _dashboardService.GetRegisteredUserChartData());
        }

        public async Task<IActionResult> GetRevenueChartData()
        {
            return Json(await _dashboardService.GetRevenueChartData());
        }

        public async Task<IActionResult> GetBookingPieChartData()
        {
            return Json(await _dashboardService.GetBookingPieChartData());
        }

        public async Task<IActionResult> GetMemberAndBookingLineChartData()
        {
            return Json(await _dashboardService.GetMemberAndBookingLineChartData());

        }
    }
}
