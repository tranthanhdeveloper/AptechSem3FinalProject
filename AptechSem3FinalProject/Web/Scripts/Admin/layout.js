
var file_images = [];
var file_images_id = 0;

//General
String.prototype.removeMVCPath = function (n) {
    var str = this;
    if (str.indexOf("~/") == 0) {
        str = str.substr(1);
    }

    return str;
};

String.prototype.replaceAll = function (find, replace) {
    var str = this;
    return str.replace(new RegExp(find.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&'), 'g'), replace);
};

String.prototype.toStringDecimal = function () {
    var str = this;
    return str.replaceAll(',', '.');
};

String.prototype.toDecimal = function () {
    var str = this;
    return parseFloat(str.toStringDecimal());
};

String.prototype.toNumberFromPrice = function () {
    var str = this;
    if (str) {
        str = str.replaceAll(',', '').replaceAll('.', '');
        return parseInt(str);
    }
    return 0;
};

String.prototype.formatMoney = function () {
    var n = parseInt(this.replace(/\D/g, ''), 10);
    if (n) {
        return n.formatMoney();
    } 

    return 0;
};

String.prototype.removeNonASCII = function () {
    var str = this;
    if ((str === null) || (str === ''))
        return false;
    else
        str = str.toString();

    return str.replace(/[\x00-\x1F\x7F-\x9F]/g, "");
};

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = c == undefined ? 0 : (isNaN(c = Math.abs(c)) ? 2 : c),
        d = d == undefined ? "," : d,
        t = t == undefined ? "." : t,
        s = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

Date.prototype.ddmmyyyy = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy; // padding
};

Date.prototype.mmyyyy = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    return (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy; // padding
};

// a and b are javascript Date objects
var _MS_PER_DAY = 1000 * 60 * 60 * 24;
function dateDiffInDays(a, b) {
    // Discard the time and time-zone information.
    var utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    var utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

    return Math.floor((utc2 - utc1) / _MS_PER_DAY);
}

function parseDateFromString(str)
{
    var parts = str.split("/");
    return new Date(parseInt(parts[2], 10),
                      parseInt(parts[1], 10) - 1,
                      parseInt(parts[0], 10));
}

function daysInMonth(month, year) {
    return new Date(year, month, 0).getDate();
}

function isScrolledIntoView(elem) {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height();

    var elemTop = $(elem).offset().top;
    var elemBottom = elemTop + $(elem).height();

    return ((elemBottom >= docViewTop) && (elemTop <= docViewBottom)
      && (elemBottom <= docViewBottom) && (elemTop >= docViewTop));
}

// Check định dạng Email
function xfomatEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\ ".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function CheckAuthentication() {
    var isAuthenticated = $("#isAuthenticated").val();
    if (isAuthenticated == 0 || isAuthenticated == false || isAuthenticated == "false") {
        return false;
    }
    return true;
}

/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
///reponsive

var X_SMARTPHONE_WIDTH = 480;
var SMARTPHONE_WIDTH = 768;
var LAPTOP_WIDTH = 992;
var window_width = $(window).width();

function is_xs() {
    return window_width < X_SMARTPHONE_WIDTH;
}

function is_sm() {
    return window_width < SMARTPHONE_WIDTH;
}

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//show city name in url

//google translate - not use anymore 
//$('input:text').each(function (index) {

//    //Call the Google API
//    $.ajax({
//        type: "GET",
//        url: "https://ajax.googleapis.com/ajax/services/language/translate",
//        dataType: 'jsonp',
//        cache: false,
//        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
//        data: "v=1.0&q=" + $(this).val() + "&langpair=en|vi",
//        success: function (iData) {
//            //update the value 
//            if (iData && iData["responseData"]) {
//                $(this).val(iData["responseData"]["translatedText"]);
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) { }
//    });
//});

$("body").on("mouseup", ".auto-select", function () {
    $(this).select();
});

/////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
$('.btn-copy-translation').click(function () {
    var label = $(this).prev();
    var input = $(this).nextAll('input').first();
    if (input.length == 0)
        input = $(this).nextAll('textarea').first();
    input.val(label.text().replaceAll("<br/>", "\r\n"));
});
$('.btn-original').click(function () {
    var label = $(this).prev();
    var input = $(this).next();
    input.val(label.text());
});

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//admin side menu action

var active_action = $("#active_action").val();
$(".action-menu").each(function () {
    var action = $(this).attr("data-action");
    if (active_action == action)
    {
        $(this).addClass("active");
        $(this).parents(".treeview").addClass("active");
        return false;
    }
});

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//Layout Elements Initiation

function PluginLoadInputMaskAndMonthPicker() {
    if ($("input[data-mask]").length > 0) {
        $("input[data-mask]").inputmask();
    }
    //datepicker
    if ($(".monthpicker").length > 0) {
        $(".monthpicker").datepicker({
            format: "mm/yyyy",
            minViewMode: 1,
            autoclose: true
        }).on("changeDate", function (e) {
            var date = $(this).datepicker("getDate");
            var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
            var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            $(this).attr("data-start-date", firstDay.ddmmyyyy());
            $(this).attr("data-end-date", lastDay.ddmmyyyy());
        });
    }

    if ($(".monthdatepicker").length > 0) {
        $(".monthdatepicker").datepicker({
            format: "dd/mm/yyyy",
            minViewMode: 0,
            autoclose: true
        }).on("changeDate", function (e) {
            var date = $(this).datepicker("getDate");
            var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
            var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            $(this).attr("data-start-date", firstDay.ddmmyyyy());
            $(this).attr("data-end-date", lastDay.ddmmyyyy());
        });
    }

    $(".textbox-price").on("keyup", function (e) {
        //var val = parseInt($(this).val());
        var n = parseInt($(this).val().replace(/\D/g, ''), 10);
        if (n) {
            $(this).val(n.formatMoney());
        }
        //$(this).val(val.formatMoney());
    });

   

    $('.js-make-raty').raty({
        numberMax: 5,
        number: 5,
        path: '/images/',
        half: false,
        score: function () {
            return $(this).attr('data-score');
        },
        scoreName: function () {
            return $(this).attr('data-score-name');
        },
    })
}

function PluginLoad() {
    console.log("PluginLoad");
    $("select.select2").select2();
    PluginLoadInputMaskAndMonthPicker();
}

PluginLoad();

//INITIALIZE SPARKLINE CHARTS
function LoadUserPerfomanceChart() {
    $(".sparkline").each(function () {
        var $this = $(this);
        var user_id = $this.attr("data-id");
        $.ajax({
            url: "/Admin/GetDailyDeal",
            type: 'POST',
            content: "application/json;charset=utf-8",
            dataType: 'json',
            data: {
                user_id: user_id
            },
            success: function (data) {
                var distance = 7;
                var today = new Date();
                var myvalues = getChartValuesInit(distance);
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var Deal_Date = data[i].Deal_Date;
                        var date = parseDateFromString(Deal_Date);

                        var diff = dateDiffInDays(date, today);
                        var index = Math.floor(diff / distance);
                        if (index < distance) {
                            myvalues[index]++;
                        }
                    }
                }

                myvalues = myvalues.reverse();
                $this.sparkline(myvalues, $this.data());
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
            }
        });
    });
}
LoadUserPerfomanceChart();

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//Chart Report

var REPORT_UNIT = 1000000;
var COLOR_HOPDONG = "0,166,90";
var COLOR_COC = "53,126,165";
var COLOR_DATCHO = "243,156,18";

function getChartValuesInit(num) {
    var myvalues = [];
    for (var i = 1; i <= num; i++) {
        myvalues.push(0);
    }
    return myvalues;
}

function getChartDatesInit(distance, year, prefix) {
    var myvalues = [];
    for (var i = 1; i <= distance; i++) {
        myvalues.push(prefix + i + "/" + year);
    }
    return myvalues;
}

function viewChartLegend(chart, canvas, isPie) {
    //legend
    if (isPie) {
        $(canvas.canvas).parents(".labeled-chart-container").find(".legend-container .chart-legend").remove();
    }
    else {
        $(canvas.canvas).parent().find(".chart-legend").remove();
    }
    helpers = Chart.helpers;
    var legendHolder = document.createElement('div');
    legendHolder.innerHTML = chart.generateLegend();
    // Include a html legend template after the module doughnut itself
    //if (withHover) {
    //    helpers.each(legendHolder.firstChild.childNodes, function (legendNode, index) {
    //        helpers.addEvent(legendNode, 'mouseover', function () {
    //            var activeSegment = chart.segments[index];
    //            activeSegment.save();
    //            activeSegment.fillColor = activeSegment.highlightColor;
    //            chart.showTooltip([activeSegment]);
    //            activeSegment.restore();
    //        });
    //    });
    //    helpers.addEvent(legendHolder.firstChild, 'mouseout', function () {
    //        chart.draw();
    //    });
    //}

    if (isPie) {
        $(canvas.canvas).parents(".labeled-chart-container").find(".legend-container").html(legendHolder.firstChild);
    }
    else {
        canvas.canvas.parentNode.appendChild(legendHolder.firstChild);
    }
}

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

function setupBarChartData(labels, data_hopdong, data_coc)
{
    return barChartData = {
        labels: labels,
        datasets: [
          {
              label: "Hợp đồng",
              fillColor: "rgba(" + COLOR_HOPDONG + ",1)",
              strokeColor: "rgba(" + COLOR_HOPDONG + ", 1)",
              pointColor: "rgba(" + COLOR_HOPDONG + ", 1)",
              pointStrokeColor: "#c1c7d1",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgba(" + COLOR_HOPDONG + ",1)",
              data: data_hopdong
          },
          {
              label: "Cọc",
              fillColor: "rgba(" + COLOR_COC + ",0.9)",
              strokeColor: "rgba(" + COLOR_COC + ",0.8)",
              pointColor: "#3b8bba",
              pointStrokeColor: "rgba(" + COLOR_COC + ",1)",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgba(" + COLOR_COC + ",1)",
              data: data_coc
          }
        ]
    };
}


/////////////////////////////////////////////////////////////////////////////////////////////////
///lazyload
$(window).scroll(function () {
    $("img.lazyload").lazy({
        //placeholder: "../Images/spinner.gif",
        //effect: "fadeIn",
        //effectspeed: 900
    });
});



$('body').on('click', '*[data-target="#myModal"]', function () {
    //if ($(this).data("content") != "") {
    var content = $(this).data("content");
    $("#myModal .modal-title").html(content);
    //}
});

function myFunction() {
    var x = document.getElementById("myTopnav");
    if (x.className === "header head topnav") {
        x.className += " responsive";
        document.getElementById("myTopnav").style.minHeight = "280px";
        document.getElementById("form-header").style.display = "block";
        var dom = document.querySelectorAll("#Search_Result");
        dom[0].setAttribute('style', 'margin-bottom: 240px !important');
    } else {
        x.className = "header head topnav";
        document.getElementById("myTopnav").style.minHeight = "100px";
        document.getElementById("form-header").style.display = "none";
        var dom = document.querySelectorAll("#Search_Result");
        dom[0].setAttribute('style', 'margin-bottom: 100px !important');
    }
}

var flag = 0;
function myFunction() {
    if (flag == 0) {
        $('.head').css('height', '280px');
        $('#form-header').css('display', 'block');
        flag = 1;
    }
    else {
        $('.head').css('height', '94px');
        $('#form-header').css('display', 'none');
        flag = 0;
    }
}

function openNav() {
    document.getElementById("mHeader").style.width = "250px";
}

function closeNav() {
    document.getElementById("mHeader").style.width = "0px";
}
