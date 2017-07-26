using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTool; 
using BudgetTool.Queries;
using NUnit.Framework; 
namespace BudgetToolTests.QueryTests {
    public class StoreTests {
        [Test]
        public void TestReturnAllStores() {
            var reset = new ResetTablesInDB();
            reset.ResetRowsForStore(); 
            var store = new StoreQueries();
            var expectedCountOfStores = store.ReturnAllStores().Count();
            var actualCountOfStores = 8;
            Assert.AreEqual(expectedCountOfStores, actualCountOfStores); 
        }
        [Test]
        public void TestReturnSingleStore() {
            var reset = new ResetTablesInDB();
            reset.ResetRowsForStore(); 
            var store = new StoreQueries();
            var bestRestaurantEver = store.ReturnSingleStore("Pierre's");
            var stillBestRestaurantEver = "Pierre's";
            Assert.AreEqual(bestRestaurantEver.StoreName, stillBestRestaurantEver); 
        }
        [Test]
        public void TestRemoveALlStores() {
            var store = new StoreQueries();
            store.RemoveAllStoresAvailable(); 
            var remainingStoreCount = store.ReturnAllStores().Count();
            var expectedRemainingStoreCount = 2;
            Assert.AreEqual(remainingStoreCount, expectedRemainingStoreCount); 
        }
        [Test]
        public void TestAddSingleStore() {
            var store = new StoreQueries();
            var countOfStoresBefore = store.ReturnAllStores().Count(); 
            store.AddSingleStore("Test Store Name");
            var countOfStoresAfter = store.ReturnAllStores().Count();
            Assert.AreEqual(countOfStoresBefore + 1, countOfStoresAfter); 
        }
        [Test]
        public void TestEditSingleStore() {
            var store = new StoreQueries();
            store.EditSingleStore("Test Store Name", "Williams and Sonoma");
            var context = new MyBudgetEntities();
            var coolStore = store.ReturnSingleStore("Williams and Sonoma");
            Assert.AreEqual("Williams and Sonoma", coolStore.StoreName); 
        }
    }
}
