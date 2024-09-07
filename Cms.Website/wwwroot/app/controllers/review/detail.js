var ReviewEditController = function () {

    this.initialize = function () {
        registerEvents();
    };

    function registerEvents() {
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
            var author = $('#txtAuthor').val();
            var content = $('#txtContent').val();
            var icon = $('#txtIcon').val();
            var vote = $('#slVote').val();
            var social = $('#slSocial').val();
            var imageUrl;
            var fileUpload = $('#fileInputImage').get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
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
                        url: "/Admin/review/SaveEntity",
                        data: {
                            Id: id,
                            Name: name,
                            Author: author,
                            Vote: vote,
                            Content: content,
                            Social: social,
                            Avartar: imageUrl,
                            Icon: icon
                        },
                        dataType: "json",
                        beforeSend: function () {
                            system.startLoading();
                        },
                        success: function (response) {
                            system.notify('Update review successful', 'success');

                            window.location.href = "/admin/review";

                            system.stopLoading();
                            loadData(true);
                        },
                        error: function () {
                            system.notify('Has an error in save review progress', 'error');
                            system.stopLoading();
                        }
                    });
                },
                error: function () {
                    system.notify('There was error uploading files!', 'error');
                }
            });
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
}