using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.Http.OData.Routing;
using WebAPI.ViewModels;
using Microsoft.Data.OData;
using WebAPI.Models;
using System.Data.Entity;
namespace WebAPI.API.OdataAPI
{

    public class CodeManagersController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/CodeManagers
        public IHttpActionResult GetCodeManagers(ODataQueryOptions<CodeManagerViewModel> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
                var result = db.CodeManagers.Select(s => new CodeManagerViewModel
                {
                    Id = s.Id,
                    Prefix = s.Prefix,
                    DateFormat = s.DateFormat,
                    NumberOfZeroInNumber = s.NumberOfZeroInNumber,
                    CodeDefine = s.CodeDefine, 
                    Element = s.Element
                });
                return Ok(result);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: odata/CodeManagers(5)
        public IHttpActionResult GetCodeManagerViewModel([FromODataUri] int key, ODataQueryOptions<CodeManagerViewModel> queryOptions)
        {
            // validate the query.
            try
            {
                var result = db.CodeManagers.Where(i => i.Element == key).Select(s => new CodeManagerViewModel
                {
                    Id = s.Id,
                    Prefix = s.Prefix,
                    DateFormat = s.DateFormat,
                    NumberOfZeroInNumber = s.NumberOfZeroInNumber,
                    CodeDefine=s.CodeDefine,
                    Element = s.Element
                }).FirstOrDefault();
                return Ok(result);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: odata/CodeManagers(5)
        public IHttpActionResult Put([FromODataUri] int key,CodeManagerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var code = db.CodeManagers.Where(i => i.Element == key).FirstOrDefault();
            code.DateFormat = model.DateFormat;
            code.NumberOfZeroInNumber = model.NumberOfZeroInNumber;
            code.Prefix = model.Prefix;
            try
            {
                db.Entry(code).State = EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
