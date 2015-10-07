using System;
using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stores player stats for a league
    /// 
    /// Do not update stats manually. User UpdateStats()
    /// 
    /// This class is used to easily get top scorers for a league. Without this class it would require more calculation
    /// to accurately retreive top league scorers
    /// 
    /// MvpAwards can be updated 
    /// </summary>
    public class PlayerLeagueStats : Entity
    {
        public PlayerLeagueStats() 
        {
            MvpAwards = 0;
        }

        public PlayerLeagueStats(Player player,
            Season season,
            League league,
            int totalPoints,
            int totalFouls,
            int gamesPlayed,
            int mvpAwards)
            : this()
        {
            Check.Require(player != null, "player must be provided");
            Check.Require(season != null, "season must be provided");
            Check.Require(league != null, "league must be provided");

            this.Season = season;
            this.Player = player;
            this.League = league;

            UpdateStats(totalPoints, totalFouls, gamesPlayed, mvpAwards);
        }

        // Class won't be edited by users so validation isn't necessary
        public virtual Player Player { get; set; }

        public virtual Season Season { get; set; }

        public virtual League League { get; set; }

        private int _totalPoints;
        public virtual int TotalPoints { get { return _totalPoints; } set { _totalPoints = value; } }

        private decimal _pointsPerGame;
        public virtual decimal PointsPerGame { get { return _pointsPerGame; } set { _pointsPerGame = value; } }

        private int _totalFouls;
        public virtual int TotalFouls { get { return _totalFouls; } set { _totalFouls = value; } }

        private decimal _foulsPerGame;
        public virtual decimal FoulsPerGame { get { return _foulsPerGame; } set { _foulsPerGame = value; } }

        private int _gamesPlayed;
        public virtual int GamesPlayed { get { return _gamesPlayed; } set { _gamesPlayed = value; } }

        public virtual int MvpAwards { get; set; }

        public virtual void UpdateStats(int totalPoints, int totalFouls, int gamesPlayed, int mvpAwards)
        {
            // Avoid divide by zero problems
            if (gamesPlayed > 0)
            {
                // Prevent minus numbers being added
                _totalPoints = totalPoints > 0 ? totalPoints : 0;
                _totalFouls = totalFouls > 0 ? totalFouls : 0;
                _gamesPlayed = gamesPlayed;

                _pointsPerGame = _totalPoints / (decimal)_gamesPlayed;
                _foulsPerGame = _totalFouls / (decimal)_gamesPlayed;
            }
            else
            {
                _totalPoints = 0;
                _pointsPerGame = 0;
                _totalFouls = 0;
                _foulsPerGame = 0;
                _gamesPlayed = 0;
            }

            this.MvpAwards = mvpAwards;
        }
    }
}
