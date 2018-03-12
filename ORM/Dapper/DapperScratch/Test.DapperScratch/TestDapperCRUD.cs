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

        [Fact]
        public void TestUpdate()
        {
            var testUserName = $"JeremyLiu_{DateTime.Now.Ticks}";

            dapperCrud.SingleInsert(testUserName, "dapper@test.com", "dummy address");
            var insertResult = dapperCrud.QueryByUserName(testUserName).SingleOrDefault();

            var updateUserName = $"JeremyLiu_update_{DateTime.Now.Ticks}";
            dapperCrud.UpdateUser(insertResult.UserID, updateUserName);

            var updateResult = dapperCrud.QueryByUserName(updateUserName).SingleOrDefault();
            Assert.NotNull(updateResult);
        }

        [Fact]
        public void TestDelete()
        {
            var testUserName = $"JeremyLiu_{DateTime.Now.Ticks}";

            dapperCrud.SingleInsert(testUserName, "dapper@test.com", "dummy address");
            var insertResult = dapperCrud.QueryByUserName(testUserName).SingleOrDefault();

            dapperCrud.Delete(insertResult.UserID);

            var updateResult = dapperCrud.QueryByUserName(testUserName).FirstOrDefault();
            Assert.Null(updateResult);
        }

        [Fact]
        public void TestInClauseSearch()
        {
            // Given
            var testUserNameEmail = $"JeremyLiu@{DateTime.Now.Ticks}.com";
            var newUsers = Enumerable.Range(0, 6).Select(i => new Users
            {
                UserName = $"JeremyLiu_{i}",
                Email = testUserNameEmail,
                Address = $"dummy address-{i}"
            }).ToArray();
            dapperCrud.BulkInsert(newUsers);

            var testUserNameEmail2 = $"JeremyLiu@{DateTime.Now.Ticks}.com";
            var newUsers2 = Enumerable.Range(0, 4).Select(i => new Users
            {
                UserName = $"JeremyLiu_{i}",
                Email = testUserNameEmail2,
                Address = $"dummy address-{i}"
            });
            dapperCrud.BulkInsert(newUsers2);

            // When
            var searchUsers = dapperCrud.InClause(new[] { testUserNameEmail, testUserNameEmail2 });

            // Then
            Assert.Equal(10, searchUsers.Length);

            var testUser = searchUsers.Where(u => u.Email == testUserNameEmail);
            var testUser2 = searchUsers.Where(u => u.Email == testUserNameEmail2);
            Assert.Equal(6, testUser.Count());
            Assert.Equal(4, testUser2.Count());
        }

        [Fact]
        public void TestMultipleReader()
        {
            // Given

            // When
            // Then
        }
    }
}
