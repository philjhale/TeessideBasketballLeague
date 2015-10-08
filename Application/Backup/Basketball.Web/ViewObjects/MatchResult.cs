using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewObjects
{
    public class MatchResult
    {
        public Fixture @Fixture { get; set; }
        public List<PlayerFixture> HomeTopPlayers { get; set; }
        public List<PlayerFixture> AwayTopPlayers { get; set; }
    }
}
