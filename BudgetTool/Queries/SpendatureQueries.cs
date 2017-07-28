using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTool.Queries {
    /*
    remove single
    remove all
    edit store type of one
    edit store of one
    edit spendature type of one
    */
    public class SpendatureQueries {
        public List<Spendature> ReturnAllSpendatures() {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            return allMySpendatures;
        }
        public List<Spendature> ReturnSelectedSpendatures(List<int> spendatureIdsStrings) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            var spendaturesToReturn = new List<Spendature>();
            foreach (var id in spendatureIdsStrings) {
                spendaturesToReturn.Add(allMySpendatures.Where(x => x.SpendatureId == id).First());
            }
            return spendaturesToReturn;
        }
        public Spendature ReturnSingleSpendature(int id) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var spendatureToReturn = (from spend in mySpendatures where spend.SpendatureId == id select spend).First();
            return spendatureToReturn;
        }
        //populate view
        //return view
        public List<DefinedSpendatures> ReturnDefinedSpendaturesView() {
            var context = new MyBudgetEntities();
            var myDefinedSpendatures = context.DefinedSpendatures;
            var allMyDefinedSpendatures = (from spend in myDefinedSpendatures select spend).ToList();
            return allMyDefinedSpendatures;
        }
        public void PopulateDefinedSpendaturesViewWithCurrentSpendatures() {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            var myDefinedSpendatures = context.DefinedSpendatures;
            var allMyDefinedSpendatures = (from def in myDefinedSpendatures select def).ToList();
            var allMyDefinedSpendaturesIds = allMyDefinedSpendatures.Select(x => x.SpendatureId).ToList();
            foreach (var spend in allMySpendatures) {
                if (!allMyDefinedSpendaturesIds.Contains(spend.SpendatureId)) {
                    AddDefinedSpendatureToView(spend.SpendatureId);
                }
            }
        }
        public void AddDefinedSpendatureToView(int id) {
            //exception if spendature id is not in table
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var myStores = context.Store;
            var myStoreTypes = context.StoreType;
            var mySpendatureTypes = context.SpendatureType;
            var myDefinedSpendatures = context.DefinedSpendatures;
            var spendatureToReturn = (from spend in mySpendatures where spend.SpendatureId == id select spend).First();
            var newDefinedSpendature = new DefinedSpendatures {
                SpendatureId = spendatureToReturn.SpendatureId,
                StoreName = (from store in myStores where store.StoreId == spendatureToReturn.StoreId select store.StoreName).First(),
                StoreTypeName = (from stType in myStoreTypes where stType.StoreTypeId == spendatureToReturn.StoreTypeId select stType.StoreTypeName).First(),
                AmountSpent = spendatureToReturn.AmountSpent,
                PurchaseDate = spendatureToReturn.PurchaseDate,
                SpendatureTypeName = (from spType in mySpendatureTypes where spType.SpendatureTypeId == spendatureToReturn.SpendatureTypeId select spType.SpendatureTypeName).First()
            };
            myDefinedSpendatures.Add(newDefinedSpendature);
            context.SaveChanges();
        }
        public void AddSingleSpendature(string spendStoreName, string spendStoreTypeName, string spendSpendatureTypeName, string spendAmount) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var myStores = context.Store;
            var myStoreTypes = context.StoreType;
            var mySpendatureTypes = context.SpendatureType;
            var newSpendature = new Spendature {
                StoreId = (from s in myStores where s.StoreName == spendStoreName select s.StoreId).First(),
                StoreTypeId = (from stType in myStoreTypes where stType.StoreTypeName == spendStoreTypeName select stType.StoreTypeId).First(),
                SpendatureTypeId = (from spType in mySpendatureTypes where spType.SpendatureTypeName == spendSpendatureTypeName select spType.SpendatureTypeId).First(),
                AmountSpent = decimal.Parse(spendAmount),
                PurchaseDate = DateTime.Now
            };
            mySpendatures.Add(newSpendature);
            context.SaveChanges();
        }

    }
}