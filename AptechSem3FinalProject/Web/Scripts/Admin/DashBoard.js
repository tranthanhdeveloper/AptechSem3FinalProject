/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
//-------------
//- REPORT CHART - MONTH
//-------------

var lineChartOptions = {
    //Boolean - If we should show the scale at all
    showScale: true,
    //Boolean - Whether grid lines are shown across the chart
    scaleShowGridLines: false,
    //String - Colour of the grid lines
    scaleGridLineColor: "rgba(0,0,0,.05)",
    //Number - Width of the grid lines
    scaleGridLineWidth: 1,
    //Boolean - Whether to show horizontal lines (except X axis)
    scaleShowHorizontalLines: true,
    //Boolean - Whether to show vertical lines (except Y axis)
    scaleShowVerticalLines: true,
    //Boolean - Whether the line is curved between points
    bezierCurve: true,
    //Number - Tension of the bezier curve between points
    bezierCurveTension: 0.3,
    //Boolean - Whether to show a dot for each point
    pointDot: true,
    //Number - Radius of each point dot in pixels
    pointDotRadius: 4,
    //Number - Pixel width of point dot stroke
    pointDotStrokeWidth: 1,
    //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    pointHitDetectionRadius: 20,
    //Boolean - Whether to show a stroke for datasets
    datasetStroke: true,
    //Number - Pixel width of dataset stroke
    datasetStrokeWidth: 2,
    //Boolean - Whether to fill the dataset with a color
    datasetFill: true,
    //String - A legend template
    legendTemplate: "<ul class=\"chart-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].pointColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
    //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    maintainAspectRatio: true,
    //Boolean - whether to make the chart responsive to window resizing
    responsive: true,
    //multiTooltipTemplate: "<%= value %> triệu",
};

function viewMonthlyChartReport(canvas, data, unit) {
    canvas = canvas.get(0).getContext("2d");
    chart = new Chart(canvas);

    var chartData = setupLineChartData(data);
    lineChartOptions.multiTooltipTemplate = "<%= value %> " + unit;
    chart = chart.Line(chartData, lineChartOptions);

    viewChartLegend(chart, canvas, true);
}


var COLOR_HOPDONG = "0,166,90";
var COLOR_COC = "53,126,165";
function setupLineChartData(data)
{
    var datasets = [
        {
            label: "Course",
            fillColor: "rgba(" + COLOR_HOPDONG + ",0.2)",
            strokeColor: "rgba(" + COLOR_HOPDONG + ",1)",
            pointColor: "rgba(" + COLOR_HOPDONG + ",1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(" + COLOR_HOPDONG + ",1)",
            data: data.data_hopdong
        },
        {
            label: "Order",
            fillColor: "rgba(" + COLOR_COC + ",0.2)",
            strokeColor: "rgba(" + COLOR_COC + ",1)",
            pointColor: "rgba(" + COLOR_COC + ",1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(" + COLOR_COC + ",1)",
            data: data.data_coc
        }];
    if (data.data_datcho) {
        datasets.push({
            label: "Đặt chỗ",
            fillColor: "rgba(" + COLOR_DATCHO + ",0.2)",
            strokeColor: "rgba(" + COLOR_DATCHO + ",1)",
            pointColor: "rgba(" + COLOR_DATCHO + ",1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(" + COLOR_DATCHO + ",1)",
            data: data.data_datcho
        });
    }
    return chartData = {
        labels: data.labels,
        datasets: datasets
    };
}

function getChartCurrentMonthInit(distance, month, year) {
    var myvalues = [];
    for (var i = 1; i <= distance - 1; i++) {
        myvalues.push(i + "/" + month + "/" + year);
    }

    return myvalues;
}

function getChartValuesInit(num) {
    var myvalues = [];
    for (var i = 1; i <= num; i++) {
        myvalues.push(0);
    }
    return myvalues;
}
function daysInMonth(month, year) {
    return new Date(year, month, 0).getDate();
}

function getChartCurrentMonthLabels(distance, month, year) {
    var myvalues = [];
    for (var i = 1; i <= distance - 1; i++) {
        if (i % 7 == 1) {
            myvalues.push(i + "/" + month + "/" + year);
        }
        else {
            myvalues.push(i + "/" + month + "/" + year);
            //myvalues.push("");
        }
    }
    myvalues.push(distance + "/" + month + "/" + year);

    return myvalues;
}

$(".reportChart").each(function () {
    var revenueChartReport = $(this).find(".salesChart");
    var countChartReport = $(this).find(".dealsChart");

    var id = $(this).attr("data-id");
    var time = $(this).attr("data-time");
    var type = $(this).attr("data-type");
    var home_month = $(this).attr("data-month");

    var today = new Date();
    //var today = parseDateFromString("1/3/2016");//for test
    var year = today.getFullYear();
    var month = today.getMonth() + 1;
    if (home_month > -1)
        month = home_month;
    //alert(month);//3
    //var month = today.getMonth();// + 1;for demo
    var distance = daysInMonth(month, year);

    var Dates = getChartCurrentMonthInit(distance, month, year);
    var labels = getChartCurrentMonthLabels(distance, month, year);
    var count_Course = getChartValuesInit(distance);
    var count_Order = getChartValuesInit(distance);
    var total_Course = 0;
    var total_Order = 0;


    $.ajax({
        type: "POST",
        url: "/Admin/DashBoard/ChartReport",
        content: "application/json; charset=utf-8",
        dataType: 'json',
        //data: {
        //    user_id: id,

        //    time: time,
        //    year: year,
        //    month: month
        //},
        success: function (data) {

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var Date = data[i].Date;
                    var index = Dates.indexOf(Date);

                    count_Course[index] = data[i].CountCourse;
                    total_Course += count_Course[index];
                    count_Order[index] = data[i].CountOrder;
                    total_Order += count_Order[index];
                    //count_Booking[index] = data[i].CountBooking;
                    //total_Booking += count_Booking[index];
                    //count_SubscribeHouse[index] = data[i].CountSubscribeHouse;
                    //total_SubscribeHouse += count_SubscribeHouse[index];
                }
            }

            viewMonthlyChartReport(
                revenueChartReport,
                {
                    labels: labels,
                    data_hopdong: count_Course,
                    data_coc: count_Order,
                    data_datcho: null
                }, "New");
            //viewMonthlyChartReport(
            //    countChartReport,
            //    {
            //        labels: labels,
            //        data_hopdong: count_Booking,
            //        data_coc: count_SubscribeHouse,
            //        data_datcho: null
            //    }, "giao dịch");
            $(".total_House").html(total_Course);
            $(".total_Subscribe").html(total_Order);
            //$(".total_Booking").html(total_Booking);
            //$(".total_SubscribeHouse").html(total_SubscribeHouse);
            $(".start_date").html(labels[0]);
            $(".end_date").html(labels[labels.length - 1]);
           

        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            //alert(xhr.responseText);
            //alert('Có lỗi xảy ra, vui lòng thực hiện lại. Xin cảm ơn!');
        }
    });
});