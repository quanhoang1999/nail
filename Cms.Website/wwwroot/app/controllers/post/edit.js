var postController = function () {
    this.initialize = function () {
        registerEvents();
        registerControls();
    };

    function registerEvents() {
        //Init validation
        //$('#txtTagM').tagsinput({
        //    trimValue: true
        //});
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true }

            }
        });
        //todo: binding events to controls


        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        //$("#fileInputImage").on('change', function () {
        //    readURL(this);
        //});

        $("#txtNameM").on('change', function () {

            $("#txtSeoPageTitleM").val(RemoveUnicodeHyphenDot($("#txtNameM").val()));

        });

        $("#fileInputImage").on('change', function () {
            var fileUpload = $(this).get(0);
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
                    $('#txtImg').val(path);
                    $('#image_upload_preview').attr('src', "/uploaded/images/" + path);
                    system.notify('Upload image succesful!', 'success');

                },
                error: function () {
                    system.notify('There was error uploading files!', 'error');
                }
            });
        });



        $('#btnSave').on('click', function (e) {
            savePost(e);
        });
    }

    function registerControls() {
        $('#image_upload_preview').attr('src', $("#hdFullImage").val());
        $('#ckStatusM').prop('checked', $("#hidStatus").val() === "Active");
        $('#ckHotM').prop('checked', $("#hidHotFlag").val() === 'True');
        $('#ckShowHomeM').prop('checked', $("#hidHomeFlag").val() === 'True');
        CKEDITOR.replace('txtContent', {});
    }

    function savePost(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();

            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var description = $('#txtDescM').val();
            var image = $('#txtImg').val();

            var tags = $('#txtTagM').val();
            var seoKeyword = $('#txtMetakeywordM').val();
            var seoMetaDescription = $('#txtMetaDescriptionM').val();
            var seoPageTitle = $('#txtSeoPageTitleM').val();
            var seoAlias = $('#txtSeoAliasM').val();
            var DateCreated = moment($('#txtDateCreated').val(), 'MM/DD/YYYY').format();
            var content = CKEDITOR.instances.txtContent.getData();
            var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
            var hot = $('#ckHotM').prop('checked');
            var showHome = $('#ckShowHomeM').prop('checked');
            $.ajax({
                type: "POST",
                url: "/Admin/Post/SaveEntity",
                data: {
                    Id: id,
                    Name: name,
                    Image: image,
                    Description: description,
                    DateCreated: DateCreated,
                    Content: content,
                    HomeFlag: showHome,
                    HotFlag: hot,
                    Tags: tags,
                    Status: status,
                    SeoPageTitle: seoPageTitle,
                    SeoAlias: seoAlias,
                    SeoKeywords: seoKeyword,
                    SeoDescription: seoMetaDescription
                },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Update product successful', 'success');
                    system.stopLoading();
                    window.location.href = "/admin/post";

                },
                error: function () {
                    system.notify('Has an error in save product progress', 'error');
                    system.stopLoading();
                }
            });
            return false;
        }
    }

    function RemoveUnicodeHyphenDot(str) {
        //str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        //Hoàng thêm 24/01/2018
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        //End Hoàng 24/01/2018
        //  
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|,|\/|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");


        //
        return str;
    }
};