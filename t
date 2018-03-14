[1mdiff --git a/ORM/Dapper/DapperScratch/DapperScratch/DapperCRUD.cs b/ORM/Dapper/DapperScratch/DapperScratch/DapperCRUD.cs[m
[1mindex ada59dd..cbab797 100644[m
[1m--- a/ORM/Dapper/DapperScratch/DapperScratch/DapperCRUD.cs[m
[1m+++ b/ORM/Dapper/DapperScratch/DapperScratch/DapperCRUD.cs[m
[36m@@ -22,8 +22,8 @@[m [mnamespace DapperScratch[m
 [m
         public int SingleInsertProduct(string name, string description, DateTime createTime)[m
         {[m
[31m-            var affectedRows = connection.Execute("insert into Product values (@ProductName, @ProductDesc, @CreateTime)",[m
[31m-                new { ProductName = name, ProductDesc = description, CreateTime = createTime });[m
[32m+[m[32m            var affectedRows = connection.Execute("insert into Product values (@ProductName, @ProductDesc, @UserID, @CreateTime)",[m
[32m+[m[32m                new { ProductName = name, ProductDesc = description, UserID = (int?)null, CreateTime = createTime });[m
 [m
             return affectedRows;[m
         }[m
[36m@@ -51,12 +51,12 @@[m [mnamespace DapperScratch[m
 [m
         public void UpdateUser(int userId, string userName)[m
         {[m
[31m-            connection.Execute("update Users set UserName=@UserName where UserID=@UserID", new {UserID=userId, UserName = userName});[m
[32m+[m[32m            connection.Execute("update Users set UserName=@UserName where UserID=@UserID", new { UserID = userId, UserName = userName });[m
         }[m
 [m
         public void Delete(int userId)[m
         {[m
[31m-            connection.Execute("delete from Users where UserID=@userId", new {UserID = userId});[m
[32m+[m[32m            connection.Execute("delete from Users where UserID=@userId", new { UserID = userId });[m
         }[m
 [m
         public Users[] InClause(IEnumerable<string> searchEmails)[m
[36m@@ -86,6 +86,23 @@[m [mnamespace DapperScratch[m
         public string ProductDesc { get; set; }[m
         public int UserID { get; set; }[m
         public DateTime CreateTime { get; set; }[m
[32m+[m
[32m+[m[32m        public override bool Equals(object obj)[m
[32m+[m[32m        {[m
[32m+[m[32m            if (obj == null || GetType() != obj.GetType())[m
[32m+[m[32m            {[m
[32m+[m[32m                return false;[m
[32m+[m[32m            }[m
[32m+[m
[32m+[m[32m            var product = obj as Products;[m
[32m+[m
[32m+[m[32m            return product.ProductName == this.ProductName && product.ProductDesc == this.ProductDesc && product.UserID == this.UserID;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public override int GetHashCode()[m
[32m+[m[32m        {[m
[32m+[m[32m            return this.ProductName.GetHashCode() ^ this.ProductDesc.GetHashCode() ^ this.UserID.GetHashCode();[m
[32m+[m[32m        }[m
     }[m
 [m
     public class Users[m
[36m@@ -94,5 +111,22 @@[m [mnamespace DapperScratch[m
         public string UserName { get; set; }[m
         public string Email { get; set; }[m
         public string Address { get; set; }[m
[32m+[m
[32m+[m[32m        public override bool Equals(object obj)[m
[32m+[m[32m        {[m
[32m+[m[32m            if (obj == null || GetType() != obj.GetType())[m
[32m+[m[32m            {[m
[32m+[m[32m                return false;[m
[32m+[m[32m            }[m
[32m+[m
[32m+[m[32m            var user = obj as Users;[m
[32m+[m[41m            [m
[32m+[m[32m            return user.UserName == this.UserName && user.Email == this.Email && user.Address == this.Address;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public override int GetHashCode()[m
[32m+[m[32m        {[m
[32m+[m[32m            return this.UserName.GetHashCode() ^ this.Email.GetHashCode() ^ this.Address.GetHashCode();[m
[32m+[m[32m        }[m
     }[m
 }[m
[1mdiff --git a/ORM/Dapper/DapperScratch/Test.DapperScratch/TestDapperCRUD.cs b/ORM/Dapper/DapperScratch/Test.DapperScratch/TestDapperCRUD.cs[m
[1mindex 15bef84..45e4a42 100644[m
[1m--- a/ORM/Dapper/DapperScratch/Test.DapperScratch/TestDapperCRUD.cs[m
[1m+++ b/ORM/Dapper/DapperScratch/Test.DapperScratch/TestDapperCRUD.cs[m
[36m@@ -109,9 +109,26 @@[m [mnamespace Test.DapperScratch[m
         public void TestMultipleReader()[m
         {[m
             // Given[m
[32m+[m[32m            var originalResults = dapperCrud.MultipleReader();[m
[32m+[m
[32m+[m[32m            var testUserName = $"JeremyLiu_{DateTime.Now.Ticks}";[m
[32m+[m[32m            dapperCrud.SingleInsert(testUserName, "dapper@test.com", "dummy address");[m
[32m+[m
[32m+[m[32m            var testProductName = $"Product1_{DateTime.Now.Ticks}";[m
[32m+[m[32m            dapperCrud.SingleInsertProduct(testProductName, "product1 desc", DateTime.Now);[m
 [m
             // When[m
[32m+[m[32m            var searchResults = dapperCrud.MultipleReader();[m
[32m+[m
             // Then[m
[32m+[m[32m            var productData = searchResults.Item1;[m
[32m+[m[32m            var userData = searchResults.Item2;[m
[32m+[m
[32m+[m[32m            var newUser = userData.Except(originalResults.Item2).Single();[m
[32m+[m[32m            Assert.Equal(testUserName, newUser.UserName);[m
[32m+[m
[32m+[m[32m            var newProduct = productData.Except(originalResults.Item1).Single();[m
[32m+[m[32m            Assert.Equal(testProductName, newProduct.ProductName);[m
         }[m
     }[m
 }[m
