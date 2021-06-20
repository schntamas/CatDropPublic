using System;
using System.Net.Http;
using CatDrop.Interfaces;
using CatDrop.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CatDrop.Services.Test
{
  [TestClass]
  public class WebCatServiceTest
  {
    [TestMethod]
    public void HttpClientExceptionTest()
    {
      var catHttpClientICatMock = new Mock<ICatHttpClient>();
      var loggerMock = new Mock<ILogger>();

      catHttpClientICatMock.Setup(x => x.GetStreamAsync(It.IsAny<string>())).Throws(new HttpRequestException());
      var webCatService = new CatService(catHttpClientICatMock.Object, loggerMock.Object);

      try
      {
        byte[] b = webCatService.GetFlippedCatAsync().Result;
        Assert.Fail("WebCatException excpected");
      }
      catch (Exception e)
      {
        Assert.IsInstanceOfType(e.InnerException, typeof(WebCatException));
      }
    }
  }
}
