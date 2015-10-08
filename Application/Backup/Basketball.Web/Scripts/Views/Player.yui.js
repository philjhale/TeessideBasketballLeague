var players;

function SearchPlayers(surnameValue) {
    var playerMatch = $("#playerMatch");

    playerMatch.empty();

    if (surnameValue.length < 2)
        return;

    $.each(players, function (index, value) {
        if (value.Name.toLowerCase().indexOf(surnameValue.toLowerCase()) > -1) {
            playerMatch.append($("<li></li>").append($("<a></a>").attr("href", "/Admin/Players/Edit/" + value.Id).html(value.Name + " (" + value.Team + ")")));
        }
    });
}
//http: //localhost:54381/Stats/Player/118

$().ready(function () {
    $.getJSON("/Admin/Players/GetPlayers", function (data) {
        if (data.length == 0)
            return;

        players = data;

        $("#Player_Surname").keyup(function () {
            SearchPlayers($("#Player_Surname").val());
        });

    });
});