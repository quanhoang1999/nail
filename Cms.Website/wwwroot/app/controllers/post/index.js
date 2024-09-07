var postController = function () {
    this.initialize = function () {     
        loadData();
        registerEvents();
        registerControls();      
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
                ddlCategoryIdM: { required: true }
            }
        });
        //todo: binding events to controls
   

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
            $('#modal-add-edit').modal('show');

        });

        $('#btnSelectImg').on('click', function () {
            $('#fileInputImage').click();
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
                    $('#txtImage').val(path);
                    system.notify('Upload image succesful!', 'success');

                },
                error: function () {
                    system.notify('There was error uploading files!', 'error');
                }
            });
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deletePost(that);
        });

        $('#btnSave').on('click', function (e) {
            savePost(e);
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
                url: '/Admin/Post/ImportExcel',
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
                url: "/Admin/Post/ExportExcel",
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
    }

    function registerControls() {
        CKEDITOR.replace('txtContent', {});

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

    }

    function savePost(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtNameM').val();
            var categoryId = $('#ddlCategoryIdM').combotree('getValue');
            console.log(categoryId);
            var description = $('#txtDescM').val();
         
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


            $.ajax({
                type: "POST",
                url: "/Admin/Post/SaveEntity",
                data: {
                    Id: id,
                    Name: name,
                    //CategoryId: categoryId,
                    Image: image,
                    //Price: price,
                    //OriginalPrice: originalPrice,
                    //PromotionPrice: promotionPrice,
                    Description: description,
                    Content: content,
                    HomeFlag: showHome,
                    HotFlag: hot,
                    Tags: tags,
                    Categories: categoryId,
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
            return false;
        }
    }

    function deletePost(id) {
        system.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Post/Delete",
                data: { id: that },
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
            url: "/Admin/Post/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                system.startLoading();
            },
            success: function (response) {
                var data = response;

                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                initTreeDropDownCategory(data.Categories);

                $('#txtDescM').val(data.Description);
                $('#txtUnitM').val(data.Unit);

                $('#txtPriceM').val(data.Price);
                $('#txtOriginalPriceM').val(data.OriginalPrice);
                $('#txtPromotionPriceM').val(data.PromotionPrice);

                $('#txtImageM').val(data.Image);

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
            url: "/Admin/Post/GetAllCategory",
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
                    data: arr,
                    multiple: true
                });

                $('#ddlCategoryIdImportExcel').combotree({
                    data: arr
                });
                if (selectedId !== undefined) {
                    var arrays = selectedId.split(',');
                    for (var i = 0; i < arrays.length; i++) {
                        $('#ddlCategoryIdM').combotree('setValue', arrays[i]);
                    }
                }
                //if (selectedId !== undefined) {
                //    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                //}
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

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'POST',
            data: {
              //  CategoryId: $('#ddlCategorySearch').val(),
                KeyWord: $('#txtKeyword').val(),
                PageIndex: system.configs.pageIndex,
                PageSize: system.configs.pageSize
            },
            url: '/admin/post/Filter',
            dataType: 'json',
            success: function (response) {
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Image: item.Image === undefined ? '<img src="/admin-side/images/user.png" width=25 />' : '<img src="' + item.FullImageUrl + '" width=25 />',
                        //CategoryName: item.PostCates.Name,
                        //Price: system.formatNumber(item.Price, 0),
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