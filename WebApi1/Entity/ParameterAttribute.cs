using System;
using WebApi1.EnumBase;


namespace WebApi1.Entity
{
    public class ParameterAttribute : Attribute
    {
        public string Field { get; set; }
        public ParameterType Type { get; set; }

        public bool OrderByDesc { get; set; }
        public bool OrderByAsc { get; set; }

        public bool GroupBy { get; set; }
    }
}