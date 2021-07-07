using System;

namespace ASPAlliance.Common
{
	/// <summary>
	/// Holds information about a particular regular expression
	/// </summary>
	[Obsolete("Move to RegExLib.PatternInfo")]
	public class RegExpDetails 
	{
		/// <summary>
		/// 
		/// </summary>
		public int		user_id;
		/// <summary>
		/// 
		/// </summary>
		public int		regexp_id;
		/// <summary>
		/// 
		/// </summary>
		public String	regular_expression;
		/// <summary>
		/// 
		/// </summary>
		public String	matches;
		/// <summary>
		/// 
		/// </summary>
		public String	not_matches;
		/// <summary>
		/// 
		/// </summary>
		public String	source;
		/// <summary>
		/// 
		/// </summary>
		public String	description;
		/// <summary>
		/// 
		/// </summary>
		public DateTime create_date;
		/// <summary>
		/// 
		/// </summary>
		public bool		disable;
		/// <summary>
		/// 
		/// </summary>
		public int		rating;
	}
}
