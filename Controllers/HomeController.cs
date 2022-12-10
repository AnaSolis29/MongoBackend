using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MongoBackend.DatabaseHelper;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.VisualBasic;
using MongoBackend.Models;

namespace AsignacionMonogo.Controllers
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
            MongoBackend.DatabaseHelper.Database db = new MongoBackend.DatabaseHelper.Database();

            ViewBag.UserList = db.getUsers();

            return View();
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SaveUser(string txtName, string txtEmail, string txtPhone, string txtAddress)
        {
            MongoBackend.DatabaseHelper.Database db = new MongoBackend.DatabaseHelper.Database();

            db.insertUser(new Users()
            {
                name = txtName,
                email = txtEmail,
                phone = Convert.ToInt32(txtPhone),
                address = txtAddress,
                dateIn = DateTime.Now
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(string id)
        {
            MongoBackend.DatabaseHelper.Database db = new MongoBackend.DatabaseHelper.Database();

            ViewBag.userListID = db.getUsersID(id);

            return View();
        }

        public IActionResult UpdateUser(string txtId, string txtName, string txtEmail, string txtPhone, string txtAddress)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://AnaSolis:Admin1234@cluster0.xf0cuge.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("Asignacion3");

            var filter = Builders<Users>.Filter.Eq("_id", ObjectId.Parse(txtId));
            var update = Builders<Users>.Update
                .Set("name", txtName)
                .Set("email", txtEmail)
                .Set("phone", txtPhone)
                .Set("address", txtAddress)
                .Set("dateIn", DateTime.Now);
            var users = db.GetCollection<Users>("Users").UpdateOne(filter, update);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(string id)
        {
            MongoBackend.DatabaseHelper.Database db = new MongoBackend.DatabaseHelper.Database();

            ViewBag.userListID = db.getUsersID(id);

            return View();
        }

        public IActionResult DeleteUser(String txtId)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://AnaSolis:Admin1234@cluster0.xf0cuge.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(txtId));

            users.DeleteOne(filter);

            return RedirectToAction("Index", "Home");
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
    }
}