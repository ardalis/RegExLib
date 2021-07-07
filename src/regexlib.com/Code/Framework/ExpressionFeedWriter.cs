using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

using RegExLib.Data;

namespace RegExLib.Framework {

    public class ExpressionFeedWriter : BaseFeedWriter {

        protected override string Title {
            get { return "RegExLib.com - Recent Patterns"; }
        }

        protected override void WriteItems(XmlTextWriter writer) {
            List<Expression> items = ExpressionManager.ListExpressionsByMostRecent(Globals.FeedItemCount, true);

            if (items == null) return;

            foreach (Expression item in items) {
				if ( item.Enabled )
				{
					this.WriteItem( writer, item.AuthorName, item.Title, Formats.FormatExpressionLink( item.Id ), item.Description, item.DateModified );
				}
            }
        }
    }
}