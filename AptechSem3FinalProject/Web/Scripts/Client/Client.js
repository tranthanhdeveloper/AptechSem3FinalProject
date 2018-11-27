
var Course = (function () {

    return {
        filterCourse: function () {

        },
        sortCourse: function () {

        }
    }
})();


$(document).on('click', '#CourseSearchBtn', function (event) {
    event.preventDefault();
    var formElement = $('#CourseSearchForm');
    var formData = new FormData(formElement[0]);
    var postUrl = formElement.attr('action');
    $.ajax({
        url: postUrl,
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            $("#courseDisplayArea").html(result);
        },
        error: function () {
            alert('Has error orcurred');
        }
    });
});
