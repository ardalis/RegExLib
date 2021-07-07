using System ;

namespace RegexLib
{
	/// <summary>
	///  A simple data class to contain the fields for a user.
	/// </summary>
	[Serializable()]
	public class UserInfo
	{
		private string _ticket ;	
		private string _email ;
		private string _firstName ;
		private string _surname ;
		
		/// <summary>
		/// .ctor
		/// </summary>
		public UserInfo(){}

		public string Ticket 
		{
			get { return _ticket ; }
			set { _ticket = value ; }
		}

		public string Email 
		{
			get { return _email ; }
			set { _email = value ; }
		}

		public string FirstName 
		{
			get { return _firstName ; }
			set { _firstName = value ; }
		}

		public string Surname 
		{
			get { return _surname ; }
			set { _surname = value ; }
		}
	}

}