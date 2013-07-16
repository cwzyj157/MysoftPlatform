using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace 文档合并工具
{
    public static class Ext
    {
        public static XmlNode CloneNode(this XmlNode node, XmlDocument doc)
        {
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, node.Name, "");

            foreach (XmlAttribute attr in node.Attributes)
            {
                XmlAttribute newAttr = doc.CreateAttribute(attr.Name);
                newAttr.Value = attr.Value;
                newNode.Attributes.Append(newAttr);
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                newNode.AppendChild(childNode.CloneNode(doc));
            }

            return newNode;
        }
    }
}
