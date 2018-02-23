using GraphDB.Models.Logic;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB.Models.Ontology
{
    public class Entity
    {
        public long Id { get; private set; }

        public string[] Labels { get; private set; }

        public Dictionary<string, object> Properties { get; private set; } = new Dictionary<string, object>();

        public LogicElement LogicElement { get; private set; }

        public Entity(string label) : base()
        {
            Labels = new string[1] { label };
        }

        public Entity(string[] labels) : base()
        {
            Labels = labels;
        }

        public Entity(long id) : base()
        {
            Id = id;
        }

        public Entity()
        {
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

        internal Entity RebuildSelf(INode node)
        {
            Id = node.Id;
            Labels = node.Labels.ToArray();
            foreach (var item in node.Properties)
            {
                if (Properties.ContainsKey(item.Key)) Properties.Remove(item.Key);
                Properties.Add(item.Key, item.Value);
            }
            
            LogicElement = new LogicElement(Properties);

            return this;
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
    }
}
