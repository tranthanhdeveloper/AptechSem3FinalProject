

//////////////////////////////////////////////////////
/////////////////////////////////////////////////////
//Manage Action

$('body').on("click", ".btn-admin-action", function (e) {
    e.preventDefault();
    var action = $(this).attr("data-action");
    var id = $(this).attr("data-id");
    var current_id = $(this).attr("data-current-id");
    var data = current_id ? { id: id, current_id: current_id } : { id: id };
    var api_url = $(this).attr("data-api");

    var callback_func = null;
    if ($(this).closest(".can-delete").length > 0)
    {
        var li = $(this).closest(".can-delete");
        callback_func = function () {
            li.remove();
        };
    }

    if (typeof ReloadTableDataAfterSubmit == 'function') {
        callback_func = ReloadTableDataAfterSubmit;
    }

    AskAndActionData(action, data, api_url, null, callback_func);
});

function AskAndActionData(action, data, api_url, redirect_url) {
    BootstrapDialog.show({
        cssClass: 'delete-car-dialog',
        title: 'Danger',
        message: 'Do you want to ' + action + ' this information?',
        buttons: [{
                label: 'No',
                cssClass: 'btn-default',
                action: function (dialog) {
                    dialog.close();
                }
            },
            {
                label: 'Yes',
                cssClass: 'btn-danger',
                action: function (dialog) {
                    ActionData(data, api_url, redirect_url);
                    dialog.close();
                }
            }]
    }).setType(BootstrapDialog.TYPE_DANGER);
}


function ActionData(data, api_url, redirect_url, callback_func) {
    //with_popup = typeof with_popup !== 'undefined' ? with_popup : false;
    redirect_url = typeof redirect_url !== 'undefined' ? redirect_url : "";
    if (data && api_url) {
        $.ajax({
            type: "POST",
            url: api_url,
            content: "application/json;charset=utf-8",
            dataType: "json",
            data: data,
            success: function (d) {
                ProcessAjaxSuccess(d, redirect_url, callback_func);
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                ProcessAjaxError(xhr);
            }
        });
    }
}

function ProcessAjaxError(xhr) {
    if (xhr.responseJSON.message == "NotAuthorized") {
        location.href = xhr.responseJSON.LogOnUrl;
    }
    else {
        alert('Something went wrong');
    }
}
function ProcessAjaxSuccess(d, redirect_url, callback_func) {
    if (d.success == true) {
        if (d.redirect) {
            redirect_url = d.redirect;
        }
        if (d.message) {
            alert(d.message);
            console.log(d.message);
        }
        if (redirect_url) {
            window.location = redirect_url;
        }
        else {
            if (!callback_func)
                callback_func = ReloadTableDataAfterSubmit;
            if (typeof callback_func == 'function') {
                callback_func();
            }
            else {
                if (d.reload)
                    window.location.reload();
                else
                    alert(d.message);
            }
        }
    }
    else {
        if (d.message) {
            alert(d.message);
            console.log(d.message);
        }
        else {
            alert("Can't send request!");
        }
    }
}

function ReloadChildrenData($this, page, type)
{
    var parent = $this.parents(".ajax-search-group");
    var user_id = parent.attr("data-id");
    var url = parent.attr("data-url");

    var $container = parent.find(".Search_Result");
    $container.addClass("loading");

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: {
            user_id: user_id,
            page: page,
            type: type
        },
        success: function (d) {
            $container.html(d);
            $container.removeClass("loading");
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            alert(xhr.responseText);
            
        }
    });
}

//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
////////////////////////////// Profile Page///////////////////////////

$('body').on("click", ".ajax-search-group-paging .pagination-container .pagination > li > a", function (e) {
    e.preventDefault();
    var $this = $(this);

    ReloadChildrenData($this, $this.attr("href").replace('#', ''));
});

$("body").on("click", ".ajax-search-group-paging ul.dropdown-menu > li > a", function (e) {
    e.preventDefault();
    var $this = $(this);

    var type = $this.attr("data-type");
    var type_name = $this.html();
    $this.parents(".ajax-search-group").attr("data-type", type);
    $this.parents(".ajax-search-group").find(".dropdown-toggle").html(type_name + ' <span class="caret"></span>');

    ReloadChildrenData($this, 1, type);
});

//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
////////////////////////////// Ajax search///////////////////////////
var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$(".textboxSearch, .txtSearchBox").each(function () {
    $(this).data("reservedInfo", $(this).parents(".ajax-search-group").find(".Search_Result").html());
});

function doTextboxSearch($this, url, getData)
{
    var reservedInfo = $this.data("reservedInfo");
    delay(function () {
        var name = $this.val();
        var $container = $this.parents(".ajax-search-group").find(".Search_Result");
        if (name && name.length >= 1) {
            $container.addClass("loading");

            var data = getData();

            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json; charset=utf-8",
                data: data,
                success: function (d) {
                    $container.html(d);
                    $container.removeClass("loading");
                },
                error: function (xhr, textStatus, errorThrown) {
                    // TODO: Show error
                    console.log(xhr.responseText);
                    alert(xhr.responseText);
                    //alert('Có lỗi xảy ra, vui lòng thực hiện lại. Xin cảm ơn!');
                }
            });
        }
        else {
            if (!name) {
                $container.html(reservedInfo);
            }
        }

    }, 500);
}

$(".textboxSearch").keyup(function () {
    var $this = $(this);
    //var url = $this.closest("form").attr("action");
    var url = $this.closest(".search-form").attr("data-action");
    var id = $this.parents(".ajax-search-group").attr("data-id");
    
    doTextboxSearch($this, url, function () {
        return {
            name: $this.val(),
            id: id
        };
    });
});
$(".txtSearchBox").keyup(function () {
    var $this = $(this);
    var url = $this.closest(".search-form").attr("data-action");
    var model = $this.closest(".search-form").attr("data-model"); //$this.closest("form").data("model");

    doTextboxSearch($this, url, function () {
        return {
            name: $this.val(),
            model: model
        };
    });
});

//////////////////////////////////////////////////////
/////////////////////////////////////////////////////
//Show row detail

$("body").on("click", "table.dataTable tbody a.viewdetail", function (e) {
    e.preventDefault();
    var tr = $(this).parents("tr");
    var tbody = tr.parent();

    if (tr.hasClass("active")) {
        tr.removeClass("active");
    }
    else {
        tbody.removeClass("active");
        tr.addClass("active");
    }
});

//////////////////////////////////////////////////////
/////////////////////////////////////////////////////
//Filter Form

if ($("#filter-form").length > 0) {
    //reserve search result container
    var $container = $("#filter-form").parents(".ajax-search-group").find(".Search_Result");
    var $form = $("#filter-form");
    var url = $form.attr("action");

    function FilterForm(loading) {
        console.log("FilterForm");

        //loading = loading == undefined ? false : loading;

        var fd = new FormData();
        var values = $form.serializeArray();
        for (var i in values) {
            fd.append(values[i].name, values[i].value);
        }

        $container.addClass("loading");
        $.ajax({
            url: url,
            data: fd,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (d) {
                $container.html(d);
                $container.removeClass("loading");
                //PluginLoad();
                $form.find("input[name='pageChange']").val(0);

                //PagePluginLoad
                if (typeof PagePluginLoad == 'function') {
                    PagePluginLoad();
                }
                else {
                    if (typeof PluginLoad == 'function') {
                        PluginLoad();
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                //alert(xhr.responseText);
                alert('Something went wrong!');
            }
        });
    }

   
    //ajax actions
    $("#filter-form select").on("change", function (e) {
        e.preventDefault();

        var isAjax = $(this).attr("data-isajax");
        if (isAjax != "none")
            FilterForm();
    });

    $("#filter-form input[type='text']").keyup(function (e) {
        if (e.which == 13) return false;
        delay(function () {
            FilterForm();
        }, 500);
    });

    $("#filter-form input[type='text']").keypress(function (e) {
        if (e.which == 13) return false;
        delay(function () {
            FilterForm();
        }, 500);
    });


    $('body').on("click", ".ajax-search-group .pagination-container .pagination > li > a", function (e) {
        e.preventDefault();

        //var page = $(this).html();
        var page = $(this).attr("href").replace('#', '');

        $("#filter-form input[name='page']").val(page);
        $("#filter-form input[name='pageChange']").val(1);

        FilterForm();
    });

    function ReloadTableDataAfterSubmit() {
       
        HideModal();
        FilterForm();
       
    }
}
