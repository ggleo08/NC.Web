﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NC.Core.Repositories;
using NC.Core.Services;
using NC.Model.EntityModels;
using NC.Service;
using NC.Common.log;

namespace NC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepository<Blog, Guid> _repository;
        private readonly IService<Blog, Guid> _service;
        public ValuesController(ILogger<ValuesController> logger, IRepository<Blog, Guid> repository, IService<Post, Guid> service) // , IRepository<Blog,Guid> repository
        {
            _logger = logger;
            //_repository = repository;
            //_service = service;
        }

        ////! 注入IUnitOfWork
        //private readonly IUnitOfWork _unitOfWork;
        //public ValuesController(IUnitOfWork unitOfWork, ILogger<ValuesController> logger)
        //{
        //    //_logger = logger;
        //    _unitOfWork = unitOfWork;
        //    var repo = unitOfWork.GetRepository<Post>();
        //    var cusRepo = unitOfWork.GetRepository<Post>(true);
        //}

        #region default actions
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInformation("This log is written by ILogger");
            Common.log.log4net.Info("lllllllllllllllll");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

    }
}
