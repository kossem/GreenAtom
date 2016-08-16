using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

public class XmlParse : MonoBehaviour {

    public class Action
    {
        [XmlAttribute]
        public string title { get; set; }

        [XmlAttribute]
        public string screenUrl { get; set; }

        [XmlAttribute]
        public string type { get; set; }
    }
    public class Category
    {
        [XmlElement("action")]
        public Action[] ActionList { get; set; }

        [XmlAttribute]
        public string title { get; set; }

        [XmlAttribute]
        public string icon { get; set; }

        [XmlAttribute]
        public string subtitle { get; set; }
    }
    [XmlRoot("root")]
    public class CategoryContainer
    {
        [XmlArray("content")]
        [XmlArrayItem("category")]
        public Category[] Content { get; set; }

        public static CategoryContainer load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryContainer));
            using (var stream = File.OpenRead(path))
                return serializer.Deserialize(stream) as CategoryContainer;
        }
    }
    public class Menu
    {
        public List<Category> category = new List<Category>();
        private Category[] categories;

        public Menu(string path)
        {
            categories = CategoryContainer.load(path).Content;
            foreach (Category cat in categories)
            {
                category.Add(cat);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
