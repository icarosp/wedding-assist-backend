//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Net;
//using WeddingAssist.Api.Models;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WeddingAssist.Api.Controllers
//{
//    [Route("api/[controller]")]
//    public class BaseController : Controller
//    {
//        private HttpResponseMessage _responseMessage;

//        public Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object obj)
//        {

//            if ((int)code > 300)
//            {
//                _responseMessage = new HttpResponseMessage(code);
//                _responseMessage.Content = new Result(obj);
//            }
//            else {

//            }
//        }
//}
