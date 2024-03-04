using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShareHub.Core.Domain.Enums
{
	public enum ChatType
	{
		Public,
		Private
	}

	public enum UserChatStatus
	{
		Admin,
		Moderator,
		User
	}
}
