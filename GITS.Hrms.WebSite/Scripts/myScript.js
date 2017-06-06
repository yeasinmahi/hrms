function openNav() {
    $(".sidenav").css("width", "20%");
    $("#main").css("width", "80%");
    $("#mainMenu").css("width", "45%");
    $("#subMenu").css("width", "55%");
    $("#closebtn").removeClass("hidden");
    $("#openNav").addClass("hidden");
    $(".hideText").show();
    $(".icon").css("width", "20px");
    $("#subMenu").fadeIn();
    $.cookie("sideNav", 1);
//    $("#main").css("background-color", "rgba(0,0,0,0.4)");
}

function closeNav() {
    $(".sidenav").css("width", "4%");
    $("#main").css("width", "96%");
    $("#mainMenu").css("width","100%");
    $("#subMenu").css("width","0%");
    $("#openNav").removeClass("hidden");
    $("#closebtn").addClass("hidden");
    $(".hideText").hide();
    $(".icon").css("width", "30px");
    $("#subMenu").fadeOut();
    $.removeCookie("sideNav");
//    $("#main").css("background-color", "white");
}

function slideUp() {
    $.cookie("slide", 1);
    $("#slideUp").hide();
    $("#slideDown").show();
    $("header").slideUp();
    //        $(".row.content").css("height", "88%");
    $(".row.content").animate({
        height: "87.5%"
    }, 400);
}

function slideDown() {
    $.removeCookie("slide");
    $("#slideUp").show();
    $("#slideDown").hide();
    $("header").slideDown();
    //        $(".row.content").css("height", "75%");
    $(".row.content").animate({
        height: "76%"
    }, 400);
}

$(document).ready(function() {
    $(".myButton, .myButton li, .myButton li a").removeClass('level1 static');
    $(".myButton, .myButton li").removeAttr('style');
    $(".myButton li a").addClass("btn btn-primary btn-xs");
    $(".myButton li a img").css("cssText", "width: 20px !important;");
    $(".myButton li a img").css("height", "20px");

    if (!!$.cookie('sideNav')) {
        openNav();
    } else {
        closeNav();
    }
    if (!!$.cookie('slide')) {
        slideUp();
    } else {
        slideDown();
    }

    
});

function pageLoad(sender, args) {
    $(document).ready(function () {
        $(".myButton, .myButton li, .myButton li a").removeClass('level1 static');
        $(".myButton, .myButton li").removeAttr('style');
        $(".myButton li a").addClass("btn btn-primary btn-xs");
        $(".myButton li a img").css("cssText", "width: 20px !important;");
        $(".myButton li a img").css("height", "20px");

    });
}