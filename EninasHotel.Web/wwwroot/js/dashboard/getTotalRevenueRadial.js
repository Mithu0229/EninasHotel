$(document).ready(function () {
    loadRevenueRadialChart();
});

function loadRevenueRadialChart() {
    $(".chart-spinner").show();

    $.ajax({
        url: "/Dashboard/GetRevenueChartData",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            document.querySelector("#spanTotalRevenueCount").innerHTML = data.totalCount;

            var sectionCurrentCount = document.createElement("span");
            if (data.hasRatioIncreased) {
                sectionCurrentCount.className = "text-success me-1";
                sectionCurrentCount.innerHTML = '<i class="bi bi-arrow-up-right-circle me-1"></i> <span> ' + data.countInCurrentMonth + '</span>';
            }
            else {
                sectionCurrentCount.className = "text-danger me-1";
                sectionCurrentCount.innerHTML = '<i class="bi bi-arrow-down-right-circle me-1"></i> <span> ' + data.countInCurrentMonth + '</span>';
            }

            document.querySelector("#sectionRevenueCount").append(sectionCurrentCount);
            document.querySelector("#sectionRevenueCount").append("since last month");

            loadRadialBarChart22("totalRevenueRadialChart", data);

            $(".chart-spinner").hide();
        }
    });
}


function loadRadialBarChart22(id, data) {
    var ids = "#" + id
    var chartColors = getChartColorsArray22(id);
    var options = {
        fill: {
            colors: chartColors
        },
        chart: {
            height: 90,
            width: 90,
            type: "radialBar",
            sparkline: {
                enabled: true
            },
            offsetY: -10,
        },
        series: data.series,
        plotOptions: {
            radialBar: {
                dataLabels: {
                    value: {
                        offsetY: -10,
                        color: chartColors[0],
                    }
                }
            }
        },
        labels: [""]
    };
    var chart = new ApexCharts(document.querySelector(ids), options);
    chart.render();

}

function getChartColorsArray22(id) {
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
