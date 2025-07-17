using Core.Interface;
using Demo.Core.Interface.Services;
using Demo.Models;
using Demo.UnitOfWork.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCore.Models;

namespace WebApplicationCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;
        private ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ICustomerService customerService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _customerService = customerService;
        }       
        //Add Comments
        public IActionResult Index()
        {
            var token = GenerateJwtToken("erp");
            return View("Token", model: token);
        }

        public IActionResult Token()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
            }
            return View();
        }




        private string GenerateJwtToken(string username)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my test secret key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult AuthToken()
        {
            // Validate user
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, "Admin"),
            new Claim(ClaimTypes.Role, "Admin") // Or other roles
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Store in cookie or local storage depending on your preference
            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Home");

        }

        public IActionResult _index()
        {
            try
            {

                //PeramModel peram = new PeramModel();
                //IndexModel index = new IndexModel();
                string userName = User.Identity.Name;
                //ApplicationUser user = _applicationDb.Users.FirstOrDefault(model => model.UserName == userName);

                var search = Request.Form["search[value]"].FirstOrDefault();
                string indexSearch = Request.Form["indexSearch"].ToString();
                string BranchId;

                if (string.IsNullOrEmpty(indexSearch))
                {
                    BranchId = Request.Form["branchid"].ToString();
                }
                else
                {
                    BranchId = indexSearch;
                }
                if (BranchId == "-1")
                {
                    //peram.UserLogInId = User.GetUserId();
                    BranchId = "";
                }

                string code = Request.Form["code"].ToString();
                string ponumber = Request.Form["ponumber"].ToString();
                string push = Request.Form["ispush"].ToString();
                string post = Request.Form["ispost"].ToString();
                if (post == "Select")
                {
                    post = "";

                }
                if (push == "Select")
                {
                    push = "";
                }

                string draw = Request.Form["draw"].ToString();
                var startRec = Request.Form["start"].FirstOrDefault();
                var pageSize = Request.Form["length"].FirstOrDefault();
                var orderName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][Name]"].FirstOrDefault();
                var orderDir = Request.Form["order[0][dir]"].FirstOrDefault();
                //index.SearchValue = Request.Form["search[value]"].FirstOrDefault();
                string orderResult = (orderName == "" || orderName == null) ? "ReceiptNumber" : orderName;

                //index.OrderName = orderResult;
                //index.orderDir = orderDir;
                //index.startRec = Convert.ToInt32(startRec);
                //index.pageSize = Convert.ToInt32(pageSize);
                //index.CurrentBranchid = BranchId;
                //index.createdBy = userName;


                string[] conditionalFields = new[]
                                        {

                            "Code like",
                            "PONumber like",
                            "IsPost like",
                            "IsPush like",
                            "Code like",
                            "PONumber like",
                            "IsPost like",
                            "IsPush like"

                        };

                if (push.ToLower() != "all")
                {
                    conditionalFields.Append("IsPush ");
                }
                if (post.ToLower() != "all")
                {
                    conditionalFields.Append("IsPost ");
                }
                string?[] conditionalValue = new[] { search, search, search, search, code, ponumber, post, push };


                //ResultModel<List<ICReceipts>> indexData =_receiptMaserService.GetIndexData(index,conditionalFields, conditionalValue, peram);
                List<Customer> indexData = GetList();

                //ResultModel<int> indexDataCount = _receiptMaserService.GetIndexDataCount(index, conditionalFields, conditionalValue, peram);
                int indexDataCount = 15;
                //int result = _receiptMaserService.GetCount(TableName.ICReceipts, "Id", null, null);
                int result = 20;


                //return Ok(new { data = indexData.Data, draw, recordsTotal = result, recordsFiltered = indexDataCount.Data });
                return Ok(new { data = indexData, draw, recordsTotal = result, recordsFiltered = indexDataCount });


            }
            catch (Exception e)
            {
                //e.LogAsync(ControllerContext.HttpContext);
                //return Ok(new { Data = new List<ICReceipts>(), draw = "", recordsTotal = 0, recordsFiltered = 0 });
                return Ok(new { });
            }


        }

        public List<Customer> GetList()
        {
            List<Customer> customers = new List<Customer>()
{
    new Customer { Name = "Admin", Code = "CS-1234", Address = "Gazipur", Country = "Bangladesh" },
    new Customer { Name = "John Doe", Code = "CS-1235", Address = "Dhaka", Country = "Bangladesh" },
    new Customer { Name = "Jane Smith", Code = "CS-1236", Address = "Chittagong", Country = "Bangladesh" },
    new Customer { Name = "Ali Khan", Code = "CS-1237", Address = "Sylhet", Country = "Bangladesh" },
    new Customer { Name = "Maria Gomez", Code = "CS-1238", Address = "Khulna", Country = "Bangladesh" },
    new Customer { Name = "Robert Lang", Code = "CS-1239", Address = "Barisal", Country = "Bangladesh" },
    new Customer { Name = "Laila Rahman", Code = "CS-1240", Address = "Rajshahi", Country = "Bangladesh" },
    new Customer { Name = "Tom Hossain", Code = "CS-1241", Address = "Comilla", Country = "Bangladesh" },
    new Customer { Name = "Sana Ahmed", Code = "CS-1242", Address = "Narayanganj", Country = "Bangladesh" },
    new Customer { Name = "Zahidul Islam", Code = "CS-1243", Address = "Mymensingh", Country = "Bangladesh" },
    new Customer { Name = "Emily Chowdhury", Code = "CS-1244", Address = "Bogura", Country = "Bangladesh" },
    new Customer { Name = "Tanvir Alam", Code = "CS-1245", Address = "Jessore", Country = "Bangladesh" },
    new Customer { Name = "Nusrat Jahan", Code = "CS-1246", Address = "Cox's Bazar", Country = "Bangladesh" },
    new Customer { Name = "Hasibul Hasan", Code = "CS-1247", Address = "Rangpur", Country = "Bangladesh" },
    new Customer { Name = "Farzana Noor", Code = "CS-1248", Address = "Pabna", Country = "Bangladesh" }
};


            return customers;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
