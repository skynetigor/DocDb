using System;
using System.Collections.Generic;
using System.Text;
using DocDb.Core.Abstracts;

namespace DocDb.Mongo.Abstracts
{
    internal interface ILazyLoadingIncludableQueryable<T>: IIncludableQueryable<T>
    {
    }
}
