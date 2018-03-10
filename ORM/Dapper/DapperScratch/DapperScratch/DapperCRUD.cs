using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DapperScratch
{
    public class DapperCRUD
    {
        IDbConnection connection = new SqlConnection("Data Source=.;Initial Catalog=DapperLab;Integrated Security=True;MultipleActiveResultSets=True");

        public int SingleInsert(string userName, string email, string address)
        {
            var affectedRows = connection.Execute("insert into Users values (@UserName, @Email, @Address)",
                                           new { UserName = userName, Email = email, Address = address });

            return affectedRows;
        }

        public int BulkInsert(IEnumerable<Users> newUsers)
        {
            return connection.Execute("insert into Users values (@UserName, @Email, @Address)", newUsers);
        }

        public IEnumerable<Users> QueryByUserName(string userName)
        {
            var result = connection.Query<Users>("select * from Users where UserName=@UserName",
                new { UserName = userName });

            return result;
        }

        public IEnumerable<Users> SearchByUserName(string userName)
        {
            var result = connection.Query<Users>("select * from Users where UserName like @UserName",
                new { UserName = userName + "%" });

            return result;
        }
    }

    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
