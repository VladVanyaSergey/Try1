using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OntologyModel
{
    public class Link
    {
        public Entity Source { get; set; }
        public Entity Target { get; set; }

        public long id { get; private set; }
        public string Class { get; } = "Link";
        public string Name { get; set; }
        public bool IsActive { get; set; }

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

        

        public Link()
        {
            
        }

        public Link(string Id)
        {
            
        }




        public string GetId()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetParameters()
        {
            var rlt = new Dictionary<string, object>();

            rlt.Add("Id", id);
            rlt.Add("IsActive", IsActive);

            if (Class != null) rlt.Add("Class", Class);
            if (Name != null) rlt.Add("Name", Name);
            if (Atributes != null) rlt.Add("Atributes", Atributes);

            return rlt;
        }

        public Link BuildItem(Dictionary<string, object> dictionary)
        {


            //if (dictionary["Class"] != null ? dictionary["Class"] is string : false) Class = (string)dictionary["Class"];
            if (dictionary["Name"] != null ? dictionary["Name"] is string : false) Name = (string)dictionary["Name"];
            if (dictionary["IsActive"] != null ? dictionary["IsActive"] is bool : false) IsActive = (bool)dictionary["IsActive"];
            if (dictionary["Atributes"] != null ? dictionary["Atributes"] is string[] : false) Atributes = (string[])dictionary["Atributes"];

            return this;
        }
    }
}
