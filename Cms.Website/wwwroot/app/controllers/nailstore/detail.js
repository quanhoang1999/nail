var NailStoreEditController = function () {

    this.initialize = function () {
        registerEvents();
    };

    function registerEvents() {
        CKEDITOR.replace('txtDescription', {});
        $('#btnSave').on('click', function (e) {
            saveChanges(e);
        });
        $('#btnCancel').on('click', function (e) {
            window.location.href = "/admin/nailstore";
        });
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            readURL(this);
        });

        var imagePath = $("#hidImage").val();
        if (imagePath !== undefined)
            $('#image_upload_preview').attr('src', imagePath);
        else
            $('#image_upload_preview').attr('src', "");
    }
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_upload_preview').attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }

    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var description = CKEDITOR.instances.txtDescription.getData();
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            var storeModel = {
                Id: id,
                Name: name,
                Description: description,
                Image: null
            };
            if (files.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Upload/UploadImage",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (path) {
                        storeModel.Image = path;
                        save(storeModel);
                    },
                    error: function () {
                        system.notify('There was error uploading files!', 'error');
                    }
                });
            }
            else {
                save(storeModel);
            }

            return false;
        }
    }
    function save(e) {
        $.ajax({
            type: "POST",
            url: "/Admin/nailstore/SaveEntity",
            data: {
                Id: e.Id,
                Name: e.Name,
                Description: e.Description,
                Image: e.Image
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                system.notify('Update store successful', 'success');

                window.location.href = "/admin/nailstore";

                system.stopLoading();

            },
            error: function () {
                system.notify('Has an error in save review progress', 'error');
                system.stopLoading();
            }
        });
    }
    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        $('#txtAuthor').val('');
        $('#txtContent').val('');
        $('#txtIcon').val('');
        $('#slVote').val('');
        $('#slSocial').val('');
    }
};