using System;

namespace DocDb.Mongo.Implementation.Serializers.SerializationProvider
{
    internal interface IDbDocSerializationProvider
    {
        object TryGetSerializer(Type type);
    }
}
