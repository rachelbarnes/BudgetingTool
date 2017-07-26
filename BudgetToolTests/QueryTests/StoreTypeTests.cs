using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTool.Queries;
using BudgetTool; 
using NUnit.Framework; 
namespace BudgetToolTests {
    public class StoreTypeTests {
        //still to be tested: return select store types, remove single, 
        [Test]
        public void TestStoreTypeReturnAll() {
            var st = new StoreTypeQueries();
            var expectedReturn = st.ReturnAllStoreTypes();
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var actualReturn = (from type in myStoreTypes select type).ToList();
            Assert.AreEqual(expectedReturn.Count(), actualReturn.Count()); 
        }
        [Test]
        public void TestReturnSingleStoreType() {
            var st = new StoreTypeQueries();
            var expectedReturn = "Abby";
            var actualReturn = st.ReturnSingleStoreType("Abby");
            Assert.AreEqual(expectedReturn, actualReturn.StoreTypeName); 
        }
        [Test]
        public void TestRemoveAllStoreTypesAvailable() {
            var st = new StoreTypeQueries();
            var expectedReturnCount = 2;
            st.RemoveAllStoreTypesAvailable(); 
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var actualReturnCount = (from type in myStoreTypes select type).ToList().Count();
            Assert.AreEqual(expectedReturnCount, actualReturnCount); 
        }
        [Test]
        public void TestAddSingleStore() {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var st = new StoreTypeQueries();
            var actualReturnCountBefore = (from type in myStoreTypes select type).ToList().Count(); 
            st.AddSingleStoreType("Entertainment");
            var actualReturnCountAfter = (from type in myStoreTypes select type).ToList().Count();
            Assert.AreEqual(actualReturnCountBefore + 1, actualReturnCountAfter); 
        }
        [Test]
        public void TestEditSingleStoreType() {
            var st = new StoreTypeQueries();
            st.EditSingleStoreType("Food Shopping", "Groceries"); 
            var expectedStoreTypeName = "Groceries"; 
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var actualStoreTypeName = (from type in myStoreTypes where type.StoreTypeName == "Groceries" select type.StoreTypeName).First();
            Assert.AreEqual(expectedStoreTypeName, actualStoreTypeName); 
        }
    }
}