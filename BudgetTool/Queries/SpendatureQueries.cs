using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTool.Queries {
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
        public void AddSingleSpendature(Spendature newSpendatureToAdd) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var myStores = context.Store;
            var myStoreTypes = context.StoreType;
            var mySpendatureTypes = context.SpendatureType;
            mySpendatures.Add(newSpendatureToAdd);
            context.SaveChanges();
        }
        public void RemoveSingleSpendature(string id) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var spendatureId = int.Parse(id);
            var spendatureToRemove = (from spend in mySpendatures where spend.SpendatureId == spendatureId select spend).First();
            mySpendatures.Remove(spendatureToRemove);
            context.SaveChanges();
        }
        public void RemoveSingleSpendature(int id) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var spendatureToRemove = (from spend in mySpendatures where spend.SpendatureId == id select spend).First();
            mySpendatures.Remove(spendatureToRemove);
            context.SaveChanges();
        }
        public void RemoveAllSpendatures() {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            foreach (var record in allMySpendatures) {
                RemoveSingleSpendature(record.SpendatureId);
            }
        }
        public void EditStoreTypeOfSelectedSpendature(string spendatureId, string previousStoreTypeName, string udpatedStoreTypeName) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var myStoreTypes = context.StoreType;
            var spendatureIdInt = int.Parse(spendatureId);
            var spendatureToEdit = (from spend in mySpendatures where spend.SpendatureId == spendatureIdInt select spend).First();
            var previousStoreType = (from type in myStoreTypes where type.StoreTypeName == previousStoreTypeName select type).First();
            var udpatedStoreType = (from type in myStoreTypes where type.StoreTypeName == udpatedStoreTypeName select type).First();
            if (spendatureToEdit.StoreTypeId == previousStoreType.StoreTypeId) {
                spendatureToEdit.StoreTypeId = udpatedStoreType.StoreTypeId;
                context.SaveChanges();
            }
            //else throw exception
        }
        public void EditStoreOfSelectedSpendature(string spendatureId, string previousStoreName, string updatedStoreName) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var myStores = context.Store;
            var spendatureIdInt = int.Parse(spendatureId);
            var spendatureToEdit = (from spend in mySpendatures where spend.SpendatureId == spendatureIdInt select spend).First();
            var previousStore = (from store in myStores where store.StoreName == previousStoreName select store).First();
            var updatedStore = (from store in myStores where store.StoreName == updatedStoreName select store).First();
            if (spendatureToEdit.StoreId == previousStore.StoreId) {
                spendatureToEdit.StoreId = updatedStore.StoreId;
                context.SaveChanges();
            }
            //else throw exception
        }
        public void EditSpendatureTypeOfSelectedSpendature(string spendatureId, string previousSpendatureTypeName, string updatedSpendatureTypeName) {
            var context = new MyBudgetEntities();
            var mySpendatures = context.Spendature;
            var mySpendatureTypes = context.SpendatureType;
            var spendatureIdInt = int.Parse(spendatureId);
            var spendatureToEdit = (from spend in mySpendatures where spend.SpendatureId == spendatureIdInt select spend).First();
            var previousStore = (from type in mySpendatureTypes where type.SpendatureTypeName == previousSpendatureTypeName select type).First();
            var updatedSpendatureType = (from type in mySpendatureTypes where type.SpendatureTypeName == updatedSpendatureTypeName select type).First();
            if (spendatureToEdit.SpendatureTypeId == previousStore.SpendatureTypeId) {
                spendatureToEdit.SpendatureTypeId = updatedSpendatureType.SpendatureTypeId;
                context.SaveChanges();
            }
            //else throw exception
        }
    }
}