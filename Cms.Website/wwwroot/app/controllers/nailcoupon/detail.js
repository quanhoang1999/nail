var NailCouponEditController = function () {

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
                slCustomer: { required: true }
            }
        });
        $('#btnSave').on('click', function (e) {
            e.preventDefault();
            saveChanges(e);
        });
        $("#btnSearch").on('click', function (e) {
            loadData();
        });
        $('body').on('click', '.btn-search', function (e) {
            e.preventDefault();
            $('#modal-add-edit').modal('show');
            loadData();
            system.stopLoading();
        });
    }

    function saveChanges(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var customerId = $('#slCustomer').val();
            var dateStarted = moment($('#txtDateStarted').val(), 'MM/DD/YYYY').format();
            var dateExpired = moment($('#txtDateExpired').val(), 'MM/DD/YYYY').format();
            var discountType = $('#slDiscountType').val();
            var discount = $('#txtDiscount').val();
            var nailStoreId = $('#slNailStoreId').val();
            $.ajax({
                type: "POST",
                url: "/Admin/customercoupon/SaveEntity",
                data: {
                    Id: id,
                    NailCustomerId: customerId,
                    DateStarted: dateStarted,
                    DateExpired: dateExpired,
                    DiscountType: discountType,
                    Discount: discount,
                    NailStoreId: nailStoreId
                },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Update coupon successful', 'success');

                    window.location.href = "/admin/customercoupon";

                    system.stopLoading();
                    loadData(true);
                },
                error: function () {
                    system.notify('Has an error in save review progress', 'error');
                    system.stopLoading();
                }
            });
            return false;
        }
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "POST",
            url: "/admin/NailCustomer/filter",
            data: {
                Keyword: $('#txtKeyword').val(),
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
                            FullName: item.FullName,
                            Email: item.Email,
                            Phone: item.Phone,
                            CreatedDate: system.dateTimeFormatJson(item.DateCreated),
                            Id: item.Id
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
}