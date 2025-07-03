$(function () {

    //Filter foodtems
    $('#filterIcon').on("click",function () {
        $('.filterPanel').slideToggle();
    });

    // Enter key triggers search form
    $('#searchIcon').on("click",function () {
            $('.searchForm')[0].submit();
    });


   
 //mouse zoom to Food container
$('.Food-items').on('mouseenter touchstart', function () {
        $(this).addClass('zoom');
}).on('mouseleave touchend', function () {
        $(this).removeClass('zoom');
});
    
});


