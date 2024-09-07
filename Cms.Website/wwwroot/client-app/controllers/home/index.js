var HomeController = function () {
    this.initialize = function () {
        var firstStore = $('.nav-pills li:first-child');
        if (firstStore) {
            $(firstStore).trigger('click');
            var store = $(firstStore).data('store');
            loadGallery(store);
            loadDescription(store);
        }
        $('.nav-pills li').click(function () {
            $('.nav-link').removeClass('active');
            $(this).find('a').addClass('active');
            var store = $(this).data('store');
            loadGallery(store);
            loadDescription(store);
        });
        //jQuery('#rev_slider_4').show().revolution({
        //    dottedOverlay: 'none',
        //    delay: 5000,
        //    startwidth: 865,
        //    startheight: 450,

        //    hideThumbs: 200,
        //    thumbWidth: 200,
        //    thumbHeight: 50,
        //    thumbAmount: 2,

        //    navigationType: 'thumb',
        //    navigationArrows: 'solo',
        //    navigationStyle: 'round',

        //    touchenabled: 'on',
        //    onHoverStop: 'on',

        //    swipe_velocity: 0.7,
        //    swipe_min_touches: 1,
        //    swipe_max_touches: 1,
        //    drag_block_vertical: false,

        //    spinner: 'spinner0',
        //    keyboardNavigation: 'off',

        //    navigationHAlign: 'center',
        //    navigationVAlign: 'bottom',
        //    navigationHOffset: 0,
        //    navigationVOffset: 20,

        //    soloArrowLeftHalign: 'left',
        //    soloArrowLeftValign: 'center',
        //    soloArrowLeftHOffset: 20,
        //    soloArrowLeftVOffset: 0,

        //    soloArrowRightHalign: 'right',
        //    soloArrowRightValign: 'center',
        //    soloArrowRightHOffset: 20,
        //    soloArrowRightVOffset: 0,

        //    shadow: 0,
        //    fullWidth: 'on',
        //    fullScreen: 'off',

        //    stopLoop: 'off',
        //    stopAfterLoops: -1,
        //    stopAtSlide: -1,

        //    shuffle: 'off',

        //    autoHeight: 'off',
        //    forceFullWidth: 'on',
        //    fullScreenAlignForce: 'off',
        //    minFullScreenHeight: 0,
        //    hideNavDelayOnMobile: 1500,

        //    hideThumbsOnMobile: 'off',
        //    hideBulletsOnMobile: 'off',
        //    hideArrowsOnMobile: 'off',
        //    hideThumbsUnderResolution: 0,


        //    hideSliderAtLimit: 0,
        //    hideCaptionAtLimit: 0,
        //    hideAllCaptionAtLilmit: 0,
        //    startWithSlide: 0,
        //    fullScreenOffsetContainer: ''
        //});
    }
    function loadGallery(nailStoreId) {
        var componentData = {};
        componentData.pageSize = 6;
        componentData.pageIndex = 1;
        $.ajax({
            url: window.location.origin + "/AjaxContent/StoreGalleryIndex?nailStoreId=" + nailStoreId,
            type: "get",
            dataType: "json",
            beforeSend: function (x) {

            },
            complete: function (result) {
                if (result.responseText && result.responseText.length > 0) {
                    $(".store-gallery-index").html(result.responseText);
                    $("#owl-home-main-slider-showroom").owlCarousel({
                        items: 1,
                        itemsDesktop: [1000, 1],
                        itemsDesktopSmall: [900, 1],
                        itemsTablet: [600, 1],
                        itemsMobile: false,
                        navigation: true,
                        pagination: false,
                        slideSpeed: 1000,
                        autoPlay: 2000,
                        paginationSpeed: 1000,
                        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
                    });
                }
                else {

                }

            }
        });
    };
    function loadDescription(nailStoreId) {
        var componentData = {};
        componentData.pageSize = 6;
        componentData.pageIndex = 1;
        $.ajax({
            url: window.location.origin + "/AjaxContent/StoreDescriptionIndex?nailStoreId=" + nailStoreId,
            type: "get",
            dataType: "json",
            beforeSend: function (x) {

            },
            complete: function (result) {
                if (result.responseText && result.responseText.length > 0) {
                    $(".showroom-des").html(result.responseText);
                    
                }
                else {

                }

            }
        });
    };
    
}