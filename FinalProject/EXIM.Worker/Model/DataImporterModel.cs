using Autofac;
using AutoMapper;
using EXIM.DataImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.Worker.Model
{
    public class DataImporterModel
    {
        private IImportService _importService;
        private readonly ILifetimeScope _scope;

        public DataImporterModel(IImportService importService, ILifetimeScope scope)
        {
            _importService = importService;
            _scope = scope;
        }

        public void ImportData()
        {
            _importService.GetImportFiles();
        }
    }
}
