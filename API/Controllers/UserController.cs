using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Core.Model;
using Serilog;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _service;
        private readonly Serilog.ILogger _logger;

        public UserController(IUserService service, Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var lst = await _service.GetUsers();



                if (lst.Count != 0)
                {
                    return new OkObjectResult(lst);
                }
                else
                {
                    var showmessage = "Pas d'element dans la liste";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {


                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Erreur " + ex.ToString());
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var lst = await _service.GetUser(id);



                if (lst != null)
                {
                    return new OkObjectResult(lst);
                }
                else
                {
                    var showmessage = "User inexistant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {


                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Erreur " + ex.ToString());
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<ActionResult> AddUser(User e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.AddUser(e)
                          .ConfigureAwait(false);

                var showmessage = "Insertion effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Erreur " + ex.ToString());
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public async Task<ActionResult> UpdUser(User e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.UpdUser(e)
                          .ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);




            }
            catch (Exception ex)
            {
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Erreur " + ex.ToString());
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<ActionResult> DelUser(string id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.DeleteUser(id)
                          .ConfigureAwait(false);

                var showmessage = "Suppression effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);




            }
            catch (Exception ex)
            {
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Erreur " + ex.ToString());
                return BadRequest(dict);
            }
        }



        [Route("IsLogin")]
        [HttpGet]
        public async Task<ActionResult<bool>> IsLogin(string username, string password)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            try
            {
                // Perform login verification logic using your IUserService
                bool isLoginValid = await _service.VerifyLogin(username, password);

                if (isLoginValid)
                {
                    var showmessage = "Login successful";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }
                else
                {
                    var showmessage = "Invalid username or password";
                    dict.Add("Message", showmessage);
                    return Unauthorized(dict);
                }
            }
            catch (Exception ex)
            {
                var showmessage = "Error: " + ex.Message;
                dict.Add("Message", showmessage);
                _logger.Error("Error: " + ex.ToString());
                return BadRequest(dict);
            }
        }




    }
}

