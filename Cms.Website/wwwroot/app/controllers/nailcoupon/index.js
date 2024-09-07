var NailCouponController = function () {

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

    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "POST",
            url: "/admin/CustomerCoupon/filter",
            data: {
                Keyword: $('#txtKeyword').val(),
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
                            Name: item.NailCustomer.FullName,
                            CouponCode: item.CouponCode,
                            CreatedDate: system.dateTimeFormatJson(item.DateCreated),
                            StoreName: item.NailStoreName,
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