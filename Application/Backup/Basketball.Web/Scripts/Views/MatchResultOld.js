$().ready(function () {
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

    // Initialise points and fouls inputs
    $("input[type=checkbox][id$=HasPlayed]").each(function () {
        setPlayed(this);
    });

    // Add validation to fouls inputs
    $("input[type=text][id$=Fouls]").blur(function () {
        zeroIfInvalid(this);
    });

    initDaft();
});



/*
* Loops through home or away player points and sets team total
*/
function calcPointsTotal(pointsInputId, pointsTotalId) {
    var total = 0;

    $("input[id^=" + pointsInputId + "][id$=PointsScored]").each(function (index) {
        // Replace value with zero if Nan or < 0
        zeroIfInvalid(this);

        total += parseInt($(this).val(), 10);
    });

    $("#" + pointsTotalId).val(total);
}

/*
* Enables or disables points and fouls inputs based on played checkbox value
*/
function setPlayed(playedCheckbox) {
    var parent = $(playedCheckbox).parent().parent();
    var points = parent.find("input[type=text][id$=PointsScored]");
    var fouls = parent.find("input[type=text][id$=Fouls]");
    var mvp = parent.find("input[type=radio][id$=MvpPlayerId]");

    if ($(playedCheckbox).is(":checked")) {
        // Enable points and fouls
        points.removeAttr("disabled");
        points.removeClass("disabled");
        fouls.removeAttr("disabled");
        fouls.removeClass("disabled");
        mvp.removeAttr("disabled");
        mvp.removeClass("disabled");
    }
    else {
        // Zero and disable points and fouls
        points.val(0);
        fouls.val(0);
        mvp.attr("checked", false);
        
        points.attr("disabled", "disabled");
        points.addClass("disabled");
        fouls.attr("disabled", "disabled");
        fouls.addClass("disabled");
        mvp.attr("disabled", "disabled");
        mvp.addClass("disabled");

        calcPointsTotal("HomePlayerStats", "HomeTeamScore");
        calcPointsTotal("AwayPlayerStats", "AwayTeamScore");
    }
}


/*
* Zeros the element if NaN or less than zero
*/
function zeroIfInvalid(element) {
    if (element != null && (isNaN($(element).val()) || $(element).val() < 0 ))
        $(element).val(0);
}


// ******************** Draft methods
var draftCookieKey = "matchResultDaft";
var draftCookieExpiryDays = 30;

function initDaft() {
    if(isBlankMatchResult()) {
        if (hasSavedDataForFixture()) {
            // Ask for confirmation to load
            if(confirm("Draft data has been found for this match result. Would you like to load it?")) {
                loadDraftData();                
            }
        }

        // Configure draft saving
        initDraftAutoSave();
    }
}

function isBlankMatchResult() {
    return $("#HomeTeamScore").val() == "0" && $("#AwayTeamScore").val() == "0";
}

function getFixtureId() {
    return $("#FixtureId").val();
}

function hasSavedDataForFixture() {
    var draftCookie = getDraftCookie();

    return draftCookie != null && draftCookie.FixtureId == getFixtureId();
}

function loadDraftData() {
    // Just to be sure
    if (!hasSavedDataForFixture())
        return;

    var fixtureData = getDraftCookie();

    $("#HomeTeamScore").val(fixtureData.HomeTeamScore);
    $("#AwayTeamScore").val(fixtureData.AwayTeamScore);
    $("#Report").elrte("val", fixtureData.Report);

    if (fixtureData.HomeMvpPlayerId != null)
        $("#homeTeamStats input[id$=MvpPlayerId][value=" + fixtureData.HomeMvpPlayerId + "]").prop("checked", true);
    if (fixtureData.AwayMvpPlayerId != null)
        $("#awayTeamStats input[id$=MvpPlayerId][value=" + fixtureData.AwayMvpPlayerId + "]").prop("checked", true);

    for (var i = 0; i < fixtureData.HomePlayerStats.length; i++) {
        var homePlayerStat = fixtureData.HomePlayerStats[i];
        var $playerRow = $("#homeTeamStats tr td input[id$=PlayerId][value="+ homePlayerStat.PlayerId +"]").parents("tr");

        $playerRow.find("td input[id$=PointsScored]").val(homePlayerStat.PointsScored).removeClass("disabled").removeAttr("disabled");
        $playerRow.find("td input[id$=Fouls]").val(homePlayerStat.Fouls).removeClass("disabled").removeAttr("disabled");
        $playerRow.find("td input[id$=HasPlayed]").prop("checked", true);
        $playerRow.find("td input[id$=MvpPlayerId]").val(homePlayerStat.Fouls).removeClass("disabled").removeAttr("disabled");
    }

    for (var i = 0; i < fixtureData.AwayPlayerStats.length; i++) {
        var awayPlayerStat = fixtureData.AwayPlayerStats[i];
        var $playerRow = $("#awayTeamStats tr td input[id$=PlayerId][value=" + awayPlayerStat.PlayerId + "]").parents("tr");

        $playerRow.find("td input[id$=PointsScored]").val(homePlayerStat.PointsScored).removeClass("disabled").removeAttr("disabled");
        $playerRow.find("td input[id$=Fouls]").val(homePlayerStat.Fouls).removeClass("disabled").removeAttr("disabled");
        $playerRow.find("td input[id$=HasPlayed]").prop("checked", true);
        $playerRow.find("td input[id$=MvpPlayerId]").val(homePlayerStat.Fouls).removeClass("disabled").removeAttr("disabled");
    }
}

function initDraftAutoSave() {
    // #report blur doesn't work because of eltre
    $(".matchResultTable input").blur(function () {
        saveDraft();
    });

    $("input[type=submit]").click(function () {
        saveDraft();
    });
}

function saveDraft() {
    // Object structure
    // Fixture
        // FixtureId
        // HomeTeamScore
        // AwayTeamScore
        // Report
        // HomeMvpPlayerId
        // AwayMvpPlayerId
        // HomePlayerStats[PlayerId]/AwayPlayerStats[PlayerId] Only players that have played
            // PlayerId
            // PointsScored
            // Fouls

    var fixtureData = new Object();
    fixtureData.FixtureId = getFixtureId();
    fixtureData.HomeTeamScore = $("#HomeTeamScore").val();
    fixtureData.AwayTeamScore = $("#AwayTeamScore").val();
    fixtureData.Report = $("#Report").elrte("val");

    fixtureData.HomeMvpPlayerId = $("#homeTeamStats input[id$=MvpPlayerId]:checked").val();
    fixtureData.AwayMvpPlayerId = $("#awayTeamStats input[id$=MvpPlayerId]:checked").val();

    fixtureData.HomePlayerStats = new Array();
    $("#homeTeamStats tr td input[id$=HasPlayed]:checked").parents("tr").each(function (index) {
        var playerStat = new Object();
        playerStat.PlayerId = $(this).find("td input[id$=PlayerId]").val();
        playerStat.PointsScored = $(this).find("td input[id$=PointsScored]").val();
        playerStat.Fouls = $(this).find("td input[id$=Fouls]").val();

        fixtureData.HomePlayerStats[index] = playerStat;
    });

    fixtureData.AwayPlayerStats = new Array();
    $("#awayTeamStats tr td input[id$=HasPlayed]:checked").parents("tr").each(function (index) {
        var playerStat = new Object();
        playerStat.PlayerId = $(this).find("td input[id$=PlayerId]").val();
        playerStat.PointsScored = $(this).find("td input[id$=PointsScored]").val();
        playerStat.Fouls = $(this).find("td input[id$=Fouls]").val();

        fixtureData.AwayPlayerStats[index] = playerStat;
    });

    saveDraftCookie(fixtureData);
    
    return fixtureData;
}

function saveDraftCookie(fixtureData) {
    $.cookie(draftCookieKey, JSON.stringify(fixtureData), { expires: draftCookieExpiryDays });
    
}

function getDraftCookie() {
    return JSON.parse($.cookie(draftCookieKey));
}