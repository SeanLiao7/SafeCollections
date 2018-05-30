using AutoFixture;
using FluentAssertions;
using SafeCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SafeCollectionsTests
{
    public class SafeListTests
    {
        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void SafeList中移除指定Index元素_集合包含該元素且移除元素成功_集合長度減1()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            safeList.RemoveAt(0);
            const int expectedCount = 4;
            safeList.Count.Should().Be(expectedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void SafeList中移除指定元素_集合不包含該元素且移除元素失敗_集合長度不變且回傳False()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var isSuccessful = safeList.Remove(6);
            const int expectedCount = 5;
            safeList.Count.Should().Be(expectedCount);
            isSuccessful.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void SafeList中移除指定元素_集合包含該元素且移除元素成功_集合長度減少1且回傳True()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var isSuccessful = safeList.Remove(4);
            const int expectedCount = 4;
            safeList.Count.Should().Be(expectedCount);
            isSuccessful.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void SafeList中插入指定元素_插入元素成功_集合中的對應位置應為該元素()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            const int insertIndex = 2;
            safeList.Insert(insertIndex, 4);
            const int expectedValue = 4;
            safeList[insertIndex].Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 初始化一個SafeList並加入1個元素_加入成功_集合中應包含該元素且長度為1()
        {
            var safeList = new SafeList<int>();
            var fixture = new Fixture();
            var element = fixture.Create<int>();
            safeList.Add(element);
            const int expecetedCount = 1;
            safeList[0].Should().Be(element);
            safeList.Count.Should().Be(expecetedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 取得SafeList中指定元素的Index_該元素在不在集合中_回傳負1()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            var index = safeList.IndexOf(4);
            const int expectedIndex = -1;
            index.Should().Be(expectedIndex);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 取得SafeList中指定元素的Index_該元素在集合中_回傳對應的Index()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            var index = safeList.IndexOf(3);
            const int expectedIndex = 2;
            index.Should().Be(expectedIndex);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 拷貝SafeList_從Index0開始拷貝_拷貝的結果應該與SafList相同()
        {
            var safeList = new SafeList<int>();
            var fixture = new Fixture();
            safeList.AddMany(fixture.Create<int>, 3);
            var array = new int[3];
            safeList.CopyTo(array, 0);
            array.Should().BeEquivalentTo(safeList);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 清除含有3個元素SafeList_清除成功_集合長度應為0()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            safeList.Clear();
            const int expecetedCount = 0;
            safeList.Count.Should().Be(expecetedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 移除SafeList中符合條件的元素_移除成功_集合長度與回傳值應為指定值()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var removedCount = safeList.RemoveAll(x => x < 4);
            const int expectedCount = 2;
            const int expectedRemovedCount = 3;
            safeList.Count.Should().Be(expectedCount);
            removedCount.Should().Be(expectedRemovedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 設定SafeList中指定Index元素_設定成功_該Index值應為指定值()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            safeList[0] = 10;
            const int expected = 10;
            safeList[0].Should().Be(expected);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 詢問SafeList是否包含目標元素_不含有該元素_應回傳False()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            var isContain = safeList.Contains(4);
            isContain.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 詢問SafeList是否包含目標元素_含有該元素_應回傳True()
        {
            var safeList = new SafeList<int> { 1, 2, 3 };
            var isContain = safeList.Contains(2);
            isContain.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 繞行SafeList_繞行成功_繞行結果需與原本集合相同()
        {
            var safeList = new SafeList<int>();
            var fixture = new Fixture();
            safeList.AddMany(fixture.Create<int>, 10);
            var result = new List<int>();
            foreach (var x in safeList)
                result.Add(x);
            result.Should().BeEquivalentTo(safeList);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 搜尋集和符合條件的單一元素_搜尋成功_搜尋結果應符合期待()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var result = safeList.Find(x => x > 2);
            const int expectedResult = 3;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 搜尋集合所有符合條件的元素_搜尋成功_搜尋結果應符合期待()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var result = safeList.FindAll(x => x < 6);
            var expectedResult = new List<int> { 1, 2, 3, 4, 5 };
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 批次新增元素至集合_新增成功_新增結果應符合期待()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            safeList.AddRange(Enumerable.Range(7, 3));
            var expectedResult = new List<int> { 1, 2, 3, 4, 5, 7, 8, 9 };
            safeList.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void ForEach繞行集合_繞行成功_繞行結果應符合期待()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var result = "";
            safeList.ForEach(x => result += x.ToString());
            const string expectedResult = "12345";
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 取得依照指定條件元素是否存在於集合_元素存在_回傳True()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var result = safeList.Exists(x => x > 3);
            const bool expectedResult = true;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 取得依照指定條件元素是否存在於集合_元素存在_回傳False()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };
            var result = safeList.Exists(x => x > 5);
            const bool expectedResult = false;
            result.Should().Be(expectedResult);
        }

        [Fact(Skip = "MutiThread scenario result in long running, this test case should be executed only on demand.")]
        //[Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 繞行SafeList操作集合_不同執行緒同時對SafeList新增與刪除元素_不應擲出例外()
        {
            var safeList = new SafeList<int> { 1, 2, 3, 4, 5 };

            Task.Run(() =>
            {
                while (true)
                {
                    safeList.Add(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeList.Remove(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    var result = safeList.FindAll(x => x < 1);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeList.AddRange(Enumerable.Range(0, 3));
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeList.RemoveAll(x => x < 1);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeList.Clear();
                }
            });

            Action action = () => IterateList(safeList).Wait();
            action.Should().NotThrow();
        }

        private static async Task IterateList<T>(IList<T> list, int interation = 1000)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < interation; i++)
                {
                    foreach (var x in list)
                    {
                        var dummy = x;
                    }
                }
            });
        }
    }
}