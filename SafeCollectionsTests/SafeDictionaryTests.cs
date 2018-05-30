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
    public class SafeDictionaryTests
    {
        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 依照指定鍵值利用TryGetValue取得SafeDictionary指定值_取得失敗_out應回傳預設值且回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 },
                { 3, 9 }
            };
            const int key = 6;
            var isSuccessful = safeDictionary.TryGetValue(key, out var result);
            const bool expectedResult = false;
            isSuccessful.Should().Be(expectedResult);
            const int expectedValue = default(int);
            result.Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 依照指定鍵值利用TryGetValue取得SafeDictionary指定值_取得成功_out應回傳指定值且回傳True()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 },
                { 3, 9 }
            };
            const int key = 3;
            var isSuccessful = safeDictionary.TryGetValue(key, out var result);
            const bool expectedResult = true;
            isSuccessful.Should().Be(expectedResult);
            const int expectedValue = 9;
            result.Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 取得SafeDictionary是否含有指定鍵值_SafeDictionary內不包含該鍵值_應回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            const int key = 5;
            var result = safeDictionary.ContainsKey(key);
            const bool expectedResult = false;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 取得SafeDictionary是否含有指定鍵值_SafeDictionary內包含該鍵值_應回傳True()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            const int key = 2;
            var result = safeDictionary.ContainsKey(key);
            const bool expectedResult = true;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 取得SafeDictionary是否含有指定鍵值對_SafeDictionary內已包含該鍵值_應回傳True()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 5 },
                { 2, 8 }
            };
            var pair = new KeyValuePair<int, int>(2, 8);
            var result = ((ICollection<KeyValuePair<int, int>>)safeDictionary).Contains(pair);
            const bool expectedResult = true;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 取得SafeDictionary是否含有指定鍵值對_SafeDictionary內不包含該鍵值_應回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 5 },
                { 2, 3 }
            };
            var pair = new KeyValuePair<int, int>(3, 5);
            var result = ((ICollection<KeyValuePair<int, int>>)safeDictionary).Contains(pair);
            const bool expectedResult = false;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 取得SafeDictionary是否含有指定鍵值對_SafeDictionary內包含該鍵值但值不符合_應回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 5 },
                { 2, 3 }
            };
            var pair = new KeyValuePair<int, int>(2, 6);
            var result = ((ICollection<KeyValuePair<int, int>>)safeDictionary).Contains(pair);
            const bool expectedResult = false;
            result.Should().Be(expectedResult);
        }

        [Fact]
        [Trait("SafeCollections", "SafeList")]
        public void 拷貝SafeDictionary_從Index0開始拷貝_拷貝的結果應該與SafeDictionary相同()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            var fixture = new Fixture();
            safeDictionary.AddMany(fixture.Create<KeyValuePair<int, int>>, 3);
            var array = new KeyValuePair<int, int>[3];
            safeDictionary.CopyTo(array, 0);
            array.SequenceEqual(safeDictionary).Should().Be(true);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 清除SafeDictionary_清除成功_SafeDictionary長度應為0()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 2 },
                { 2, 3 },
                { 3, 4 }
            };
            safeDictionary.Clear();
            const int expectedCount = 0;
            safeDictionary.Count.Should().Be(expectedCount);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 移除SafeDictionary指定鍵值_移除失敗_字典內不應包含該鍵值且回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            const int key = 3;
            var isSuccessful = safeDictionary.Remove(key);
            const bool expectedResult = false;
            isSuccessful.Should().Be(expectedResult);
            var isContainsKey = safeDictionary.ContainsKey(key);
            const bool expectedIsContain = false;
            isContainsKey.Should().Be(expectedIsContain);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 移除SafeDictionary指定鍵值_移除成功_字典內不應包含該鍵值且回傳True()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            const int key = 2;
            var isSuccessful = safeDictionary.Remove(key);
            const bool expectedResult = true;
            isSuccessful.Should().Be(expectedResult);
            var isContainsKey = safeDictionary.ContainsKey(key);
            const bool expectedIsContain = false;
            isContainsKey.Should().Be(expectedIsContain);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 移除SafeDictionary指定鍵值對_移除失敗_字典內不應包含該鍵值且回傳False()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            var pair = new KeyValuePair<int, int>(6, 1);
            var isSuccessful = ((ICollection<KeyValuePair<int, int>>)safeDictionary).Remove(pair);
            const bool expectedResult = false;
            isSuccessful.Should().Be(expectedResult);
            var isContainsKey = safeDictionary.Contains(pair);
            const bool expectedIsContain = false;
            isContainsKey.Should().Be(expectedIsContain);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 移除SafeDictionary指定鍵值對_移除成功_字典內不應包含該鍵值且回傳True()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 }
            };
            var pair = new KeyValuePair<int, int>(2, 8);
            var isSuccessful = ((ICollection<KeyValuePair<int, int>>)safeDictionary).Remove(pair);
            const bool expectedResult = true;
            isSuccessful.Should().Be(expectedResult);
            var isContainsKey = safeDictionary.Contains(pair);
            const bool expectedIsContain = false;
            isContainsKey.Should().Be(expectedIsContain);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 新增鍵值對_SafeDictionary內已包含該鍵值_SafeDictionary內應包含該原本的鍵值對且不應擲出例外()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 5 }
            };
            const int key = 1;
            const int value = 2;
            Action addAction = () => safeDictionary.Add(key, value);
            addAction.Should().NotThrow();
            const int expectedValue = 5;
            safeDictionary[1].Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 新增鍵值對_新增成功_SafeDictionary內應包含該鍵值對()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            const int key = 1;
            const int value = 5;
            safeDictionary.Add(key, value);
            const int expectedValue = 5;
            safeDictionary[1].Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 新增鍵值對KeyValuePair型別_SafeDictionary內已包含該鍵值_SafeDictionary內應包含該原本的鍵值對且不應擲出例外()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 5 }
            };
            var pair = new KeyValuePair<int, int>(1, 2);
            Action addAction = () => ((ICollection<KeyValuePair<int, int>>)safeDictionary).Add(pair);
            addAction.Should().NotThrow();
            const int expectedValue = 5;
            safeDictionary[1].Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 新增鍵值對KeyValuePair型別_新增成功_SafeDictionary內應包含該鍵值對()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            var pair = new KeyValuePair<int, int>(1, 5);
            ((ICollection<KeyValuePair<int, int>>)safeDictionary).Add(pair);
            const int expectedValue = 5;
            safeDictionary[1].Should().Be(expectedValue);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 繞行SafeDictionary_繞行成功_繞行結果需與原本集合相同()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            var fixture = new Fixture();
            safeDictionary.AddMany(fixture.Create<KeyValuePair<int, int>>, 10);
            var result = new Dictionary<int, int>();
            foreach (var x in safeDictionary)
                result.Add(x.Key, x.Value);
            result.Should().BeEquivalentTo(safeDictionary);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 繞行SafeDictionary的Key_繞行成功_繞行結果需與原本集合相同()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            var fixture = new Fixture();
            safeDictionary.AddMany(fixture.Create<KeyValuePair<int, int>>, 10);
            var result = new Collection<int>();
            foreach (var x in safeDictionary.Keys)
                result.Add(x);
            result.Should().BeEquivalentTo(safeDictionary.Keys);
        }

        [Fact]
        [Trait("SafeCollections", "SafeDictionary")]
        public void 繞行SafeDictionary的Values_繞行成功_繞行結果需與原本集合相同()
        {
            var safeDictionary = new SafeDictionary<int, int>();
            var fixture = new Fixture();
            safeDictionary.AddMany(fixture.Create<KeyValuePair<int, int>>, 10);
            var result = new Collection<int>();
            foreach (var x in safeDictionary.Values)
                result.Add(x);
            result.Should().BeEquivalentTo(safeDictionary.Values);
        }

        [Fact(Skip = "MutiThread scenario result in long running, this test case should be executed only on demand.")]
        //[Fact]
        [Trait("SafeCollections", "SafeCollection")]
        public void 繞行SafeDictionary操作集合_不同執行緒同時對SafeDictionary新增與刪除元素_不應擲出例外()
        {
            var safeDictionary = new SafeDictionary<int, int>
            {
                { 1, 3 },
                { 2, 8 },
                { 3, 9 }
            };

            Task.Run(() =>
            {
                while (true)
                {
                    safeDictionary.Add(0, 1);
                    safeDictionary.Add(5, 7);
                    safeDictionary.Add(2, 3);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Add(new KeyValuePair<int, int>(6, 1));
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Add(new KeyValuePair<int, int>(7, 10));
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Add(new KeyValuePair<int, int>(8, 4));
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Remove(new KeyValuePair<int, int>(6, 1));
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Remove(new KeyValuePair<int, int>(7, 10));
                    ((ICollection<KeyValuePair<int, int>>)safeDictionary).Remove(new KeyValuePair<int, int>(8, 4));
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeDictionary.Remove(2);
                    safeDictionary.Remove(5);
                    safeDictionary.Remove(0);
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    safeDictionary.Clear();
                }
            });

            var iterateKeyValuePairs = IterateDictionary(safeDictionary);
            var iterateKeys = IterateCollection(safeDictionary.Keys);
            var iterateValues = IterateCollection(safeDictionary.Values);
            Action action = () => Task.WaitAll(iterateKeys, iterateValues, iterateKeyValuePairs);
            action.Should().NotThrow();
        }

        private static async Task IterateCollection<T>(ICollection<T> collection, int interation = 10000)
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

        private static async Task IterateDictionary<TKey, TValue>(IDictionary<TKey, TValue> table, int interation = 10000)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < interation; i++)
                {
                    foreach (var x in table)
                    {
                        var dummy = x;
                    }
                }
            });
        }
    }
}