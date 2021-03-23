using System.Xml;
using System.Xml.Serialization;

public class Item
{


    [XmlAttribute("name")]
    public string Name;
    public bool Collected;
    public int ID;
    public string Memory;
}
