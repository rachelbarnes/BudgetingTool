using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTool.Queries {

    public class StoreQueries {
        public List<Store> ReturnAllStores() {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var allMyStores = (from store in myStores select store).ToList();
            return allMyStores; 
        }
        public List<Store> ReturnSelectedStores(List<string> selectedStores) {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var allMyStores = (from store in myStores select store).ToList();
            var listOfStoresToReturn = new List<Store>();
            foreach (var record in selectedStores) {
                listOfStoresToReturn.Add(allMyStores.Where(x => x.StoreName == record).First());
            }
            return listOfStoresToReturn;
        }
        public Store ReturnSingleStore(string selectedStoreName) {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var storeToReturn = (from store in myStores where store.StoreName == selectedStoreName select store).First();
            return storeToReturn;  
        }
        public void AddSingleStore(string storeToAdd) {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var newStore = new Store { StoreName = storeToAdd };
            myStores.Add(newStore);
            context.SaveChanges(); 
        }
        public void RemoveSingleStore(string storeToRemove) {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var singleStoreToRemove = (from store in myStores where store.StoreName == storeToRemove select store).First();
            myStores.Remove(singleStoreToRemove);
            context.SaveChanges(); 
        }
        public void RemoveAllStoresAvailable() {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var mySpendatures = context.Spendature;
            var allMyStores = (from type in myStores select type).ToList();
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            var storesInSpendatures = new List<int>();
            foreach (var spend in allMySpendatures) {
                if (!storesInSpendatures.Contains(spend.StoreId))
                    storesInSpendatures.Add(spend.StoreId);
            }
            foreach (var type in allMyStores) {
                if (!storesInSpendatures.Contains(type.StoreId)) {
                    myStores.Remove(type);
                    context.SaveChanges();
                }
            }
            var allMyStoreTypesUpdated = (from store in allMyStores select store).ToList();
        }
        public void EditSingleStore(string oldStoreName, string newStoreName) {
            var context = new MyBudgetEntities();
            var myStores = context.Store;
            var storeToEdit = (from store in myStores where store.StoreName == oldStoreName select store).First();
            storeToEdit.StoreName = newStoreName;
            context.SaveChanges();
        }
    }
}