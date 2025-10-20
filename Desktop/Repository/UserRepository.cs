using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entities;

namespace Desktop.Repository
{
    public class UserRepository
    {
		private static List<UserModel> users = new List<UserModel>();

		public bool Register(UserModel newUser)
		{
			foreach (var user in users)
			{
				if (user.Username == newUser.Username)
				{
					return false;
				}
			}

			users.Add(newUser);
			return true;
		}

		public bool Login(string username, string password)
		{
			foreach (var user in users)
			{
				if (user.Username == username && user.Password == password)
				{
					return true;
				}
			}
			return false;
		}
	}
}
