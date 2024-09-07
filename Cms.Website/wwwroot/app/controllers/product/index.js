var productController = function () {
    var quantityManagement = new QuantityManagement();
    var imageManagement = new ImageManagement();
    var wholePriceManagement = new WholePriceManagement();

    this.initialize = function () {
        loadCategories();
        loadData();
        registerEvents();
        //    registerControls();
        //quantityManagement.initialize();
        //imageManagement.initialize();
        //wholePriceManagement.initialize();
    };
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_upload_preview').attr('src', e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }

    function registerEvents() {
        //Init validation
        $('#txtTagM').tagsinput({
            trimValue: true
        });
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                ddlCategoryIdM: { required: true },
                txtPriceM: {
                    required: true,
                    number: true
                }
            }
        });
        //todo: binding events to controls
        $('#ddlShowPage').on('change', function () {
            system.configs.pageSize = $(this).val();
            system.configs.pageIndex = 1;
            loadData(true);
        });

        $('#btnSearch').on('click', function () {
            loadData();
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });

        $("#btnCreate").on('click', function () {
            resetFormMaintainance();
            initTreeDropDownCategory();
            $('#modal-add-edit').modal('show');

        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
        });
        $("#fileInputImage").on('change', function () {
            readURL(this);
        });


    

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteProduct(that);
        });

        $('#btnSave').on('click', function (e) {
            saveProduct(e);
        });

        $('#btn-import').on('click', function () {
            initTreeDropDownCategory();
            $('#modal-import-excel').modal('show');
        });

        $('#btnImportExcel').on('click', function () {
            var fileUpload = $("#fileInputExcel").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append("files", files[i]);
            }
            // Adding one more key to FormData object  
            fileData.append('categoryId', $('#ddlCategoryIdImportExcel').combotree('getValue'));
            $.ajax({
                url: '/Admin/Product/ImportExcel',
                type: 'POST',
                data: fileData,
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                success: function (data) {
                    $('#modal-import-excel').modal('hide');
                    loadData();

                }
            });
            return false;
        });

        $('#btn-export').on('click', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Product/ExportExcel",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    window.location.href = response;
                    system.stopLoading();
                },
                error: function () {
                    system.notify('Has an error in progress', 'error');
                    system.stopLoading();
                }
            });
        });

        $('body').on('click', '.btn-copy', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            var these = $(this).data('name');
            copySlide(that, these);
        });
        $('#btnCopySave').on('click', function (e) {
            saveCopy(e);
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
                url: "/Admin/Product/Copy",
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

    function saveProduct(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var categoryId = $('#ddlCategoryIdM').combotree('getValue');

            var description = $('#txtDescM').val();
            var unit = $('#txtUnitM').val();

            var price = $('#txtPriceM').val();
            var originalPrice = $('#txtOriginalPriceM').val();
            var promotionPrice = $('#txtPromotionPriceM').val();

            var image = $('#txtImage').val();

            var tags = $('#txtTagM').val();
            var seoKeyword = $('#txtMetakeywordM').val();
            var seoMetaDescription = $('#txtMetaDescriptionM').val();
            var seoPageTitle = $('#txtSeoPageTitleM').val();
            var seoAlias = $('#txtSeoAliasM').val();

            var content = CKEDITOR.instances.txtContent.getData();
            var status = $('#ckStatusM').prop('checked') === true ? 1 : 0;
            var hot = $('#ckHotM').prop('checked');
            var showHome = $('#ckShowHomeM').prop('checked');
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
                        url: "/Admin/Product/SaveEntity",
                        data: {
                            Id: id,
                            Name: name,
                            CategoryId: categoryId,
                            Image: imageUrl,
                            Price: price,
                            OriginalPrice: originalPrice,
                            PromotionPrice: promotionPrice,
                            Description: description,
                            Content: content,
                            HomeFlag: showHome,
                            HotFlag: hot,
                            Tags: tags,
                            Unit: unit,
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
                },
                error: function () {
                    system.notify('There was error uploading files!', 'error');
                }
            });
            return false;
        }
    }




    function deleteProduct(id) {
        system.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Product/Delete",
                data: { id: id },
                dataType: "json",
                beforeSend: function () {
                    system.startLoading();
                },
                success: function (response) {
                    system.notify('Delete successful', 'success');
                    system.stopLoading();
                    loadData();
                },
                error: function (status) {
                    system.notify('Has an error in delete progress', 'error');
                    system.stopLoading();
                }
            });
        });
    }

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Product/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                var data = response;

                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                initTreeDropDownCategory(data.CategoryId);

                $('#txtDescM').val(data.Description);
                $('#txtUnitM').val(data.Unit);

                $('#txtPriceM').val(data.Price);
                $('#txtOriginalPriceM').val(data.OriginalPrice);
                $('#txtPromotionPriceM').val(data.PromotionPrice);

                //$('#txtImageM').val(data.Image);

                $('#txtTagM').tagsinput('removeAll');
                if (data.Tags !== undefined) {
                    $.each(data.Tags.split(','), function (index, value) {
                        $('#txtTagM').tagsinput('add', value);

                    });
                }

                $('#txtMetakeywordM').val(data.SeoKeywords);
                $('#txtMetaDescriptionM').val(data.SeoDescription);
                $('#txtSeoPageTitleM').val(data.SeoPageTitle);
                $('#txtSeoAliasM').val(data.SeoAlias);

                CKEDITOR.instances.txtContent.setData(data.Content);
                $('#ckStatusM').prop('checked', data.Status === 1);
                $('#ckHotM').prop('checked', data.HotFlag);
                $('#ckShowHomeM').prop('checked', data.HomeFlag);
                if (data.ImageFullUrl !== undefined)
                    $('#image_upload_preview').attr('src', data.ImageFullUrl);
                else
                    $('#image_upload_preview').attr('src', "");
                $('#modal-add-edit').modal('show');
                system.stopLoading();

            },
            error: function (status) {
                system.notify('Có lỗi xảy ra', 'error');
                system.stopLoading();
            }
        });
    }

    function initTreeDropDownCategory(selectedId) {
        $.ajax({
            url: "/Admin/ProductCategory/GetAll",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = system.unflattern(data);
                $('#ddlCategoryIdM').combotree({
                    data: arr
                });

                $('#ddlCategoryIdImportExcel').combotree({
                    data: arr
                });
                if (selectedId !== undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtNameM').val('');
        initTreeDropDownCategory('');

        $('#txtDescM').val('');
        $('#txtUnitM').val('');

        $('#txtPriceM').val('0');
        $('#txtOriginalPriceM').val('');
        $('#txtPromotionPriceM').val('');

        $('#txtImage').val('');

        $('#txtTagM').val('');
        $('#txtMetakeywordM').val('');
        $('#txtMetaDescriptionM').val('');
        $('#txtSeoPageTitleM').val('');
        $('#txtSeoAliasM').val('');

        //CKEDITOR.instances.txtContentM.setData('');
        $('#ckStatusM').prop('checked', true);
        $('#ckHotM').prop('checked', false);
        $('#ckShowHomeM').prop('checked', false);

    }

    function loadCategories() {
        $.ajax({
            type: 'GET',
            url: '/admin/product/GetAllCategories',
            dataType: 'json',
            success: function (response) {
                var render = "<option value=''>--Select category--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
                $('#ddlCategorySearch').html(render);
            },
            error: function (status) {
                console.log(status);
                system.notify('Cannot loading product category data', 'error');
            }
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'POST',
            data: {
                CategoryId: $('#ddlCategorySearch').val(),
                KeyWord: $('#txtKeyword').val(),
                PageIndex: system.configs.pageIndex,
                PageSize: system.configs.pageSize
            },
            url: '/admin/product/GetAllPaging',
            dataType: 'json',
            success: function (response) {
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Image: item.Image === undefined ? '<img src="/admin-side/images/user.png" width=25' : '<img src="' + item.ImageFullUrl + '" width=25 />',
                        CategoryName: item.ProductCategory.Name,
                        Price: system.formatNumber(item.Price, 0),
                        CreatedDate: system.dateTimeFormatJson(item.DateCreated),
                        Status: system.getStatus(item.Status)
                    });

                });
                $('#lblTotalRecords').text(response.RowCount);
                if (render !== '') {
                    $('#tbl-content').html(render);
                }
                if (response.RowCount > system.PageSize) {
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                }

            },
            error: function (status) {
                console.log(status);
                system.notify('Cannot loading data', 'error');
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