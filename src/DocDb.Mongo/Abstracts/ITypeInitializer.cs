using System;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Abstracts
{
    internal interface ITypeInitializer
    {
        TypeMetadata GetTypeMetadata<T>();

        TypeMetadata GetTypeMetadata(Type type);

        void RegisterType<T>();

        void RegisterType(Type type);

        bool IsTypeRegistered(Type type);
    }
}
