using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTool.Queries {
    public class SpendatureTypeQueries {
        /*
        return all
        return select
        return sginel
        add single
        remove single
        remove all
        edit single
        */
        public List<SpendatureType> ReturnAllSpendatureTypes() {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var allMySpendatureTypes = (from type in mySpendatureTypes select type).ToList();
            return allMySpendatureTypes;
        }
        public List<SpendatureType> ReturnListOfSelectedSpendatureTypes(List<string> selectedSpendatureTypes) {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var allMySpendatureTypes = (from type in mySpendatureTypes select type).ToList();
            var listOfSpendatureTypesToReturn = new List<SpendatureType>();
            foreach (var record in selectedSpendatureTypes) {
                listOfSpendatureTypesToReturn.Add(allMySpendatureTypes.Where(x => x.SpendatureTypeName == record).First());
            }
            return listOfSpendatureTypesToReturn;
        }
        public SpendatureType ReturnSingleSpendatureType(string selectedSpendatureType) {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var spendatureTypeToReturn = (from type in mySpendatureTypes where type.SpendatureTypeName == selectedSpendatureType select type).First();
            return spendatureTypeToReturn;
        }
        public void AddSpendatureType(string spendatureTypeToAdd) {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var allMySpendatureTypeNames = (from type in mySpendatureTypes select type.SpendatureTypeName).ToList();
            if (!allMySpendatureTypeNames.Contains(spendatureTypeToAdd)) {
                var newSpendatureType = new SpendatureType { SpendatureTypeName = spendatureTypeToAdd };
                mySpendatureTypes.Add(newSpendatureType);
                context.SaveChanges();
            }
            //else throw exception
        }
        public void RemoveSingleSpendatureType(string selectedSpendatureType) {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var spendatureTypeToRemove = (from type in mySpendatureTypes where type.SpendatureTypeName == selectedSpendatureType select type).First();
            mySpendatureTypes.Remove(spendatureTypeToRemove);
            context.SaveChanges();
        }
        public void RemoveAllSpendatureTypesAvailable() {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var mySpendatures = context.Spendature;
            var allMySpendatureTypes = (from type in mySpendatureTypes select type).ToList();
            var allMySpendatures = (from spend in mySpendatures select spend).ToList();
            var spendatureTypesInSpendatures = new List<int?>();
            foreach (var spend in allMySpendatures) {
                if (!spendatureTypesInSpendatures.Contains(spend.SpendatureTypeId) && spend.SpendatureTypeId != null)
                    spendatureTypesInSpendatures.Add(spend.SpendatureTypeId);
            }
            foreach (var type in allMySpendatureTypes) {
                if (!spendatureTypesInSpendatures.Contains(type.SpendatureTypeId)) {
                    mySpendatureTypes.Remove(type);
                    context.SaveChanges();
                }
            }
        }
        public void EditSingleSpendatureTypeName(string oldSpendatureType, string newSpendatureType) {
            var context = new MyBudgetEntities();
            var mySpendatureTypes = context.SpendatureType;
            var spendatureTypeToEdit = (from type in mySpendatureTypes where type.SpendatureTypeName == oldSpendatureType select type).First();
            spendatureTypeToEdit.SpendatureTypeName = newSpendatureType;
            context.SaveChanges(); 
        }
    }
}