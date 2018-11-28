$('input[name="BirthDay"]').datepicker({
    showButtonPanel: true,
    changeYear: true,
    changeMonth: true,
    showOtherMonths: true,
    selectOtherMonths: true,
    yearRange: '1950:2000'
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
                if (typeof PluginLoad == 'function') {
                    PluginLoad();
                }
            
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
            console.log(xhr.responseText);
            //alert(xhr.responseText);
            alert('Something went wrong, do it again, thanks');
        }
    });
}


$('body').on("click", ".ajax-search-group .pagination-container .pagination > li > a", function (e) {
    e.preventDefault();

    //var page = $(this).html();
    var page = $(this).attr("href").replace('#', '');

    $("#filter-form input[name='page']").val(page);
    $("#filter-form input[name='pageChange']").val(1);

    FilterForm();
});