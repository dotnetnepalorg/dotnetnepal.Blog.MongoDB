﻿using System.Threading.Tasks;

namespace dotnetnepal.Data
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database();
        IMongoClient Client();
        TResult RunCommand<TResult>(string command);
        TResult RunCommand<TResult>(string command, ReadPreference readpreference);
        BsonValue RunScript(string command, CancellationToken cancellationToken);
        Task<BsonValue> RunScriptAsync(string command, CancellationToken cancellationToken);
    }

}