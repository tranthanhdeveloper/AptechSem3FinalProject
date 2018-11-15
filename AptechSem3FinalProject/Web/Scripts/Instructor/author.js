$(function () {
    'use strict'

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
        event.preventDefault();
        var formElement = $('#AddModuleLessonForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        var moduleBoxTbl = $('#module_'+formElement.find("input[name='ModuleId']").val()).find('table tbody');
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                moduleBoxTbl.append(result);
                formElement.trigger('reset');
                $('#addLessonModal').modal('hide');
                window.location.reload();
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
        event.preventDefault;
        var formElement = $('#AuthorCourseSearchForm');
        var formData = new FormData(formElement[0]);
        var postUrl = formElement.attr('action');
        var moduleBoxTbl = $('.course-display-area')[0];
        $.ajax({
            url: postUrl,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                moduleBoxTbl.html(result);
                location.reload();
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



});