using System;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Implementation.TypeMetadataInitializer
{
    internal class TypeInitializerImpl : ITypeInitializer
    {
        public void RegisterType<T>()
        {
            TypeInitializerStatic.RegisterType(typeof(T));
        }

        public void RegisterType(Type type)
        {
            TypeInitializerStatic.RegisterType(type);
        }

        public bool IsTypeRegistered(Type type)
        {
            return TypeInitializerStatic.IsTypeRegistered(type);
        }

        public TypeMetadata GetTypeMetadata<T>()
        {
            return TypeInitializerStatic.GetTypeMetadata(typeof(T));
        }

        public TypeMetadata GetTypeMetadata(Type type)
        {
            return TypeInitializerStatic.GetTypeMetadata(type);
        }
    }
}
