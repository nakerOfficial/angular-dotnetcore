using System.Xml;

namespace WebApplication1
{
    class XmlReaderParser
    {
        private XmlTextReader _reader;
        private List<Element> _elements = new List<Element>();
        private Element? _currentElement;

        public List<Element> Elements { get { return _elements; } }

        public XmlReaderParser(XmlTextReader reader)
        {
            _reader = reader;
            StartReading();
        }

        public void StartReading()
        {
            while (_reader.Read())
            {
                switch (_reader.NodeType)
                {
                    case XmlNodeType.Element: XmlNodeType_Element(); break;
                    case XmlNodeType.Text: XmlNodeType_Text(); break;
                    case XmlNodeType.EndElement: XmlNodeType_EndElement(); break;
                }
            };
        }

        private void XmlNodeType_Element()
        {
            _currentElement = new Element();
            _currentElement.Name = _reader.Name;

            while (_reader.MoveToNextAttribute())
            {
                _currentElement.Attributes.Add(new Attribute(_reader.Name, _reader.Value));
            }
        }

        private void XmlNodeType_Text() => _currentElement.Text = _reader.Value;

        private void XmlNodeType_EndElement()
        {
            if (_currentElement != null)
            {
                _elements.Add(_currentElement);
                _currentElement = null;
            }
        }
    }

    class Element
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Attribute> Attributes { get; set; } = new List<Attribute>();
    }

    class Attribute
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Attribute(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
