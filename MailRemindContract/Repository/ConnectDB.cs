using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailRemindContract.Repository
{
    class ConnectDB
    {
        private static string ConnectionString { get; set; }
        private static MongoUrl MongoUrl { get; set; }
        private static MongoClient MongoClient { get; set; }
        public static IMongoDatabase db { get; set; }

        public IMongoDatabase GetDB()
        {
            ConnectionString = ConfigurationSettings.AppSettings["ConnectString"];
            MongoUrl = new MongoUrl(ConnectionString);
            MongoClient = new MongoClient(MongoUrl);
            db = MongoClient.GetDatabase(MongoUrl.DatabaseName);

            return db;
        }
    }
}
