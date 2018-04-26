using System;
using System.Collections.Generic;
using System.Text;

namespace DocDb.Mongo.Abstracts
{
    interface IStateManager: IChangesSaver
    {
        IState<T> GetOrCreateState<T>() where T : class;
    }
}
