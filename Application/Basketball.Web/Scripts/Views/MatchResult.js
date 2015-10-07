var pointsCalculator = (function ($, undefined) {
    var init = function () {
        // TODO How easy is this to unit test?
        // TODO Is it okay to hardcode so many element ids etc.? My initial thought is no, however this isn't designed for re-use.
        // It will never be used on another page
        // TODO Add variables for selectors

        // Total home points
        $("input[id^=HomePlayerStats]").blur(function () {
            calcPointsTotal("HomePlayerStats", "HomeTeamScore");
        });

        // Total away points
        $("input[id^=AwayPlayerStats]").blur(function () {
            calcPointsTotal("AwayPlayerStats", "AwayTeamScore");
        });

        // Hook up click events on all played checkboxes
        $("input[type=checkbox][id$=HasPlayed]").click(function () {
            setPlayed(this);
        });

        // Add validation to fouls inputs
        $("input[type=text][id$=Fouls]").blur(function () {
            zeroIfInvalid(this);
        });


        $("#forfeitControl").show();
        $(".forfeitGameButton").click(function () {
            if (!confirm("Are you sure you want " + $(this).data("teamName") + " to forfeit the game?"))
                return false;

            $("#IsForfeit").val(true);
            $("#HasPlayerStats").prop("checked", false);
            $("#IsPenaltyAllowed").prop("checked", false);
            $("#HomeTeamScore").val(0);
            $("#AwayTeamScore").val(0);
            $("input[name$=HasPlayed]:checked").each(function () {
                $(this).prop("checked", false);
                setPlayed(this);
            });
            $("#ForfeitingTeamId").val($(this).data("forfeitingTeamId"));
        });
    },

    // Zeros the element if NaN or less than zero
    zeroIfInvalid = function (element) {
        if (element != null && (isNaN($(element).val()) || $(element).val() < 0))
            $(element).val(0);
    },

    // Public - Loops through home or away player points and sets team total
    calcPointsTotal = function (pointsInputId, pointsTotalId) {
        var total = 0;

        $("input[id^=" + pointsInputId + "][id$=PointsScored]").each(function (index) {
            // Replace value with zero if Nan or < 0
            zeroIfInvalid(this);

            total += parseInt($(this).val(), 10);
        });

        $("#" + pointsTotalId).val(total);
    },

    // Public - Enables or disables points and fouls inputs based on played checkbox value
    setPlayed = function (playedCheckbox) {
        var $parent = $(playedCheckbox).parent().parent();
        var $points = $parent.find("input[type=text][id$=PointsScored]");
        var $fouls = $parent.find("input[type=text][id$=Fouls]");
        var $mvp = $parent.find("input[type=radio][id$=MvpPlayerId]");

        if ($(playedCheckbox).is(":checked")) {
            // Enable points and fouls
            $points.removeAttr("disabled");
            $points.removeClass("disabled");
            $fouls.removeAttr("disabled");
            $fouls.removeClass("disabled");
            $mvp.removeAttr("disabled");
            $mvp.removeClass("disabled");
        }
        else {
            // Zero and disable points and fouls
            $points.val(0);
            $fouls.val(0);
            $mvp.attr("checked", false);

            $points.attr("disabled", "disabled");
            $points.addClass("disabled");
            $fouls.attr("disabled", "disabled");
            $fouls.addClass("disabled");
            $mvp.attr("disabled", "disabled");
            $mvp.addClass("disabled");

            calcPointsTotal("HomePlayerStats", "HomeTeamScore");
            calcPointsTotal("AwayPlayerStats", "AwayTeamScore");
        }
    };

    // Expose public members
    return { init: init };
} (jQuery));

$().ready(function () {
    pointsCalculator.init();
});
