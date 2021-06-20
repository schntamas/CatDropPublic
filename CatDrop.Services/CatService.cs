using CatDrop.Interfaces;
using CatDrop.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CatDrop.Services
{
  public class CatService : ICatService
  {

    private const string RandomCatEndpoint = "cat";

    private readonly ICatHttpClient _catHttpClient;
    private readonly ILogger _logger;

    internal string WebURL { get; set; }

    public CatService(ICatHttpClient catHttpClient, ILogger logger)
    {
       _catHttpClient = catHttpClient;
      _logger = logger;
    }

    public async Task<byte[]> GetFlippedCatAsync()
    {
      return await GetChangedCatAsync(RotateFlipType.RotateNoneFlipY);
    }

    public async Task<byte[]> GetCatAsync()
    {
      return await GetChangedCatAsync();
    }

    private  async Task<byte[]> GetChangedCatAsync(RotateFlipType? rotateFlipType = null)
    {
      var CatStream = await GetCatStreamAsync();
      try
      {
        Image image = Bitmap.FromStream(CatStream);

        if (rotateFlipType != null)
        {
          image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }
        return GetImageBytes(image);
      }
      catch(Exception exception)
      {
        _logger.Error("Image processing failed", exception);
        throw new WebCatException("Image processing failed", exception);
      }
    }

    private byte[] GetImageBytes(Image i)
    {
      using (MemoryStream mStream = new MemoryStream())
      {
        i.Save(mStream, ImageFormat.Jpeg);
        return mStream.ToArray();
      }
    }

    private async Task<Stream> GetCatStreamAsync()
    {
      try
      {
        return await _catHttpClient.GetStreamAsync(RandomCatEndpoint);
      }
      catch (HttpRequestException exception)
      {
        _logger.Error("Loading random cat image failed", exception);
        throw new WebCatException("Loading random cat image failed", exception);
      }
    }
  }
}
