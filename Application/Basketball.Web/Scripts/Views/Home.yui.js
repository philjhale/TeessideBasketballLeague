/*
* Set the news item to toggle on click
*/
$(document).ready(function () {
    // Set up the news slide animation
    $("div.newsContent").hide();
    $("div#content div strong").addClass("hoverPointer");
    $("div#content div strong").click(function () {
        $(this).parents("div.newsItem").find("div div.newsContent").slideToggle();
    });

    // Division tabs
    $("#leagueSlider").easySlider({
        prevText: '<< Previous Division',
        nextText: 'Next Division >>',
        controlsFade: false
    });
    
    initStatsTicker();
});

function initStatsTicker() {
    // Get stats to display in ticker
    $.getJSON("/Home/GetTickerStats", function (data) {
        var $statsTicker = $("#statsTicker");

        // Exit if no stats returned
        if (data.length == 0)
            return;

        $statsTicker.append("<ul></ul>");

        $.each(data, function (index, item) {
            $statsTicker.find("ul").append("<li><strong>" + item.Description + ": </strong>" + item.Stats + "</li>");
        });

        $("#statsTickerWrapper").fadeIn("slow");

        // Stats ticker
        $("#statsTicker").easySlider({
            prevText: '',
            nextText: '',
            auto: true,
            controlsShow: true,
            prevId: 'tickerPrevBtn',
            nextId: 'tickerNextBtn',
            continuous: true,
            pause: 10000
        });
    });
}