﻿using EninasHotel.Application.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Application.Services.Interface
{
    public interface IDashboardService
    {
        Task<RadialBarChartDto> GetTotalBookingRadialChartData();
        Task<RadialBarChartDto> GetRegisteredUserChartData();
        Task<RadialBarChartDto> GetRevenueChartData();
        Task<PieChartDto> GetBookingPieChartData();
        Task<LineChartDto> GetMemberAndBookingLineChartData();
    }

    public interface IDashboardACMService
    {
        Task<RadialBarChartDto> GetTotalCashInRadialChartData();
        Task<RadialBarChartDto> GetCashOutChartData();
        Task<RadialBarChartDto> GetRevenueChartData();
        Task<PieChartDto> GetBookingPieChartData();
        Task<LineChartDto> GetMemberAndBookingLineChartData();
    }
}
