using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB
{
    public class Neo4jUnwrapedResult
    {
        private List<string> responseVariables;

        public Dictionary<string, Element> responseObjectsCollections = new Dictionary<string, Element>();
        private int iterator = 0;
        private string key = String.Empty;

        public List<Entity> AgregatedNodes = new List<Entity>();
        public List<Link> AgregatedLinks = new List<Link>();


        public Neo4jUnwrapedResult(IStatementResult result)
        {
            Parce(result);
        }


        private void Parce(IStatementResult result)
        {
            responseVariables = new List<string>();
            foreach (var collection in result)
            {
                foreach (var key in collection.Keys)
                {
                    UnwindElement(collection[key], key);
                }
            }
        }

        private void UnwindElement(object v, string key)
        {
            if (v is INode)
            {
                if (this.key != key)
                {
                    responseObjectsCollections.Add(key + iterator, new Element(v as INode));
                    this.key = key;
                    iterator++;
                }
                else
                {
                    responseObjectsCollections.Add(key + iterator, new Element(v as INode));
                    iterator++;
                }



                AgregatedNodes.Add(new Entity().RebuildSelf(v as INode));
            }
            else if (v is IRelationship)
            {
                if (this.key != key)
                {
                    responseObjectsCollections.Add(key + iterator, new Element(v as IRelationship));
                    this.key = key;
                    iterator++;
                }
                else
                {
                    responseObjectsCollections.Add(key + iterator, new Element(v as IRelationship));
                    iterator++;
                }
                AgregatedLinks.Add(new Link().RebuildSelf((v as IRelationship)));
            }
            else if (v is IPath) responseObjectsCollections.Add(key, new Element(v as IPath));
            else if (v is IDictionary) responseObjectsCollections.Add(key, new Element(v as IDictionary));
            else if (v is IList) responseObjectsCollections.Add(key, new Element(v as IList));
            else if (v is String) responseObjectsCollections.Add(key, new Element(v as String));
            else if (v is Byte[]) responseObjectsCollections.Add(key, new Element(v as Byte[]));
            else if (v is Double) responseObjectsCollections.Add(key, new Element((Double)v));
            else if (v is Int64) responseObjectsCollections.Add(key, new Element((Int64)v));
            else if (v is Boolean) responseObjectsCollections.Add(key, new Element((Boolean)v));
            else if (v is null) responseObjectsCollections.Add(key, Element.Empty);
            else throw new InvalidCastException("Converter not support that Neo4j type");
        }
    }
}
