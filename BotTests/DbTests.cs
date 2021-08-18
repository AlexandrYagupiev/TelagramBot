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
        }

        [Test]
        public void ApplicationAddToDb()
        {
         var aplicationContext =  new AplicationContext(@"Server=DESKTOP-4C6PQII\SQLEXPRESS;Database=botDb;Trusted_Connection=True;");
         var aplication = new Application();
         aplication.ProductCategory = ProductCategory.Notebooks;
         aplication.ProductName = "HP 23";
         var entityEntry =aplicationContext.Applications.Add(aplication);
         aplicationContext.SaveChanges();
         var applicationFromBase = aplicationContext.Applications.Single((t) => t.Guid == entityEntry.Entity.Guid);
            Assert.AreEqual(aplication.ProductCategory, applicationFromBase.ProductCategory);
            Assert.AreEqual(aplication.ProductName, applicationFromBase.ProductName);
        }
    }
}