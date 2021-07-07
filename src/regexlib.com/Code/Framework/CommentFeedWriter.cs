using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

using RegExLib.Data;

namespace RegExLib.Framework {

    public class CommentFeedWriter : BaseFeedWriter {

        protected override string Title {
            get { return "RegExLib.com - Recent Comments"; }
        }

        protected override void WriteItems(XmlTextWriter writer) {
            List<CommentInfo> items = CommentManager.ListCommentsByMostRecent(Globals.FeedItemCount);

            if (items == null) return;

            foreach (CommentInfo item in items) {
                this.WriteItem(writer, item.Name, item.Title, Formats.FormatCommentLink(item.ExpressionId, item.Id), item.Comment, item.DateCreated);
            }
        }
    }
}