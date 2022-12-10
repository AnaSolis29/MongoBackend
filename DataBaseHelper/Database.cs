using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoBackend.Models;
using System.Text.Json.Nodes;


namespace MongoBackend.DatabaseHelper
{
    public class Database
    {
        public List<Users> getUsers()
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://AnaSolis:Admin1234@cluster0.xf0cuge.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("Asignacion3");

            var users = db.GetCollection<BsonDocument>("Users");
            List<Users> userList = new List<Users>();

            foreach (BsonDocument bsonUser in userArray)
            {
                Users user = BsonSerializer.Deserialize<Users>(bsonUser);
                userList.Add(user);
            }

            return userList;
        }

        public List<Users> getUsersID(string id)
        {

            MongoClient mongoClient = new MongoClient("mongodb+srv://AnaSolis:Admin1234@cluster0.xf0cuge.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("Asignacion3");

            var usersID = db.GetCollection<BsonDocument>("Users");

            List<BsonDocument> userArray = usersID.Find(new BsonDocument { { "_id", new ObjectId(id) } }).ToList();
            List<Users> userListID = new List<Users>();

            foreach (BsonDocument bsonUser in userArray)
            {
                Users user = BsonSerializer.Deserialize<Users>(bsonUser);
                userListID.Add(user);
            }

            return userListID;
        }

        public void insertUser(Users user)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://AnaSolis:Admin1234@cluster0.xf0cuge.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("Asignacion3");

            var users = db.GetCollection<BsonDocument>("Users");

            var doc = new BsonDocument
            {
                { "name", user.name },
                { "email", user.email },
                { "phone", user.phone },
                { "address", user.address},
                { "dateIn", user.dateIn }
            };

            users.InsertOne(doc);
        }
    }
}
