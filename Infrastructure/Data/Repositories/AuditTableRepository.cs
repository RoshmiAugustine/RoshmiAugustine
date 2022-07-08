// -----------------------------------------------------------------------
// <copyright file="AuditTableRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AuditTableRepository : BaseRepository<AuditTableName>, IAuditTableRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public AuditTableRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }


        /// <summary>
        /// To get details AuditTableName.
        /// </summary>
        /// <param id="id">.</param>
        /// <returns>.AgencyDTO</returns>
        public List<AuditTableNameDTO> GetAuditableTableField(string tableName)
        {
            string sql = @"
            select AT.TableName, AF.FieldName from AuditTableName AT 
                JOIN AuditFieldName AF ON AT.TableName = AF.TableName
                where AT.Name = '" + tableName + "'";

            var result = ExecuteSqlQuery(sql, x => x != null ? new AuditTableNameDTO { TableName = (string)x[0], FieldName = (string)x[1] } : null);
            return result;
        }


    }
}
