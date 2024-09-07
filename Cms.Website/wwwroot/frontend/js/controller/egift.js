var EgiftController = function () {
    isFinish = false;
    isReset = true;

    var carddata = {
        services: "",
        cusInfo: {}
    }
    var cusInfo = {

    }
    var cacheCardegiftObjs = []; //list  services add into card
    this.initialize = function () {
        registerEvents();
        //egift.id = $(this).attr("data-id");
        //egift.name = $(this).attr("data-name");
        //egift.urln = $(this).attr("data-url");
        //egift.Price = $(this).attr("data-pice");
        $('#cart_product_discount_value').html('$');
        $('#cart_tax').html('$');
        $('#cart_payment_total').html('$');

    }

    function registerEvents() {


        //$('#datePick').val(formattedDate);

        $('#datePick').datepicker({
            language: 'en',
            minDate: new Date(),
            dateFormat: 'mm/dd/yyyy',

        })

        $('#datePick').datepicker().data('datepicker').selectDate(new Date());
     
        $.validator.addMethod("customemail",
            function validateEmail(email) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(String(email).toLowerCase());
            },
            "Sorry, I've enabled very strict email validation"
        );
        $.validator.addMethod("noSpace",
            function (value, element) {
                return value.trim().length == 0;
            }, "No space please and don't leave it empty"
        );
        $('#frmpayment').validate({
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

                RecipientName: { required: true },
                RecipientPhoneNumber: { required: true },
                RecipientEmail: { required: true, customemail: true },
                RecipientNote: {},
            },
            messages:
            {
                Name: { required: "Enter Name" },
                PhoneNumber: { required: "Enter the Phone Nubmer" },
                Email: { required: "Enter the Email" },

                RecipientName: { required: "Enter Name" },
                RecipientPhoneNumber: { required: "Enter the Phone Nubmer" },
                RecipientEmail: { required: "Enter the Email" }

            },
            //errorContainer: "#validationSummary",
            //errorLabelContainer: "#validationSummary ul",
            //wrapper: "li",
            submitHandler: function (form) {

            }
        });
        $('#btnCheckOut').on('click', function (e) {

            if (isFinish) {

                $("#frmpayment").clearForm();
                $("#yourname").focus();
                isFinish = false;
                isReset = true;

            }

            $('#btnCheckOut').trigger('submit');
            if ($("#frmpayment").valid()) {
                var paramsFromForm = {};
                $.each($("#frmpayment").serializeArray(), function (index, value) {
                    paramsFromForm[value.name] = paramsFromForm[value.name] ? paramsFromForm[value.name] || value.value : value.value;
                });
                cusInfo = paramsFromForm;
                if (!cacheCardegiftObjs || (cacheCardegiftObjs && cacheCardegiftObjs.length <= 0)) {
                    e.preventDefault();
                    return false;
                }
                else {
                    carddata.services = cacheCardegiftObjs;
                    carddata.cusInfo = cusInfo;
                    var datatime = $('#datePick').val();
                    let today = new Date(datatime)
                    carddata.cusInfo.datePick = today.toLocaleString();
                    $.ajax({
                        type: "POST",
                        url: '/egift/add',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(carddata),
                        beforeSend: function () {
                            helper.startLoading();
                        },
                        success: function (response) {
                            isFinish = true;
                            helper.stopLoading();

                        },
                        failure: function (response) {
                            alert(response.responseText);
                            helper.stopLoading();
                        },
                        error: function (response) {
                            alert(response.responseText);
                            helper.stopLoading();
                        },
                        complete: function () {
                            helper.stopLoading();
                        }
                    });
                }
            }

        });
        $("#place-order").click(function () {
            if (cacheCardegiftObjs && cacheCardegiftObjs.length > 0) {
                $("#frm-payment").toggle();
                $("#errServiceStep").html('');
            }
            else {
                $("#errServiceStep").html('please add a gift into your card');

            }
        });

        $('.egiftimage').on('click', function (e) {
            $('#txtName').html($(this).attr("data-id"));
            $('#txtPrice').html($(this).attr("data-pice"));
            $('#txtimg').attr("src", $(this).attr("data-url"));
            $('#txtaddid').val($(this).attr("data-id"));
            $('#txtaddName').val($(this).attr("data-name"));
            $('#txtaddurl').val($(this).attr("data-url"));
            $('#txtaddSalePrice').val($(this).attr("data-pice"));
        });
        $('#add-egift').click(function () {
            var egift = {};
            if (!cacheCardegiftObjs.find(c => c.id == $('#txtaddid').val())) {
                egift.Quantity = $('#Quantity').val();
                egift.id = $('#txtaddid').val();
                egift.urln = $('#txtaddurl').val();
                egift.Price = parseInt($('#txtaddSalePrice').val());
                egift.SalePrice = parseInt($('#txtaddSalePrice').val());

                egift.Name = $('#txtaddName').val();
                cacheCardegiftObjs.push(egift);
            }
            else {
                var id = $('#txtaddid').val();
                var tempProcessUser = $.grep(cacheCardegiftObjs, function (v) {
                    return v.id == id;
                });
                if (tempProcessUser != null && tempProcessUser.length > 0 && tempProcessUser != undefined) {
                    tempProcessUser[0].Quantity = parseInt(tempProcessUser[0].Quantity) + parseInt($('#Quantity').val());
                    tempProcessUser[0].Price = parseInt(tempProcessUser[0].Quantity) * parseInt($('#txtaddSalePrice').val());
                }

            }

            renderGirdCardegift()
        });
    }
    function renderGirdCardegift() {
        var template = $('#table-template-egiftcard').html();
        var render = "";
        var total = 0;
        $.each(cacheCardegiftObjs, function (i, item) {
            total = total + item.Price;
            render += Mustache.render(template, {
                Id: item.id,
                urlimg: item.urln,
                Price: item.Price,
                Quantity: item.Quantity
            });
        });
        if (render != '' || (render == '' && cacheCardegiftObjs.length == 0)) {
            $('#lstegiftcard').html(render);
            $('#cart_subtotal').html(total);
            $('#cart_product_discount_value').html('$0');
            $('#cart_tax').html('$0');
            $('#cart_payment_total').html('$' + total);

            $('#lstegiftcard li div a').on('click', function () {
                var serviceID = $(this).data('id');
                if (cacheCardegiftObjs.find(c => c.id == serviceID)) {
                    cacheCardegiftObjs = cacheCardegiftObjs.filter(c => c.id != serviceID);
                    renderGirdCardegift();
                }
            });
        }
    }
}