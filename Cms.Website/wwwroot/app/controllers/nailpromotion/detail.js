var NailPromotionEditController = function () {

    this.initialize = function () {
        registerEvents();
    };

    function registerEvents() {
        $("select").selectpicker();
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true }
            }
        });
        $('#btnSave').on('click', function (e) {
            saveChanges(e);
        });
        $('#btnCancel').on('click', function (e) {
            window.location.href = "/admin/nailpromotion";
        });
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            readURL(this);
        });
        $('#ckStatusM').prop('checked', $("#hidActive").val() === 'True');
        $('#ckHotM').prop('checked', $("#hidShowHomePage").val() === 'True');
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
            var salePrice = $('#txtSalePrice').val();
            var dateStarted = moment($('#txtDateStarted').val(), 'MM/DD/YYYY').format();
            var dateExpired = moment($('#txtDateExpired').val(), 'MM/DD/YYYY').format();
            var description = $('#txtDescription').val();        
            var nailStoreId = $('#slNailStoreId').val();
            var status = $('#ckStatusM').prop('checked');
            var showHome = $('#ckShowHomeM').prop('checked');
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            if (files.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Upload/UploadImage",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (path) {
                        $.ajax({
                            type: "POST",
                            url: "/Admin/nailpromotion/SaveEntity",
                            data: {
                                Id: id,
                                Name: name,
                                SalePrice: salePrice,
                                StartDate: dateStarted,
                                ExpriredDate: dateExpired,
                                Description: description,
                                Image: path,
                                IsActive: status,
                                NailStoreId: nailStoreId,
                                IsShowHomePage: showHome
                            },
                            dataType: "json",
                            beforeSend: function () {
                                system.startLoading();
                            },
                            success: function (response) {
                                system.notify('Update promotion successful', 'success');

                                window.location.href = "/admin/nailpromotion";

                                system.stopLoading();
                                loadData(true);
                            },
                            error: function () {
                                system.notify('Has an error in save promotion progress', 'error');
                                system.stopLoading();
                            }
                        });
                    },
                    error: function () {
                        system.notify('There was error uploading files!', 'error');
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Admin/nailpromotion/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        SalePrice: salePrice,
                        StartDate: dateStarted,
                        ExpriredDate: dateExpired,
                        Description: description,
                        Image: $("#hidImageName").val(),
                        IsActive: status,
                        NailStoreId: nailStoreId,
                        IsShowHomePage: showHome
                    },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function (response) {
                        system.notify('Update promotion successful', 'success');

                        window.location.href = "/admin/nailpromotion";

                        system.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        system.notify('Has an error in save promotion progress', 'error');
                        system.stopLoading();
                    }
                });
            }
               
            return false;
        }
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