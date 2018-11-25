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
    $('.tree li:has(ul)').addClass('parent_li1').find(' > span').attr('title', 'Collapse this branch');
    $('.tree li.parent_li1').each(function (e) {
        var children = $(this).find(' > ul > li');
        children.hide();
        $(this).find(" > span").attr('title', 'Click để mở').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
    });
}
//$.when(build_tree()).done(function () {
//        //alert("done");
//        $(".tree").removeClass("loading");
//});

$("body").on("click", ".tree li.parent_li1 > span", function (e) {
    var $this = $(this);
    var $parent_li = $this.parent('li.parent_li1');
    var children = $parent_li.find(' > ul > li');
    if (children.length > 0) {
        if (children.is(":visible")) {
            children.hide('fast');
            $this.attr('title', 'Click để mở').find(' > i').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
        } else {
            children.show('fast');
            $this.attr('title', 'Click để thu gọn').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
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
                
                $this.attr('title', 'Click để thu gọn').find(' > i').addClass('fa-minus-square-o').removeClass('fa-plus-square-o');
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
