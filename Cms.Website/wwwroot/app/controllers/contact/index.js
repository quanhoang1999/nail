var ContactController = function () {

    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {

        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtName: { required: true },
                txtPhone: { required: true },
                txtEmail: { required: true },
            }
        });




        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var name = $('#txtName').val();
                var phone = $('#txtPhone').val();
                var email = $('#txtEmail').val();
                var website = $('#txtWebsite').val();
                var address = $('#txtAddress').val();
                var other = $('#txtOther').val();
                var lat = $('#txtLat').val();
                var lng = $('#txtLng').val();
                $.ajax({
                    type: "POST",
                    url: "/Admin/Contact/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Phone: phone,
                        Email: email,
                        Website: website,
                        Address: address,
                        Other: other,
                        Lat: lat,
                        Status: 1,
                        Lng: lng
                    },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function (response) {
                        system.notify('Cập nhật thông tin thành công', 'success');
                        system.stopLoading();
                    },
                    error: function () {
                        system.notify('Has an error in progress', 'error');
                        system.stopLoading();
                    }
                });
                return false;
            }

        });
    };
}