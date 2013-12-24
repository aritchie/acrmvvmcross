using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Acr.MvvmCross.Plugins.Cache.Tests {

    [TestFixture]
    public class CacheServiceTests {

        [Test]
        public void BasicEndToEnd() {
            CacheConfig.CleanUpTimer = TimeSpan.FromSeconds(3);
            CacheConfig.DefaultLifeSpan = TimeSpan.FromSeconds(1);

            using (var cache = new CacheServiceImpl()) {
                cache.Add("Test", new object());
                Assert.IsNotNull(cache.Get<object>("Test"), "Test object should not be null");

                Thread.Sleep(TimeSpan.FromSeconds(4));
                Assert.IsNull(cache.Get<object>("Test"), "Test object should be null");
            }
        }


        [Test]
        public void AddOrGet_AddWinner() {
            using (var cache = new CacheServiceImpl()) {
                var ran = false;
                var obj = cache.AddOrGet("Test", () => Task<object>.Factory.StartNew(() => {
                    ran = true;
                    return new object();
                }));
                Assert.True(ran, "Did not run");
                Assert.IsNotNull(obj, "Object is null");
            }
        }


        [Test]
        public void AddOrGet_GetWinner() {
            using (var cache = new CacheServiceImpl()) {
                cache.Add("Test", new object());
                var ran = false;
                var obj = cache.AddOrGet("Test", () => Task<object>.Factory.StartNew(() => {
                    ran = true;
                    return new object();
                }));
                Assert.False(ran, "Task ran");
                Assert.IsNotNull(obj, "Object is null");
            }
        }


        [TearDown]
        public void OnTestTeardown() {
            CacheConfig.CleanUpTimer = TimeSpan.FromSeconds(30);
            CacheConfig.DefaultLifeSpan = TimeSpan.FromSeconds(10);
        }
    }
}
