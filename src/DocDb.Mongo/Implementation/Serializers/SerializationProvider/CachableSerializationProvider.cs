﻿using System;
using System.Collections.Generic;
using System.Linq;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Extensions;
using MongoDB.Bson.Serialization;

namespace DocDb.Mongo.Implementation.Serializers.SerializationProvider
{
    class CacheableSerializationProvider : AbstractCacheableServiceProvider<IBsonSerializer>, IBsonSerializationProvider
    {
        private static readonly IDictionary<string, Type> SerializersTypes;

        private ITypeInitializer TypeInitializer { get; }

        static CacheableSerializationProvider()
        {
            var ctx = typeof(CacheableSerializationProvider).Assembly.GetTypes();

            SerializersTypes = typeof(CacheableSerializationProvider).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && t.GetInterfaces()
                                .Contains(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBsonSerializer<>)))
                .ToDictionary(t => t.GetInterfaces().FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IBsonSerializer<>))
                    .GetGenericArguments()[0].ToString());
        }

        public CacheableSerializationProvider(ITypeInitializer typeInitializer) : base()
        {
            TypeInitializer = typeInitializer;
        }

        protected override IDictionary<string, Type> TypesDictionary => SerializersTypes;

        protected override object CreateInstance(Type instanceType)
        {
            return Activator.CreateInstance(instanceType);
        }

        public IBsonSerializer GetSerializer(Type type)
        {
            if (IsTypeRegistered(type))
            {
                return this.GetServiceInstance(type, typeof(ModelsSerializer<>).MakeGenericType(type));
            }

            return null;
        }

        private bool IsTypeRegistered(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            return TypeInitializer.IsTypeRegistered(type);
        }
    }
}
