///////////////////////////////////////////////////
var postAddLink = $("#postAddLink").val();
var postEditLink = $("#postEditLink").val();
var postDeleteLink = $("#postDeleteLink").val();
var isUploadImage = $("#postIsUploadImage").val();
var postDeleteImageLink = $("#postDeleteImageLink").val();

function addDataForm(frm, api)//this.form
{
    if (typeof checkDataValidation == 'function') {
        if (checkDataValidation(frm) == false)
            return;
    }

    var form_api = $(frm).attr("data-api");
    if (!form_api) {
        form_api = $(frm).attr("action");
    }
    if (form_api) {
        api = form_api;
    }
    if (!api) {
        console.log("form action null");
        return;
    }
    
    var form_isUploadImage = $(frm).attr("data-image");
    if (form_isUploadImage) {
        isUploadImage = form_isUploadImage;
    }

    var frmData = processFormData(frm);
    //if (isUploadImage == "true") {
        var frmJson = convertJSONToFormData(frmData);
        SendRequestToServer(frm, frmJson, api);
    window.location.reload();
    //}
    //else {
    //    var redirect_url = $(frm).attr("data-redirect");
    //    ActionForm(frmData, api, redirect_url);
    //}
}

function processFormData(form) {
    ////////////////////////////////////////////////////////////////////////////
    //pre-processing
    $(form).find("input[type='text']").each(function () {
        var val = $(this).val();
        if (val) {
            val = val.removeNonASCII();
            $(this).val(val);
        }
        else
            console.log(val);
    });
    $(form).find(".textbox-price").each(function () {
        var val = $(this).val();
        val = val.replaceAll(',', '').replaceAll('.', '');
        $(this).val(val);
    });
    $(form).find(".textbox-float").each(function () {
        var val = $(this).val();
        val = val.replaceAll('.', ',');
        $(this).val(val);
    });

    ////////////////////////////////////////////////////////////////////////////
    //process data - form.serializeArray
    var fd = $(form).serializeArray();
    //images
    for (var i in file_images) {
        fd.push({
            name: file_images[i].name,
            value: file_images[i].value
        });
    }
    //CKEditor
    var textarea = $(form).find("textarea");
    for (var i = 0; i < textarea.length; i++) {
        var id = textarea.eq(i).attr("id");
        var name = textarea.eq(i).attr("data-name");
        var isEditor = textarea.eq(i).attr("data-editor");
        if (isEditor == "true") {
            var value = CKEDITOR.instances[id].getData();
            fd.push({
                name: name,
                value: value
            });
        }
    }

    return fd;
}

function convertJSONToFormData(json) {
    var fd = new FormData();
    for (var i in json) {
        fd.append(json[i].name, json[i].value);
    }

    return fd;
}

$("form.form-submit-data").submit(function (e) {
    addDataForm(this, postAddLink);
});

function HideModal() {
    $(".modal").modal("hide");
    $('body').removeClass('modal-open');
    $('body').css("padding", "0px");
    $('.modal-backdrop').remove();
}


$('body').on("click", ".btnAdd", function (e) {
    if (typeof btnAdd_PreProcessing == 'function') {
        btnAdd_PreProcessing(this);
    }

    e.preventDefault();
    //HideModal();

    var $frm = $(this.form);
    var api = postAddLink;
    var form_api = $frm.find("#postAddLink").val();
    if (form_api) {
        api = form_api;
    }
    addDataForm(this.form, api);
});

$('body').on("click", ".btnEdit", function (e) {
    if (typeof btnEdit_PreProcessing == 'function') {
        btnEdit_PreProcessing(this);
    }

    e.preventDefault();
    //HideModal();

    var $frm = $(this.form);
    var api = postEditLink;
    var form_api = $frm.find("#postEditLink").val();
    if (form_api) {
        api = form_api;
    }
    addDataForm(this.form, api);
})

/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
//SendRequestToServer
function SendRequestToServer(form, fd, url) {


    function uploadComplete(evt) {
        $(form).find(".form-horizontal #uploadProgress").css("width", '100%');

        console.log("Upload photos completed!");

        //var d = jQuery.parseJSON(evt.srcElement.response);
        var srcElement = evt.target || evt.srcElement;

        var d = jQuery.parseJSON(srcElement.response);

        ProcessAjaxSuccessAndReloadData(d);

        $(form).removeClass("loading");
    }

    function uploadFailed(evt) {
        alert("There was an error attempting to upload the file.");
        $(form).removeClass("loading");

        //console.log("There was an error attempting to upload the file.");
    }

    //loading
    $(form).addClass("loading");

    var xhr = new XMLHttpRequest();
    //xhr.upload.addEventListener("progress", uploadProgress, false);
    xhr.addEventListener("load", uploadComplete, false);
    xhr.addEventListener("error", uploadFailed, false);

    xhr.open("POST", url);
    xhr.send(fd);

}

