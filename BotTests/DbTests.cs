using NUnit.Framework;
using BotTest;
using System.Linq;

namespace BotTests
{
    public class DbTests
    {
        [SetUp]
        public void Setup()
        {
            aplicationContext = new AplicationContext(@"Server=DESKTOP-4C6PQII\SQLEXPRESS;Database=botDbTest;Trusted_Connection=True;");
        }

        private AplicationContext aplicationContext;

        [Test]
        public void ApplicationAddToDb()
        {
         var aplication = new ApplicationModel();
         aplication.ProductCategory = ProductCategoryModel.Notebooks;
         aplication.ProductName = "HP 23";
         var entityEntry =aplicationContext.Applications.Add(aplication);
         aplicationContext.SaveChanges();
         var applicationFromBase = aplicationContext.Applications.Single((t) => t.Guid == entityEntry.Entity.Guid);
            Assert.AreEqual(aplication.ProductCategory, applicationFromBase.ProductCategory);
            Assert.AreEqual(aplication.ProductName, applicationFromBase.ProductName);
        }
    }
}