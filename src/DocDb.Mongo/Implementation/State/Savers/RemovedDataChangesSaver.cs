using System.Collections.Generic;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.State.Savers
{
    internal class RemovedDataChangesSaver<TEntity> : AbstractChangesSaver<TEntity>
    {
        public RemovedDataChangesSaver(IMongoDatabase database, ITypeInitializer typeInitializer) : base(database,
            typeInitializer)
        {
        }

        protected override void SaveChanges(IEnumerable<TEntity> dataForSaving)
        {

            dataForSaving.ForEach(entity =>
            {
                object id = this.CurrentTypeModel.IdProperty.GetValue(entity);

                if (id != null)
                {
                    var filter = new BsonDocument
                    {
                        {"_id", (string) id}
                    };

                    this.Database.GetCollection<BsonDocument>(CurrentTypeModel.CollectionName)
                        .FindOneAndDelete(entity.ToBsonDocument());
                }
            });
        }
    }
}
