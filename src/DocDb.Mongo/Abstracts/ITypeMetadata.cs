using System;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Abstracts
{
    interface ITypeMetadata
    {
        void Map(TypeMetadata model, Type currentType);
    }
}
