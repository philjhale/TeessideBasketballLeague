using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }

        public int TeamId { get; set; }

        public List<SelectListItem> Teams { get; set; }

        public UserViewModel()
        {
            User = new User();
        }

        public void MapToModel(User user)
        {
            User = user;
            TeamId = user.Team.Id;
        }
    }
}