using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OntologyModel
{
    /// <summary>
    /// Представляет модель данных узла для взаимодействия с БД Neo4j
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Уникальный идентификатор записи. Используется для упрощения автоматизированного поиска сущности. Предотвращает дублирование записей
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Название класса сущности
        /// </summary>
        public string Class { get; } = "Entity";

        /// <summary>
        /// Название сущности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Состояние узла. Элемент логики
        /// </summary>
        public bool IsActive { get; set; }

        public LogicalElement Logic { get; set; }


        public Dictionary<string, string> AtributesDictionary = new Dictionary<string, string>();

        public string[] Atributes
        {
            get
            {
                return AtributesDictionary.Select(x => x.Key + ":" + x.Value).ToArray();
            }
            set
            {
                foreach (var item in value)
                {
                    var parcedString = item.Split(':');
                    if (AtributesDictionary.ContainsKey(parcedString[0]))
                    {
                        if (AtributesDictionary[parcedString[0]] != parcedString[1]) AtributesDictionary[parcedString[0]] = parcedString[1];
                    }
                    else AtributesDictionary.Add(parcedString[0], parcedString[1]);
                }
            }
        }

        public Entity()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public Entity(string Id)
        {
            if (Guid.TryParse(Id, out Guid res))
            {
                this.Id = Id;
            }
        }

        public Entity BuildItem(Dictionary<string, object> dictionary)
        {
            if (dictionary["Id"] != null ? dictionary["Id"] is string : false) Id = (string)dictionary["Id"];
            //if (dictionary["Class"] != null ? dictionary["Class"] is string : false) Class = (string)dictionary["Class"];
            if (dictionary["Name"] != null ? dictionary["Name"] is string : false) Name = (string)dictionary["Name"];
            if (dictionary["IsActive"] != null ? dictionary["IsActive"] is bool : false) IsActive = (bool)dictionary["IsActive"];
            if (dictionary["Atributes"] != null ? dictionary["Atributes"] is string[] : false) Atributes = (string[])dictionary["Atributes"];

            Logic = Logic.BuildSelf(dictionary);


            return this;
        }

        public Dictionary<string, object> GetParameters()
        {
            var rlt = new Dictionary<string, object>();

            if (Id != null) rlt.Add("Id", Id);
            if (Class != null) rlt.Add("Class", Class);
            if (Name != null) rlt.Add("Name", Name);
            rlt.Add("IsActive", IsActive);
            if (Atributes != null) rlt.Add("Atributes", Atributes);

            if (Logic != null)
            {
                foreach (var item in Logic.CreateLogicParams())
                {
                    rlt.Add(item.Key, item.Value);
                }
            }
            return rlt; 
        }

        public string GetId()
        {
            return Id;
        }
    }
}
