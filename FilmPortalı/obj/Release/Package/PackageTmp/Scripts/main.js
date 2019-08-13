window.addEventListener('DOMContentLoaded', (event) => {
    $(".kind-container").hide();
});

$("#kinds").click(function () {
    $("#kind-container").toggle();
});
$("#mobile-kinds").click(function () {
    var i = $("#mobile-kinds i");
    $("#mobile-kind-container").toggle();
    changeClass(i, "fas fa-caret-up", "fas fa-caret-down", $("#mobile-kind-container").is(":visible"))
});
/* Profile */
$(".show-profile-list").click(function () {
    $(".profile-list").toggle();
    changeClass($(this), "fas fa-caret-up", "fas fa-caret-down", $(".profile-list").is(":visible"));
});



$(".search").focus(function () {
    $(".search-film-list").show("slow");
});
$(".search").blur(function () {
    $(".search-film-list").hide("slow");
});

/* Mobile Search Start */
$(".mobile-search-icon").click(function () {
    $("#mobile-search-container").show();
    $(this).hide();
    $("#mobile-search-input").focus();
});

$("#mobile-search-input").blur(function () {
    $("#mobile-search-container").hide();
    $(".mobile-search-icon").show();
});

$("#open-header").click(function () {
    $(".header-container").toggle();
    changeClass($(this), "fas fa-times", "fas fa-bars", $(".header-container").is(":visible"));
});
/* Mobile Search End */

function changeClass(view, c1, c2, status) {
    view.removeClass();
    if (status) {
        view.addClass(c1);
    } else {
        view.addClass(c2);
    }

}


//$("#yorums").on("click", "#comment-form-add", function (e) {

//    alert($(this).index());
//    var html = '<br /> <br /> <div class="yorum-input" > @Html.Hidden("filmId", Model.film.FId) @Html.TextArea("commentText", new { @class = "textarea", style = "width: 90%;resize: none;", placeholder = "yorum yaz...", rows = "5"}) < button type = "submit" style = "margin: auto;" > <i class="fas fa-share" style="font-size: 25px"></i></button > </div >';
//    html = "<h1>Test</h1>";
//    $(this).siblings()[1].find("span.blur-text").append(html);
//    e.preventDefault();

//});
function successSign(data) {
    PNotify.alert({
        text: data.message,
        type: (data.status == 1 ? "success" : "error")
    });

    if (data.status == 1)
        location.reload();
}
function successLogin(data) {
    PNotify.alert({
        text: data.message,
        type: (data.status == 1 ? "success" : "error")
    });
}

/* Giriþ */
$("#signin-form").submit(function () {

    //$("#signin-panel").html(data);
    $('.jquery-modal:visible').remove();

});
$("#login-form").submit(function () {

    //$("#signin-panel").html(data);
    $('.jquery-modal:visible').remove();

});

//Film Search
$(".search").keyup(function (e) {

    var val = $(this).val();
    var list = $(".search-film-list");
    if (val.trim() != "") {

        $.ajax({
            url: "/Partial/SearchFilm",
            data: { val },
            type: "GET",
            success: function (data) {
                list.html(data);
            },
            error: function (error) {
                console.log(error);
            }
        });

    } else {
        list.html("");
    }

});

//kayýt ekraný password eþitleme
$("#pass").keyup(function () {
    var npass = $("#npass");
    var pass1 = $(this).val().trim();
    var pass2 = npass.val().trim();
    isCorrect(pass1, pass2);
});

$("#npass").keyup(function () {
    var pass = $("#pass");
    var pass1 = $(this).val().trim();
    var pass2 = pass.val().trim();
    isCorrect(pass1, pass2);
});

function isCorrect(pass1, pass2) {
    var btn = $("#login-btn");
    if (pass1.length >= 6) {
        if (pass1 == pass2) {
            btn.attr("disabled", false);
            btn.removeClass("disable");
        } else {
            btn.attr("disabled", true);
            btn.addClass("disable");
        }
    }
}


// Ajax Error
function showAjaxError() {
    PNotify.error({
        text: "Bir Hata Olustu.Lutfen Daha Sonra Tekrar Deneyin."
    });
}

window.addEventListener("DOMContentLoaded", function () {

    //lazy load images
    showImage();

});

function showImage() {
    $(".lazy").each(function () {
        var _image = $(this);
        _image.removeClass("lazy");
        var image = new Image();
        image.src = $(this).data("src");
        image.onload = function () {
            _image.attr("src", $(this).attr("src"));
        }

    });
}



// Film Detay
$(".film-wrapper").on("mouseover",".film-box",function () {

    var index = $(this).index();
    $(".film-box:eq(" + index + ")").find(".overlay").slideDown("fast");
});

$(".film-wrapper").on("mouseleave", ".film-box", function () {
    var index = $(this).index();
    $(".film-box:eq(" + index + ")").find(".overlay").fadeOut("slow", function () {
        $(this).hide();
    });
});


// Rastgele Film

$(".random").click(function() {
    $.ajax({
        url: "/rastgele",
        type: "GET",
        success: function(data) {
            window.location.href = "/detay/" + data.filmName;
        }
    });
})