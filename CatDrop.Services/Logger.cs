using CatDrop.Interfaces;
using CatDrop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ILogger = NLog.ILogger;

namespace CatDrop.Services
{
  public class Logger : Interfaces.ILogger
  {

    private static ILogger NLogger = LogManager.GetCurrentClassLogger();
    public void Activity(string message, string userName)
    {
      NLogger.Info(String.Format("{0} - {1}", userName, message));
    }

    public void Error(string errorMessage, Exception exception)
    {
      NLogger.Error(exception, errorMessage);
    }
  }
}
