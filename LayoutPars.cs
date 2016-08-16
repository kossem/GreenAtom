using System;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class LayoutPars {

    public static Queue<Element> Xml(string filename)
    {
        Queue<Element> queue = new Queue<Element>();
        XmlTextReader reader = null;
        try
        {
            reader = new XmlTextReader(filename);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        queue.Enqueue(new Element(reader.Value));
                        break;
                    case XmlNodeType.Element:
                        if (reader.GetAttribute("type") == "image")
                        {
                            reader.Read();
                            queue.Enqueue(new Element(reader.GetAttribute("title"), reader.GetAttribute("src")));
                        }
                        break;
                }
            }
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
        return queue;
    }

}
