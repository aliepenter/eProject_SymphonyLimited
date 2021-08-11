/*jQ vertical scroll*/
var $el = $(".table-responsive");

function anim() {
    var st = $el.scrollTop();
    var sb = $el.prop("scrollHeight") - $el.innerHeight();
    $el.animate({ scrollTop: st < sb / 2 ? sb : 0 }, 10000, anim);
}

function stop() {
    $el.stop();
}
anim();
$el.hover(stop, anim);


//fix Menu
window.onscroll = function() { scrHeader() };

var header = document.getElementById("headerDefault");
var headerMobile = document.getElementById("headerMobile");
var sticky = header.offsetTop;

function scrHeader() {
    if (window.pageYOffset > sticky) {
        document.getElementById('headerDefault').classList.add('menufix');
    } else {
        document.getElementById("headerDefault").classList.remove('menufix');
    }
    if (window.pageYOffset > sticky) {
        document.getElementById('headerMobile').classList.add('menufix');
        document.getElementById('childMenu').classList.add('childMenufix');
    } else {
        document.getElementById("headerMobile").classList.remove('menufix');
        document.getElementById("childMenu").classList.remove('childMenufix');
    }
}

//menuMobile

var flagsMobile = true;

$("#menuBar").click(function() {
    if (flagsMobile == true) {
        $('#menu-ac').addClass('menuMobileActive');
        $('#opAll').addClass('opacityAll');
        flagsMobile = false;
    } else {
        $('#menu-ac').removeClass('menuMobileActive');
        $('#opAll').removeClass('opacityAll');
        flagsMobile = true;
    }
});

$("#opAll").click(function() {
    $('#menu-ac').removeClass('menuMobileActive');
    $('#opAll').removeClass('opacityAll');
    flagsMobile = true;
});

var flagChildBar = true
$("#childBar").click(function() {
    if (flagChildBar == true) {
        $('#childBarI').addClass('childBarActive');
        $('#childCourseCategory').addClass('childMenuMobileActiveDown');
        $('#childCourseCategory').removeClass('childMenuMobileActiveUp');
        flagChildBar = false;
    } else {
        $('#childBarI').removeClass('childBarActive');
        $('#childCourseCategory').addClass('childMenuMobileActiveUp');
        $('#childCourseCategory').removeClass('childMenuMobileActiveDown');

        flagChildBar = true;
    }
});
//$("#courseSubMenu").hover(
//    function () {
//        $("#subMenuCourse").css("display","block");
//    }, function () {
//        $("#subMenuCourse").css("display", "none");
//    }
//);var baseUrl = window.location.protocol + "//" + window.location.host + "/";

$('.slider-for').slick({
    slidesToShow: 1,
    //slidesToScroll:1,
    arrows: false,
    fade: true,
    asNavFor: '.slider-nav'
});
$('.slider-nav').slick({
    //slidesToScroll: 1,
    arrows: false,
    infinite: true,
    asNavFor: '.slider-for',
    focusOnSelect: true,
    variableWidth: true,
    autoplay: true,
    autoplaySpeed: 3000
});
var owl = $('.owl-carousel');
owl.owlCarousel({
    items: 5,
    loop: true,
    margin: 10,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true
});
$('.play').on('click', function () {
    owl.trigger('play.owl.autoplay', [1000])
})
$('.stop').on('click', function () {
    owl.trigger('stop.owl.autoplay')
})