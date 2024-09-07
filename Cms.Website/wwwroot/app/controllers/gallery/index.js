var SlideController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                txtNameM: { required: true }
            }
        });

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#ddl-show-page").on('change', function () {
            system.configs.pageSize = $(this).val();
            system.configs.pageIndex = 1;
            loadData(true);
        });
       
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            system.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Gallery/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function () {
                        system.notify('Delete slide successful', 'success');
                        system.stopLoading();
                        loadData();
                    },
                    error: function () {
                        system.notify('Have an error in progress', 'error');
                        system.stopLoading();
                    }
                });
            });
        });

        $('body').on('click', '.btn-copy', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            var these = $(this).data('name');
            copySlide(that, these);
        });
        $('#btnSave').on('click', function (e) {
            saveCopy(e);
        });
    }


    function registerControls() {

    }

    function copySlide(that, these) {
        $('#txtName').val(these + ' - copy');
        $('#hdId').val(that);
        $('#modal-add-edit').modal('show');
        system.stopLoading();
    }

    function saveCopy(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var name = $('#txtName').val();
            var IsCopy = $('#ckCopyIMage').prop('checked');
            var id = $('#hdId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/gallery/Copy",
                data: {
                    Id: id,
                    Name: name,
                    IsCopy: IsCopy
                },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Update slide successful', 'success');
                    $('#modal-add-edit').modal('hide');
                    resetFormMaintainance();

                    system.stopLoading();
                    loadData(true);
                },
                error: function () {
                    system.notify('Has an error in save product progress', 'error');
                    system.stopLoading();
                }
            });
        }
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtName').val('');
      

    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "POST",
            url: "/admin/gallery/GetAllPaging",
            data: {
                Keyword: $('#txt-search-keyword').val(),
                PageIndex: system.configs.pageIndex,
                PageSize: system.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Image: item.Image === undefined ? '<img src="/admin-side/images/user.png" width=25' : '<img src="' + item.ImageFullUrl + '" width=25 />',
                            Name: item.Name,
                            Id: item.Id,
                            Status: system.getStatus(item.Status)
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render !== undefined) {
                        $('#tbl-content').html(render);

                    }
                    if (response.RowCount > system.PageSize) {
                        wrapPaging(response.RowCount, function () {
                            loadData();
                        }, isPageChanged);
                    }


                }
                else {
                    $('#tbl-content').html('');
                }
                system.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / system.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                system.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }

}