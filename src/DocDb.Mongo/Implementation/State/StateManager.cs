using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Mongo.Implementation.State
{
    internal class StateManager: IStateManager
    {
        private readonly IList<IChangesSaver> _states = new List<IChangesSaver>();

        private IServiceProvider ServiceProvider { get; }

        public StateManager(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void SaveChanges()
        {
            _states.ForEach(st => st.SaveChanges());
        }

        public IState<T> GetOrCreateState<T>() where T : class
        {
            var state = _states.FirstOrDefault(st => st is IState<T>);

            if (state == null)
            {
                state = ServiceProvider.GetService<IState<T>>();
                _states.Add(state);
            }

            return (IState<T>)state;
        }
    }
}
