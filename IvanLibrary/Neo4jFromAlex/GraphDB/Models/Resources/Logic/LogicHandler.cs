using GraphDB.Models.Errors;
using GraphDB.Models.Ontology;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDB.Models.Logic
{
    public class LogicHandler
    {
        private TransactionManager _mng;

        public LogicHandler(TransactionManager manager)
        {
            _mng = manager;
        }

        public bool Handle(LogicElement element)
        {
            var entities = new List<Entity>();
            foreach (var item in element.TargetsKeys)
            {
                var rlt = _mng.GetEntityById(item);

                rlt = _mng.GetEntityById(item);
                foreach (var field in rlt.AgregatedNodes)
                {

                    entities.Add(field);

                }
            }
            foreach (var item in element.TargetsKeys)
            {
                if (!entities.Any(x => x.Id == item)) throw new EntityNotFoundException();
            }

            var activeEntities = entities.Where(x => x.LogicElement.IsActive == true).ToList();

            switch (element.LogicType)
            {
                case LogicType.ValueSender:
                    return true;
                case LogicType.OneOfTargets:
                    return Handle_OneOfTargets(activeEntities);
                case LogicType.AllTargets:
                    return Handle_AllTargets(activeEntities);
                case LogicType.ValueMoreThan:
                    return Handle_ValueMoreThan(activeEntities, (int)element.Value);
                case LogicType.valueLessThan:
                    return Handle_valueLessThan(activeEntities, (int)element.Value);
                case LogicType.Dummy:
                    return true;
                default:
                    throw new Exception("Logic type incorrect");
            }
        }

        private bool Handle_valueLessThan(List<Entity> entities, int value) => entities.Sum(x => x.LogicElement.Value) <= value;

        private bool Handle_ValueMoreThan(List<Entity> entities, int value) => entities.Sum(x => x.LogicElement.Value) >= value;

        private bool Handle_AllTargets(List<Entity> entities) => entities.All(x => x.LogicElement.IsActive == true);

        private bool Handle_OneOfTargets(List<Entity> entities) => entities.Any(x => x.LogicElement.IsActive == true);

    }
}
