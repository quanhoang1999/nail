﻿var NailCustomerController = function () {

    this.initialize = function () {
        registerEvents();
        loadData();
    };

    function registerEvents() {

        //Init validation      
        $('#btnSearch').on('click', function () {
            loadData();
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            system.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/nailcustomer/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function () {
                        system.notify('Delete customer successful', 'success');
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

    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "POST",
            url: "/admin/nailcustomer/filter",
            data: {
                Keyword: $('#txtKeyword').val(),
                SearchType: $("#slSearchType").val(),
                PageIndex: system.configs.pageIndex,
                PageSize: system.configs.pageSize,
                StoreId: $("#slStore").val()
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
                            FullName: item.FullName,
                            CreatedDate: system.dateTimeFormatJson(item.DateCreated),
                            Id: item.Id,
                            PhoneNumber: item.Phone,
                            StoreName: item.NailStoreName,
                            Email: item.Email
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
                url: "/Admin/nailcustomer/Copy",
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
                    system.notify('Update customer successful', 'success');
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
            first: '<i class="fas fa-angle-double-left"></i>',
            prev: '<i class="fas fa-angle-left"></i>',
            next: '<i class="fas fa-angle-right"></i>',
            last: '<i class="fas fa-angle-double-right"></i>',
            onPageClick: function (event, p) {
                system.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
};