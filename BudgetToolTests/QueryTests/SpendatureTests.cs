using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTool;
using BudgetTool.Queries;
using NUnit.Framework;
namespace BudgetToolTests.QueryTests {
    public class SpendatureTests {
        [Test]
        public void ReturnAllSpendatures() {
            var spend = new SpendatureQueries();
            var expectedCount = spend.ReturnAllSpendatures().Count();
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var allMySpendatures = (from s in mySpendatures select s).ToList();
            var actualCount = allMySpendatures.Count();
            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void ReturnSingleSpendature() {
            var spend = new SpendatureQueries();
            var spendature = spend.ReturnSingleSpendature(1);
            var expectedAmount = 30.65m;
            var actualAmount = spendature.AmountSpent;
            Assert.AreEqual(expectedAmount, actualAmount);
        }
        [Test]
        public void TestReturnDefinedSpendatureView() {
            var spend = new SpendatureQueries();
            var expectedDefinedSpendatures = spend.ReturnDefinedSpendaturesView();
            var actualDefinedSpendatures = new List<DefinedSpendatures> {
                new DefinedSpendatures {
                    SpendatureId = 1, StoreName = "Shop Rite", StoreTypeName = "Groceries", AmountSpent = 30.65m, PurchaseDate = DateTime.Parse("2017-06-25"), SpendatureTypeName="Groceries"
                },
                new DefinedSpendatures {
                    SpendatureId = 2, StoreName = "Shop Rite", StoreTypeName = "Groceries", AmountSpent = 45.89m, PurchaseDate = DateTime.Parse("2017-05-31"), SpendatureTypeName="Groceries"
                },
                new DefinedSpendatures {
                    SpendatureId = 3, StoreName = "Petsmart", StoreTypeName = "Abby", AmountSpent = 28.01m, PurchaseDate = DateTime.Parse("2017-05-13"), SpendatureTypeName="Groceries"
                }
            };
            Assert.AreEqual(expectedDefinedSpendatures.Count(), actualDefinedSpendatures.Count());
            Assert.AreEqual(expectedDefinedSpendatures[0].SpendatureId, actualDefinedSpendatures[0].SpendatureId); 
            Assert.AreEqual(expectedDefinedSpendatures[1].SpendatureId, actualDefinedSpendatures[1].SpendatureId); 
            Assert.AreEqual(expectedDefinedSpendatures[2].SpendatureId, actualDefinedSpendatures[2].SpendatureId); 
            //Assert.AreEqual(expectedDefinedSpendatures, actualDefinedSpendatures); 
        }
        //still have to test add single spendature, add defined spendature to view, populate view and other methods still writing for SpendatureQueries
    }
}
