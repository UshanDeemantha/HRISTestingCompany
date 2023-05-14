$(document).ready(function () {
    /*$('.log-out a.log-out-drop').css("line-height",$('.log-out').parents().height()+"px")*/
    $('.log-out-options').css("width", $('.log-out a').width() + 20 + "px");
    $('.af-col-exp').on("click", function () {
        $(this).find(".fa").toggleClass("fa-minus-circle fa-plus-circle");
        $(this).parents().find(".af-tiles").slideToggle();
    });
    $('.log-out a').on("click", function () {
        $(this).parents().find(".log-out-options").toggle();
    })
    $('.collapse').on('shown.bs.collapse', function (e) {
        $('.collapse').not(this).removeClass('in');
    });
    $('[data-toggle=collapse]').click(function (e) {
        $('[data-toggle=collapse]').parent('li').removeClass('active');
        $(this).parent('li').toggleClass('active');
    });
    $('[data-toggle="tooltip"]').tooltip();
    
    /* stick header*/
    var stickyNavTop = $('#stick-header').offset().top;
    $(window).scroll(function () {
        if ($(window).scrollTop() > stickyNavTop) {
            $('#stick-header').addClass('fixed');
        }
        else {
            $('#stick-header').removeClass('fixed');
        }
    });
})