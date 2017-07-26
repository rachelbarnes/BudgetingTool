using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTool.Queries {
    public class StoreTypeQueries {
        public List<StoreType> ReturnAllStoreTypes() {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var allMyStoreTypes = (from type in myStoreTypes select type).ToList();
            return allMyStoreTypes;
        }
        public List<StoreType> ReturnSelectStoreTypes(List<string> selection) {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var allMyStoreTypes = (from type in myStoreTypes select type).ToList();
            var allMyStoreTypesNames = allMyStoreTypes.Select(x => x.StoreTypeName).ToList();
            var typesToReturn = new List<StoreType>();
            foreach (var type in selection) {
                if (allMyStoreTypesNames.Contains(type))
                    typesToReturn.Add(allMyStoreTypes.Where(x => x.StoreTypeName == type).First());
            }
            return typesToReturn;
        }
        public StoreType ReturnSingleStoreType(string typeToReturn) {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var storeTypeToReturn = (from type in myStoreTypes where type.StoreTypeName == typeToReturn select type).First();
            return storeTypeToReturn;
        }
        public void AddSingleStoreType(string typeToAdd) {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var newStoreType = new StoreType { StoreTypeName = typeToAdd };
            myStoreTypes.Add(newStoreType);
            context.SaveChanges();
        }
        public void RemoveSingleStoreType(string typeToRemove) {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var storeTypeToRemove = (from type in myStoreTypes where type.StoreTypeName == typeToRemove select type).First(); 
            myStoreTypes.Remove(storeTypeToRemove);
            context.SaveChanges();
        }
        public void RemoveAllStoreTypesAvailable() {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var mySpendatures = context.Spendature; 
            var allMyStoreTypes = (from type in myStoreTypes select type).ToList();
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            var storeTypesInSpendatures = new List<int>(); 
            foreach (var spend in allMySpendatures) {
                if (!storeTypesInSpendatures.Contains(spend.StoreTypeId))
                    storeTypesInSpendatures.Add(spend.StoreTypeId); 
            }
            foreach (var type in allMyStoreTypes) {
                if (!storeTypesInSpendatures.Contains(type.StoreTypeId)){
                    myStoreTypes.Remove(type);
                    context.SaveChanges(); 
                }
            }
            var allMyStoreTypesUpdated = (from type in allMyStoreTypes select type).ToList(); 
        }
        public void EditSingleStoreType(string oldType, string newType) {
            var context = new MyBudgetEntities();
            var myStoreTypes = context.StoreType;
            var storeTypeToEdit = (from type in myStoreTypes where type.StoreTypeName == oldType select type).First();
            storeTypeToEdit.StoreTypeName = newType;
            context.SaveChanges(); 
        }
    }
}