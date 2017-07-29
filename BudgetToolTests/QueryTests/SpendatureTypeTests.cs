using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTool;
using BudgetTool.Queries;
using NUnit.Framework; 
namespace BudgetToolTests.QueryTests {
    public class SpendatureTypeTests {
        [Test]
        public void TestSpendatureTypeReturnAll() {
            var reset = new ResetTablesInDB();
            reset.ResetRowsForSpendatureType(); 
            var st = new SpendatureTypeQueries();
            var expectedReturn = st.ReturnAllSpendatureTypes();
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var actualReturn = (from type in mySpendatureTypes select type).ToList();
            Assert.AreEqual(expectedReturn.Count(), actualReturn.Count());
        }
        [Test]
        public void TestReturnSingleStoreType() {
            var st = new SpendatureTypeQueries();
            var expectedReturn = "Social";
            var actualReturn = st.ReturnSingleSpendatureType("Social");
            Assert.AreEqual(expectedReturn, actualReturn.SpendatureTypeName);
        }
        [Test]
        public void TestRemoveAllStoreTypesAvailable() {
            var st = new SpendatureTypeQueries();
            st.RemoveAllSpendatureTypesAvailable();
            var expectedReturnCount = st.ReturnAllSpendatureTypes().Count();
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var actualReturnCount = (from type in mySpendatureTypes select type).ToList().Count();
            Assert.AreEqual(expectedReturnCount, actualReturnCount);
        }
        [Test]
        public void TestAddSingleStore() {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var st = new SpendatureTypeQueries();
            var actualReturnCountBefore = (from type in mySpendatureTypes select type).ToList().Count();
            st.AddSpendatureType("Hospitality");
            var actualReturnCountAfter = (from type in mySpendatureTypes select type).ToList().Count();
            Assert.AreEqual(actualReturnCountBefore, actualReturnCountAfter);
        }
        [Test]
        public void TestEditSingleStoreType() {
            var st = new SpendatureTypeQueries();
            st.EditSingleSpendatureTypeName("Necessity", "Groceries");
            var expectedStoreTypeName = "Groceries";
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var actualStoreTypeName = (from type in myStoreTypes where type.StoreTypeName == "Groceries" select type.StoreTypeName).First();
            Assert.AreEqual(expectedStoreTypeName, actualStoreTypeName);
        }
        
    }
}
