var NailCustomerEditController = function () {

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


    }

    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var lastName = $('#txtLastName').val();
            var email = $('#txtEmail').val();
            var phone = $('#txtPhone').val();
            var nailStoreId = $("#slNailStoreId").val();
            var birthdate = moment($('#txtBirthday').val(), 'MM/DD/YYYY').format();
       
            $.ajax({
                type: "POST",
                url: "/Admin/nailcustomer/SaveEntity",
                data: {
                    Id: id,
                    FirstName: name,
                    LastName: lastName,
                    Email: email,
                    Phone: phone,
                    Birthdate: birthdate,
                    NailStoreId: nailStoreId
                },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Update customer successful', 'success');


                    window.location.href = "/admin/nailcustomer";
                    system.stopLoading();
                },
                error: function () {
                    system.notify('Has an error in save review progress', 'error');
                    system.stopLoading();
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
};