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
        }
        [Test]
        public void TestAddSpendature() {
            var spend = new SpendatureQueries();
            var newSpendature = new Spendature {
                SpendatureId = 8,
                StoreId = 1022,
                StoreTypeId = 3,
                AmountSpent = 256.68m,
                PurchaseDate = DateTime.Now,
                SpendatureTypeId = 2022
            };
            var lastSpendature = spend.ReturnAllSpendatures().Last();
            Assert.AreEqual(newSpendature.SpendatureId, lastSpendature.SpendatureId);
            Assert.AreEqual(newSpendature.StoreId, lastSpendature.StoreId);
            Assert.AreEqual(newSpendature.StoreTypeId, lastSpendature.StoreTypeId);
            Assert.AreEqual(newSpendature.SpendatureTypeId, lastSpendature.SpendatureTypeId);
            Assert.AreEqual(newSpendature.AmountSpent, lastSpendature.AmountSpent);
        }
        [Test]
        public void TestAddSpendature2() {
            var spend = new SpendatureQueries();
            var newSpendature = new Spendature {
                SpendatureId = 9,
                StoreId = 1022,
                StoreTypeId = 3,
                AmountSpent = 56.69m,
                PurchaseDate = DateTime.Now,
                SpendatureTypeId = 2022
            };
            spend.AddSingleSpendature(newSpendature);
            var lastSpendature = spend.ReturnAllSpendatures().Last();
            Assert.AreEqual(newSpendature.SpendatureId, lastSpendature.SpendatureId);
            Assert.AreEqual(newSpendature.StoreId, lastSpendature.StoreId);
            Assert.AreEqual(newSpendature.StoreTypeId, lastSpendature.StoreTypeId);
            Assert.AreEqual(newSpendature.SpendatureTypeId, lastSpendature.SpendatureTypeId);
            Assert.AreEqual(newSpendature.AmountSpent, lastSpendature.AmountSpent);
        }
        [Test]
        public void TestRemoveSingleSpendature() {
            var spend = new SpendatureQueries();
            spend.RemoveSingleSpendature("1");
            var countOfRemainingActual = spend.ReturnAllSpendatures().Count();
            Assert.AreEqual(3, countOfRemainingActual);
        }
        [Test]
        public void TestEditStoreTypeInSpendature() {
            var spend = new SpendatureQueries();
            spend.EditStoreTypeOfSelectedSpendature("18", "Groceries", "Abby");
            var actualSpendature = spend.ReturnSingleSpendature(18);
            Assert.AreEqual(3, actualSpendature.StoreTypeId);
        }
        [Test]
        public void TestEditStoreInSpendature() {
            var spend = new SpendatureQueries();
            spend.EditStoreOfSelectedSpendature("19", "Petsmart", "PetValu");
            var actualSpendature = spend.ReturnSingleSpendature(19);
            Assert.AreEqual(1017, actualSpendature.StoreId);
        }
        [Test]
        public void TestEditSpendatureTypeInSpendature() {
            var spend = new SpendatureQueries();
            spend.EditSpendatureTypeOfSelectedSpendature("21", "Hospitality", "Social");
            var actualSpendature = spend.ReturnSingleSpendature(21);
            Assert.AreEqual(2024, actualSpendature.SpendatureTypeId); 
        }


        [Test]
        public void TestRemoveAndResetSpendatureTable() {
            var reset = new ResetTablesInDB();
            var spend = new SpendatureQueries();
            spend.RemoveAllSpendatures();
            Assert.AreEqual(0, spend.ReturnAllSpendatures().Count());
            reset.ResetRowsForSpendatures();
            var allMySpendatures = spend.ReturnAllSpendatures();
            Assert.AreEqual(4, allMySpendatures.Count());
            Assert.AreEqual(18, allMySpendatures[0].SpendatureId);
            Assert.AreEqual(19, allMySpendatures[1].SpendatureId);
            Assert.AreEqual(20, allMySpendatures[2].SpendatureId);
            Assert.AreEqual(21, allMySpendatures[3].SpendatureId);
        }
        //still have to test still writing for SpendatureQueries
    }
}
