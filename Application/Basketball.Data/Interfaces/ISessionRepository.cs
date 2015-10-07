using System.Collections.Generic;

namespace Basketball.Data.Interfaces 
{
	public interface ISessionRepository 
	{
		List<string> Roles();
		void Roles(List<string> roles);
        string GetLoggedInUsername();
	}
}