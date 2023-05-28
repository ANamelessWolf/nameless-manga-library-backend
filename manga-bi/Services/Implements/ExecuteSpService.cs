using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nameless.MangaBI.Services.Interfaces;
using Nameless.MangaBI.Models;

namespace Nameless.MangaBI.Services.Implements
{
    public class ExecuteSpService<T> : IExecuteSpService<T> where T : class
    {
        protected DbContext _spContext;

        public ExecuteSpService(DbContext context)
        {
            _spContext = context;
        }

        private String PrepareSqlExecute(RequestStoredProcedure request, out String strParameters)
        {

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbParams = new StringBuilder();
            string strComa = string.Empty;
            strComa = " ";
            if (String.IsNullOrEmpty(request.SpName))
                throw new Exception("StoreProcedureName is null or empty");
            sbSql.Append("Exec " + request.SpName);
            foreach (SqlParameter item in request.Parameters)
            {
                sbSql.Append(strComa + item.ParameterName + (item.Direction == ParameterDirection.Output || item.Direction == ParameterDirection.InputOutput ? " Output" : ""));
                if (item.Direction == ParameterDirection.Input || item.Direction == ParameterDirection.InputOutput)
                {
                    sbParams.Append(String.Format("{0} {1}={2}", strComa, item.ParameterName, item.Value == DBNull.Value ? "Null" : item.Value));
                }
                strComa = ",";
            }
            strParameters = sbParams.ToString();
            return sbSql.ToString();
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedure(RequestStoredProcedure request)
        {
            IEnumerable<T> result = null;
            String sbSql = String.Empty;
            String sbParams = String.Empty;
            StringBuilder sbError = new StringBuilder();
            if (request == null)
                throw new Exception("Parameter request is null");
            try
            {
                sbSql = PrepareSqlExecute(request, out sbParams);
                result = await _spContext.Set<T>().FromSqlRaw(sbSql.ToString(), request.Parameters).ToListAsync();
            }
            catch (SqlException ex)
            {
                foreach (SqlError item in ex.Errors)
                {
                    sbError.Append(String.Format("{0} - {1}\n", item.Number, item.Message));
                }
            }
            catch (Exception e)
            {
                sbError.Append(e.Message);
            }
            if (!String.IsNullOrEmpty(sbError.ToString()))
                throw new Exception(sbError.ToString());// "Unexpected error. Check with your administrator");
            return result;
        }

        public async Task ExecuteVoidStoredProcedure(RequestStoredProcedure request)
        {
            int result = 0;
            String sbSql = String.Empty;
            String sbParams = String.Empty;
            StringBuilder sbError = new StringBuilder();
            if (request == null)
                throw new Exception("Parameter request is null");
            try
            {
                sbSql = PrepareSqlExecute(request, out sbParams);
                result = await _spContext.Database.ExecuteSqlRawAsync(sbSql.ToString(), request.Parameters);
            }
            catch (SqlException ex)
            {
                foreach (SqlError item in ex.Errors)
                {
                    sbError.Append(String.Format("{0} - {1}\n", item.Number, item.Message));
                }
            }
            catch (Exception e)
            {
                sbError.Append(e.Message);
            }
            if (!String.IsNullOrEmpty(sbError.ToString()))
                throw new Exception("Unexpected error. Check with your administrator");
        }
    }
}
