var NailEmployeeEditController = function () {

    this.initialize = function () {
        registerEvents();
    };

    function registerEvents() {
        $("select").selectpicker();
        $('#btnSave').on('click', function (e) {
            saveChanges(e);
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
            var shortName = $('#txtShortname').val();
            var address = $('#txtAddress').val();
            var phone = $('#txtPhone').val();
            var nailStoreId = $("#slNailStoreId").val();
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            var employee = {
                Id: id,
                Name: name,
                ShortName: shortName,
                Address: address,
                PhoneNumber: phone,
                NailStoreId: nailStoreId,
                Avatar: null
            };
            if (files.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Upload/UploadImage",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        employee.Avatar = response;
                        save(employee);
                    },
                    error: function () {
                        system.notify('There was error uploading files!', 'error');
                    }
                });
            }
            else {
                save(employee);
            }
        }
    }

    function save(e) {
        $.ajax({
            type: "POST",
            url: "/Admin/nailemployee/SaveEntity",
            data: {
                Id: e.Id,
                Name: e.Name,
                ShortName: e.ShortName,
                Address: e.Address,
                PhoneNumber: e.PhoneNumber,
                NailStoreId: e.NailStoreId,
                Avatar: e.Avatar
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                system.notify('Update employee successful', 'success');
                window.location.href = "/admin/nailemployee";
                system.stopLoading();
                loadData(true);
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