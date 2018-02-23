using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDB.Models.Errors
{
    public class EntityNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "По указанным параметрам сущность не найдена. Проверьте базу и параметры запроса";
            }
        }
    }
}
