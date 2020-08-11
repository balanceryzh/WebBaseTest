using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WebApi1.Dapper
{
    public class DapperBase
    {

    }

    public class Person
    {
        public int Id { get; set; }
        public string OpenId { get; set; }
        public string Name { get; set; }
        public ushort Age { get; set; }
        public PersonGroup Group { get; set; }
    }
    public class PersonGroup
    {
        public int Id { get; set; }
        public string OpenId { get; set; }
        public string Name { get; set; }
        public List<Person> Persons { get; set; }
    }
    public class UserDapper
    {
        DbConnection GetConnection()
        {
            string dbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["ms_colleges"].ToString();
            DbConnection dbConnection = new MySqlConnection(dbConnStr);
            dbConnection.Open();
            return dbConnection;
        }

        public void Show()
        {
            try
            {
                List<Person> personList;
                List<PersonGroup> personGroupList;
                Person person1;
                Person person2;
                var obj = new { OpenId = "d3d19ed7376d4a388a1a8723c92ac05d", OpenId2 = "41c2f8fe6d084cb3a6be0b95df2bf0af" };
                using (IDbConnection conn = GetConnection())
                {
                    //返回受影响行数
                    string sql = "UPDATE `person` SET Age=Age+1;";
                    int number = conn.Execute(sql, obj);//返回受影响行数

                    //返回集合
                    string sql1 = "SELECT * FROM `person`;";
                    personList = conn.Query<Person>(sql1, obj).ToList();//查询多条

                    //查询1条
                    string sql2 = "SELECT * FROM `person` WHERE OpenId=@OpenId;";
                    person1 = conn.QueryFirst<Person>(sql2, obj);//多条结果时，返回第1条。无结果时抛出异常
                    person2 = conn.QueryFirstOrDefault<Person>(sql2, obj);//多条结果时，返回第1条。无结果时返回默认值
                    string sql3 = "SELECT * FROM `person` ORDER BY Id DESC LIMIT 1 ;";
                    person1 = conn.QuerySingle<Person>(sql3, obj);//多条结果时或无结果时抛出异常
                    person2 = conn.QuerySingleOrDefault<Person>(sql3, obj);//多条结果时抛出异常，无结果时返回默认值

                    //多条语句执行 注意查询语句顺序与读取顺序
                    string sql4 = "SELECT * FROM `person` WHERE OpenId=@OpenId;SELECT * FROM `person_group` WHERE OpenId=@OpenId2;";
                    using (var reader = conn.QueryMultiple(sql4, obj))
                    {
                        personList = reader.Read<Person>().ToList();
                        personGroupList = reader.Read<PersonGroup>().ToList();
                    }

                    //结果多映射  注意查询语句顺序与数据模型顺序
                    //一对一  注意调用Distinct()
                    string sql5 = "SELECT * FROM `person` p LEFT JOIN `person_group` pg ON p.GroupId=pg.Id;";
                    personList = conn.Query<Person, PersonGroup, Person>(sql5, (person, personGroup) =>
                    {
                        person.Group = personGroup;
                        return person;
                    }).Distinct().ToList();
                    //一对多 注意调用Distinct() 
                    var groupDic = new Dictionary<int, PersonGroup>();
                    personGroupList = conn.Query<Person, PersonGroup, PersonGroup>(sql5, (person, personGroup) =>
                    {
                        PersonGroup groupEntry;
                        if (!groupDic.TryGetValue(personGroup.Id, out groupEntry))
                        {
                            groupEntry = personGroup;
                            groupEntry.Persons = new List<Person>();
                            groupDic.Add(groupEntry.Id, groupEntry);
                        }
                        groupEntry.Persons.Add(person);
                        return groupEntry;
                    }).Distinct().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}