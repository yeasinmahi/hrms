function openNav() {
    $(".sidenav").css("width", "20%");
    $("#main").css("width", "80%");
    $("#mainMenu").css("width", "45%");
    $("#subMenu").css("width", "55%");
    $("#closebtn").toggleClass("hidden");
    $("#openNav").toggleClass("hidden");
    $(".hideText").show();
    $(".icon").css("width", "20px");
    $("#subMenu").fadeIn();
//    $("#main").css("background-color", "rgba(0,0,0,0.4)");
}

function closeNav() {
    $(".sidenav").css("width", "4%");
    $("#main").css("width", "96%");
    $("#mainMenu").css("width","100%");
    $("#subMenu").css("width","0%");
    $("#openNav").toggleClass("hidden");
    $("#closebtn").toggleClass("hidden");
    $(".hideText").hide();
    $(".icon").css("width", "30px");
    $("#subMenu").fadeOut();
//    $("#main").css("background-color", "white");
}

function slideUp() {
    $("#slideUp").hide();
    $("#slideDown").show();
    $("header").slideUp();
    //        $(".row.content").css("height", "88%");
    $(".row.content").animate({
        height: "87.4%"
    }, 400);
}

function slideDown() {
    $("#slideUp").show();
    $("#slideDown").hide();
    $("header").slideDown();
    //        $(".row.content").css("height", "75%");
    $(".row.content").animate({
        height: "75%"
    }, 400);
}

$(document).ready(function() {
    $(".myButton, .myButton li, .myButton li a").removeClass('level1 static');
    $(".myButton, .myButton li").removeAttr('style');

    $(".myButton li a").addClass("btn btn-primary btn-xs");

});
