using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OntologyModel
{
    public class LogicalElement
    {
        public Entity[] Targets { get; set; }

        public int Value { get; set; }

        public LogicalType Type { get; set; }






        public LogicalElement BuildSelf(Dictionary<string, object> collection)
        {
            //var targ = new List<INeo4jObject>();
            //var i = 0;

            //while (collection.ContainsKey("Target_" + i))
            //{
            //    targ.Add(new Entity(collection["Target_" + i] as string));
            //}
            //Targets = targ.ToArray();

            //if (EventAvalible)
            //{
            //    foreach (var item in Targets)
            //    {
            //        var rlt = Rebuiding.Invoke(item);
            //    }
            //}


            if (collection.ContainsKey("Value") && collection["Value"] is int) Value = (int)collection["Value"];
            if (collection.ContainsKey("Type") && collection["Type"] is LogicalType) Type = (LogicalType)collection["Type"];

            return this;

        }
        public Dictionary<string, object> CreateLogicParams()
        {
            var dictionary = new Dictionary<string, object>();
            switch (this.Type)
            {
                case LogicalType.AnyActive:
                    ExtractTargets(dictionary);
                    ExtractValues(dictionary);
                    break;
                case LogicalType.AllActive:
                    ExtractTargets(dictionary);
                    ExtractValues(dictionary);
                    break;
                case LogicalType.ValueActivation:
                    ExtractTargets(dictionary);
                    ExtractValues(dictionary);
                    break;
                case LogicalType.ValueSender:
                    ExtractValues(dictionary);
                    break;
                default:
                    break;
            }

            

            return dictionary;
        }

        private void ExtractValues(Dictionary<string, object> dictionary)
        {
            dictionary.Add("Type", Type.ToString());
            dictionary.Add("Value", Value);
        }

        private void ExtractTargets(Dictionary<string, object> dictionary)
        {
            for (int i = 0; i < Targets.Length; i++)
            {
                dictionary.Add("Target_" + i, Targets[i].GetId());
            }
        }

        public Dictionary<string, object> CreateLogicParams(Dictionary<string, object> dictionary)
        {
            return dictionary.Concat(CreateLogicParams()) as Dictionary<string, object>;
        }
    }

    public enum LogicalType
    {
        AnyActive,
        AllActive,
        ValueActivation,
        ValueSender
    }
}
