using Autofac;
using AutoMapper;
using EXIM.Common.Utilities;
using EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class ExportService : IExportService
    {
        private IDataImporterUnitOfWork _dataImporterUnitOfWork;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;

        public ExportService(IDataImporterUnitOfWork dataImporterUnitOfWork,
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

        public List<ExcelData> GroupData(int id)
        {
            var resultData = _dataImporterUnitOfWork.ExcelData.GetAll()
                .Where(e => e.GroupId == id)
                .ToList();
            var entities = (from entity in resultData
                            select _mapper.Map<ExcelData>(entity)).ToList();
            return entities;
        }

        public DataSet Data(int id)
        {
            var entities = GroupData(id);

            var rowNums = new List<int>();

            foreach (var item in entities)
            {
                rowNums.Add(item.Id);
            }

            var dataSet = new DataSet();
            var dataTable = new DataTable();
            DataColumn dataColumn;
            DataRow dataRow;

            var firstRow = rowNums[0];

            var columns = Row(firstRow);
            foreach (var item in columns)
            {
                dataColumn = new DataColumn()
                {
                    ColumnName = item.Name,
                    DataType = item.Name.GetType()
                };
                dataTable.Columns.Add(dataColumn);
            }            

            foreach (var num in rowNums)
            {
                dataRow = dataTable.NewRow();

                var row = Row(num);

                foreach (var item in row)
                {
                    dataRow[item.Name] = item.Value;
                }
                dataTable.Rows.Add(dataRow);
            }
            dataSet.Tables.Add(dataTable);

            return dataSet;
        }

        public List<ExcelFieldData> Row(int num)
        {
            var allData = _dataImporterUnitOfWork.ExcelFieldData.GetAll()
                    .Where(e => e.ExcelDataId == num).ToList();
            var rowDataList = (from entity in allData
                        select _mapper.Map<ExcelFieldData>(entity)).ToList();

            return rowDataList;
        }

        public string FileNameGenerator(int id)
        {
            var entity = _dataImporterUnitOfWork.Groups.GetById(id);
            var groupName =  entity.Name;

            var fileName = new StringBuilder();

            fileName.Append(groupName);
            fileName.Append(_dateTimeUtility.Now.ToString());
            fileName.Append(".xlsx");            

            return fileName.ToString();            
        }

        public void CreateExportStatus(int id)
        {
            var entity = _dataImporterUnitOfWork.Groups.GetById(id);

            var export = new Export
            {
                GroupId = id,
                GroupName = entity.Name,
                Date = _dateTimeUtility.Now
            };

            _dataImporterUnitOfWork.Exports.Add(
                _mapper.Map<Entities.Export>(export)
            );

            _dataImporterUnitOfWork.Save();
        }
    }
}
