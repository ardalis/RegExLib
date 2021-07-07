using System;
using System.Data;

namespace RegExLib.Data {
    /// <summary>
    /// This object represents the properties and methods of a Category.
    /// </summary>
    public class Category : BaseDataObject, IDataObject {

        public Category() {}

        public override string DependsOnTable
        {
            get { return "dbo.rxl_Category"; }
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
            _description = reader.GetString(1); // mandatory field
            base.Fill(reader);
        }
    }
}





