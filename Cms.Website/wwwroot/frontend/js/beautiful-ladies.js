// menu mobile
$(document).on('click', '.btn-menu-mobile', function () {
    $('body').addClass('pace-done sidebar-open').prepend('<div class="app-backdrop backdrop-sidebar"></div>');
});
$(document).on('click', '.backdrop-sidebar', function () {
    $('body').removeClass('pace-done  sidebar-open');
    $('.backdrop-sidebar').remove();
});
$(document).on('click', '.btn-close-menu', function () {
    $('body').removeClass('pace-done  sidebar-open');
    $('.backdrop-sidebar').remove();
});
// menu footer
$(document).on('click', '.menu-footer .menu-link', function () {
    var $submenu = $(this).next('.footer-list');
    if ($submenu.length < 1)
        return;
    if ($submenu.is(":visible")) {
        $submenu.slideUp(function () {
            $('.menu-footer.open').removeClass('open');
        });
        $(this).removeClass('open');
        return;
    }
    $('.menu-footer .footer-list:visible').slideUp();
    $('.menu-footer .menu-link').removeClass('open');
    $submenu.slideToggle(function () {
        $('.menu-footer.open').removeClass('open');
    });
    $(this).addClass('open');
});
$(document).on('click', '.menu-mobile .megamenu.has-sub .menu-item-link', function () {
    var $submenu = $(this).next('.megamenu-menu');
    if ($submenu.length < 1)
        return;
    if ($submenu.is(":visible")) {
        $submenu.slideUp(function () {
            $('.menu-mobile .megamenu.has-sub.open').removeClass('open');
        });
        $(this).removeClass('open');
        return;
    }
    $('.menu-mobile .megamenu.has-sub  .megamenu-menu:visible').slideUp();
    $('.menu-mobile .megamenu.has-sub .menu-item-link').removeClass('open');
    $submenu.slideToggle(function () {
        $('.megamenu.has-sub.open').removeClass('open');
    });
    $(this).addClass('open');
});
$(document).on('click', '.megamenu-sub.has-sub-sub .menu-item-link', function () {
    var $submenu = $(this).next('.megamenu-menu-sub');
    if ($submenu.length < 1)
        return;
    if ($submenu.is(":visible")) {
        $submenu.slideUp(function () {
            $('.megamenu-sub.has-sub-sub.open').removeClass('open');
        });
        $(this).removeClass('open');
        return;
    }
    $('.megamenu-sub.has-sub-sub .megamenu-menu-sub:visible').slideUp();
    $('.megamenu-sub.has-sub-sub .menu-item-link').removeClass('open');
    $submenu.slideToggle(function () {
        $('.megamenu-sub.has-sub-sub.open').removeClass('open');
    });
    $(this).addClass('open');
});
// Tab
function openProTabs(evt, cityName) {
    var i, pro_tabcontent, pro_tablinks;
    pro_tabcontent = document.getElementsByClassName("pro-tabcontent");
    for (i = 0; i < pro_tabcontent.length; i++) {
        pro_tabcontent[i].style.display = "none";
    }
    pro_tablinks = document.getElementsByClassName("pro-tablinks");
    for (i = 0; i < pro_tablinks.length; i++) {
        pro_tablinks[i].className = pro_tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
if ($('#defaultOpenProTabs').length) {
    document.getElementById("defaultOpenProTabs").click();
}
function openTab(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
if ($('#defaultOpen').length) {
    document.getElementById("defaultOpen").click();
}
// show hình ảnh page product detail
$(document).ready(function () {
    if ($('#zoom').length) {
        $('#zoom').ezPlus({
            zoomWindowFadeIn: 500,
            zoomLensFadeIn: 500,
            gallery: 'gallery_01',
            imageCrossfade: true,
            zoomWindowWidth: 411,
            zoomWindowHeight: 274,
            zoomWindowOffsetX: 10,
            scrollZoom: true,
            cursor: 'pointer',
            galleryActiveClass: 'active',
            responsive: true,
            loadingIcon: true
        });
        $('#zoom').bind('click', function (e) {
            var ez = $('#zoom').data('ezPlus');
            $.fancyboxPlus(ez.getGalleryList());
            return false;
        });
    }
});
$(document).ready(function () {
    $("#slider-product").owlCarousel({
        items: 4,
        itemsDesktop: [1000, 4],
        itemsDesktopSmall: [900, 3],
        itemsTablet: [600, 2],
        itemsMobile: [400, 1],
        navigation: true,
        pagination: false,
        autoPlay: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    
    $("#slider-product-gift").slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 3,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 3
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
    
    // $("#slider-product-gift").owlCarousel({
    //     stagePadding: 50,
    //     loop: true,
    //     margin: 10,
    //     nav: true,
    //     responsive: {
    //         0: {
    //             items: 1
    //         },
    //         600: {
    //             items: 3
    //         },
    //         1000: {
    //             items: 5
    //         }
    //     },
    //     navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    //     // stagePadding: 5000,
    //     // loop: true,
    //     // margin: 100,
    //     // items: 4,
    //     // itemsDesktop: [1000, 1],
    //     // itemsDesktopSmall: [900, 1],
    //     // itemsTablet: [600, 1],
    //     // itemsMobile: false,
    //     // navigation: true,
    //     // pagination: false,
    //     // autoPlay: false,
    //     // slideSpeed: 1000,
    //     // paginationSpeed: 1000,
    //     // navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    // });
    $("#zoom-gallery").owlCarousel({
        items: 1,
        itemsDesktop: [1000, 1],
        itemsDesktopSmall: [900, 1],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: true,
        autoPlay: 2000,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-home-main-slider").owlCarousel({
        items: 1,
        itemsDesktop: [1000, 1],
        itemsDesktopSmall: [900, 1],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: false,
        pagination: true,
        slideSpeed: 1000,
        autoPlay: 5000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#ProductThumbs").owlCarousel({
        items: 4,
        itemsDesktop: [992, 4],
        itemsDesktopSmall: [992, 3],
        itemsTablet: [768, 2],
        itemsMobile: [576, 1],
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#home-brand-slider").owlCarousel({
        items: 5,
        itemsDesktop: [992, 5],
        itemsDesktopSmall: [992, 3],
        itemsTablet: [768, 2],
        itemsMobile: [576, 1],
        navigation: false,
        pagination: false,
        slideSpeed: 300,
        paginationSpeed: 400,
        autoPlay: false,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#home-news-slider").owlCarousel({
        items: 3,
        itemsDesktop: [992, 3],
        itemsDesktopSmall: [992, 2],
        itemsTablet: [768, 2],
        itemsMobile: [576, 1],
        navigation: true,
        pagination: false,
        slideSpeed: 300,
        paginationSpeed: 400,
        autoplay: false,
        autoplayTimeout: 5000,
        autoplayHoverPause: false,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#home-promotion-slider").owlCarousel({
        items: 4,
        itemsDesktop: [992, 4],
        itemsDesktopSmall: [992, 3],
        itemsTablet: [769, 2],
        itemsMobile: [576, 1],
        navigation: true,
        pagination: false,
        slideSpeed: 300,
        paginationSpeed: 400,
        autoplay: false,
        autoplayTimeout: 5000,
        autoplayHoverPause: false,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider1").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider2").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider3").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider4").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider5").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider6").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider7").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider8").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider9").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
    $("#owl-blog-single-slider10").owlCarousel({
        items: 2,
        itemsDesktop: [1000, 2],
        itemsDesktopSmall: [900, 2],
        itemsTablet: [600, 1],
        itemsMobile: false,
        navigation: true,
        pagination: false,
        slideSpeed: 1000,
        paginationSpeed: 1000,
        navigationText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>']
    });
});
// slide 
var acc = document.getElementsByClassName("accordion");
var i;
for (i = 0; i < acc.length; i++) {
    acc[i].onclick = function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
        }
    }
}
if ($(window).width() > 992) {
    $('.accordion.col-sb-trigger').trigger('click');
}

$(function () {
    //defining all needed variables
    var $overlay = $('.overlay');
    var $mainPopUp = $('.main-popup')
    var $signIn = $('#sign-in');
    var $register = $('#register');
    var $formSignIn = $('form.sign-in');
    var $formRegister = $('form.register');
    var $firstChild = $('nav ul li:first-child');
    var $secondChild = $('nav ul li:nth-child(2)');
    var $thirdChild = $('nav ul li:nth-child(3)');
    //defining function to create underline initial state on document load
    function initialState() {
        $('.underline').css({
            "width": $firstChild.width(),
            "left": $firstChild.position().left,
            "top": $firstChild.position().top + $firstChild.outerHeight(true) + 'px'
        });
    }
    initialState(); //() used after calling function to call function immediately on doc load
    //defining function to change underline depending on which li is active
    function changeUnderline(el) {
        $('.underline').css({
            "width": el.width(),
            "left": el.position().left,
            "top": el.position().top + el.outerHeight(true) + 'px'
        });
    } //note: have not called the function...don't want it called immediately
    $firstChild.on('click', function () {
        var el = $firstChild;
        changeUnderline(el); //call the changeUnderline function with el as the perameter within the called function
        $secondChild.removeClass('active');
        $thirdChild.removeClass('active');
        $(this).addClass('active');
    });
    $secondChild.on('click', function () {
        var el = $secondChild;
        changeUnderline(el); //call the changeUnderline function with el as the perameter within the called function
        $firstChild.removeClass('active');
        $thirdChild.removeClass('active');
        $(this).addClass('active');
    });
    $thirdChild.on('click', function () {
        var el = $thirdChild;
        changeUnderline(el); //call the changeUnderline function with el as the perameter within the called function
        $firstChild.removeClass('active');
        $secondChild.removeClass('active');
        $(this).addClass('active');
    });
    $('.login-trigger').on('click', function () {
        $overlay.addClass('visible');
        $mainPopUp.addClass('visible');
        $signIn.addClass('active');
        $register.removeClass('active');
        $formRegister.removeClass('move-left');
        $formSignIn.removeClass('move-left');
    });
    $overlay.on('click', function () {
        $(this).removeClass('visible');
        $mainPopUp.removeClass('visible');
    });
    $('#popup-close-button').on('click', function (e) {
        e.preventDefault();
        $overlay.removeClass('visible');
        $mainPopUp.removeClass('visible');
    });
    $signIn.on('click', function () {
        $signIn.addClass('active');
        $register.removeClass('active');
        $formSignIn.removeClass('move-left');
        $formRegister.removeClass('move-left');
    });
    $register.on('click', function () {
        $signIn.removeClass('active');
        $register.addClass('active');
        $formSignIn.addClass('move-left');
        $formRegister.addClass('move-left');
    });
    $('input').on('submit', function (e) {
        e.preventDefault(); //used to prevent submission of form...remove for real use
    });
});
// show giỏ hàng
$(function () {

    $('.facebookLink').attr('href', 'https://www.facebook.com/louginailsandspa?mibextid=ZbWKwL');
    $('.instagramLink').attr('href', 'https://www.instagram.com/louginailsandspa_?igshid=OGQ5ZDc2ODk2ZA==');
    $('.googleLink').attr('href', 'https://g.co/kgs/WXyZsC');

    // ngày 2/2/2024
    $('#homehourscontent').html(`<li>Mon – Fri: 9:00 AM- 7:00 PM </li>
                            <li>Sat: 9:00 AM-6:00 PM </li>
                            <li>Sun: Closed</li>`);
    $('#Copyrightsid').html(` <div class="footer-bottom-contact">
                            Copyrights © 2023 by <a target="_blank" href="https://louginailsspa.com">Lougi Nails Spa</a>. <a target="_blank"
                        </div>`);
    

    $('.googleLink').attr('href', 'tel:3173608888');
    $('.phone-number').html(`<a class="all_onlinelink" href="tel:3173608888"><i class= "fa fa-volume-control-phone" ></i ><span>(303) 288-6999</span></as>`);
    $('.footer-list_content').html(`<li>
                            <i class="ti-home" aria-hidden="true"></i>
                            <p>
                                Address: 4952 E 62nd Ave Unit A5, Commerce City, CO 80022
                            </p>
                        </li>
                        <li>
                            <i class="fa fa-volume-control-phone" aria-hidden="true"></i>
                            <p>
                                Phone:
                                <a class="all_onlinelink" href="">(303) 288-6999</a>
                            </p>
                        </li>
                        <li>
                            <i class="ti-email" aria-hidden="true"></i>
                            <p>
                                Email :
                                <a href="mailto: info@labrisenaillounge.com">info@louginailsspa.com</a>
                            </p>
                        </li>
                        <li>
                            <i class="ti-world" aria-hidden="true"></i>
                            <p>
                                Website :
                                <a href="https://louginailsspa.com">louginailsspa.com</a>
                            </p>
                        </li>` );
    $('.footer-list_map').html(`<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3064.902008978786!2d-104.9291892!3d39.80918979999999!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x876c7a19b10a55a1%3A0x2f1d5418ba40bc23!2s4952%20E%2062nd%20Ave%20Unit%20A5%2C%20Commerce%20City%2C%20CO%2080022%2C%20USA!5e0!3m2!1sen!2s!4v1703995430403!5m2!1sen!2s"
                                width="300" height="250" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>`);
    $('head').append(`<meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Lougi Nails Spa - Zionsville Indianapolis</title>
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
    <meta name="description" content="La Brise Nail Lounge more than just a nails salon we are your sanctuary for pampering, relaxation, and rejuvenation. Nestled in the heart of Indianapolis, our luxurious salon offers an array of services that will leave you feeling refreshed, revitalized, and ready to conquer the world." />
    <meta name="keywords" content="Nails near me, Nails salon near me, Nails salon in Zionsville, La Brise Nail Lounge, manicure, pedicure, dipping nails, acrylic nails, nails design, fullset nails, pretty nails, Good nails near me, Best nails near me, Nails in Indianapolis" />
    <meta name="news_keywords" content="Nail salon near me, Pedicure, Acrylic, Dip & more, Waxing, Eyelashes, Facial, Drinks, Massage near me" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="generator" content="nopCommerce" />
    <meta content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=5, user-scalable=1" name="viewport" />

    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-title" content="labrisenaillounge.com" />
    <!-- META FOR FACEBOOK -->
    <meta property="og:site_name" content="labrisenaillounge.com" />
    <meta property="og:rich_attachment" content="true" />
    <meta property="og:type" content="website" />
    <meta property="article:publisher" content="https://www.facebook.com/profile.php?id=61550569873322&mibextid=LQQJ4d" />
    <meta property="og:url" itemprop="url" content="https://labrisenaillounge.com/" />
    <meta property="og:image" itemprop="thumbnailUrl" content="https://labrisenaillounge.com/favicon.png" />
    <meta property="og:image:width" content="200" />
    <meta property="og:image:height" content="200" />
    <meta content="La Brise Nail Lounge - Zionsville Indianapolis" itemprop="headline" property="og:title" />
    <meta content="La Brise Nail Lounge more than just a nails salon we are your sanctuary for pampering, relaxation, and rejuvenation. Nestled in the heart of Indianapolis, our luxurious salon offers an array of services that will leave you feeling refreshed, revitalized, and ready to conquer the world." itemprop="description" property="og:description" />
    <!-- END META FOR FACEBOOK -->
    <meta content="news" itemprop="genre" name="medium" />
    <meta content="en-US" itemprop="inLanguage" />
    <meta content="" itemprop="articleSection" />
    <meta content="La Brise Nail Lounge - Zionsville Indianapolis" itemprop="sourceOrganization" name="source" />
    <meta name="copyright" content="La Brise Nail Lounge - Zionsville Indianapolis" />
    <meta name="author" content="Trầm Mộc Hương Trầm Tiên Phước Quảng Nam" />
    <meta name="robots" content="noarchive,index,follow" />
    <meta name="googlebot" content="noarchive" />
    <meta name="geo.placename" content="Zionsville, Indianapolis" />
    <meta name="geo.region" content="US-IN" />
    <meta name="geo.position" content="39.997194251289365, -86.26321234722096" />
    <meta name="ICBM" content="39.997194251289365, -86.26321234722096" />
    <meta name="revisit-after" content="days" />
    <!-- Twitter Card -->
    <meta name="twitter:card" value="summary" />
    <meta name="twitter:url" content="https://labrisenaillounge.com/" />
    <meta name="twitter:title" content="La Brise Nail Lounge - Zionsville Indianapolis" />
    <meta name="twitter:description" content="La Brise Nail Lounge more than just a nails salon we are your sanctuary for pampering, relaxation, and rejuvenation. Nestled in the heart of Indianapolis, our luxurious salon offers an array of services that will leave you feeling refreshed, revitalized, and ready to conquer the world." />
    <meta name="twitter:image" content="https://labrisenaillounge.com/favicon.png" />
    <!-- End Twitter Card -->
`);
   
    
});
jQuery(document).ready(function () {
   
    
    var offset = 220;
    var duration = 500;
    jQuery('#back-to-top').fadeOut(duration);
    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop() > offset) {
            jQuery('#back-to-top').fadeIn(duration);
        } else {
            jQuery('#back-to-top').fadeOut(duration);
        }
    });
    jQuery('#back-to-top').click(function (event) {
        event.preventDefault();
        jQuery('html, body').animate({
            scrollTop: 0
        }, duration);
        return false;
    });
    window.onscroll = changePos;
    function changePos() {
        var header = $("#header");
        var headerheight = $("#header").height();
        if (window.pageYOffset > headerheight) {
            header.addClass('scrolldown');
        } else {
            header.removeClass('scrolldown');
        }
    }
});

