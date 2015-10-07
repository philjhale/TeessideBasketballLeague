using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewObjects
{
    public class TeamsStartingWithLetter
    {
        public string Letter { get; set; }
        public List<Team> Teams { get; set; }

        public TeamsStartingWithLetter()
        {
            Teams = new List<Team>();
        }
    }
}
