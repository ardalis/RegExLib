using System;
using System.Text;

namespace RegExLib.Framework {

    public class Formats {

        private Formats() { }

        public static string FormatExpressionLink(int expressionId) {
            return string.Format("{0}REDetails.aspx?regexp_id={1}", Globals.BaseUrl, expressionId);
        }

        public static string FormatCommentLink(int expressionId, int commentId) {
            return string.Format("{0}REDetails.aspx?regexp_id={1}&commentId={2}", Globals.BaseUrl, expressionId, commentId);
        }
    }
}