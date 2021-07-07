using System;
using System.Data;

namespace RegExLib.Data {
    /// <summary>
    /// This object represents the properties and methods of a Expression.
    /// </summary>
    public class Expression : BaseDataObject, IDataObject {

        public Expression() { }


        public override string DependsOnTable {
            get { return "dbo.rxl_Expression"; }
        }

        protected Guid _authorId;
		public Guid AuthorId
		{
            get { return _authorId; }
            set {
                if (value == _authorId) return;
                _authorId = value;
                this.IsDirty = true;
            }
        }

        private string _authorName;
        public string AuthorName {
            get { return _authorName; }
            set { _authorName = value; }
        }


        private int _providerId;
        public int ProviderId {
            get { return _providerId; }
            set {
                if (value == _providerId) return;
                _providerId = value;
                this.IsDirty = true;
            }
        }

        private string _title = String.Empty;
        public string Title {
            get { return _title; }
            set {
                if (value == _title) return;
                _title = value;
                this.IsDirty = true;
            }
        }

        private string _pattern = String.Empty;
        public string Pattern {
            get { return _pattern; }
            set {
                if (value == _pattern) return;
                _pattern = value;
                this.IsDirty = true;
            }
        }

        private string _matchingText = String.Empty;
        public string MatchingText {
            get { return _matchingText; }
            set {
                if (value == _matchingText) return;
                _matchingText = value;
                this.IsDirty = true;
            }
        }
        public string MatchingTextAsHtml
        {
            get
            {
                return System.Web.HttpUtility.HtmlEncode(MatchingText).Replace("|||", " <span class='separator'>|</span> ").Replace("||", " <span style='color:black'>|</span> "); 
            }
        }

        public string DescriptionAsHtml
        {
            get
            {
                return System.Web.HttpUtility.HtmlEncode(Description);
            }
        }

        public string TitleAsHtml
        {
            get
            {
                return System.Web.HttpUtility.HtmlEncode(Title);
            }
        }

        public string PatternAsHtml
        {
            get
            {
                return System.Web.HttpUtility.HtmlEncode(Pattern);
            }
        }

        private string _nonMatchingText = String.Empty;
        public string NonMatchingText {
            get { return _nonMatchingText; }
            set {
                if (value == _nonMatchingText) return;
                _nonMatchingText = value;
                this.IsDirty = true;
            }
        }

        public string NonMatchingTextAsHtml
        {
            get
            {
                return System.Web.HttpUtility.HtmlEncode(NonMatchingText).Replace("|||", " <span class='separator'>|</span> ").Replace("||", " <span style='color:black'>|</span> ");
            }
        }


        private bool _enabled;
        public bool Enabled {
            get { return _enabled; }
            set {
                if (value == _enabled) return;
                _enabled = value;
                this.IsDirty = true;
            }
        }

        private int _rating;
        public int Rating {
            get { return _rating; }
            set {
                if (value == _rating) return;
                _rating = value;
                this.IsDirty = true;
            }
        }

        private string _source = String.Empty;
        public string Source {
            get { return _source; }
            set {
                if (value == _source) return;
                _source = value;
                this.IsDirty = true;
            }
        }

        private string _description = String.Empty;
        public string Description {
            get { return _description; }
            set {
                if (value == _description) return;
                _description = value;
                this.IsDirty = true;
            }
        }


        public override void Fill(IDataReader reader) {
			this.AuthorId = reader.GetGuid( 1 ); //mandatory field


            this.AuthorName = reader.GetString(2); // mandatory field
            if (!reader.IsDBNull(3)) this.ProviderId = reader.GetInt32(3);
            if (!reader.IsDBNull(4)) this.Title = reader.GetString(4);
            this.Pattern = reader.GetString(5); // mandatory field
            this.MatchingText = reader.GetString(6); // mandatory field
            this.NonMatchingText = reader.GetString(7); // mandatory field
            this.Enabled = reader.GetBoolean(8); // mandatory field
            if (!reader.IsDBNull(9)) this.Rating = reader.GetInt32(9);
            if (!reader.IsDBNull(10)) this.Source = string.IsNullOrEmpty(reader.GetString(10)) ? string.Empty : reader.GetString(10);
            if (!reader.IsDBNull(11)) this.Description = reader.GetString(11);
            base.Fill(reader);
        }

        public ASPAlliance.Common.RegExpDetails ToRegExpDetails()
        {
            ASPAlliance.Common.RegExpDetails details = new ASPAlliance.Common.RegExpDetails();
            details.create_date = this.DateCreated;
            details.description = this.Description;
            details.disable = !this.Enabled;
            details.matches = this.MatchingText;
            details.not_matches = this.NonMatchingText;
            details.rating = this.Rating;
            details.regexp_id = this.Id;
            details.regular_expression = this.Pattern;
            details.source = this.Source;
            details.user_id = 0;

            return details;
        }
    }
}
