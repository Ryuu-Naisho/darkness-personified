using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;


[XmlRoot("ItemCollection")]
public class ItemContainer
{
    //Store all the items in a list.
    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public List<Item> Items = new List<Item>();


    ///<summary>Load xml data as ItemContainer.</summary>
    public static ItemContainer Load()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Items");
        using(TextReader textReader = new StringReader(textAsset.text))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer));
            ItemContainer xmlData = serializer.Deserialize(textReader) as ItemContainer;
            return xmlData;
        }
    }
}
