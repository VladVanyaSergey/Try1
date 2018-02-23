using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB.Models.Logic
{
    public class LogicElement
    {
        private const string Prefix = "Logic_";

        public long[] TargetsKeys { get; private set; }
        private const string TargetsKeysFieldName = "TargetsKeys";

        public int Value { get; set; }
        private const string ValueFieldName = "Value";

        public LogicType LogicType { get; private set; }
        private const string LogicTypeFieldName = "LogicType";

        public bool IsActive { get; private set; }
        private const string IsActiveFieldName = "IsActive";

        public LogicElement(LogicType type)
        {
            LogicType = type;
        }

        public LogicElement(Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey(Prefix + TargetsKeysFieldName))
            {
                TargetsKeys = dictionary[Prefix + TargetsKeysFieldName].As<List<long>>().ToArray();
                               
            }
            if (dictionary.ContainsKey(Prefix + ValueFieldName))
            {
                Value = dictionary[Prefix + ValueFieldName].As<int>();
                
            }
            if (dictionary.ContainsKey(Prefix + LogicTypeFieldName))
            {
                System.Enum.TryParse(dictionary[Prefix + LogicTypeFieldName] as string, out LogicType type);
                LogicType = type;
            }
            if (dictionary.ContainsKey(Prefix + IsActiveFieldName))
            {
                IsActive = dictionary[Prefix + IsActiveFieldName].As<Boolean>();
            }
        }

        public Dictionary<string, object> ToDictionary()
        {

            var rlt = new Dictionary<string, object>
            {
                { Prefix + ValueFieldName, Value },
                { Prefix + LogicTypeFieldName, LogicType.ToString() },
                { Prefix + IsActiveFieldName, IsActive }
            };
            if (TargetsKeys != null)
            {
                rlt.Add(Prefix + TargetsKeysFieldName, TargetsKeys); 
            }
            return rlt;

        }

        public bool AddTarget(Entity node)
        {
            if (TargetsKeys == null) { TargetsKeys = new long[0]; }
            var a = new List<long>(TargetsKeys);
            a.Add(node.Id);
            TargetsKeys = a.ToArray();
            return true;
        }

        public void Activate()
        {
            IsActive = true;

        }
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
