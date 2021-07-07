using System;
using System.Diagnostics;

namespace RegExLib.Web.UI.Controls
{
    /// <summary>
    /// Summary description for LQBaseAdControl.
    /// </summary>
    public abstract class LQBaseAdControl : System.Web.UI.WebControls.WebControl
    {
        const int LQPUBLISHERID = 50;
        protected int _channel = 1;
        public int Channel
        {
            [DebuggerStepThrough]
            get
            {
                return _channel;
            }
            [DebuggerStepThrough]
            set
            {
                _channel = value;
            }
        }
        protected int _format = 1;
        public int Format
        {
            [DebuggerStepThrough]
            get
            {
                return _format;
            }
            [DebuggerStepThrough]
            set
            {
                _format = value;
            }
        }
        protected int _publisher = LQPUBLISHERID;
        public int Publisher
        {
            [DebuggerStepThrough]
            get
            {
                return _publisher;
            }
            [DebuggerStepThrough]
            set
            {
                _publisher = value;
            }
        }
        protected int _zone = 1;
        public int Zone
        {
            [DebuggerStepThrough]
            get
            {
                return _zone;
            }
            [DebuggerStepThrough]
            set
            {
                _zone = value;
            }
        }
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.Write(@"<script type=""text/javascript"">
lqm_channel=" + Channel + @";
lqm_publisher=" + Publisher + @";
lqm_format=" + Format + @";
lqm_zone=" + Zone + @";
</script>
<script src=""http://a.lakequincy.com/s.js"" type=""text/javascript""></script>");
            base.Render(writer);
        }

    }
}
