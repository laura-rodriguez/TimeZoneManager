using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeZoneManager.API.Services;
using TimeZoneManager.DAL.Repository;
using System.Linq;
using TimeZoneManager.API.Contracts;

namespace TimeZoneManager.API.Tests.Services
{
    [TestClass]
    public class TimeZoneServiceTests
    {
        [TestMethod]
        public void GetAllOnEmptyRepositoryShouldReturnEmptyTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            var allItems = timeZoneService.GetAll();

            Assert.AreEqual(allItems.Count(), 0);
        }

        [TestMethod]
        public void GetAllOnNonEmptyRepositoryShouldReturnNonEmptyTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            var allItems = timeZoneService.GetAll();

            Assert.AreEqual(allItems.Count(), 1);
        }

        [TestMethod]
        public void GetByIDInvalidIDTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            var item = timeZoneService.GetByID(2);

            Assert.IsNull(item);
        }

        [TestMethod]
        public void GetByIDValidIDTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            var item = timeZoneService.GetByID(1);

            Assert.IsNotNull(item);
            Assert.AreEqual(item.ID, 1);
        }

        [TestMethod]
        public void AddMultipleItemsTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            timeZoneService.Add(GetMockTimeZone(2));
            timeZoneService.Add(GetMockTimeZone(3));

            Assert.AreEqual(timeZoneService.GetAll().Count(), 3);
        }

        [TestMethod]
        public void GetByOwnerWithNoRecordsShouldReturnEmptyTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            timeZoneService.Add(GetMockTimeZone(2));
            timeZoneService.Add(GetMockTimeZone(3));

            Assert.AreEqual(timeZoneService.GetByOwner("abc").Count(), 0);
        }

        [TestMethod]
        public void GetByOwnerWithMultipleRecordsShouldReturnAllTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            timeZoneService.Add(GetMockTimeZone(2));
            timeZoneService.Add(GetMockTimeZone(3));
            timeZoneService.Add(GetMockTimeZone(4, "anotherOwner"));

            Assert.AreEqual(timeZoneService.GetByOwner("Owner").Count(), 3);
        }

        [TestMethod]
        public void RemoveValidItemShouldRemoveItTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            timeZoneService.Add(GetMockTimeZone(1));
            timeZoneService.Add(GetMockTimeZone(2));
            timeZoneService.Add(GetMockTimeZone(3));

            Assert.IsNotNull(timeZoneService.GetByID(1));
            timeZoneService.Remove(1);
            Assert.IsNull(timeZoneService.GetByID(1));
        }

        [TestMethod]
        public void UpdateValidItemTest()
        {
            var timeZoneService = new TimeZoneService(new MockTimeZoneRepository());
            var item = GetMockTimeZone(1);
            timeZoneService.Add(item);
            item.Name = "updated";
            timeZoneService.Update(item);

            Assert.AreEqual(item.Name, timeZoneService.GetByID(1).Name);
        }

        private TimeZoneDTO GetMockTimeZone(int id) {
            return new Contracts.TimeZoneDTO() { ID = id, City = "Test", GMTOffset = 0, Name = "Test", Owner = "Owner" };
        }

        private TimeZoneDTO GetMockTimeZone(int id, string owner)
        {
            return new Contracts.TimeZoneDTO() { ID = id, City = "Test", GMTOffset = 0, Name = "Test", Owner = owner };
        }
    }
}
