using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibrePost.Models;
using System.IO;

namespace LibrePost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("this is basic...");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public void CreateAccount(String userName){
            if ((userName != null) && (userName != String.Empty)){
                userName = userName.Trim();
                userName = userName.Replace(" ", "");
                var cwd = Directory.GetCurrentDirectory();
                _logger.Log(LogLevel.Information,cwd);
                var userDir = Path.Combine(cwd,"wwwroot",userName);
                Directory.CreateDirectory(userDir);
                _logger.LogInformation(userDir);
                _logger.LogInformation(Directory.Exists(userDir).ToString());
                System.IO.File.AppendAllText(Path.Combine(userDir,"test.htm"),"<div>big test</div>");
                _logger.LogInformation("Wrote file.");
                System.IO.File.Copy(Path.Combine(cwd,"wwwroot","templates","index.htm"),
                    Path.Combine(userDir,"index.htm"));
                _logger.LogInformation("Created index.htm from template.");

            }
            else{
                var cwd = Directory.GetCurrentDirectory();
                _logger.Log(LogLevel.Information,cwd);
                _logger.Log(LogLevel.Information, "userName is empty");

            }
            Response.Redirect($"../{userName}/index.htm");
            //return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
