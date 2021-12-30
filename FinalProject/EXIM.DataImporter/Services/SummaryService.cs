using AutoMapper;
using EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.Exceptions;
using EXIM.DataImporter.UnitOfWorks;
using EXIM.Membership.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IDataImporterUnitOfWork _dataImporterUnitOfWork;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public SummaryService(IDataImporterUnitOfWork dataImporterUnitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataImporterUnitOfWork = dataImporterUnitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public (IList<Import> records, int total, int totalDisplay) GetImports
            (int pageIndex, int pageSize, string searchText, string sortText)
        {
            var summaryData = _dataImporterUnitOfWork.Imports.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from summaries in summaryData.data
                              select _mapper.Map<Import>(summaries)).ToList();

            return (resultData, summaryData.total, summaryData.totalDisplay);
        }

        public IList<Import> GetImportList()
        {
            var summaryEntity = _dataImporterUnitOfWork.Imports.GetAll();

            var summaries = new List<Import>();

            foreach (var entity in summaryEntity)
            {
                var summary = _mapper.Map<Import>(entity);
                summaries.Add(summary);
            }
            return summaries;
        }

        public (IList<Export> records, int total, int totalDisplay) GetExports
            (int pageIndex, int pageSize, string searchText, string sortText)
        {
            var summaryData = _dataImporterUnitOfWork.Exports.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from summaries in summaryData.data
                              select _mapper.Map<Export>(summaries)).ToList();

            return (resultData, summaryData.total, summaryData.totalDisplay);
        }

        public IList<Export> GetExportList()
        {
            var summaryEntity = _dataImporterUnitOfWork.Exports.GetAll();

            var summaries = new List<Export>();

            foreach (var entity in summaryEntity)
            {
                var summary = _mapper.Map<Export>(entity);
                summaries.Add(summary);
            }
            return summaries;         
        }
    }
}
