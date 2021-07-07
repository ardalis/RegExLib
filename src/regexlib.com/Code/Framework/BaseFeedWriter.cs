using System;
using System.Data;
using System.Xml;

using RegExLib.Data;

namespace RegExLib.Framework {

    abstract public class BaseFeedWriter {

        protected virtual string Title { get { return "RegExLib.com - Rss Feed"; } }
        protected virtual string Description { get { return "A Library of Regular Expressions"; } }
        protected virtual string Copyright { get { return "Copyright © 2001-2005, ASPSmith.com"; } }
        protected virtual string Generator { get { return "RegexLibRssFeed v1.0"; } }


        public virtual void WriteFeed(XmlTextWriter writer) {
            this.WriteDocumentPrologue(writer);
            this.WriteItems(writer);
            this.WriteDocumentClose(writer);
        }

        protected virtual void WriteDocumentPrologue(XmlTextWriter writer) {
            writer.WriteStartDocument();
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/");
            writer.WriteStartElement("channel");
            writer.WriteElementString("title", this.Title);
            writer.WriteElementString("link", "http://regexlib.com/");
            writer.WriteElementString("description", this.Description );
            writer.WriteElementString("copyright", this.Copyright);
            writer.WriteElementString("generator", this.Generator);
        }

        protected virtual void WriteDocumentClose(XmlTextWriter writer) {
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        protected abstract void WriteItems(XmlTextWriter writer);

        protected void WriteItem(XmlTextWriter writer, string creator, string title, string link, string description)
        {
            WriteItem(writer, creator, title, link, description, DateTime.Now);
        }

        protected void WriteItem(XmlTextWriter writer, string creator, string title, string link, string description, DateTime pubDate) {
            writer.WriteStartElement("item");
            writer.WriteElementString("dc:creator", creator);
            writer.WriteElementString("title", title);
            writer.WriteElementString("link", link);
            writer.WriteElementString("pubDate", pubDate.ToString("r"));
            writer.WriteElementString("guid", link);
            writer.WriteElementString("comments", link + "#comments");
            writer.WriteElementString("description", description);
            writer.WriteEndElement();
        }
    }
}
