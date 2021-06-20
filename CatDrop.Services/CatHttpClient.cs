using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CatDrop.Interfaces;

namespace CatDrop.Services
{
  public class CatHttpClient : ICatHttpClient
  {
    internal string WebURL { get; set; }
    private const string WebServiceConfigItem = "CatWebApiUrl";

    public CatHttpClient()
    {
      WebURL = ConfigurationManager.AppSettings[WebServiceConfigItem];
    }

    public async Task<Stream> GetStreamAsync( string endPoint)
    {
      using (var httpClient = new HttpClient())
      {
        httpClient.BaseAddress = new Uri(WebURL);
        return await httpClient.GetStreamAsync(endPoint);
      }
    }
  }
}
