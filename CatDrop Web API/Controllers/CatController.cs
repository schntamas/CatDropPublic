using CatDrop.Interfaces;
using CatDrop.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CatDrop_Web_API.Controllers
{
  [BasicAuthentication]
  public class CatController : ApiController
  {

    private readonly ICatService _catService;
    private readonly ILogger _logger;
    private readonly IUserService _userService;

    public CatController(ICatService webCatService, ILogger logger, IUserService userService)
    {
      _catService = webCatService;
      _logger = logger;
      _userService = userService;
    }

    // GET: normal
    ////////////////////////////////////////////////////////////////////////////////////////////////////    
    /// <summary> Gets a random cat image. </summary>    
    ///    
    /// <remarks> The cat image loaded from the exteranal Web API will be left unchanged. </remarks>    
    ///    
    /// <returns>  An image of a nice cat. </returns>    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // [Route("normal")]
    [HttpGet]
    public async Task<HttpResponseMessage> Normal()
    {
      var response = new HttpResponseMessage(HttpStatusCode.OK);
      byte[] catArray = await _catService.GetCatAsync();
      response.Content = new ByteArrayContent(catArray);
      response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

      _logger.Activity("Normal image request handled", HttpContext.Current.User.Identity.Name);
      _userService.ServiceUsed(HttpContext.Current.User.Identity.Name);

      return response;
    }

    // GET: flipped
    ////////////////////////////////////////////////////////////////////////////////////////////////////    
    /// <summary>   Gets a random cat image and rotates by 180 degree. </summary>    
    ///    
    /// <remarks> The cat image loaded from the exteranal Web API will be rotated by 180 degree. </remarks>    
    ///    
    /// <returns>   An image of a nice cat upside down. </returns>    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [HttpGet]
  //  [Route("flipped")]

    public async Task<HttpResponseMessage> Flipped()
    {
      var response = new HttpResponseMessage(HttpStatusCode.OK);
      byte[] catArray = await _catService.GetFlippedCatAsync();
      response.Content = new ByteArrayContent(catArray);
      response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

      _logger.Activity("Flipped image request handled", HttpContext.Current.User.Identity.Name);
      _userService.ServiceUsed(HttpContext.Current.User.Identity.Name);

      return response;
    }
  }
}