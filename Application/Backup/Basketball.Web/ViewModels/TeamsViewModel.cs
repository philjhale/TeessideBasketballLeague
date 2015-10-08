using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.ViewModels
{
    public class TeamsViewModel
    {
        public List<TeamsStartingWithLetter> TeamsStartingWithLetter { get; set; }

        public TeamsViewModel()
        {
            this.TeamsStartingWithLetter = new List<TeamsStartingWithLetter>();
        }
    }
}
