using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewModels
{
    public class ViewCupViewModel
    {
        public string                   CupName         { get; private set; }
        public List<List<Fixture>>      FixturesByRound { get; private set; }
        public List<PlayerCupStats>     TopAvgScorers   { get; private set; } 
        public int                      NumberOfRounds  { get { return FixturesByRound.Count; }}

        public ViewCupViewModel(string cupName, List<List<Fixture>> fixturesByRound, List<PlayerCupStats> topAvgScorers)
        {
            this.CupName         = cupName;
            this.FixturesByRound = fixturesByRound;
            this.TopAvgScorers   = topAvgScorers;
        }
    }
}