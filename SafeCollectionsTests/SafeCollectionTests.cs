using AutoFixture;
using FluentAssertions;
using SafeCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SafeCollectionsTests
{
    public class SafeCollectionTests
    {
        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void SafeCollection中移除指定元素_集合不包含該元素且移除元素失敗_集合長度不變且回傳False()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3, 4, 5 };
            var isSuccessful = safeCollection.Remove(6);
            const int expectedCount = 5;
            safeCollection.Count.Should().Be(expectedCount);
            isSuccessful.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void SafeCollection中移除指定元素_集合包含該元素且移除元素成功_集合長度減少1且回傳True()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3, 4, 5 };
            var isSuccessful = safeCollection.Remove(4);
            const int expectedCount = 4;
            safeCollection.Count.Should().Be(expectedCount);
            isSuccessful.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 初始化一個SafeCollection並加入1個元素_加入成功_集合中應包含該元素且長度為1()
        {
            var safeCollection = new SafeCollection<int>();
            var fixture = new Fixture();
            var element = fixture.Create<int>();
            safeCollection.Add(element);
            const int expecetedCount = 1;
            safeCollection.First().Should().Be(element);
            safeCollection.Count.Should().Be(expecetedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 拷貝SafeCollection_從Index0開始拷貝_拷貝的結果應該與SafList相同()
        {
            var safeCollection = new SafeCollection<int>();
            var fixture = new Fixture();
            safeCollection.AddMany(fixture.Create<int>, 3);
            var array = new int[3];
            safeCollection.CopyTo(array, 0);
            array.SequenceEqual(safeCollection).Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 清除含有3個元素的SafeCollection_清除成功_集合長度應為0()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3 };
            safeCollection.Clear();
            const int expecetedCount = 0;
            safeCollection.Count.Should().Be(expecetedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 詢問SafeCollection是否包含目標元素_不含有該元素_應回傳False()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3 };
            var isContain = safeCollection.Contains(4);
            isContain.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 詢問SafeCollection是否包含目標元素_含有該元素_應回傳True()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3 };
            var isContain = safeCollection.Contains(2);
            isContain.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 繞行SafeCollection_繞行成功_繞行結果需與原本集合相同()
        {
            var safeCollection = new SafeCollection<int>();
            var fixture = new Fixture();
            safeCollection.AddMany(fixture.Create<int>, 10);
            var result = new Collection<int>();
            foreach (var x in safeCollection)
                result.Add(x);
            result.SequenceEqual(safeCollection).Should().Be(true);
        }

        [Fact(Skip = "MutiThread scenario result in long running, this test case should be executed only on demand.")]
        //[Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 繞行SafeCollection操作集合_不同執行緒同時對SafeCollection新增與刪除元素_不應擲出例外()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3, 4, 5 };

            Task.Run(() =>
            {
                while (true)
                {
                    safeCollection.Add(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeCollection.Remove(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeCollection.Clear();
                }
            });

            Action action = () => IterateCollection(safeCollection).Wait();
            action.Should().NotThrow();
        }

        private static async Task IterateCollection<T>(ICollection<T> collection, int interation = 1000)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < interation; i++)
                {
                    foreach (var x in collection)
                    {
                        var dummy = x;
                    }
                }
            });
        }
    }
}