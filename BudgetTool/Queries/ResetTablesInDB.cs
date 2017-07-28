using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BudgetTool; 
namespace BudgetTool.Queries {
    public class ResetTablesInDB {
        public void ResetRowsForSpendatureType() {
            var context = new MyBudgetEntities();
            var stq = new SpendatureTypeQueries();
            stq.RemoveAllSpendatureTypesAvailable();
            var mySpendatureTypes = context.SpendatureType;
            var allMySpendatureTypes = (from types in mySpendatureTypes select types).ToList();
            var allMySpendatureTypeNamesCurrent = allMySpendatureTypes.Select(x => x.SpendatureTypeName).ToList();
            var testSpendatureTypesToReset = new List<SpendatureType> {
                new SpendatureType { SpendatureTypeName = "Necessity" },
                new SpendatureType { SpendatureTypeName = "Social" },
                new SpendatureType { SpendatureTypeName = "Hospitality" },
                new SpendatureType { SpendatureTypeName = "Baking Non-Profit" },
                new SpendatureType { SpendatureTypeName = "Baking For-Profit" }
            };
            var listOfSpendatureTypeNames = new List<string>();
            //there is something that is taking FOREVER to get here
            foreach (var record in testSpendatureTypesToReset) {
                listOfSpendatureTypeNames.Add(record.SpendatureTypeName);
            }
            foreach (var record in testSpendatureTypesToReset) {
                if (!allMySpendatureTypeNamesCurrent.Contains(record.SpendatureTypeName))
                    stq.AddSpendatureType(record.SpendatureTypeName);
            }
            var allMySpendatureTypesAfterReset = stq.ReturnAllSpendatureTypes();
        }
        public void ResetRowsForStore() {
                var context = new MyBudgetEntities();
                var store = new StoreQueries();
                store.RemoveAllStoresAvailable();
                var myStores = context.Store;
                var allMyStores = (from s in myStores select s).ToList();
                var allMyStoresNamesCurrent = allMyStores.Select(x => x.StoreName).ToList();
                var sampleStoresToReset = new List<Store> {
                new Store {StoreName = "Shop Rite" },
                new Store {StoreName = "Lowes" },
                new Store {StoreName = "Petsmart" },
                new Store {StoreName = "PetValu" },
                new Store {StoreName = "Giant" },
                new Store {StoreName = "BP" },
                new Store {StoreName = "Pierre's" },
                new Store {StoreName = "Torrid" }
             };
                var listOfStoreNames = new List<string>();
                foreach (var record in sampleStoresToReset) {
                    listOfStoreNames.Add(record.StoreName);
                }
                foreach (var record in sampleStoresToReset) {
                    if (!allMyStoresNamesCurrent.Contains(record.StoreName)) {
                        store.AddSingleStore(record.StoreName);
                    }
                }
                var allMyStoresAfterReset = store.ReturnAllStores();
            }
            public void ResetRowsForStoreType() {
                var context = new MyBudgetEntities();
                var storeType = new StoreTypeQueries();
                storeType.RemoveAllStoreTypesAvailable();
                var myStoreTypes = context.StoreType;
                var allMyStoreTypes = (from type in myStoreTypes select type).ToList();
                var allMyStoreTypeNamesCurrent = allMyStoreTypes.Select(x => x.StoreTypeName).ToList();
                var testStoreTypesToReset = new List<StoreType> {
                new StoreType {StoreTypeName = "Grocieries" },
                new StoreType {StoreTypeName = "Abby" },
                new StoreType {StoreTypeName = "Entertainment" },
                new StoreType {StoreTypeName = "Eatting Out" },
                new StoreType {StoreTypeName = "Coffee" }
            };
                var listOfStoreTypeNames = new List<string>();
                foreach (var record in testStoreTypesToReset) {
                    listOfStoreTypeNames.Add(record.StoreTypeName);
                }
                foreach (var record in testStoreTypesToReset) {
                    if (!allMyStoreTypeNamesCurrent.Contains(record.StoreTypeName)) {
                        storeType.AddSingleStoreType(record.StoreTypeName);
                    }
                }
                var allMyStoreTypesAfterReset = (from type in myStoreTypes select type).ToList();
        }
    }
}