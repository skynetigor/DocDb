﻿using System;
using System.Reflection;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Helpers
{
    class TrackingListHelper
    {
        public static object CreateNewTrackingList(Type entityType, object enumerable)
        {
            var trListType = typeof(TrackingList<>).MakeGenericType(entityType);
            return trListType.GetMethod(nameof(CreateNewTrackingList), BindingFlags.Public | BindingFlags.Static)
                .Invoke(null, new object[] { enumerable });
        }
    }
}
