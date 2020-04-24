using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.SqLite
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
