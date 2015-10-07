using System;
using Basketball.Domain.Entities;
using Basketball.Common.BaseTypes.Interfaces;
namespace Basketball.Service.Interfaces
{
    public partial interface IPenaltyService : IBaseService<Penalty>
    {
        TeamLeague UpdateTeamLeaguePenaltyPoints(int leagueId, int teamId);
        bool DoesPenaltyExist(int fixtureId, int teamId);
    }
}
