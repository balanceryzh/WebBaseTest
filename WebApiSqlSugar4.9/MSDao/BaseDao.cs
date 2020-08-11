using System;
using System.Collections.Generic;
using System.Reflection;
using WebApi1.Entity;
using WebApi1.EnumBase;
using WebApi1.Librarys;

namespace WebApi1.MSDao
{

        public class BaseDao
        {
            protected DBHelper _helper = new DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["ms_colleges"].ToString());
            public BaseDao()
            {
            }

            public T AutoTry<T>(Func<T, T> ac)
            {
                dynamic result = (T)Activator.CreateInstance(typeof(T));
                try
                {
                    result = ac.Invoke(result);
                }
                catch (Exception ex)
                {
                    Log.WriteLogLocal(result + " 错误信息：" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.TargetSite + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, "MSDao");
                }
                return result;
            }


            public string GetGUID()
            {
                return Guid.NewGuid().ToString("N");
            }

            /// <summary> 将id数组转换成id字符串（逗号分割）</summary>
            public string GetIdsString(IList<string> ids)
            {
                string strIds = "";
                foreach (string id in ids)
                    strIds += "'" + id + "',";
                strIds = strIds.TrimEnd(',');

                return strIds;
            }

            public string GetIdsString(IList<long> ids)
            {
                string strIds = "";
                foreach (long id in ids)
                    strIds += id + ",";
                strIds = strIds.TrimEnd(',');

                return strIds;
            }

            public static string GetSearchSqlCondition(object request, bool limit = true)
            {
                string result = "";
                try
                {
                    Type type = request.GetType();
                    int from = 0;
                    int count = 0;
                    List<string> orderListDesc = new List<string>();
                    List<string> orderListAsc = new List<string>();
                    List<string> groupbyList = new List<string>();
                    bool kd = true;
                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        kd = true;
                        string key = prop.Name;
                        string symbol = "=";
                        string value = "";
                        if (prop.GetValue(request) != null)
                        {
                            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(decimal))
                            {
                                if (Convert.ToInt64(prop.GetValue(request)) > 0 || Convert.ToInt32(prop.GetValue(request)) > 0 || Convert.ToDecimal(prop.GetValue(request)) > 0 || Convert.ToDouble(prop.GetValue(request)) > 0)
                                {
                                    value = prop.GetValue(request).ToString();
                                    kd = false;
                                }
                            }
                            else if (prop.PropertyType == typeof(DateTime))
                            {
                                if (Convert.ToDateTime(prop.GetValue(request)) > Convert.ToDateTime("1900/01/01"))
                                {
                                    value = prop.GetValue(request).ToString();
                                }
                            }
                            else if (prop.PropertyType == typeof(bool?) || prop.PropertyType == typeof(bool))
                            {
                                if ((bool)prop.GetValue(request))
                                    value = "1";
                                else
                                    value = "0";
                                kd = false;
                            }
                            else
                            {
                                value = prop.GetValue(request).ToString();
                            }
                        }
                        var x = prop.GetCustomAttribute(typeof(ParameterAttribute), true);
                        if (x != null)
                        {

                            ParameterAttribute item = (ParameterAttribute)x;
                            if (!string.IsNullOrEmpty(item.Field))
                                key = item.Field;
                            if (value == "1" && !string.IsNullOrEmpty(item.Field) && (prop.PropertyType == typeof(bool?) || prop.PropertyType == typeof(bool)))
                            {
                                if (item.OrderByDesc && !orderListDesc.Contains(key))
                                    orderListDesc.Add(key);
                                if (item.OrderByAsc && !orderListAsc.Contains(key))
                                    orderListAsc.Add(key);

                                value = null;
                            }
                            else if (value == "0" && !string.IsNullOrEmpty(item.Field) && (prop.PropertyType == typeof(bool?) || prop.PropertyType == typeof(bool)))
                            {
                                value = null;
                            }
                            else
                            {
                                if (item.OrderByDesc && string.IsNullOrEmpty(value) && !orderListDesc.Contains(key))
                                    orderListDesc.Add(key);
                                if (item.OrderByAsc && string.IsNullOrEmpty(value) && !orderListAsc.Contains(key))
                                    orderListAsc.Add(key);
                            }

                            if (item.GroupBy && !groupbyList.Contains(key))
                            {
                                groupbyList.Add(key);
                            }

                            switch (item.Type)
                            {
                                case ParameterType.EQUAL:
                                    symbol = "=";
                                    break;
                                case ParameterType.LIKE:
                                    symbol = "like";
                                    if (prop.GetValue(request) != null && !string.IsNullOrEmpty(prop.GetValue(request).ToString()))
                                        value = "%" + prop.GetValue(request).ToString() + "%";
                                    break;
                                case ParameterType.RLIKE:
                                    symbol = "like";
                                    if (prop.GetValue(request) != null && !string.IsNullOrEmpty(prop.GetValue(request).ToString()))
                                        value = prop.GetValue(request).ToString() + "%";
                                    break;
                                case ParameterType.LLIKE:
                                    symbol = "like";
                                    if (prop.GetValue(request) != null && !string.IsNullOrEmpty(prop.GetValue(request).ToString()))
                                        value = "%" + prop.GetValue(request).ToString();
                                    break;
                                case ParameterType.GREATER:
                                    symbol = ">";
                                    break;
                                case ParameterType.LESS:
                                    symbol = "<";
                                    break;
                                case ParameterType.GREATERINCLUDE:
                                    symbol = ">=";
                                    break;
                                case ParameterType.LESSINCLUDE:
                                    symbol = "<=";
                                    break;
                                case ParameterType.UNEQUAL:
                                    symbol = "<>";
                                    break;
                                case ParameterType.IN:
                                    symbol = "in";
                                    if (prop.GetValue(request) != null)
                                    {
                                        string inValue = "";
                                        foreach (var v in (Array)prop.GetValue(request))
                                        {
                                            inValue += "'" + v + "',";
                                        }
                                        value = "(" + inValue.TrimEnd(',') + ")";
                                        kd = false;
                                    }
                                    break;
                                case ParameterType.NOTIN:
                                    symbol = "not in";
                                    if (prop.GetValue(request) != null)
                                    {
                                        string inValue = "";
                                        foreach (var v in (Array)prop.GetValue(request))
                                        {
                                            inValue += "'" + v + "',";
                                        }
                                        value = "(" + inValue.TrimEnd(',') + ")";
                                        kd = false;
                                    }
                                    break;
                                case ParameterType.LIMITFROM:
                                    from = Convert.ToInt32(prop.GetValue(request));
                                    value = "";
                                    break;
                                case ParameterType.LIMITCOUNT:
                                    count = Convert.ToInt32(prop.GetValue(request));
                                    value = "";
                                    break;

                            }
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (kd)
                                    result += string.Format(" and {0} {1} '{2}' ", key, symbol, value);
                                else
                                    result += string.Format(" and {0} {1} {2} ", key, symbol, value);
                            }

                        }

                    }
                    if (orderListDesc.Count > 0 && limit)
                    {
                        string orderValue = "";
                        foreach (var v in orderListDesc)
                        {
                            orderValue += v + " desc ,";
                        }
                        result += string.Format(" order by {0}", orderValue.TrimEnd(','));
                    }

                    if (orderListAsc.Count > 0 && limit)
                    {
                        string orderValue = "";
                        foreach (var v in orderListAsc)
                        {
                            orderValue += v + " asc ,";
                        }
                        result += string.Format(" order by {0}", orderValue.TrimEnd(','));
                    }

                    if (groupbyList.Count > 0)
                    {
                        string groupbyValues = "";
                        foreach (var item in groupbyList)
                        {
                            groupbyValues += item + " , ";
                        }
                        char[] MyChar = { ',', ' ' };
                        result += string.Format(" group by {0}", groupbyValues.TrimEnd(MyChar));
                    }

                    if ((from > 0 || count > 0) && limit)
                        result += string.Format(" limit {0},{1}", from, count);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }

            public static string GetUpdateSqlCondition(object request)
            {
                string result = "";
                try
                {
                    Type type = request.GetType();
                    List<string> orderListDesc = new List<string>();
                    List<string> orderListAsc = new List<string>();
                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        string key = prop.Name;
                        string symbol = "=";
                        string value = "";
                        if (prop.GetValue(request) != null)
                        {

                            if (prop.PropertyType == typeof(string))
                            {
                                if (!string.IsNullOrEmpty(prop.GetValue(request).ToString()))
                                {
                                    result += key + symbol + "'" + prop.GetValue(request).ToString() + "',";
                                }
                            }
                            else if (prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(long?) || prop.PropertyType == typeof(double?) || prop.PropertyType == typeof(decimal?))
                            {
                                if (prop.GetValue(request) != null)
                                {
                                    result += key + symbol + prop.GetValue(request).ToString() + ",";
                                }
                            }
                            else if (prop.PropertyType == typeof(DateTime?))
                            {
                                if (prop.GetValue(request) != null)
                                {
                                    result += key + symbol + prop.GetValue(request).ToString() + ",";
                                }
                            }
                            else if (prop.PropertyType == typeof(bool?))
                            {
                                if (prop.GetValue(request) != null)
                                {
                                    result += key + symbol + prop.GetValue(request).ToString() + ",";
                                }
                            }
                        }

                    }
                    result = result.TrimEnd(',');

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return result;
            }
        }
    
}