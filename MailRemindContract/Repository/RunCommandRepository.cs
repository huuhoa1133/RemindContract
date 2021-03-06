﻿using MailRemindContract.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailRemindContract.Repository
{
    public class RunCommandRepository
    {
        private IMongoDatabase _db;

        public RunCommandRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<DataResultDto> GetContractRemind()
        {
            try
            {
                string content = $"{{ eval: \"contract_get_remind()\" }}";
                var result = await RunCommandAsync<DataRunCommand<DataResultDto>>(content);
                return result.retval;
            }catch(Exception ex)
            {
                WriteLogAsync(ex.Message, "GetContractRemind");
                return null;
            }
        }

        public async Task<T> RunCommandAsync<T>(string eval)
        {
            try
            {
                var command = new JsonCommand<BsonDocument>(eval);
                var result = await _db.RunCommandAsync(command);
                return BsonSerializer.Deserialize<T>(result);
            }
            catch (Exception ex)
            {
                WriteLogAsync(ex.Message, "RunCommandAsync");
                throw;
            }
        }

        public void WriteLogAsync(string message, string function)
        {
            _db.GetCollection<object>(CollectionNames.log).InsertOne(new { function = function, message = message, date = DateTime.UtcNow });
        }

        public void HistoryAsync(Contract[] contract)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var model = new
            {
                date = new
                {
                    Year = now.Year,
                    Month = now.Month,
                    Day = now.Day,
                    Hour = now.Hour,
                    Minute = now.Minute,
                    Second = now.Second
                },
                contract = contract
            };
            _db.GetCollection<object>(CollectionNames.contract_remind).InsertOne(model);
        }
    }
}
