using GraphDB.Models.Logic;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB.Models.Ontology
{
    public class Link
    {
        public long Id { get; set; }

        public long StartNodeId { get; private set; }
        public long EndNodeId { get; private set; }

        public string Type { get; private set; }

        public Dictionary<string, object> Properties { get; private set; } = new Dictionary<string, object>();

        public LogicElement LogicElement { get; private set; }

        public Link()
        {
            Properties = new Dictionary<string, object>();
        }

        public Link(string type) : base()
        {
            Type = type;
        }

        public Link(long id) : base()
        {
            Id = id;
        }

        public void AddConnection(Entity source, Entity target)
        {
            if (source.Id == 0 || target.Id == 0) throw new ArgumentNullException();

            StartNodeId = source.Id;
            EndNodeId = target.Id;
        }

        public bool AddProperty(string key, object item)
        {
            key = key.Replace(' ', '_');
            if (Properties.ContainsKey(key))
            {
                Properties.Remove(key);
            }
            Properties.Add(key, item);
            return true;

        }

        public void AddProperty(Dictionary<string, object> dictionary)
        {
            foreach (var item in dictionary)
            {
                AddProperty(item.Key, item.Value);
            }
        }

        public bool AddLogic(LogicElement logic)
        {
            if (LogicElement == null)
            {
                LogicElement = logic;
                foreach (var item in logic.ToDictionary())
                {
                    Properties.Add(item.Key, item.Value);
                }
                return true;
            }
            try
            {
                foreach (var item in LogicElement.ToDictionary())
                {
                    Properties.Remove(item.Key);
                }
                foreach (var item in logic.ToDictionary())
                {
                    Properties.Add(item.Key, item.Value);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool RemoveProperty(string key)
        {
            try
            {
                return Properties.Remove(key);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Link RebuildSelf(IRelationship link)
        {
            Id = link.Id;
            Type = link.Type;
            foreach (var item in link.Properties)
            {
                if (Properties.ContainsKey(item.Key)) Properties.Remove(item.Key);
                Properties.Add(item.Key, item.Value);
            }

            LogicElement = new LogicElement(Properties);

            return this;
        }
    }
}
