window.addEventListener('DOMContentLoaded', (event) => {
    $(".kind-container").hide();
});

$("#kinds").click(function(){
    $("#kind-container").toggle();
}); 
$("#mobile-kinds").click(function(){
    var i=$("#mobile-kinds i");
    $("#mobile-kind-container").toggle();
    changeClass(i,"fas fa-caret-up","fas fa-caret-down",$("#mobile-kind-container").is(":visible"))
}); 
/* Profile */
$(".show-profile-list").click(function(){
    $(".profile-list").toggle();
    changeClass($(this),"fas fa-caret-up","fas fa-caret-down",$(".profile-list").is(":visible"));
});

$('.owl-carousel').owlCarousel({
    loop:true,
    margin:15,
    dots:true,
    autoplay:true,
    autoplayTimeout:3000,
    autoplayHoverPause:true,
    responsiveClass:true,
    responsive:{
        0:{
            items:1,
            nav:true
        },
        600:{
            items:3,
            nav:true
        },
        1000:{
            items:6,
            nav:true,
            loop:true
        }
    }
});

$(".search").focus(function(){
    $(".search-film-list").css("display","block");
});
$(".search").blur(function(){
    $(".search-film-list").css("display","none");
});

/* Mobile Search */
$(".mobile-search-icon").click(function(){
    $("#mobile-search-container").show();
    $(this).hide();
    $("#mobile-search-input").focus();
});

$("#mobile-search-input").blur(function(){
    $("#mobile-search-container").hide();
    $(".mobile-search-icon").show();
});
/* Mobile Search */
$("#watch").click(function(){
    $("#source").toggle();
});

$("#open-header").click(function(){
    $(".header-container").toggle();
    changeClass($(this),"fas fa-times","fas fa-bars",$(".header-container").is(":visible"));
});


$(".profile-btns a").click(function(event){
    event.preventDefault();
    $(this).parent().children().addClass("active");
    $(this).parent().siblings().children().removeClass("active");
    var tab=$(this).attr("href");
    $(".profile-wrapper").not(tab).css("display","none");
    $(tab).fadeIn();
});



function changeClass(view,c1,c2,status){
    view.removeClass();
    if(status){
        view.addClass(c1);
    }else{
        view.addClass(c2);
    }

}