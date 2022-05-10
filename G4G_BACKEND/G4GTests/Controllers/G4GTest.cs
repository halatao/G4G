using G4G.Data;
using G4G.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace G4G.Controllers.Tests
{
    [TestClass()]
    public class G4GTest
    {
        [TestMethod()]
        public void AccountNotExists()
        {
            //Arrange
            var db = new G4GContext();
            var sut = new AccountsController(db);

            //Act
            var result = sut.GetAccount("unittest");

            //Assert
            Assert.AreEqual(result.IsCompletedSuccessfully, false);
        }

        [TestMethod()]
        public void CommentGet()
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<G4GContext>().UseSqlServer("Data Source = localhost; Initial Catalog = G4G; Integrated Security = True");
            var db = new G4GContext(builder.Options);
            var sut = new CommentsController(db);

            //Act
            var comment = sut.GetCommentById(1);

            //Assert
            Assert.AreEqual(comment.Result.Value.IdComment, 1);
        }
    }
}