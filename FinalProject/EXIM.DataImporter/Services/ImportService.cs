using Autofac;
using AutoMapper;
using EXIM.Common.Utilities;
using EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class ImportService : IImportService
    {
        private IDataImporterUnitOfWork _dataImporterUnitOfWork;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;

        public ImportService(IDataImporterUnitOfWork dataImporterUnitOfWork,
            IMapper mapper,
            IDateTimeUtility dateTimeUtility)
        {
            _dataImporterUnitOfWork = dataImporterUnitOfWork;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _dataImporterUnitOfWork = scope.Resolve<IDataImporterUnitOfWork>();
            _mapper = scope.Resolve<IMapper>();
            _dateTimeUtility = scope.Resolve<IDateTimeUtility>();
        }

        public void ImportExceData(string path, string groupName, int id)
        {
            var filePath = path;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo existingFile = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var fieldData = new List<ExcelFieldData>();
                    AddTableRow(groupName, id);
                    var rowId = _dataImporterUnitOfWork.ExcelData.GetMaxId();
                    
                    for (int col = 1; col <= colCount; col++)
                    {
                        var header = worksheet.Cells[1, col].Value?.ToString().Trim();
                        var value = worksheet.Cells[row, col].Value?.ToString().Trim();

                        fieldData.Add(
                            new ExcelFieldData
                            {
                                Name = header,
                                Value = value,
                                ExcelDataId = rowId
                            });
                    }
                    AddFieldData(fieldData);
                }
            }
        }

        public void AddFieldData(List<ExcelFieldData> excelFieldData)
        {

            foreach (var data in excelFieldData)
            {
                _dataImporterUnitOfWork.ExcelFieldData.Add(
                    _mapper.Map<Entities.ExcelFieldData>(data)
                );
            }           

            _dataImporterUnitOfWork.Save();
        }
        
        public void AddTableRow(string groupName, int id)
        {

            var excelData = new ExcelData()
            {
                Date = _dateTimeUtility.Now,
                GroupId = id,
                GroupName = groupName
            };

            _dataImporterUnitOfWork.ExcelData.Add(
                _mapper.Map<Entities.ExcelData>(excelData)
            );

            _dataImporterUnitOfWork.Save();
        }
        public void GetImportFiles()
        {
            var entities = _dataImporterUnitOfWork.Imports.GetAll();            

            foreach (var entity in entities)
            {
                if (entity.Status != "Done")
                {
                    ImportExceData(entity.FilePath, entity.GroupName, entity.Id);
                    UpdateStatus(entity);
                    File.Delete(entity.FilePath);
                }                
            }
        }

        public void UpdateStatus(Entities.Import import)
        {
            if (import == null)
                throw new InvalidOperationException("Import is missing");

            var importEntity = _dataImporterUnitOfWork.Imports.GetById(import.Id);

            import.Status = "Done";

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
