
var galleryEditController = function () {
    var galleryId = $("#hidIdM").val();
    if (galleryId === undefined || galleryId === null || galleryId === "")
        galleryId = 0;
    this.initialize = function () {

        registerEvents();
        registerControls();

    };
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_upload_preview').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    function registerEvents() {
        //Init validation

        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },

            }
        });


        $("#btnCreate").on('click', function () {
            resetFormMaintainance();
        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            readURL(this);
        });
        $("#slFileType").on('change', function () {
            var fileType = $(this).val();
            if (fileType == 1) {
                $("#blockYoutube").removeAttr("style");
                $("#blockFile").css("display", "none");
            }
            else {
                $("#blockYoutube").css("display", "none");
                $("#blockFile").removeAttr("style");
            }


        });

        if (galleryId > 0) {
            loadDetails($("#hdFullImage").val());
            loadFileType($("#slFileType").val());
        }



        $('#btnSave').on('click', function (e) {
            saveSlide(e);
        });

    }

    function registerControls() {


    }

    function saveSlide(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var name = $('#txtNameM').val();
            var fileType = $("#slFileType").val();
            if (fileType == 1) {
                var gallery = {
                    Id: galleryId,
                    Name: name,
                    FileType: fileType,
                    FileUrl: $("#fileUrl").val()
                };
                saveChanges(gallery);
            }
            else {
                var fileUpload = $('#fileInputImage').get(0);
                var files = fileUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                var galleryFile = {
                    Id: galleryId,
                    Name: name,
                    FileType: fileType,
                    FileUrl: $('#hdImage').val()
                };
                if (files.length > 0) {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/Upload/UploadImage",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (path) {
                            galleryFile.FileUrl = path;
                            saveChanges(galleryFile);
                        },
                        error: function () {
                            system.notify('There was error uploading files!', 'error');
                        }
                    });
                }
                else {
                    saveChanges(galleryFile);
                }

            }

            return false;
        }
    }

    function saveChanges(e) {
        $.ajax({
            type: "POST",
            url: "/Admin/Gallery/SaveEntity",
            data: {
                Id: e.Id,
                Name: e.Name,
                FileUrl: e.FileUrl,
                FileType: e.FileType
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                system.notify('Update slide successful', 'success');
                slideId = response.Id;
                window.location.href = "/admin/gallery";
                system.stopLoading();
            },
            error: function () {
                system.notify('Has an error in save product progress', 'error');
                system.stopLoading();
            }
        });
    }


    function deleteProduct(id) {
        system.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Product/Delete",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Delete successful', 'success');
                    system.stopLoading();
                    loadData();
                },
                error: function (status) {
                    system.notify('Has an error in delete progress', 'error');
                    system.stopLoading();
                }
            });
        });
    }

    function loadDetails(that) {
        if (that !== undefined)
            $('#image_upload_preview').attr('src', that);
        else
            $('#image_upload_preview').attr('src', "");


    }

    function loadFileType(e) {
        if (e == 1) {
            $("#blockYoutube").removeAttr("style");
            $("#blockFile").css("display", "none");
        }
        else {
            $("#blockYoutube").css("display", "none");
            $("#blockFile").removeAttr("style");
        }
    }
    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');


        $('#txtDescM').val('');
        $('#txtUnitM').val('');



        $('#txtImage').val('');

        //CKEDITOR.instances.txtContentM.setData('');
        $('#ckStatusM').prop('checked', true);


    }

    function loadCategories() {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllCategories',
            dataType: 'json',
            success: function (response) {
                var render = "<option value=''>--Select category--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlCategorySearch').html(render);
            },
            error: function (status) {
                console.log(status);
                system.notify('Cannot loading product category data', 'error');
            }
        });
    }



}