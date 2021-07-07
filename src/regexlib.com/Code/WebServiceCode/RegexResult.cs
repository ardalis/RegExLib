using System ;

namespace RegexLib
{

	public enum RegexActionStatus {None=0, Inserted, Updated, Deleted, Failed} ;

	/// <summary>
	///  A simple data class to return info about a regex operation.
	/// </summary>
	[Serializable()]
	public class RegexResult
	{
		private PatternInfo _patternInfo ;	
		private string _message ;
		private RegexActionStatus _status ;
		
		/// <summary>
		/// .ctors
		/// </summary>
		public RegexResult(){}

		public RegexResult( PatternInfo patternInfo, string message, RegexActionStatus status )
		{
			this._patternInfo = patternInfo ;
			this._message = message ;
			this._status = status ;
		}

		public PatternInfo PatternInformation
		{
			get { return _patternInfo ; }
			set { _patternInfo = value ; }
		}

		public string Message
		{
			get { return _message ; }
			set { _message = value ; }
		}

		public RegexActionStatus Status 
		{
			get { return _status ; }
			set { _status = value ; }
		}
		
	}
}
