using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ZalandoShop.Shared;
using ZalandoShop.Contracts.Services;
using ZalandoShop.Tests.Fake;

namespace ZalandoShop.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            InstanceFactory.RegisterType<IZalandoDataService, FakeZalandoDataService>();
        }


        [TestMethod]
        public async void CheckNumberOfItemsInFakeZalandoDataServiceAreTwo()
        {
            var fakeDataService = InstanceFactory.GetInstance<IZalandoDataService>();
            Assert.AreEqual(await fakeDataService.GetArticlesPaged("s", FilterType.NoFilter), 2);
        }
    }
}
