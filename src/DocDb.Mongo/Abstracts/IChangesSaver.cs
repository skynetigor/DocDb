using System;
using System.Collections.Generic;
using System.Text;

namespace DocDb.Mongo.Abstracts
{
    interface IChangesSaver
    {
        void SaveChanges();
    }
}
