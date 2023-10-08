using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
    public class DB
    {
        public DB() { }
        static DataBase instance;
        public static DataBase GetInstance()
        {
            if (instance == null)
                instance = new DataBase();
            return instance;
        }

    }
}
