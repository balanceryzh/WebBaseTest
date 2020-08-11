using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Domains.Entities
{
    [SugarTable("person")]
    public class personEntity : FullEntity
    {
        public personEntity()
        {
        }

        public string OpenId { get; set; }


        public string Name { get; set; }

        public int Age { get; set; }



    }
}