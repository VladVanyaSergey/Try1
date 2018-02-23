using System;
using System.Collections;
using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;

namespace GraphDB
{
    public class Element
    {
        public Type valueType { get { return Value.GetType(); } }
        public object Value { get; private set; }


        public Element()
        {
        }

        public Element(INode node)
        {
            Value = new Entity().RebuildSelf(node);
        }

        public Element(IRelationship relationship)
        {
            Value = new Link().RebuildSelf(relationship);
        }

        public Element(IPath path)
        {
            Value = path;
        }

        public Element(IDictionary dictionary)
        {
            Value = dictionary;
        }

        public Element(IList list)
        {
            Value = list;
        }

        public Element(string v)
        {
            Value = v;
        }

        public Element(double v)
        {
            Value = v;
        }

        public Element(byte[] v)
        {
            Value = v;
        }

        public Element(int v)
        {
            Value = v;
        }

        public Element(bool v)
        {
            Value = v;
        }

        public static Element Empty
        {
            get
            {
                return new Element();
            }
        }
    }
}