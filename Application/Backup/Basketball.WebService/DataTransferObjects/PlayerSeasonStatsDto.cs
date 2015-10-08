using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

using Basketball.Domain.Entities;

namespace Basketball.WebService.DataTransferObjects
{
    [DataContract]
    public class PlayerCareerStatsDto
    {
        [DataMember]
        public string Year { get; private set; }
        [DataMember]
        public string Games { get; private set; }
        [DataMember]
        public string MvpAwards { get; private set; }
        [DataMember]
        public string Fouls { get; private set; }
        [DataMember]
        public string FoulsPerGame { get; private set; }
        [DataMember]
        public string Points { get; private set; }
        [DataMember]
        public string PointsPerGame { get; private set; }

        public PlayerCareerStatsDto(PlayerSeasonStats playerSeasonStats)
        {
            this.Year          = string.Format("{0}/{1}", playerSeasonStats.Season.StartYear, playerSeasonStats.Season.EndYear);
            this.Games         = playerSeasonStats.GamesPlayed.ToString();
            this.MvpAwards     = playerSeasonStats.MvpAwards.ToString();
            this.Fouls         = playerSeasonStats.TotalFouls.ToString();
            this.FoulsPerGame  = playerSeasonStats.FoulsPerGame.ToString("0.00");
            this.Points        = playerSeasonStats.TotalPoints.ToString();
            this.PointsPerGame = playerSeasonStats.PointsPerGame.ToString("0.00");
        }

        public PlayerCareerStatsDto(PlayerCareerStats playerCareerStats)
        {
            this.Year          = "Career";
            this.Games         = playerCareerStats.GamesPlayed.ToString();
            this.MvpAwards     = playerCareerStats.MvpAwards.ToString();
            this.Fouls         = playerCareerStats.TotalFouls.ToString();
            this.FoulsPerGame  = playerCareerStats.FoulsPerGame.ToString("0.00");
            this.Points        = playerCareerStats.TotalPoints.ToString();
            this.PointsPerGame = playerCareerStats.PointsPerGame.ToString("0.00");
        }
    }
}