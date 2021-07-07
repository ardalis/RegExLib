using System;
using System.Data;

namespace RegExLib.Data {
    /// <summary>
    /// This object represents the properties and methods of a Comment.
    /// </summary>
    public class CommentInfo : BaseDataObject, IDataObject {

        public CommentInfo() {}


        public override string DependsOnTable {
            get { return "dbo.rxl_Comment"; }
        }

        protected int _expressionId;
        public int ExpressionId {
            get { return _expressionId; }
            set {
                if (value == _expressionId) return;
                _expressionId = value;
                this.IsDirty = true;
            }
        }

        protected string _title = String.Empty;
        public string Title {
            get { return _title; }
            set {
                if (value == _title) return;
                _title = value;
                this.IsDirty = true;
            }
        }

        protected string _name = String.Empty;
        public string Name {
            get { return _name; }
            set {
                if (value == _name) return;
                _name = value;
                this.IsDirty = true;
            }
        }

        protected string _url = String.Empty;
        public string Url {
            get { return _url; }
            set {
                if (value == _url) return;
                _url = value;
                this.IsDirty = true;
            }
        }

        protected string _comment = String.Empty;
        public string Comment {
            get { return _comment; }
            set {
                if (value == _comment) return;
                _comment = value;
                this.IsDirty = true;
            }
        }


        public override void Fill(IDataReader reader) {
            _expressionId = reader.GetInt32(1); // mandatory field
            if (!reader.IsDBNull(2)) _title = reader.GetString(2);
            if (!reader.IsDBNull(3)) _name = reader.GetString(3);
            if (!reader.IsDBNull(4)) _url = string.IsNullOrEmpty(reader.GetString(4)) ? string.Empty : reader.GetString(4);
            _comment = reader.GetString(5); // mandatory field
            base.Fill(reader);
        }
    }
}