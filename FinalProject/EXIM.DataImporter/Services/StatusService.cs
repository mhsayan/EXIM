using AutoMapper;
using EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.Exceptions;
using EXIM.DataImporter.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class StatusService : IStatusService
    {
        private readonly IDataImporterUnitOfWork _dataImporterUnitOfWork;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public StatusService(IDataImporterUnitOfWork dataImporterUnitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataImporterUnitOfWork = dataImporterUnitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Import> GetAllImports()
        {
            var importEntities = _dataImporterUnitOfWork.Imports.GetAll();
            var imports = new List<Import>();

            foreach (var entity in importEntities)
            {
                var import = _mapper.Map<Import>(entity);
                imports.Add(import);
            }

            return imports;
        }

        public void CreateImportStatus(Import import)
        {
            if (import == null)
                throw new InvalidParameterException("Field was not provided");

            if (IsNameAlreadyUsed(import.FileName))
                throw new DuplicateTitleException("This File already exists");

            _dataImporterUnitOfWork.Imports.Add(
                _mapper.Map<Entities.Import>(import)
            );

            _dataImporterUnitOfWork.Save();
        }

        private bool IsNameAlreadyUsed(string fileName) =>
            _dataImporterUnitOfWork.Imports.GetCount(x => x.FileName == fileName) > 0;

        public (IList<Import> records, int total, int totalDisplay) GetImports(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var importData = _dataImporterUnitOfWork.Imports.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.GroupName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from imports in importData.data
                              select _mapper.Map<Import>(imports)).ToList();

            return (resultData, importData.total, importData.totalDisplay);
        }

        public Import GetImport(int id)
        {
            var import = _dataImporterUnitOfWork.Imports.GetById(id);

            if (import == null) return null;

            return _mapper.Map<Import>(import);
        }

        public void UpdateImportStatus(Import import)
        {
            if (import == null)
                throw new InvalidOperationException("Import is missing");

            var importEntity = _dataImporterUnitOfWork.Imports.GetById(import.Id);

            if (importEntity != null)
            {
                _mapper.Map(import, importEntity);
                _dataImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find import");
        }
    }
}
