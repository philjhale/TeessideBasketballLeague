using Basketball.Common.Domain;

namespace Basketball.Domain.Entities
{
    /// <summary>
    /// Stores player stats for a Career
    /// 
    /// Do not update TotalPoints, TotalFouls, GamesPlayed, PointsPerGame or FoulsPerGame manually. Use UpdateStats
    /// 
    /// MvpAwards can be updated
    ///     
    /// </summary>
    public class PlayerCareerStats : Entity
    {
        public PlayerCareerStats() 
        {
            MvpAwards = 0;
        }

        public PlayerCareerStats(Player player,
            int totalPoints,
            int totalFouls,
            int gamesPlayed,
            int mvpAwards)
            : this()
        {
            Check.Require(player != null, "player must be provided");

            this.Player = player;

            UpdateStats(totalPoints, totalFouls, gamesPlayed, mvpAwards);
        }

        // Class won't be edite by users so validation isn't necessary
        public virtual Player Player { get; set; }

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
            //Console.WriteLine("updatestats called");
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

            //Console.WriteLine("updatestats end "+ TotalPoints + " " + TotalFouls + " " + GamesPlayed);
        }
    }
}
