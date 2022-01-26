using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeeloApp.Models;
using WeeloBackEnd.Context;
using WeeloBackEnd.Models;
using WeeloBackEnd.Repositories;

namespace WeeloApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IConfiguration _configuration;
    private readonly PropertyRepo _propertyRepo;
    private readonly OwnerRepo _ownerRepo;
    private readonly PropertyImageRepo _propertyImageRepo;
    private IWebHostEnvironment _hostingEnvironment;

    public HomeController(ILogger<HomeController> logger, IConfiguration config, IWebHostEnvironment environment)
    {
        _hostingEnvironment = environment;
        _logger = logger;
        _configuration = config;
        _propertyRepo = new PropertyRepo(_configuration.GetValue<string>("ConnectionStrings:Sql"));
        _ownerRepo = new OwnerRepo(_configuration.GetValue<string>("ConnectionStrings:Sql"));
        _propertyImageRepo = new PropertyImageRepo(_configuration.GetValue<string>("ConnectionStrings:Sql"));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<string> GetProperties()
    {
        try
        {
            var properties = await _propertyRepo.GetProperties();
            if (properties.Count == 0)
                return CreateResponse("Properties Not Found", "", false).ToString();
            else
                return CreateResponse("Properties Found", JsonConvert.SerializeObject(properties), true).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Properties Not Found", ex.Message, false).ToString();
        }
    }

    [HttpPost]
    public async Task<string> SetProperties(Property property)
    {
        try
        {
            var response = await _propertyRepo.UpdateProperties(property);
            if (response)
                return CreateResponse("Properties save", "", false).ToString();
            else
                return CreateResponse("Properties don't save", "", false).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Properties save", ex.Message, false).ToString();
        }
    }

    [HttpDelete]
    public async Task<string> DeleteProperties(int idProperty)
    {
        try
        {
            var response = await _propertyRepo.DeleteProperty(idProperty);
            if (response)
                return CreateResponse("Properties deleted", "", false).ToString();
            else
                return CreateResponse("Properties don't deleted", "", false).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Properties deleted", ex.Message, false).ToString();
        }
    }

    [HttpGet]
    public async Task<string> GetOwners()
    {
        try
        {
            var owners = await _ownerRepo.GetOwners();
            if (owners.Count == 0)
                return CreateResponse("Owners Not Found", "", false).ToString();
            else
                return CreateResponse("Owners Found", JsonConvert.SerializeObject(owners), true).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Owners Not Found", ex.Message, false).ToString();
        }
    }

    [HttpPost]
    public async Task<string> GetImages(int idProperty)
    {
        try
        {
            var images = await _propertyImageRepo.GetImages(idProperty);
            if (images.Count == 0)
                return CreateResponse("Images Not Found", "", false).ToString();
            else
                return CreateResponse("Images Found", JsonConvert.SerializeObject(images), true).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Images Not Found", ex.Message, false).ToString();
        }
        
    }

    [HttpPost]
    public async Task<string> SetPhoto()
    {
        try
        {
            string file = Request.Form.Files[0].FileName;
            var fileContent = Request.Form.Files[0];
            if (fileContent != null && fileContent.Length > 0 && fileContent.ContentType == "image/jpeg" || fileContent.ContentType == "image/png")
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                if (fileContent.Length > 0)
                {
                    string filePath = Path.Combine(uploads, fileContent.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileContent.CopyToAsync(fileStream);
                        return CreateResponse("Images Found", "", false).ToString();
                    }
                }
                else
                {
                    return CreateResponse("Images Not Found", "", false).ToString();
                }
                
            }
            else
            {
                return CreateResponse("Images Not Found", "", false).ToString();
            }
            
        }
        catch (Exception ex)
        {
            return CreateResponse("Images Not Found", ex.Message, false).ToString();
        }
    }

    [HttpPost]
    public async Task<string> SetDbPhoto(string file, int idProperty)
    {
        try
        {
            var image = await _propertyImageRepo.SetPhoto(idProperty, file);
            if (!image)
                return CreateResponse("Image Not Set", "", false).ToString();
            else
                return CreateResponse("Image Set", "", false).ToString();
        }
        catch (Exception ex)
        {
            return CreateResponse("Images Not Found", ex.Message, false).ToString();
        }
    }


    private static JObject CreateResponse(string message, string value, bool type)
    {
        if (type)
            return JObject.Parse(JsonConvert.SerializeObject(new ResponseModel { Message = message, Value = (value != null ? JArray.Parse(value) : null) }));
        else
            return JObject.Parse(JsonConvert.SerializeObject(new ResponseSingleModel { Message = message, Value = value }));
    }
}
