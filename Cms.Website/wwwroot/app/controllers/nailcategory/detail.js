var NailCategoryEditController = function () {

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
                txtNameM: { required: true },
                txtOrderIndex: { number: true }
            }
        });
        $('#btnSave').on('click', function (e) {
            saveChanges(e);
        });
        $('#btnCancel').on('click', function (e) {
            window.location.href = "/admin/nailcategory";
        });
        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $('#fileInputImage').on("change", previewImages);
        $('body').on('click', '.btn-deleteFile', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            var picture = $(this).data('picture');
            var remove = $(this).parent();
            system.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/nailcategory/DeleteFile",
                    data: { id: that, images: picture },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function () {
                        remove.remove();
                        system.notify('Delete file successful', 'success');
                        system.stopLoading();
                    },
                    error: function () {
                        system.notify('Have an error in progress', 'error');
                        system.stopLoading();
                    }
                });
            });


        });
    }

    function previewImages() {

        var $preview = $('#preview').empty();
        if (this.files) $.each(this.files, readAndPreview);

        function readAndPreview(i, file) {

            if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
                return alert(file.name + " is not an image");
            } // else...

            var reader = new FileReader();

            $(reader).on("load", function () {
                $preview.append($("<img/>", { src: this.result, height: 100 }));
            });

            reader.readAsDataURL(file);

        }

    }

    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var nailStoreId = $('#slNailStoreId').val();
            var name = $('#txtNameM').val();
            var description = $("#txtDescription").val();
            var orderIndex = $("#txtOrderIndex").val();
            var showMenu = $('#ckShowMenu').prop('checked');
            var showHome = $('#ckShowHome').prop('checked');
            var active = $('#ckStatusM').prop('checked');
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append("files", files[i]);
            }
            var category = {
                Id: id,
                Name: name,
                IsShowMenu: showMenu,
                IsShowHomePage: showHome,
                Description: description,
                OrderIndex: orderIndex,
                IsActive: active,
                NailStoreId: nailStoreId,
                Pictures: null
            };
            if (files.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Upload/UploadMultipleImage",
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        category.Pictures = response;
                        save(category);
                    },
                    error: function () {
                        system.notify('There was error uploading files!', 'error');
                    }
                });
            }
            else {
                save(category);
            }

            return false;
        }
    }
    function save(e) {
        $.ajax({
            type: "POST",
            url: "/Admin/nailcategory/SaveEntity",
            data: {
                Id: e.Id,
                Name: e.Name,
                Description: e.Description,
                Pictures: e.Pictures,
                IsActive: e.IsActive,
                IsShowMenu: e.IsShowMenu,
                IsShowHomePage: e.IsShowHomePage,
                NailStoreId: e.NailStoreId,
                OrderIndex: e.OrderIndex
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                system.notify('Update category successful', 'success');

                window.location.href = "/admin/nailcategory";

                system.stopLoading();
                loadData(true);
            },
            error: function () {
                system.notify('Has an error in save category progress', 'error');
                system.stopLoading();
            }
        });
    }
};