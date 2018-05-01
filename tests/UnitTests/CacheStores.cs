﻿using Nancy.RapidCache.CacheStore;
using Nancy.RapidCache.Tests.Fakes;
using Nancy.RapidCache.Tests.Helpers;
using System;
using Xunit;

namespace Nancy.RapidCache.Tests.UnitTests
{
    public class CacheStores
    {
        private const string TEST_KEY_1 = "TestKeyRequest1";
        private const string TEST_KEY_2 = "TestKeyRequest2";
        private DateTime expirationDate = DateTime.Now.AddMinutes(15);

        [Fact]
        public void Memory_cache_set_get()
        {
            //Arrange

            var cache = new MemoryCacheStore();
            var context = new NancyContext() { Response = new FakeResponse() { } };

            //Act
            cache.Set(TEST_KEY_1, context, expirationDate);
            var response = cache.Get(TEST_KEY_1);

            //Assert
            Assert.Equal(context.Response.ContentType, response.ContentType);
            Assert.Equal(context.Response.StatusCode, response.StatusCode);
            Assert.Equal(expirationDate, response.Expiration);
            Assert.Equal(context.Response.Contents.ConvertStream(), response.Contents.ConvertStream());
        }

        [Fact]
        public void Memory_cache_set_remove_get()
        {
            //Arrange
            var expirationDate = DateTime.Now.AddMinutes(15);
            var cache = new MemoryCacheStore();
            var context = new NancyContext() { Response = new FakeResponse() { } };

            //Act
            cache.Set(TEST_KEY_2, context, expirationDate);

            cache.Remove(TEST_KEY_2);

            var response = cache.Get(TEST_KEY_2);

            //Assert
            Assert.Null(response);
            Assert.NotNull(context.Response);
        }
    }
}