﻿namespace Nancy.RapidCache
{
    public static class Defaults
    {
        public static readonly string NoRequestQueryKey = "rapidCacheDisabled";
        public static readonly string RemoveCacheKey = "rapidCacheRemove";
        public static readonly string CacheHeader = "nancy-rapidcache";
        public static readonly string CacheExpiry = "X-Nancy-RapidCache-Expiration";
    }
}
