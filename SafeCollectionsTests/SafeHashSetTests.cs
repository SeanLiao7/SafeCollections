using System;
using AutoFixture;
using FluentAssertions;
using SafeCollections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SafeCollectionsTests
{
    public class SafeHashSetTests
    {
        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數執行聯集操作_執行成功_結果應是SafeHashSet與參數的聯集()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3 };
            var param = Enumerable.Range(0, 5);
            safeHashSet.UnionWith(param);
            var expectedResult = new List<int> { 1, 2, 3, 0, 4 };
            safeHashSet.SequenceEqual(expectedResult).Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數執行交集操作_執行成功_結果應是SafeHashSet與參數的交集()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10).Where(x => x % 2 == 0);
            safeHashSet.IntersectWith(param);
            var expectedResult = new List<int> { 2, 4 };
            safeHashSet.SequenceEqual(expectedResult).Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數執行差集操作_執行成功_結果應是SafeHashSet與參數的差集()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10).Where(x => x % 2 == 0);
            safeHashSet.ExceptWith(param);
            var expectedResult = new List<int> { 1, 3, 5 };
            safeHashSet.SequenceEqual(expectedResult).Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數執行邏輯異或_執行成功_應該是SafeHashSet與參數邏輯異或後的結果()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(3, 6);
            var expectedResult = new List<int> { 0, 1, 2, 6, 8,7 };
            safeHashSet.SymmetricExceptWith(param);
            safeHashSet.Should().BeEquivalentTo(expectedResult, options => options.WithoutStrictOrderingFor(x => false));
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數子集_SafeHashSet是傳入參數的子集_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10);
            var result = safeHashSet.IsSubsetOf(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數子集_SafeHashSet不是傳入參數的子集_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 3);
            var result = safeHashSet.IsSubsetOf(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的超集_SafeHashSet是傳入參數的超集_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 3);
            var result = safeHashSet.IsSupersetOf(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的超集_SafeHashSet不是傳入參數的超集_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10);
            var result = safeHashSet.IsSupersetOf(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的真超集_SafeHashSet是傳入參數的真超集_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 5);
            var result = safeHashSet.IsProperSupersetOf(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的真超集_SafeHashSet不是傳入參數的真超集_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 6);
            var result = safeHashSet.IsProperSupersetOf(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的真子集_SafeHashSet是傳入參數的真子集_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10);
            var result = safeHashSet.IsProperSubsetOf(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否為該參數的真子集_SafeHashSet不是傳入參數的真子集_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 6);
            var result = safeHashSet.IsProperSubsetOf(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否與該參數集合有交集_SafeHashSet與傳入參數有交集_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(5, 5);
            var result = safeHashSet.Overlaps(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否與該參數集合有交集_SafeHashSet與傳入參數沒有交集_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(6, 5);
            var result = safeHashSet.Overlaps(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否與該參數集合包含相同元素_SafeHashSet與傳入參數包含相同元素_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 6).SelectMany(x => Enumerable.Repeat(x, 2));
            var result = safeHashSet.SetEquals(param);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照帶入參數檢查是否與該參數集合包含相同元素_SafeHashSet與傳入參數不包含相同元素_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var param = Enumerable.Range(0, 10);
            var result = safeHashSet.SetEquals(param);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照加入指定元素_加入成功_SafeHashSet應包含該元素並回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            const int element = 10;
            var result = safeHashSet.Add(element);
            result.Should().Be(true);
            var isContain = safeHashSet.Contains(element);
            isContain.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet依照加入指定元素_集合中已經包含該元素故加入失敗_SafeHashSet應包含該元素並應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            const int element = 3;
            var result = safeHashSet.Add(element);
            result.Should().Be(false);
            var isContain = safeHashSet.Contains(element);
            isContain.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void SafeHashSet清除所有元素_清除成功_SafeHashSet的長度應為0()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            const int expectedCount = 0;
            safeHashSet.Clear();
            safeHashSet.Count.Should().Be(expectedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 取得SafeHashSet是否包含指定元素_SafeHashSet包含該元素_應回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var result = safeHashSet.Contains(3);
            result.Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 取得SafeHashSet是否包含指定元素_SafeHashSet不包含該元素_應回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            var result = safeHashSet.Contains(10);
            result.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 複製SafeHashSet至指定陣列_複製成功_複製結果應與原集合相同()
        {
            var fixure = new Fixture();
            var safeHashSet = new SafeHashSet<int>();
            const int count = 10;
            safeHashSet.AddMany(fixure.Create<int>, count);
            var result = new int[count];
            safeHashSet.CopyTo(result, 0);
            result.Should().BeEquivalentTo(safeHashSet);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 移除SafeHashSet指定元素_移除成功_集合應不包含該元素並回傳True()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            const int target = 4;
            var result = safeHashSet.Remove(target);
            result.Should().Be(true);
            var isContain = safeHashSet.Contains(target);
            isContain.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 移除SafeHashSet指定元素_移除失敗_集合應不包含該元素並回傳False()
        {
            var safeHashSet = new SafeHashSet<int> { 0, 1, 2, 3, 4, 5 };
            const int target = 10;
            var result = safeHashSet.Remove(target);
            result.Should().Be(false);
            var isContain = safeHashSet.Contains(target);
            isContain.Should().Be(false);
        }

        [Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 繞行SafeHashSet_繞行完成_繞行結果應該與原集合相同()
        {
            var fixure = new Fixture();
            var safeHashSet = new SafeHashSet<int>();
            const int count = 10;
            safeHashSet.AddMany(fixure.Create<int>, count);
            var result = new List<int>();
            foreach (var x in safeHashSet)
                result.Add(x);
            result.Should().BeEquivalentTo(safeHashSet);
        }

        [Fact(Skip = "MutiThread scenario result in long running, this test case should be executed only on demand.")]
        //[Fact]
        [Trait("SafeCollections", "SafeHashSet")]
        public void 繞行SafeHashSet操作集合_不同執行緒同時對SafeHashSet操作新增與刪除_不應擲出例外()
        {
            var safeHashSet = new SafeHashSet<int> { 1, 2, 3, 4, 5 };

            Task.Run(() =>
            {
                while (true)
                {
                    safeHashSet.Add(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeHashSet.Remove(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeHashSet.Clear();
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeHashSet.IntersectWith(Enumerable.Range(0,3));
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeHashSet.UnionWith(Enumerable.Range(0, 3));
                }
            });

            Action action = () => IterateCollection(safeHashSet).Wait();
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