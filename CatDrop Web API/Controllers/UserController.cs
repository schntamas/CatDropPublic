using CatDrop.Interfaces;
using CatDrop.Models.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using CatDrop.Models;

namespace CatDrop.WebApi.Controllers
{
  public class UserController : ApiController
  {

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public JsonResult<User> Get(string userName)
    {
      var user = _userService.GetUser(userName);
      return Json(user);
    }



    [HttpPost]
    public HttpResponseMessage Register(string userName, string password)
    {
      try
      {
        _userService.AddUser(userName, password);
      }
      catch (UserException e)
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
      }
      return Request.CreateResponse(HttpStatusCode.OK);
    }

  }
}
