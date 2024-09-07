var slideEditController = function () {

    this.initialize = function () {

        registerEvents();
        registerControls();

    }
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
        if (slideId > 0)
            loadDetails(slideId);


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
            var description = $('#txtDescM').val();


            var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;

            var imageUrl;
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
                        imageUrl = path;
                        $.ajax({
                            type: "POST",
                            url: "/Admin/Slide/SaveEntity",
                            data: {
                                Id: slideId,
                                Name: name,
                                Image: imageUrl,
                                Description: description,
                                Status: status
                            },
                            dataType: "json",
                            beforeSend: function () {
                                system.startLoading();
                            },
                            success: function (response) {
                                system.notify('Update slide successful', 'success');
                                slideId = response.Id;
                                system.stopLoading();
                            },
                            error: function () {
                                system.notify('Has an error in save product progress', 'error');
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
                imageUrl = $('#hdImage').val();
                $.ajax({
                    type: "POST",
                    url: "/Admin/Slide/SaveEntity",
                    data: {
                        Id: slideId,
                        Name: name,
                        Image: imageUrl,
                        Description: description,
                        Status: status
                    },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function (response) {
                        system.notify('Update slide successful', 'success');
                        slideId = response.Id;
                        system.stopLoading();
                    },
                    error: function () {
                        system.notify('Has an error in save product progress', 'error');
                        system.stopLoading();
                    }
                });
            }

            return false;
        }
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
        $.ajax({
            type: "GET",
            url: "/Admin/Slide/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                var data = response;

                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);


                $('#txtDescM').val(data.Description);
                if (data.ImageFullUrl !== undefined)
                    $('#image_upload_preview').attr('src', data.ImageFullUrl);
                else
                    $('#image_upload_preview').attr('src', "");

                system.stopLoading();

            },
            error: function (status) {
                system.notify('Có lỗi xảy ra', 'error');
                system.stopLoading();
            }
        });
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