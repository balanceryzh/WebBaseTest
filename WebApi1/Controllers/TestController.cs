using System.Web.Http;
using WebApi1.Dapper;
using WebApi1.Domains;
using WebApi1.Domains.Entities;
using WebApi1.SqlSugarBase;
namespace WebApi1.Controllers
{
    public class TestController : ApiController
    {

        public string Get(string userName = null)
        {
            //UserDapper userDapper = new UserDapper();
            //userDapper.Show();
            var i=IsExist(userName);
            return i.ToString();
        }


        public bool IsExist(string userName = null)
        {
            RepositoryConnection repositoryConnection = new RepositoryConnection();
            repositoryConnection.ConnectionString = "server = localhost; uid = root; pwd = 123456; port = 3306; database = dappertest; SslMode = None; pooling = true; min pool size = 3;";
            repositoryConnection.DatabaseType = EnumBase.EnumDatabaseType.MySql;

            SqlSugarRepository sqlSugarRepository = new SqlSugarRepository(repositoryConnection);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                //userName = userName.ToUpper();
                return sqlSugarRepository.Any<personEntity>(x => x.Name == userName);
            }

            //if (!string.IsNullOrWhiteSpace(account))
            //{
            //    account = account.ToUpper();
            //    return sqlSugarRepository.Any<AccountEntity>(x => x.NormalizedAccount == account);
            //}

            //if (!string.IsNullOrWhiteSpace(mobile))
            //{
            //    return sqlSugarRepository.Any<AccountEntity>(x => x.Mobile == userName);
            //}

            //if (!string.IsNullOrWhiteSpace(email))
            //{
            //    email = email.ToUpper();
            //    return sqlSugarRepository.Any<AccountEntity>(x => x.NormalizedEmail == email);
            //}

            //if (!string.IsNullOrWhiteSpace(no))
            //{
            //    return sqlSugarRepository.Any<AccountEntity>(x => x.No == no);
            //}

            return false;
        }
    }
}
