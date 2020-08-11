using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebApi1.Entity.Data.User;
using Dapper;
using System.Data;

namespace WebApi1.MSDao.User
{
    public class UserDao : BaseDao
    {
        public TB_User AddUser(TB_User user)
        {
            return AutoTry<TB_User>((result) =>
            {
                if (string.IsNullOrEmpty(user.Id))
                    user.Id = GetGUID();
                _helper.Insert(user);
                return user;
            });
        }


        public bool BatchAddUser(List<TB_User> users)
        {
            return AutoTry<bool>((result) =>
            {
                List<string> sqls = new List<string>();
                foreach (var item in users)
                {
                    sqls.Add(_helper.LoadInsertSql(item));
                }
                _helper.BatchExecuteNonQuery(sqls);
                return true;
            });
        }
        public int RemoveUser(string id)
        {
            return AutoTry<int>((result) =>
            {
                string sql = string.Format("Delete from TB_User where Id = '{0}'", id);
                return _helper.ExecuteNonQuery(sql);
            });
        }

        public TB_User SaveUser(TB_User user)
        {
            return AutoTry<TB_User>((result) =>
            {
                _helper.Update(user);
                return user;
            });
        }
        public TB_User GetUserByAccountId(string accountid)
        {
            return AutoTry<TB_User>((result) =>
            {
                string sql = string.Format("select * from TB_User where AccountId = '{0}'", accountid);
                return _helper.ReadObject<TB_User>(sql);
            });
        }
    }

}