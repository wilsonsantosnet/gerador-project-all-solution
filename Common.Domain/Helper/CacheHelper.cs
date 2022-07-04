﻿using Common.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Common.Domain.Base
{
    public class CacheHelper
    {
        protected readonly ICache _cache;

        protected string _tagNameCache;

        public CacheHelper(ICache _cache)
        {
            this._cache = _cache;
        }

        public virtual void ClearCache()
        {
            if (!this._cache.Enabled())
                return;

            if (this._cache.IsNotNull())
            {
                var tag = this._cache.Get<List<string>>(this._tagNameCache);
                if (tag.IsNull()) return;
                foreach (var item in tag)
                {
                    this._cache.Remove(item);
                }
                this._cache.Remove(this._tagNameCache);
            }

        }

        public virtual void SetTagNameCache(string tagNameCahed)
        {
            this._tagNameCache = tagNameCahed;
        }

        public virtual string GetTagNameCache()
        {
            return this._tagNameCache;
        }

        public void AddTagCache(string filterKey, string group)
        {
            var tags = this._cache.Get<List<string>>(group);
            if (tags.IsNull()) tags = new List<string>();
            if (tags.Where(_ => _ == filterKey).IsNotAny())
            {
                tags.Add(filterKey);
                this._cache.Add(group, tags);
            }
        }
    }
}
