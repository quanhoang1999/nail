var FeedbackEditController = function () {

    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {

        $('#btnSave').on('click', function (e) {
          saveChanges(e);
        });

    };
    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
           
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();

            var message = $('#txtContent').val();

            $.ajax({
                type: "POST",
                url: "/Admin/Feedback/SaveEntity",
                data: {
                    Id: id,
                    Name: name,
                    Message: message
                },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Update feedback successful', 'success');
                    system.stopLoading();
                },
                error: function () {
                    system.notify('Has an error in save feedback progress', 'error');
                    system.stopLoading();
                }
            });

            return false;
        }
    }


}