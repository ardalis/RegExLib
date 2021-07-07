using System ;

namespace RegexLib
{
	/// <summary>
	///  A simple data class to contain the fields for a Pattern.
	/// </summary>
	[Serializable()]
	public class PatternInfo
	{
		private int _patternId ;
		private UserInfo _userInfo ;
		private int _providerId ;
		private string _regularExpression ;
		private string[] _matches ;
		private string[] _notMatches ;
		private string _source ;
		private string _description ;
		
		/// <summary>
		/// .ctor
		/// </summary>
		public PatternInfo(){}


		public int PatternId 
		{
			get { return _patternId ; }
			set { _patternId = value ; }
		}

		public UserInfo UserInfo 
		{
			get { return _userInfo ; }
			set { _userInfo = value ; }
		}

		public int ProviderId 
		{
			get { return _providerId ; }
			set { _providerId = value ; }
		}

		public string RegularExpression 
		{
			get { return _regularExpression ; }
			set { _regularExpression = value ; }
		}

		public string[] Matches 
		{
			get { return _matches ; }
			set { _matches = value ; }
		}

		public string[] NotMatches 
		{
			get { return _notMatches ; }
			set { _notMatches = value ; }
		}

		public string Source 
		{
			get { return _source ; }
			set { _source = value ; }
		}

		public string Description 
		{
			get { return _description ; }
			set { _description = value ; }
		}
		
	}
}