using System;
using System.Linq;
using DapperScratch;
using Xunit;

namespace Test.DapperScratch
{
    public class TestDapperCRUD
    {
        private DapperCRUD dapperCrud;
        public TestDapperCRUD()
        {
            dapperCrud = new DapperCRUD();
        }

        [Fact]
        public void TestSingleInsert()
        {
            var testUserName = $"JeremyLiu_{DateTime.Now.Ticks}";
            var rows = dapperCrud.SingleInsert(testUserName, "dapper@test.com", "dummy address");
            Assert.Equal(1, rows);

            var insertResult = dapperCrud.QueryByUserName(testUserName).SingleOrDefault();
            Assert.Equal(testUserName, insertResult.UserName);
        }

        [Fact]
        public void TestBulkInsert()
        {
            var testUserName = $"JeremyLiu_{DateTime.Now.Ticks}";
            var newUsers = Enumerable.Range(0, 10).Select(i => new Users
            {
                UserName = $"{testUserName}_{i}",
                Email = $"dapper@test.com_{i}",
                Address = $"dummy address-{i}"
            });

            dapperCrud.BulkInsert(newUsers);

            var bulkInsertResults = dapperCrud.SearchByUserName(testUserName).ToArray();
            Assert.Equal(10, bulkInsertResults.Count());
            Assert.True(bulkInsertResults.All(u => u.UserName.Contains(testUserName)));
        }
    }
}
