using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RemoteReviewApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteReviewApi.Controllers
{
    [ApiController]
    [Route("Review")]
    public class ReviewController:ControllerBase
    {
        IConfiguration _configure;
        private readonly DbHelper _dbHelper;
        public ReviewController(IConfiguration configuration)
        {
            _configure = configuration;
            _dbHelper = new DbHelper(_configure);
        }

        [HttpGet]
        [Route("GetAllQuerieswithComment")]
        public IEnumerable<QueryTable> GetAllQueries()
        {
            return _dbHelper.GetAllQueriesCount();
        }

        [HttpGet]
        [Route("GetFlagswithReview")]
        public IEnumerable<QuestionFlagTable> GetFlagswithReview()
        {
            return _dbHelper.GetAllFlags(true);
        }

        [HttpGet]
        [Route("validateLogin")]
        public bool ValidateLogin(string username, string password)
        {
            return _dbHelper.validatelogin(username, password);
        }

        [HttpGet]
        public IEnumerable<QueryTable> Get()
        {
            return _dbHelper.GetAllQueries();
        }

        [HttpPost]
        [Route("SaveComments")]
        public ActionResult SaveComments([FromBody] QueryTable model)
        {
            if (!string.IsNullOrWhiteSpace( model.Comments))
            {
               if (_dbHelper.SaveQueryComments(model))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetFlags")]
        public IEnumerable<QuestionFlagTable> GetFlags()
        {
            return _dbHelper.GetAllFlags();
        }

        [HttpGet]
        [Route("GetFlagsCount")]
        public IEnumerable<FormFlags> GetFlagsCount()
        {
            return _dbHelper.GetFormsFlagCount();
        }

        [HttpGet]
        [Route("GetQuestionFlags")]
        public IEnumerable<QuestionFlagTable> GetQuestionFlags(int PatientFormKey)
        {
            return _dbHelper.GetQuestion(PatientFormKey);
        }


        [HttpGet]
        [Route("GetFormFlags")]
        public int GetFormFlagsCount()
        {
            return _dbHelper.GetAllFlagsCount();
        }

        [HttpPost]
        [Route("SaveFlags")]
        public ActionResult SaveFlags([FromBody] QuestionFlagTable model)
        {
            if(_dbHelper.SaveFlags(model))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }               
        }

    }
}
