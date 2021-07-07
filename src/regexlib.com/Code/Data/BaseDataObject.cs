using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace RegExLib.Data {
    /// <summary>
    /// Provide base properties for all data objects
    /// </summary>
    public abstract class BaseDataObject : IDataObject {

        // TODO: should add all Rss feedable properties in here
        // maybe make the feedable items abstract

        // Must be set in all subclasses to enable Sql Cache Invalidation
        // TODO: make this a string array since some objects will depend on multiple tables
        public abstract string DependsOnTable
        {
            get;
        }

        public BaseDataObject() { }

        protected int _id = (int)0;
        public int Id {
            get { return _id; }
            set {
                if (value == _id) return;
                _id = value;
                this.IsDirty = true;
            }
        }

        protected DateTime _dateCreated;
        public DateTime DateCreated {
            get { return _dateCreated; }
            set {
                if (value == _dateCreated) return;
                _dateCreated = value;
                this.IsDirty = true;
            }
        }

        protected DateTime _dateModified;
        public DateTime DateModified {
            get { return _dateModified; }
            set {
                if (value == _dateModified) return;
                _dateModified = value;
                this.IsDirty = true;
            }
        }

        [NonSerialized()]
        private bool _isDirty;
        public bool IsDirty {
            get { return _isDirty; }
            set {
                _isDirty = value;
            }
        }


        public bool IsNew {
            get { return this._id > 0; }
        }


        public void AcceptChanges() {
            this.IsDirty = false;
        }


        public virtual void Fill(IDataReader reader) {
            this.Id = reader.GetInt32(0);
            this.DateCreated = (DateTime) reader["DateCreated"];
            this.DateModified = (DateTime)reader["DateModified"];
            this.AcceptChanges();
        }
    }
}