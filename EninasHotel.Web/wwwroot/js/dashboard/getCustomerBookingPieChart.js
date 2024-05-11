$(document).ready(function () {
    loadCustomerBookingPieChart();
});

function loadtest() {
    var options = {
        chart: {
            type: 'line'
        },
        series: [{
            name: 'sales',
            data: [30, 40, 35, 50, 49, 60, 70, 91, 125]
        }],
        xaxis: {
            categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999]
        }
    }

    var chart = new ApexCharts(document.querySelector("#chart"), options);

    chart.render();
}

function loadCustomerBookingPieChart() {
    $(".chart-spinner").show();

    $.ajax({
        url: "/Dashboard/GetBookingPieChartData",
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            loadPieChart("customerBookingsPieChart", data);

            $(".chart-spinner").hide();
        }
    });
}

function loadPieChart(id, data) {
    var chartColors = getChartColorsArray1(id);
    var options = {
        colors: chartColors,
        series: data.series,
        labels: data.labels,
        chart: {
            width: 380,
            type: 'pie',
        },
        stroke: {
            show: false
        },
        legend: {
            position: 'bottom',
            horizontalAlign: 'center',
            labels: {
                colors: "#fff",
                useSeriesColors: true
            },
        },
    };
    var chart = new ApexCharts(document.querySelector("#customerbookingspiechart"), options);
    chart.render();
}

function getChartColorsArray1(id) {
    debugger
    if (document.getElementById(id) !== null) {
        var colors = document.getElementById(id).getAttribute("data-colors");
        if (colors) {
            colors = JSON.parse(colors);
            return colors.map(function (value) {
                var newValue = value.replace(" ", "");
                if (newValue.indexOf(",") === -1) {
                    var color = getComputedStyle(document.documentElement).getPropertyValue(newValue);
                    if (color) return color;
                    else return newValue;
                }
            });
        }

    }
}