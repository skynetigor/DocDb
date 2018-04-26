using System;
using System.Collections.Generic;
using System.Text;

namespace DocDb.Mongo.Abstracts
{
    internal interface ITrackingList<T>
    {
        List<T> RemovedList { get; }

        List<T> AddedList { get; }

        List<T> CurrentList { get; }
    }
}
