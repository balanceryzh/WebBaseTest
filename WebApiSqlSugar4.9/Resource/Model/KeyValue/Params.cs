namespace WebApi1.Resource
{
    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="T1">参数1</typeparam>
    public class TParam<T1>
    {
        /// <summary>
        /// 参数1
        /// </summary>
        public T1 Param1 { get; set; }
    }

    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="T1">参数1</typeparam>
    /// <typeparam name="T2">参数2</typeparam>
    public class TParam<T1, T2> : TParam<T1>
    {
        /// <summary>
        /// 参数2
        /// </summary>
        public T2 Param2 { get; set; }
    }

    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="T1">参数1</typeparam>
    /// <typeparam name="T2">参数2</typeparam>
    /// <typeparam name="T3">参数3</typeparam>
    public class TParam<T1, T2, T3> : TParam<T1, T2>
    {
        /// <summary>
        /// 参数3
        /// </summary>
        public T3 Param3 { get; set; }
    }

    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="T1">参数1</typeparam>
    /// <typeparam name="T2">参数2</typeparam>
    /// <typeparam name="T3">参数3</typeparam>
    /// <typeparam name="T4">参数4</typeparam>
    public class TParam<T1, T2, T3, T4> : TParam<T1, T2, T3>
    {
        /// <summary>
        /// 参数4
        /// </summary>
        public T4 Param4 { get; set; }
    }

    /// <summary>
    /// 参数
    /// </summary>
    /// <typeparam name="T1">参数1</typeparam>
    /// <typeparam name="T2">参数2</typeparam>
    /// <typeparam name="T3">参数3</typeparam>
    /// <typeparam name="T4">参数4</typeparam>
    /// <typeparam name="T5">参数5</typeparam>
    public class TParam<T1, T2, T3, T4, T5> : TParam<T1, T2, T3, T4>
    {
        /// <summary>
        /// 参数5
        /// </summary>
        public T5 Param5 { get; set; }
    }
}