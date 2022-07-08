// -----------------------------------------------------------------------
// <copyright file="AuthorizationMiddleware.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Filters
{
    using System;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    //using System.Web.Http.Filters;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore.Storage;
    using Opeeka.PICS.Infrastructure.Data;
    //using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;

    /// <summary>
    /// The default middleware to authorize a request.
    /// </summary>
    public class TransactionFilter : ActionFilterAttribute
    {

        private readonly OpeekaDBContext _dbContext;
        private IDbContextTransaction _transactionScope;
        public TransactionFilter(OpeekaDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //private static readonly Serilog.ILogger logger = Log.ForContext<AuthorizationMiddleware>();
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _transactionScope = _dbContext.Database.BeginTransaction();
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            try
            {
                base.OnActionExecuted(actionExecutedContext);
                _transactionScope.Commit(); //Error occurred
            }
            catch (Exception ex)
            {
                _transactionScope.Rollback(); // because something went wrong during your transaction
            }
            finally
            {
                _transactionScope.Dispose(); // now that we're definitely done.
            }
        }

    }
}
