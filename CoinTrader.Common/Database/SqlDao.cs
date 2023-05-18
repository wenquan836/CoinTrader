
using System;
using System.Data;
using System.Data.SQLite;

namespace CoinTrader.Common.Database
{
   public  class SqlDao :IDisposable
    {

        private string databasePath = "";
        public string DatabasePath
        {
            get
            {
                return databasePath;
            }
            set
            {
                if(this.connection != null)
                {
                    this.CloseConnection();
                }

                this.databasePath = value;
            }
        }

        private void CloseConnection()
        {
            if (this.connection != null)
            {
                this.connection.Close();
                this.connection.Dispose();
                this.connection = null;
            }
        }

        private SQLiteConnection connection = null;

        private void InitConnection()
        {
            if (connection == null)
            {
                lock (this)
                {
                    if(connection == null)
                    {
                        SQLiteConnection cn = new SQLiteConnection("Data Source=\"" + this.DatabasePath + "\";Pooling=true;FailIfMissing=false");
                        cn.Open();
                        this.connection = cn;
                    }
                }
            }
        }

        public void Dispose()
        {
            this.CloseConnection();
        }

        public bool TableIsExist(string name)
        {
           return ExecuteScalar<long>(string.Format("SELECT COUNT(*) as c FROM sqlite_master where type='table' and name='{0}'", name)) > 0;
        }


        public T ExecuteScalar<T>(string sql)
        {
            this.InitConnection();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = this.connection;
                cmd.CommandText = sql;
                object ret = cmd.ExecuteScalar();
                return ret != null && ret != DBNull.Value ? (T)ret : default(T);
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            return this.ExecuteNonQuery(sql, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="withTransaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql,bool withTransaction)
        {
            this.InitConnection();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = this.connection;
                cmd.CommandText = sql;
                int ret = 0;

                if(withTransaction)
                {
                    using (SQLiteTransaction transction = connection.BeginTransaction())
                    {
                        try
                        {
                            ret = cmd.ExecuteNonQuery();
                            transction.Commit();
                        }
                        catch(Exception ex)
                        {
                            transction.Rollback();
                            Logger.Instance.LogException(ex);
                        }
                        finally
                        {

                        }
                    }
                }
                else
                {
                    ret = cmd.ExecuteNonQuery();
                }

                return ret;
            }
        }

        public DataTable ExecuteDatatable(string sql)
        {
            this.InitConnection();
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = this.connection;

                //读取数据
                cmd.CommandText = sql;

                using (SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        public void ExecuteSql(string sql, Action<SQLiteDataReader> callback)
        {
            this.InitConnection();
            //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = this.connection;

                //读取数据
                cmd.CommandText = sql;
                using (SQLiteDataReader dr = cmd.ExecuteReader(CommandBehavior.Default))
                {
                    callback(dr);
                    //while (dr.Read())
                    //{
                    //    str = dr.GetValue(0).ToString();
                    //    str2 = dr.GetValue(1).ToString();
                    //    Console.WriteLine("第{0} 条：{1}", dr.GetValue(0), dr.GetString(1));
                    //}

                    dr.Close();
                }
            }
        }

        private SqlDao()
        {

        }


        private static object locker = new object();
        private static SqlDao _instance = null;

        public static SqlDao Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null)
                        {
                            var dao = new SqlDao();

                            _instance = dao;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
