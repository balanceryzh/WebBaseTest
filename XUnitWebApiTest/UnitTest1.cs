
using WebApi1.Dapper;
using Xunit;

namespace XUnitWebApiTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            UserDapper userDapper = new UserDapper();
            userDapper.Show();
        }
    }
}
