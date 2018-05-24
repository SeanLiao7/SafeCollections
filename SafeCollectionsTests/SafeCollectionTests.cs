using AutoFixture;
using FluentAssertions;
using SafeCollections;
using System.Linq;
using Xunit;

namespace SafeCollectionsTests
{
    public class SafeCollectionTests
    {
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
        public void 清除含有3個元素SafeCollection_清除成功_集合長度應為0()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3 };
            safeCollection.Clear();
            const int expecetedCount = 0;
            safeCollection.Count.Should().Be(expecetedCount);
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
        public void 詢問SafeCollection是否包含目標元素_不含有該元素_應回傳False()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3 };
            var isContain = safeCollection.Contains(4);
            isContain.Should().Be(false);
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
        public void SafeCollection中移除指定元素_集合不包含該元素且移除元素失敗_集合長度不變且回傳False()
        {
            var safeCollection = new SafeCollection<int> { 1, 2, 3, 4, 5 };
            var isSuccessful = safeCollection.Remove(6);
            const int expectedCount = 5;
            safeCollection.Count.Should().Be(expectedCount);
            isSuccessful.Should().Be(false);
        }
    }
}