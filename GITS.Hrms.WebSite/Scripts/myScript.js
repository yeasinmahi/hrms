function openNav() {
    $(".sidenav").css("width", "24%");
    $("#main").css("width", "76%");
    $("#mainMenu").css("width", "50%");
    $("#subMenu").css("width", "50%");
    $("#closebtn").toggleClass("hidden");
    $("#openNav").toggleClass("hidden");
    $(".hideText").show();
    $(".icon").css("width", "20px");
    $("#subMenu").fadeIn();
//    $("#main").css("background-color", "rgba(0,0,0,0.4)");
}

function closeNav() {
    $(".sidenav").css("width", "6%");
    $("#main").css("width", "94%");
    $("#mainMenu").css("width","100%");
    $("#subMenu").css("width","0%");
    $("#openNav").toggleClass("hidden");
    $("#closebtn").toggleClass("hidden");
    $(".hideText").hide();
    $(".icon").css("width", "30px");
    $("#subMenu").fadeOut();
//    $("#main").css("background-color", "white");
}

function init() {
    $("#slideUp").click(function () {
        $("header").slideUp();
//        $(".row.content").css("height", "88%");
        $(".row.content").animate({
            height: "88%"
        }, 400);
    });
    $("#slideDown").click(function () {
        $("header").slideDown();
//        $(".row.content").css("height", "75%");
        $(".row.content").animate({
            height: "75%"
        }, 400);
    });

//    $("header").delay(5000).fadeOut('slow');
//    $(".row.content").delay(5000).css("height", "89%");
}