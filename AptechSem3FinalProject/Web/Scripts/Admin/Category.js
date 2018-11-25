/////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
/////Tree Actions
//$('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
//$('.tree li.parent_li').each(function (e) {
//    var children = $(this).find(' > ul > li');
//    children.hide();
//    $(this).find(" > span").attr('title', 'Click để mở').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
//}).done(function (n) {
//    alert("done");
//});
//$('.tree li.parent_li').promise().done(function () {
//    $("p").append(" Finished! ");
//});

function build_tree()
{
    $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
    $('.tree li.parent_li').each(function (e) {
        var children = $(this).find(' > ul > li');
        children.hide();
        $(this).find(" > span").attr('title', 'Click to open').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
    });
}
//$.when(build_tree()).done(function () {
//        //alert("done");
//        $(".tree").removeClass("loading");
//});

$("body").on("click", ".tree li.parent_li > span", function (e) {
    var $this = $(this);
    var $parent_li = $this.parent('li.parent_li');
    var children = $parent_li.find(' > ul > li');
    if (children.length > 0) {
        if (children.is(":visible")) {
            children.hide('fast');
            $this.attr('title', 'Click to open').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
        } else {
            children.show('fast');
            $this.attr('title', 'Click to close').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
        }
    }
    else {
        var id = $(this).attr("data-id");
        //load children
        $.ajax({
            type: "GET",
            url: "/Admin/Category/GetCourse",
            contentType: "application/json; charset=utf-8",
            data: {
                id: id
            },
            success: function (d) {
                $parent_li.append(d);
                
                $this.attr('title', 'Click to close').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                ProcessAjaxError(xhr);
            }
        });
    }
    e.stopPropagation();
});


function build_tree1()
{
    $('.tree li:has(ul)').addClass('parent_li_course').find(' > span').attr('title', 'Collapse this branch');
    $('.tree li.parent_li_course').each(function (e) {
        var children = $(this).find(' > ul > li');
        children.hide();
        $(this).find(" > span").attr('title', 'Click to open').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
    });
}
//$.when(build_tree()).done(function () {
//        //alert("done");
//        $(".tree").removeClass("loading");
//});

$("body").on("click", ".tree li.parent_li_course > span", function (e) {
    var $this = $(this);
    var $parent_li = $this.parent('li.parent_li_course');
    var children = $parent_li.find(' > ul > li');
    if (children.length > 0) {
        if (children.is(":visible")) {
            children.hide('fast');
            $this.attr('title', 'Click to open').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
        } else {
            children.show('fast');
            $this.attr('title', 'Click to close').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
        }
    }
    else {
        var id = $(this).attr("data-id");
        //load children
        $.ajax({
            type: "GET",
            url: "/Admin/Category/GetLecture",
            contentType: "application/json; charset=utf-8",
            data: {
                id: id
            },
            success: function (d) {
                $parent_li.append(d);
                
                $this.attr('title', 'Click to open').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
            },
            error: function (xhr, textStatus, errorThrown) {
                // TODO: Show error
                console.log(xhr.responseText);
                ProcessAjaxError(xhr);
            }
        });
    }
    e.stopPropagation();
});



$("body").on("click", ".btn-edit-category", function (e) {
    console.log("btn-edit-category");

    e.preventDefault();
    var $modal = $("#myModaleditCategory");
    var $this = $(this);
    var id = $this.attr("data-id");//deal id
    var api = $this.attr("data-api");
    //if (!api) {
    //    api = "/Admin/GetDealEditForm";
    //    if (id && id > 0) {
    //        $modal = $("#myModaledit");
    //    }
    //}
    $modal.modal("show");

    var $container = $modal.find(".modal-body");
    $container.addClass("loading");

    $.ajax({
        type: "GET",
        url: api,
        contentType: "application/json; charset=utf-8",
        data: {
            id: id
        },
        success: function (d) {
            $container.html(d);
            $container.removeClass("loading");
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            ProcessAjaxError(xhr);
        }
    });
});


$("body").on("click", ".btn-edit-course", function (e) {

    e.preventDefault();
    var $modal = $("#myModaleditCourse");
    var $this = $(this);
    var id = $this.attr("data-id");//deal id
    var api = $this.attr("data-api");
    //if (!api) {
    //    api = "/Admin/GetDealEditForm";
    //    if (id && id > 0) {
    //        $modal = $("#myModaledit");
    //    }
    //}
    $modal.modal("show");

    var $container = $modal.find(".modal-body");
    $container.addClass("loading");

    $.ajax({
        type: "GET",
        url: api,
        contentType: "application/json; charset=utf-8",
        data: {
            id: id
        },
        success: function (d) {
            $container.html(d);
            $container.removeClass("loading");
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            ProcessAjaxError(xhr);
        }
    });
});



$("body").on("click", ".btn-edit-lecture", function (e) {
    console.log("btn-edit-category");

    e.preventDefault();
    var $modal = $("#myModaleditLecture");
    var $this = $(this);
    var id = $this.attr("data-id");//deal id
    var api = $this.attr("data-api");
    //if (!api) {
    //    api = "/Admin/GetDealEditForm";
    //    if (id && id > 0) {
    //        $modal = $("#myModaledit");
    //    }
    //}
    $modal.modal("show");

    var $container = $modal.find(".modal-body");
    $container.addClass("loading");

    $.ajax({
        type: "GET",
        url: api,
        contentType: "application/json; charset=utf-8",
        data: {
            id: id
        },
        success: function (d) {
            $container.html(d);
            $container.removeClass("loading");
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            ProcessAjaxError(xhr);
        }
    });
});