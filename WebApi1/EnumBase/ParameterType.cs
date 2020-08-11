namespace WebApi1.EnumBase
{
    public enum ParameterType
    {
        EQUAL = 0,
        LIKE = 10,
        RLIKE = 11,
        LLIKE = 12,
        GREATER = 30,
        GREATERINCLUDE = 32,
        LESS = 31,
        LESSINCLUDE = 33,
        UNEQUAL = 34,
        LIMITFROM = 40,
        LIMITCOUNT = 41,
        IN = 50,
        NOTIN = 51,

    }
}