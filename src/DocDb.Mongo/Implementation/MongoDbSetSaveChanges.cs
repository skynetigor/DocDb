using System;
using System.Collections.Generic;
using DocDb.Core.Abstracts;
using DocDb.Mongo.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Mongo.Implementation
{
    class MongoDbSetSaveChanges<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private IServiceProvider ServiceProvider { get; }
        private IState<TEntity> CurrentState { get; }

        public MongoDbSetSaveChanges(IServiceProvider serviceProvider, IStateManager stateManager)
        {
            this.ServiceProvider = serviceProvider;
            this.CurrentState = stateManager.GetOrCreateState<TEntity>();
        }

        public void Add(TEntity entity)
        {
            CurrentState.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            CurrentState.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            CurrentState.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            CurrentState.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            CurrentState.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            CurrentState.UpdateRange(entities);
        }

        public IIncludableQueryable<TEntity> UseEagerLoading()
        {
            return this.ServiceProvider.GetService<IEagerLoadingIncludableQueryable<TEntity>>();
        }

        public IIncludableQueryable<TEntity> UseLazyLoading()
        {
            return this.ServiceProvider.GetService<ILazyLoadingIncludableQueryable<TEntity>>();
        }
    }
}
