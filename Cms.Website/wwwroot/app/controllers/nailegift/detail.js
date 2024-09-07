var NailEgiftEditController = function () {

    this.initialize = function () {
        registerEvents();

    };


    function registerEvents() {
        $("select").selectpicker();        
        $('#btnCancel').on('click', function (e) {
            window.location.href = "/admin/nailegift";
        });
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                slNailCategoryId: { required: true },
                txtSalePrice: { required: true, number: true },
                txtTime: { number: true }
            }
        });

        $('#btnSave').on('click', function (e) {
            saveChanges(e);
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
                    url: "/Admin/nailegift/DeleteFile",
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
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_upload_preview').attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
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
                $preview.append($("<img/>", { src: this.result, height: 80 }));
            });

            reader.readAsDataURL(file);

        }

    }

    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var time = $('#txtTime').val();
            var nailCategoryId = $('#slNailCategoryId').val();
            var employees = $('#slEmployees').val();
            var salePrice = $('#txtSalePrice').val();
            var notePrice = $('#txtNotePrice').val();
            var description = $('#txtDescription').val();
            var startDate = moment($('#txtStartDate').val(), 'MM/DD/YYYY').format();
            var expriredDate = moment($('#txtExpriredDate').val(), 'MM/DD/YYYY').format();
            var nailStoreId = $('#slNailStoreId').val();
            var showMenu = $('#ckShowMenu').prop('checked');
            var showHome = $('#ckShowHome').prop('checked');
            var active = $('#ckStatusM').prop('checked');
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append("files", files[i]);
            }
            var service = {
                Id: id,
                Name: name,
                Time: time,
                IsShowMenu: showMenu,
                IsShowHomePage: showHome,
                Description: description,
                IsActive: active,
                StartDate: startDate,
                ExpriredDate: expriredDate,
                SalePrice: salePrice,
                NotePrice: notePrice,
                NailCategoryId: nailCategoryId,
                Employees: employees,
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
                        service.Pictures = response;
                        save(service);
                    },
                    error: function () {
                        system.notify('There was error uploading files!', 'error');
                    }
                });
            }
            else {
                save(service);
            }

            return false;
        }
    }
    function save(e) {
        $.ajax({
            type: "POST",
            url: "/Admin/nailegift/SaveEntity",
            data: {
                Id: e.Id,
                Name: e.Name,
                Time: e.Time,
                StartDate: e.StartDate,
                ExpriredDate: e.ExpriredDate,
                SalePrice: e.SalePrice,
                NotePrice: e.NotePrice,
                NailCategoryId: e.NailCategoryId,
                Employees: e.Employees,
                Description: e.Description,
                Pictures: e.Pictures,
                IsShowMenu: e.IsShowMenu,
                IsShowHomePage: e.IsShowHomePage,
                NailStoreId: e.NailStoreId,
                IsActive: e.IsActive
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                system.notify('Update service successful', 'success');

                window.location.href = "/admin/nailegift";

                system.stopLoading();
                loadData(true);
            },
            error: function () {
                system.notify('Has an error in save service progress', 'error');
                system.stopLoading();
            }
        });
    }
};


