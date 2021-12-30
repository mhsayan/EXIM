using Autofac;
using EXIM.DataImporter.Services;
using EXIM.Worker.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EXIM.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DataImporterModel _dataImporterModel;

        public Worker(ILogger<Worker> logger, DataImporterModel dataImporterModel)
        {
            _logger = logger;
            _dataImporterModel = dataImporterModel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _dataImporterModel.ImportData();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
