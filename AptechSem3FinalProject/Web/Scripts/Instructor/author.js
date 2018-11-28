
$(function () {
    'use strict';

    // display when no image load
    $('img').on("error", function () {
        $(this).attr('src', 'http://www.wellesleysocietyofartists.org/wp-content/uploads/2015/11/image-not-found.jpg');
    });

    $(document).on('click', '#coursePublish', function () {
        var mainCreateCourseForm = $('#creationCourseForm');
        mainCreateCourseForm.submit();
    });

    $(document).on('click', '#courseReset', function () {
        var mainCreateCourseForm = $('#creationCourseForm');
        mainCreateCourseForm.trigger("reset");
    });

    $(document).on('click', "#AddModuleLessonForm input[name='IsPreview']", function () {
        if ($(this).prop('checked')) {
            $(this).attr('value', true);
        } else {
            $(this).attr('value', false);
        }
    });


    $(document).on('click', '#SaveChangeModuleLesson', function () {
        //event.preventDefault();        
        var formElement = $('#AddModuleLessonForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        var moduleBoxTbl = $('#module_' + formElement.find("input[name='ModuleId']").val()).find('table tbody');
        var taskId = TaskProcess.addTask(formElement.find("input[name='Title']").val());
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                TaskProcess.updateTask(formElement.find("input[name='Title']").val(), taskId);
                moduleBoxTbl.append(result);
                formElement.trigger('reset');
                $('#addLessonModal').modal('hide');
                formElement.find('video').attr('src', "");                
            },
            error: function () {
                alert('Has error orcurred');
            }
        });
    });

    $(document).on('click', '#btnCreateNewModule', function () {
        var formElement = $('#AddModuleForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                $('#courseDetailSection').append(result);
                formElement.trigger('reset');
                $('#add-module-modal').modal('hide');
                window.location.reload();
            },
            error: function (error) {
                console.log(error);
                alert('Has error orcurred');
            }
        });
    });
    
    $(document).on('click', '.add-lesson-modal', function () {
        var moduleTitle = $(this).parents('.box-info').find('.box-header h3').text();
        var moduleId = $(this).data('module-id');
        var addLessonForm = $('#AddModuleLessonForm');
        var addLessonFormTitle = $('.add-lesson-modal-title').text("Add new lesson for "+ moduleTitle);
        addLessonForm.find("input[name='ModuleId']").val(moduleId);
        var addLessonModal = $('#addLessonModal');
        addLessonModal.modal({ backdrop: 'static', keyboard: false }).modal('show');
    });

    $(document).on('click', '#authorCourseSearchSubmit', function (event) {
        event.preventDefault();
        var formElement = $('#AuthorCourseSearchForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        var moduleBoxTbl = $("#createdCourseArea .course-item-display");
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                moduleBoxTbl.html(result);
            },
            error: function () {
                alert('Has error orcurred');
            }
        });
    });

    $(document).on("change", '.video-input', function (event) {
        var previewEle = $('.video-preview');
        var videoEl = previewEle.find("video");
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return;
        videoEl[0].src = URL.createObjectURL(this.files[0]);               
        previewEle.css({ "display": "block" });
    });

    $(document).on("click", '.edit-module-title', function (event) {
        event.preventDefault();
        $(this).css({ 'color': 'green' });
        var updateModal = $('#updateModule');
        updateModal.find('input').val($(this).data('title'));
        updateModal.find("form >input[name='Id']").val($(this).data('id'));
        updateModal.modal({ backdrop: 'static', keyboard: false }).modal('show');

    });

    $(document).on("click", '#SaveUpdateModule', function (event) {
        event.preventDefault();
        var formElement = $('#updateModuleForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                var parsedJson = JSON.parse(result);
                $('#module_' + parsedJson.Id).find('h3.box-title:first').html(parsedJson.Name);
                formElement.trigger('reset');
                $('#updateModule').modal('hide');
            },
            error: function (error) {
                console.log(error);
                alert('Has error orcurred when trying to update module name');
            }
        });
    });

    $(document).on("click", ".update-lesson", function (event) {
        event.preventDefault();
        var updateModal = $('#updateLesson');
        $.ajax({
            url: UPDATE_LESSON_URL+"/"+$(this).data("id"),
            type: 'GET',
            processData: false,
            contentType: false,
            success: function (result) {
                $('#updateLesson .modal-body').html(result);
                updateModal.modal({ backdrop: 'static', keyboard: false }).modal('show');
            },
            error: function () {
                alert('Has error orcurred');
            }
        });      
    });

    $(document).on("click", ".delete-lesson", function (event) {
        event.preventDefault();
        var updateModal = $('#updateLesson');
        var lessonId = $(this).data("id");
        $.ajax({
            url: DELETE_LESSON_URL + "/" +lessonId,
            type: 'GET',
            processData: false,
            contentType: false,
            success: function (result) {
                $("tr[data-lesson-id="+lessonId+"]").remove();
            },
            error: function () {
                alert('Has error orcurred during try to delete lesson');
            }
        });
    });

    $(document).on('click', "#SaveLessonModule", function () {
        var lessonRowEl = $(this);
        var formElement = $('#UpdateLessonForm');
        var formData = new FormData(formElement[0]);
        var updatedRowId = $("tr[data-lesson-id=" + formElement.find("input[name=id]").val()+"]");
        var postUrl = formElement.attr('action');
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                console.log(result);
                updatedRowId.replaceWith(result);
                $('#updateLesson').modal('hide');
                formElement.trigger('reset');
            },
            error: function () {
                alert('Has error orcurred during update lesson information!');
            }
        });
    });

    $(document).on("click", "#hiddenAllTask", function () {
        TaskProcess.deleteAll();
    });
});

$(document).ready(function () {
    $(window).keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            return false;
        }
    });
});