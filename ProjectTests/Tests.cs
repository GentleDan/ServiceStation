using System.Collections.Generic;
using NUnit.Framework;
using ServiceStationBusinessLogic.BindingModels;
using ServiceStationBusinessLogic.BusinessLogic;
using ServiceStationDatabaseImplement.Implements;

namespace ProjectTests
{
    public class Tests
    {
        [Test]
        public void CreateOrUpdateDeal()
        {
            var storage = new TechnicalMaintenanceStorage();
            var logic = new TechnicalMaintenanceLogic(storage);
            var model = new TechnicalMaintenanceBindingModel()
            {
                TechnicalMaintenanceName = "Vasya",
                Sum = 5000,
                UserId = 1,
                TechnicalMaintenanceWorks = new Dictionary<int, (string, int)>(),
                TechnicalMaintenanceCars = new Dictionary<int, string>(),
                SelectedWorks = new List<int>(),
            };

            logic.CreateOrUpdate(model);

            Assert.True(storage.GetElement(model) != null);
        }

        [Test]
        public void ReadDeals()
        {
            var storage = new TechnicalMaintenanceStorage();
            var logic = new TechnicalMaintenanceLogic(storage);
            var result = logic.Read(null);
            Assert.IsTrue(storage.GetFullList().Count == result.Count);
        }
    }
}