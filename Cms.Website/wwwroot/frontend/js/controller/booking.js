var BookingController = function () {

    isFinish = false;
    isReset = true;
    currentTab = "start-tab"

    $("#" + currentTab).closest('li').addClass("active")
    $("#" + currentTab).addClass("active show")
    currentForm = "pills-start";
    carTotal = 0;
    cardFinishTotal = 0;
    eSTFinishTime = 0;// Apppoinment time + total time service 
    timeServiceTotal = 0;
    timePickSuggest = '';
    $("#cartTotal").html('$' + carTotal);
    $("#finishTotal").html('$' + cardFinishTotal);

    var carddata = {
        services: "",
        datePick: Date,
        timePick: Date,
        cusInfo: {}
    }



    var cusInfo = {

    }
    var cachedObj = {
        parentId: "",
        childs: [],
    }
    var serviceEmpID = "";
    var cacheObjs = []; //list  services choosed (parent, child)
    var cacheChildServiceObjs = []; //list child services choosed from parent services
    var cacheCardServiceObjs = []; //list  services add into card
    var cachelstEmpBySerivceObjs = []; //list  services add into card
    setShowHideTab();
    this.initialize = function () {
        loadServicesData();
        registerEvents();
        loadEmployeeData();

    }

    function loadEmployeeData() {
        $.ajax({
            type: 'get',
            data: this.loadChildServices,
            url: '/service/GetEmployeelst',
            dataType: 'json',
            success: function (response) {
                console.log("responsenewss", response);
                var data = response;
                cachelstEmpBySerivceObjs = data;
            },
            error: function (status) {
                console.log(status);
            }
        });
    }

    function loadServicesData() {
        $.ajax({
            type: 'get',
            data: this.loadChildServices,
            url: '/service/GetByServiceGroupID?id=' + 1,
            /*  url: '/service/GetByID?id=' + parentServiceId,*/
            dataType: 'json',
            success: function (response) {
                console.log("responsenewss", response);
                var data = response;
                cacheObjs = data;
            },
            error: function (status) {
                console.log(status);
            }
        });
    }

    function registerEvents() {
        var now = new Date();
        var formattedDate = now.getMonth() + "/" + now.getDate() + "/" + now.getFullYear()
        console.log(now, formattedDate)

        $('#datePick').datepicker({
            language: 'en',
            minDate: new Date(),
            dateFormat: 'mm/dd/yyyy',
            onSelect: function (fd, d, picker) {
                if (!d) return;
                generateDateList(d, 15)

            }
        })

        $('#datePick').datepicker().data('datepicker').selectDate(new Date());

        $("#phonenumber").mask("999-999-9999");

        // $('#slider-product').slick({
        //     slidesToShow: 4,
        //     slidesToScroll: 1,
        //     dots: false,
        //     infinite: true,
        // });
        $.validator.addMethod("customemail",
            function validateEmail(email) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(String(email).toLowerCase());
            },
            "Please enter a valid email"
        );
        $.validator.addMethod("noSpace",
            function (value, element) {
                return value.trim().length == 0;
            }, "No space please and don't leave it empty"
        );
        $('#frmbooking').validate({
            errorClass: 'has-error',
            errorPlacement: function (error, e) {
                e.parents('.col').append(error);
            },
            rules:
            {
                Name: { required: true },
                PhoneNumber: { required: true },
                Email: { required: true, customemail: true },
                Note: {},
            },
            messages:
            {
                Name: { required: "Enter Name" },
                PhoneNumber: { required: "Enter the Phone Nubmer" },
                Email: { required: "Enter the Email" }

            },
            //errorContainer: "#validationSummary",
            //errorLabelContainer: "#validationSummary ul",
            //wrapper: "li",
            submitHandler: function (form) {

            }
        });
        $('#booking-step ul li a, #services-tab-back, #services-tab-next, #time-tab-next, #time-tab-back, #finish-tab-next').on('click', function (e) {



            if (isFinish) {
                currentTab = "start-tab"

                currentForm = "pills-start";
                setShowHideTab();
                carTotal = 0;
                $("#cartTotal").html('$' + carTotal);
                carddata = {
                    services: "",
                    datePick: now.getMonth() + "/" + now.getDate() + "/" + now.getFullYear() + " 10:10",
                    timePick: Date,
                    cusInfo: {}
                }
                cusInfo = {

                }
                cachedObj = {
                    parentId: "",
                    childs: [],
                }
                cacheObjs = []; //list  services choosed (parent, child)
                cacheChildServiceObjs = []; //list child services choosed from parent services
                cacheCardServiceObjs = []; //list  services add into card
                renderGirdCardChildService();
                renderGirdChildService();
                renderFinishHeader();
                renderFinishBody();
                renderEmployeeList();

                $("#frmbooking").clearForm();
                $("#yourname").focus();
                isFinish = false;
                isReset = true;
                $('#bookingParentService input[type=checkbox]:first').trigger('click');
            }
            var id = $(this).attr('id');
            if (($(this).parent('.next') && $(this).parent('.next').length > 0) || ($(this).parent('.prev') && $(this).parent('.prev').length > 0)) {
                id = $(this).data('step');
            }

            if (id != 'start-tab') {
                $('#steponeSubmit').trigger('submit');
                if ($("#frmbooking").valid()) {
                    currentTab = id;
                    var paramsFromForm = {};
                    $.each($("#frmbooking").serializeArray(), function (index, value) {
                        paramsFromForm[value.name] = paramsFromForm[value.name] ? paramsFromForm[value.name] || value.value : value.value;
                    });
                    cusInfo = paramsFromForm;
                    $("#pills-start").removeClass("active show");
                    $("#pills-services").addClass("active show");

                    if (id != "services-tab") {
                        if (!cacheCardServiceObjs || (cacheCardServiceObjs && cacheCardServiceObjs.length <= 0)) {
                            $("#errServiceStep").html("Please Pick Services");
                            currentTab = 'services-tab';
                            e.preventDefault();
                            return false;
                        }
                        else {
                            $("#errServiceStep").html("");
                            isReset = false;
                        }
                    }
                    else if (isReset) {
                        $('#bookingParentService input[type=checkbox]:first').trigger('click');
                    }
                    // set show hide step
                    setShowHideTab();
                }

            }
            else {
                currentTab = 'start-tab';
                setShowHideTab();
            }

            generateDateList(new Date(), 15);
            if (id == 'time-tab') {

                if (timePickSuggest == '') {
                    triggerTimePick($('#lstTimePick li:first'));
                    var time = "9:30 AM";
                    if (new Date().getDay() == 0) {
                        time = "11:00 AM";
                    }

                    var datatime = $('#datePick').val() + " " + time;
                    carddata.timePick = new Date();
                    let today = new Date(datatime)
                    carddata.datePick = today.toLocaleString();
                }
                else {

                    var item = $('#lstTimePick li').find('input[data-val="' + timePickSuggest + '"]');
                    if (item && item.length > 0) {
                        triggerTimePick(item.closest('li'));
                    }
                }

            }

        });


        $('#slider-product .item').click(function () {
            $('#slider-product .item h3').removeClass('h3-service-select');
            $(this).find('h3').addClass('h3-service-select');
            $('#slider-product .item').find('h3 i').attr('hidden', true);
            $(this).find('input[type=checkbox]').prop("checked", true);
            $(this).find('h3 i').first().removeAttr('hidden');
            var vale = $(this).find('input[type=checkbox]').prop("checked");
            var serviceID = $(this).find('input[type=checkbox]').data('id');
            if (vale == true && serviceID) {
                //lstServiceChoosed.push(serviceID);
                renderGirdChildServiceNew(serviceID);
                $(this).find('input[type=checkbox]').prop("checked", true);
            }
            else {
                renderGirdChildServiceNew(serviceID, false);

            }
        });

        $('#time-tab-next, #finish-tab').click(function (e) {
            if (!cacheCardServiceObjs || (cacheCardServiceObjs && cacheCardServiceObjs.length <= 0)) {
                e.preventDefault();
                return false;
            }
            else {
                currentTab = 'finish-tab';
                setShowHideTab();
                cardFinishTotal = carTotal;
                $("#finishTotal").html('$' + cardFinishTotal);
            }


            renderFinishHeader();
            renderFinishBody();
        });

        $('#btnBooking').click(function () {
            if (!cacheCardServiceObjs || (cacheCardServiceObjs && cacheCardServiceObjs.length <= 0)) {
                e.preventDefault();
                return false;
            }
            else {
                carddata.services = cacheCardServiceObjs;
                carddata.cusInfo = cusInfo;

                var strName = cusInfo.Name.split(' ');

                var objboking = {};
                objboking.firstName = strName[0];
                objboking.lastName = strName[strName.length - 1];
                objboking.phonenumber = cusInfo.PhoneNumber.replaceAll('-', '');
                objboking.bookingDate = carddata.datePick;
                objboking.Email = cusInfo.Email;
                objboking.note = cusInfo.Note;
                objboking.services = cacheCardServiceObjs;

                $.ajax({
                    type: "POST",
                    url: '/service/Addbookingweb',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(objboking),
                    beforeSend: function () {
                        helper.startLoading();
                    },
                    success: function (response) {
                        isFinish = true;
                        helper.stopLoading();
                        $('#btnBooking').attr('hidden', true);
                        $('#modalFinish').modal('show');

                    },
                    failure: function (response) {
                        helper.stopLoading();
                        $('#modalFinishError').modal('show');
                    },
                    error: function (response) {
                        helper.stopLoading();
                        $('#modalFinishError').modal('show');

                    },
                    complete: function () {
                        helper.stopLoading();
                    }
                });
            }
        });


        $("#slider-product").owlCarousel({
            items: 4,
            itemsDesktop: [1000, 1],
            itemsDesktopSmall: [900, 1],
            itemsTablet: [600, 1],
            itemsMobile: false,
            navigation: true,
            pagination: false,
            autoPlay: false,
            slideSpeed: 1000,
            paginationSpeed: 1000,
            navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
        });

    }
    function setShowHideTab() {
        var $steps = $("#booking-step ul li a");
        $steps.each(function (i, val) {
            $(val).closest('li').removeClass("active");
            $(val).removeClass("active show");
        });
        $("#" + currentTab).closest('li').addClass("active");
        $("#" + currentTab).addClass("active show");

        $('[id^="pills-"]').removeClass("active show");
        $("#pills-" + currentTab.split('-')[0]).addClass("active show");

    }

    function loadChildServicesCardData(serviceID, isAdd = true) {
        if (isAdd) {
            if (!cacheCardServiceObjs.find(c => c.serviceId == serviceID)) {
                let item = cacheChildServiceObjs.find(c => c.serviceId == serviceID)
                if (item) {
                    cacheCardServiceObjs.push(item);
                }
            }
        }
        else {
            cacheCardServiceObjs = cacheCardServiceObjs.filter(c => c.parentId != serviceID);
        }
        calCardTotal();
        renderGirdCardChildService();
    }
    function calCardTotal() {
        carTotal = cacheCardServiceObjs.reduce(function (a, b) {
            return a + parseInt(b.servicePrice)
        }, 0);
        timeServiceTotal = cacheCardServiceObjs.reduce(function (a, b) {
            return a + parseInt(b.serviceTime || 0)
        }, 0);

        $("#cartTotal").html('$' + carTotal);
    }

    function renderGirdChildServiceNew(parentServiceId) {
        var template = $('#table-template').html();
        var render = "";
        var cacheObjsnew = cacheObjs.find(n => n.serviceGroupId == parentServiceId);
        //console.log("cacheObjsnew", cacheObjsnew, cacheObjs);
        $.each(cacheObjsnew.services, function (i, item) {
            render += Mustache.render(template, {
                Id: item.serviceId,
                Name: item.serviceName,
                SalePrice: item.servicePrice,
                Time: item.serviceTime == '' || item.serviceTime == 'null' || item.serviceTime == null ? '' : item.serviceTime + "'",
                Image: item.serviceIcon != null ? item.serviceIcon : '/images/ourservice/01.jpg'
            });
        });



        if (render != '' || (render == '' && cacheObjs.length == 0)) {
            $('#tbl-content').html(render);

            $('#tbl-content tr td div').click(function () {
              
                var div = $(this);
                var serviceID = $(div).closest('tr').find('input[type=checkbox]').data('id');
                serviceEmpID = serviceID//serive show employee modal
                renderEmployeeListNew(cacheObjsnew?.services);
                var service = cacheObjsnew.services?.find(c => c.serviceId == serviceID);
                var serviceInCard = cacheObjsnew.services?.find(c => c.serviceId == serviceID);
                if (service && serviceInCard) {
                    var empID = service["techID"];
                    var empInput = $('#lst-emp .item').find('input[data-id="' + empID + '"]');
                    if (empInput) {
                        $('#lst-emp .item h3').removeClass('h3-service-select');
                        $(empInput).closest('.item').find('h3').addClass('h3-service-select');
                        $('#lst-emp .item').find('h3 i').attr('hidden', true);
                        $(empInput).closest('.item').find('h3 i').first().removeAttr('hidden');
                        $(empInput).closest('.item').find('input[type=radio]').prop("checked", true);
                    }

                }
                else {
                    $('#lst-emp .item h3').removeClass('h3-service-select');
                    $('#lst-emp .item').find('h3 i').attr('hidden', true);
                }
              debugger
                $('#modalEmployees').modal('show');

            });

        }
    }
    function renderEmployeeListNew(dataSeviceGoupid) {
        var template = $('#table-template-lst-emp').html();
        var render = "";
        //  console.log("renderEmployeeListNew", cachelstEmpBySerivceObjs, dataSeviceGoupid);
        $.each(cachelstEmpBySerivceObjs, function (i, item) {
            render += Mustache.render(template, {
                Name: item.technicianName,
                Id: item.technicianId,
                Avartar: item.technicianAvatar != null ? item.technicianAvatar : '/images/ourservice/01.jpg',
            });

        });

        if (render != '') {
            $('#lst-emp').html(render);
            $('#lst-emp .item').on('click', function () {
              
                $('#lst-emp .item h3').removeClass('h3-service-select');
                $(this).find('h3').addClass('h3-service-select');
                $('#lst-emp .item').find('h3 i').attr('hidden', true);
                $(this).find('h3 i').first().removeAttr('hidden');
                $(this).find('input[type=radio]').prop("checked", true);
                var item = $(this).find('input[type=radio]:checked');
                var empID = $(item).data('id');
                var empName = $(item).data('name');
                var serivce = serviceEmpID
                cacheChildServiceObjs = dataSeviceGoupid;
                if (cacheChildServiceObjs.find(c => c.serviceId == serviceEmpID)) {
                    var index = cacheChildServiceObjs.findIndex(s => s.serviceId == serviceEmpID);
                    cacheChildServiceObjs[index]["technicianId"] = empID;
                    cacheChildServiceObjs[index]["techName"] = empName;
                }

                //add to card
                if (serviceEmpID) {

                    loadChildServicesCardDataNew(serviceEmpID);
                    serviceElement = $('#tbl-content tr td div input[data-id=' + serviceEmpID + ']')
                    $(serviceElement).closest('div').addClass('h3-service-select');
                    $(serviceElement).closest('div').find('i').removeAttr('hidden');

                }


                if (cacheCardServiceObjs && cacheCardServiceObjs.find(c => c.serviceId == serviceEmpID)) {
                    var index = cacheCardServiceObjs.findIndex(s => s.serviceId == serviceEmpID);
                    cacheCardServiceObjs[index]["technicianId"] = empID;
                    cacheCardServiceObjs[index]["techName"] = empName;
                    renderGirdCardChildServiceNew();
                }

                $('#modalEmployees').modal('hide');
            });
        }
    }
    function loadChildServicesCardDataNew(serviceID) {

        //if (!cacheCardServiceObjs.find(c => c.serviceId == serviceID)) {
        //    let item = cacheChildServiceObjs.find(c => c.serviceId == serviceID)
        //    if (item) {
        //        item.quantity = 1;
        //        cacheCardServiceObjs.push(item);
        //    }
        //}

        let item = cacheChildServiceObjs.find(c => c.serviceId == serviceID)
        if (item) {
            item.quantity = 1;
            cacheCardServiceObjs.push(item);
        }

        calCardTotal();
        renderGirdCardChildServiceNew();
    }

    function renderGirdCardChildServiceNew() {
        var template = $('#table-template-card').html();
        var render = "";
        $.each(cacheCardServiceObjs, function (index, item) {
            render += Mustache.render(template, {
                Id: item.serviceId,
                Name: item.serviceName,
                Time: item.serviceTime,
                SalePrice: item.servicePrice,
                Image: item.serviceIcon != null ? item.serviceIcon : '/images/ourservice/01.jpg',
                technicianId: item.technicianId,
                TechName: item.techName,
                index: index,
            });

        });
        if (render != '' || (render == '' && cacheCardServiceObjs.length == 0)) {
            $('#tbl-content-card').html(render);

            //hoc sủa nagfy 18/05/2022  $('#tbl-content-card tr td a')
            $('#tbl-content-card tr td a').on('click', function () { //remve on card
              
                var serviceID = $(this).data('id');
             
                var indexID = $(this).data('index');
                console.log("indexID", indexID, cacheCardServiceObjs)
                if (cacheCardServiceObjs.find(c => c.serviceId == serviceID)) {
                    debugger;
                    cacheCardServiceObjs.splice(indexID, 1)
                  // cacheCardServiceObjs = cacheCardServiceObjs.filter(c => c.serviceID != serviceID);
                    calCardTotal();
                    var service = $('#tbl-content tr td div input[data-id=' + serviceID + ']');
                    $(service).prop("checked", false);
                    renderGirdCardChildServiceNew();
                    $(service).closest('div').removeClass('h3-service-select');
                    $(service).closest('div').find('i').attr('hidden', true);
                    //delete tech have choosed in service child, card
                    if (cacheChildServiceObjs.find(c => c.serviceId == serviceID)) {
                        var index = cacheChildServiceObjs.findIndex(s => s.serviceId == serviceID);
                        cacheChildServiceObjs[index]["technicianId"] = null;
                        cacheChildServiceObjs[index]["techName"] = null;
                    }
                }
            });
        }
    }



    function renderGirdChildService() {
        var template = $('#table-template').html();
        var render = "";
        $.each(cacheObjs, function (i, pa) {
            lst = pa.childs
            $.each(lst, function (i, item) {
                render += Mustache.render(template, {
                    Id: item.id,
                    Name: item.name,
                    SalePrice: item.salePrice,
                    Time: item.time == '' || item.time == 'null' || item.time == null ? '' : item.time + "'",
                    Image: item.imageName != null ? item.imageName : '/images/ourservice/01.jpg'
                });

            });

        });


        if (render != '' || (render == '' && cacheObjs.length == 0)) {
            $('#tbl-content').html(render);

            $('#tbl-content tr td div div').click(function () {
               
                var div = $(this);
                var serviceID = $(div).closest('tr').find('input[type=checkbox]').data('id');
                serviceEmpID = serviceID//serive show employee modal
                var item = cacheChildServiceObjs.find(s => s.id == serviceID);
                if (item) {
                    $.ajax({
                        type: 'get',
                        data: this.loadChildServices,
                        url: '/service/GetLisEmployee?id=' + serviceID,
                        dataType: 'json',
                        success: function (response) {
                            var data = response;
                            if (data && data.length > 0) {
                                cachelstEmpBySerivceObjs = data;
                                renderEmployeeList();

                                var service = cacheChildServiceObjs.find(c => c.id == serviceID);
                                var serviceInCard = cacheCardServiceObjs.find(c => c.id == serviceID);
                                if (service && serviceInCard) {
                                    var empID = service["techID"];
                                    var empInput = $('#lst-emp .item').find('input[data-id="' + empID + '"]');
                                    if (empInput) {
                                        $('#lst-emp .item h3').removeClass('h3-service-select');
                                        $(empInput).closest('.item').find('h3').addClass('h3-service-select');
                                        $('#lst-emp .item').find('h3 i').attr('hidden', true);
                                        $(empInput).closest('.item').find('h3 i').first().removeAttr('hidden');
                                        $(empInput).closest('.item').find('input[type=radio]').prop("checked", true);
                                    }

                                }
                                else {
                                    $('#lst-emp .item h3').removeClass('h3-service-select');
                                    $('#lst-emp .item').find('h3 i').attr('hidden', true);
                                }
                                debugger;
                                $('#modalEmployees').modal('show');

                            }
                        },
                        error: function (status) {

                        }
                    });


                }
                //$(tr).closest('tr').find('input[type=checkbox]').prop("checked", true).trigger("change");
                //$(tr).closest('tr').find('input[type=checkbox]').prop("checked", true).trigger("click");
            });

        }
    }
    function renderGirdCardChildService() {
        var template = $('#table-template-card').html();
        var render = "";
        $.each(cacheCardServiceObjs, function (i, item) {
            render += Mustache.render(template, {
                Id: item.id,
                Name: item.name,
                Time: item.time,
                SalePrice: item.salePrice,
                Image: item.imageName != null ? item.imageName : '/images/ourservice/01.jpg',
                TechID: item.techID,
                TechName: item.techName
            });

        });
        if (render != '' || (render == '' && cacheCardServiceObjs.length == 0)) {
            $('#tbl-content-card').html(render);

            //hoc sủa nagfy 18/05/2022  $('#tbl-content-card tr td a')
            $('#tbl-content-card tr td a').on('click', function () { //remve on card
               
                var serviceID = $(this).data('id');
               
                console.log("serviceID", serviceID)
                if (cacheCardServiceObjs.find(c => c.id == serviceID)) {
                    cacheCardServiceObjs = cacheCardServiceObjs.filter(c => c.id != serviceID);
                    calCardTotal();
                    var service = $('#tbl-content tr td div input[data-id=' + serviceID + ']');
                    $(service).prop("checked", false);
                    renderGirdCardChildService();
                    $(service).closest('div').removeClass('h3-service-select');
                    $(service).closest('div').find('i').attr('hidden', true);
                    //delete tech have choosed in service child, card
                    if (cacheChildServiceObjs.find(c => c.id == serviceID)) {
                        var index = cacheChildServiceObjs.findIndex(s => s.id == serviceID);
                        cacheChildServiceObjs[index]["techID"] = null;
                        cacheChildServiceObjs[index]["techName"] = null;
                    }

                }
            });
        }
    }
    function renderEmployeeList() {
        var template = $('#table-template-lst-emp').html();
        var render = "";
        $.each(cachelstEmpBySerivceObjs, function (i, item) {
            render += Mustache.render(template, {
                Name: item.technicianName,
                Id: item.technicianId,
                Avartar: item.technicianAvatar != null ? item.technicianAvatar : '/images/ourservice/01.jpg',
            });

        });

        if (render != '') {
            $('#lst-emp').html(render);
            $('#lst-emp .item').on('click', function () {
             
                $('#lst-emp .item h3').removeClass('h3-service-select');
                $(this).find('h3').addClass('h3-service-select');
                $('#lst-emp .item').find('h3 i').attr('hidden', true);
                $(this).find('h3 i').first().removeAttr('hidden');
                $(this).find('input[type=radio]').prop("checked", true);
                var item = $(this).find('input[type=radio]:checked');
                var empID = $(item).data('id');
                var empName = $(item).data('name');
                var serivce = serviceEmpID
                if (cacheChildServiceObjs.find(c => c.id == serviceEmpID)) {
                    var index = cacheChildServiceObjs.findIndex(s => s.id == serviceEmpID);
                    cacheChildServiceObjs[index]["techID"] = empID;
                    cacheChildServiceObjs[index]["techName"] = empName;
                }
                //add to card
                if (serviceEmpID) {

                    loadChildServicesCardData(serviceEmpID);
                    serviceElement = $('#tbl-content tr td div input[data-id=' + serviceEmpID + ']')
                    $(serviceElement).closest('div').addClass('h3-service-select');
                    $(serviceElement).closest('div').find('i').removeAttr('hidden');

                }


                if (cacheCardServiceObjs && cacheCardServiceObjs.find(c => c.id == serviceEmpID)) {
                    var index = cacheCardServiceObjs.findIndex(s => s.id == serviceEmpID);
                    cacheCardServiceObjs[index]["techID"] = empID;
                    cacheCardServiceObjs[index]["techName"] = empName;
                    renderGirdCardChildService();
                }

                $('#modalEmployees').modal('hide');
            });
        }
    }
    function renderFinishHeader() {
        var template = $('#table-template-finish').html();
        var render = "";
        var datatime = $('#datePick').val() + " " + timePickSuggest;
        let today = new Date(datatime)
        carddata.datePick = today.toLocaleString();
        var finishDate = new Date(carddata.datePick);
        var finishDateTime = timeServiceTotal == '' || timeServiceTotal == 0 || timeServiceTotal == null ? finishDate : new Date(finishDate.setMinutes(finishDate.getMinutes() + timeServiceTotal));
        var now = new Date();
        render += Mustache.render(template, {
            CusName: cusInfo.Name,
            CusPhone: cusInfo.PhoneNumber,
            CusEmail: cusInfo.Email,
            CusMessage: cusInfo.Note,
            TimePick: timePickSuggest != '' ? (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear() + ", " + today.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
                : (today.getMonth() + 1) + "/" + today.getDate() + "/" + today.getFullYear(),
            BookTime: (now.getMonth() + 1) + "/" + now.getDate() + "/" + now.getFullYear() + ", " + now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
            ESTFinishTime: timePickSuggest != '' ? (finishDateTime.getMonth() + 1) + "/" + finishDateTime.getDate() + "/" + finishDateTime.getFullYear() + ", " + finishDateTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
                : (finishDateTime.getMonth() + 1) + "/" + finishDateTime.getDate() + "/" + finishDateTime.getFullYear()
        });
        if (render != '') {
            $('#tb-finihs-content-header').html(render);


        }
    }

    function renderFinishBody() {
        var template = $('#table-template-finish-body').html();
        var render = "";
        $.each(cacheCardServiceObjs, function (i, item) {
            render += Mustache.render(template, {
                Id: item.serviceId,
                ServiceName: item.serviceName,
                ServicePrice: item.servicePrice,
                ServiceImage: item.serviceIcon != null ? item.serviceIcon : '~/images/ourservice/01.jpg',
                ServiceTechImage: '',
                ServiceTech: item.serviceTime == null || item.serviceTime == '' ? '' : item.serviceTime + "'",
                ServiceTechName: item.techName
            });

        });
        if (render != '') {
            $('#tb-finihs-content-body').html(render);

        }
    }

    function generateDateList(date, step) {
     
        var isToday = helper.isToday(date);
        //Mon - Sat: 9:30 am - 7:30 pm
        //Sunday: 11: 00 am - 5: 00 pm
        arrTime = [];
        //set start time
        var today = isToday ? new Date() : new Date(date);
        var formattedDate = today.getMonth() + "/" + today.getDate() + "/" + today.getFullYear()
        if (formattedDate == "10/23/2023") {
            $('#time-tab-next').attr('hidden', true);
            $('#lstTimePick').html("");
            return;
        }
        else {
            $('#time-tab-next').attr('hidden', false);
        }
        var hour = today.getHours() + 2; // add 2 hour for current time
        //Mon - Sat: 9: 30 am
        var dt = new Date(1970, 0, 1, 9, 0, 0, 0);
        dt.setMinutes(dt.getMinutes() + step);
        if (hour > 9) {
            dt = new Date(1970, 0, 1, hour, 0, 0, 0);

        }
        //set end time
        if (today.getDay() != 0) {

            while (dt.getHours() <= 18) {
                var point = dt.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
                dt.setMinutes(dt.getMinutes() + step);
                arrTime.push(point);


            }
        }

        else {
            // Sunday: 11: 00 am
            //if (today.getDay() == 0) {
            //    dt = new Date(1970, 0, 1, 11, 0, 0, 0);
            //}
            //while (dt.getHours() <= 17) {
            //    var point = dt.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
            //    dt.setMinutes(dt.getMinutes() + step);
            //    arrTime.push(point);
            //}  
            //arrTime.splice(-1, 1);
        }

        var template = $('#table-time-template').html();
        var render = "";
        $.each(arrTime, function (i, item) {
            render += Mustache.render(template, {
                Time: item,
            });

        });
        if (render != '' || (render == '' && arrTime.length == 0)) {
            $('#lstTimePick').html(render);
            //$('#lstTimePick li:first').on('click', function () {
            //triggerTimePick($(this));
            //}).click();

        }
        $('#lstTimePick li').on('click', function () {
            triggerTimePick($(this));
        });
    }
    function triggerTimePick(item) {
        if (item && item.length > 0) {
            $('#lstTimePick li').removeClass('select');
            item.addClass('select');
            $('#lstTimePick li span i').attr('hidden', true);
            item.find('i').first().removeAttr('hidden');
            var input = item.find('input');
            var vale = input.prop("checked");
            var time = input.data('val');
            timePickSuggest = time;
        }

    }
    $.fn.clearForm = function () {
        return this.each(function () {
            var type = this.type, tag = this.tagName.toLowerCase();
            if (tag == 'form')
                return $(':input', this).clearForm();
            if (type == 'text' || type == 'password' || tag == 'textarea')
                this.value = '';
            else if (type == 'checkbox' || type == 'radio')
                this.checked = false;
            else if (tag == 'select')
                this.selectedIndex = -1;
        });
    };
}
