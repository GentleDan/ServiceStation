using System.Collections.Generic;
using System.Linq;
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
                TechnicalMaintenanceName = "AnotherVasya",
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

        [Test]
        public void DeleteDeal()
        {
            var storage = new TechnicalMaintenanceStorage();
            var logic = new TechnicalMaintenanceLogic(storage);
            var vModel = logic.Read(null).FirstOrDefault();
            var bModel = new TechnicalMaintenanceBindingModel { Id = vModel?.Id };
            logic.Delete(bModel);
            Assert.IsTrue(vModel != null && logic.Read(bModel)[0] == null);
        }

        [Test]
        public void CreateOrUpdateClient()
        {
            var storage = new CarStorage();
            var logic = new CarLogic(storage);
            var model = new CarBindingModel()
            {
                CarName = "VAVASYA",
                UserId = 1,
                CarSpareParts = new Dictionary<int, string>(),
            };

            logic.CreateOrUpdate(model);

            Assert.True(storage.GetElement(model) != null);
        }

        [Test]
        public void ReadClients()
        {
            var storage = new CarStorage();
            var logic = new CarLogic(storage);
            var result = logic.Read(null);
            Assert.IsTrue(storage.GetFullList().Count == result.Count);
        }

        [Test]
        public void DeleteClient()
        {
            var storage = new CarStorage();
            var logic = new CarLogic(storage);
            var vModel = logic.Read(null).FirstOrDefault();
            var bModel = new CarBindingModel() { Id = vModel?.Id };
            logic.Delete(bModel);
            Assert.IsTrue(vModel != null && logic.Read(bModel)[0] == null);
        }

        [Test]
        public void CreateOrUpdateCar()
        {
            var storage = new WorkStorage();
            var logic = new WorkLogic(storage);
            var model = new WorkBindingModel()
            {
                WorkName = "VASYAN",
                UserId = 1,
                Price = 5000,
                WorkSpareParts = new Dictionary<int, (string, int)>(),
            };

            logic.CreateOrUpdate(model);

            Assert.True(storage.GetElement(model) != null);
        }

        [Test]
        public void ReadCars()
        {
            var storage = new WorkStorage();
            var logic = new WorkLogic(storage);
            var result = logic.Read(null);
            Assert.IsTrue(storage.GetFullList().Count == result.Count);
        }

        [Test]
        public void DeleteCar()
        {
            var storage = new WorkStorage();
            var logic = new WorkLogic(storage);
            var vModel = logic.Read(null).FirstOrDefault();
            var bModel = new WorkBindingModel() { Id = vModel?.Id };
            logic.Delete(bModel);
            Assert.IsTrue(vModel != null && logic.Read(bModel)[0] == null);
        }

        [Test]
        public void CreateOrUpdateAccessories()
        {
            var storage = new SparePartStorage();
            var logic = new SparePartLogic(storage);
            var model = new SparePartBindingModel()
            {
                SparePartName = "VAAAAAAS",
                UserId = 1,
                Price = 5000,
            };

            logic.CreateOrUpdate(model);

            Assert.True(storage.GetElement(model) != null);
        }

        [Test]
        public void ReadAccessories()
        {
            var storage = new SparePartStorage();
            var logic = new SparePartLogic(storage);
            var result = logic.Read(null);
            Assert.IsTrue(storage.GetFullList().Count == result.Count);
        }

        [Test]
        public void DeleteAccessories()
        {
            var storage = new SparePartStorage();
            var logic = new SparePartLogic(storage);
            var vModel = logic.Read(null).FirstOrDefault();
            var bModel = new SparePartBindingModel() { Id = vModel?.Id };
            logic.Delete(bModel);
            Assert.IsTrue(vModel != null && logic.Read(bModel)[0] == null);
        }
    }
}