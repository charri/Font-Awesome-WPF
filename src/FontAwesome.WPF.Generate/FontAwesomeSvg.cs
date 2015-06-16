using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FontAwesome.WPF.Generate
{
    public class FontAwesomeSvg
    {
        private const string URI_SVG = "http://www.w3.org/2000/svg";
        private readonly XmlDocument _xmlDoc;
        private readonly XmlNamespaceManager _namespaceManager;

        public FontAwesomeSvg(string fileName)
        {
            var settings = new XmlReaderSettings {XmlResolver = null, DtdProcessing = DtdProcessing.Ignore};
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(XmlReader.Create(File.OpenRead(fileName), settings));
            
            _namespaceManager = new XmlNamespaceManager(_xmlDoc.NameTable);
            _namespaceManager.AddNamespace("svg", URI_SVG);
        }

        public string PathData(string unicode)
        {
            var code = int.Parse(unicode, NumberStyles.HexNumber);

            var node = _xmlDoc.SelectSingleNode(string.Format("//svg:glyph[@unicode='{0}']", char.ConvertFromUtf32(code)), _namespaceManager);

            return node.Attributes["d"].Value;
        }
    }
}
