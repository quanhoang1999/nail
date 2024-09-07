var NailOrderController = function () {

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
                    url: "/Admin/nailorder/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function () {
                        system.notify('Delete employee successful', 'success');
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
        $('body').on('click', '.btn-detail', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            detail(that);
        });
        $("#chk_all").click(function () {
            $("input[type=checkbox]").prop('checked', $(this).prop('checked'));

        });
        $('body').on('click', '.btn-updatestatus', function (e) {
            e.preventDefault();
            var type = $(this).data('status');   
          
            var ids = getSelected();
            if (ids.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "/admin/nailorder/updatestatus",
                    data: {
                        Type: type,
                        Ids: ids.split(',')
                    },
                    dataType: "json",
                    beforeSend: function () {
                        system.startLoading();
                    },
                    success: function (response) {
                        system.notify('Update status successful', 'success');
                        loadData();
                    },
                    error: function (status) {
                        console.log(status);
                    }
                });
            }
           
        });

    }
    function getSelected() {
        var grid = document.getElementById("tbl-content");

        //Reference the CheckBoxes in Table.
        var checkBoxes = grid.getElementsByTagName("INPUT");
        var message = "";
        //Loop through the CheckBoxes.
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].checked) {
                message += checkBoxes[i].value + ",";
            }
        }       
        if (message !== '')
            return message.slice(0, -1);
        return null;
    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "POST",
            url: "/admin/nailorder/filter",
            data: {
                Keyword: $('#txtKeyword').val(),
                SearchType: $("#slSearchType").val(),
                PageIndex: system.configs.pageIndex,
                PageSize: system.configs.pageSize,
                ToDate: moment($('#txtToDate').val(), 'MM/DD/YYYY').format(),
                FromDate: moment($('#txtFromDate').val(), 'MM/DD/YYYY').format(),
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
                            FullName: item.CustomerName,
                            CreatedDate: system.dateTimeFormatJson(item.DateCreated),
                            Id: item.Id,
                            Email: item.Email,
                            StoreName: item.NailStoreName,
                            DatePick: system.dateTimeFormatJson(item.DatePick),
                            PhoneNumber: item.PhoneNumber,
                            Status: item.IsFinish === true ? '<span class="badge badge-primary">Finished</span>' : '<span class="badge badge-success">UnFinished</span>'

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

    function detail(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/nailorder/GetById",
            data: {
                Id: that
            },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                $('#modal-add-edit').modal('show');
                $("#lblMessage").val(response.Note);
                $("#lblBookingDate").val(system.dateTimeFormatJson(response.DateCreated));
                $("#lblAppointmentDate").val(system.dateTimeFormatJson(response.DatePick));
                var template = $('#table-template-detail').html();
                var render = "";
                if (response.NailOrderDetails !== null) {
                    $.each(response.NailOrderDetails, function (i, item) {
                        render += Mustache.render(template, {
                            No: i + 1,
                            Name: item.NailServiceName,
                            Id: item.Id,
                            TechName: item.NailEmployeeName,
                            SalePrice: item.Price
                        });
                    });

                    if (render !== undefined) {
                        $('#tbl-content-detail').html(render);

                    }



                }
                else {
                    $('#tbl-content').html('');
                }
                system.stopLoading();
            },
            error: function () {
                system.notify('Has an error in save product progress', 'error');
                system.stopLoading();
            }
        });
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