﻿using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class NullCacheService : ICacheService
    {
        public T? Get<T>(int id) where T : IdBasedEntity<int>
        {
            return null;
        }

        public T? Get<T>(string key) where T : class
        {
            return null;
        }

        public void Refresh(string key)
        {

        }

        public void Refresh<T>(int id) where T : IdBasedEntity<int>
        {

        }

        public void Remove(string key)
        {

        }

        public void Remove<T>(int id) where T : IdBasedEntity<int>
        {

        }

        public void Set(string key, object? value)
        {

        }

        public void Set<T>(T? value) where T : IdBasedEntity<int>
        {

        }
    }

}