using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace ApiClient
{
    public class Passenger
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public override string ToString()
        {
            return $"---------------------------------- \n\nID: {id}\nFirst Name: {firstname}\nLast Name: {lastname}\n";
        }
    }

    public class Carpool
    {
        public int carpoolId { get; set; }
        public string carpoolName { get; set; }
        public string start { get; set; }
        public string destination { get; set; }
        public string time { get; set; }
        public int seatcount { get; set; }

        public int existenceOfDriver { get; set; }
        public List<Passenger> passengerInfoDto { get; set; }

        public override string ToString()
        {
            return $"---------------------------------- \n\nID: {carpoolId}\nCarpool Name: {carpoolName}\nCarpool Origin: {start}\nCarpool Destination: {destination}\nCarpool Name: {time}\n Amount of Seats: {seatcount}.";
        }
    }

    class Program
    {
        static string baseUri = "https://localhost:7030/";
        static void Main(string[] args)
        {
            MainMenu();
            var user = GetUserByIdAsync(2).Result;

        }
        public static void MainMenu()
        {
            Console.WriteLine("Choose your options: ");
            Console.WriteLine("- - - - - PASSANGER OPTIONS - - - - - ");
            Console.WriteLine("[1] - Get all passengers");
            Console.WriteLine("[2] - Get a user by his ID");
            Console.WriteLine("[3] - Delete all users");
            Console.WriteLine("[4] - Delete a user by his ID");
            Console.WriteLine("[5] - Create new user");
            Console.WriteLine(" ");
            Console.WriteLine("- - - - - CARPOOL OPTIONS - - - - -");
            Console.WriteLine("[4] - Get all carpools");
            Console.WriteLine("[5] - Get a carpool by its ID");

            Console.Write("> "); int UserMenuChoice = Convert.ToInt32(Console.ReadLine());

            if (UserMenuChoice == 1)
            {
                PrintGetAllUsers();
            }
            else if (UserMenuChoice == 2)
            {
                PrintGetUserById();
            }
            else if (UserMenuChoice == 3)
            {
                DeleteAllPassanger();
                Console.WriteLine("All users have been deleted Successfully");
            }

            else if (UserMenuChoice == 4)
            {
                PrintDeleteUserById();
            }
            else if (UserMenuChoice == 5)
            {
                PrintDeleteUserById();
            }
            else if (UserMenuChoice == 6)
            {
                PrintGetCarpoolById();
            }
            else
            {
                Console.WriteLine($"{UserMenuChoice} is not a valid option.");
                Console.Clear();
                MainMenu();
            }
        }


        #region PRINT
        public static void PrintGetAllUsers()
        {
            Console.Clear();
            var GetAllUserss = GetAllUsers().Result;
            foreach (var item in GetAllUserss)
            {
                Console.WriteLine(
                item.id.ToString(),
                item.firstname,
                item.lastname,
                item.password
                );
            }
            Console.ReadLine();
        }

        public static void PrintGetUserById()
        {
            Console.WriteLine("Please enter the ID you want to get: ");
            int IDofChoice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Passenger GetSpecificUserbyId = GetUserByIdAsync(IDofChoice).Result;

            Console.WriteLine
                (
                    GetSpecificUserbyId.ToString(),
                    GetSpecificUserbyId.firstname,
                    GetSpecificUserbyId.lastname,
                    GetSpecificUserbyId.password
                );

            Console.ReadLine();
        }


        public static void PrintDeleteUserById()
        {
            Console.Clear();
            Console.WriteLine("Pleas enter the id of the user youd like to delete: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            DeletePassengerById(Id);
            Console.WriteLine($"User {Id} deleted successfully");
            Console.ReadLine();
        }

        public static void PrintGetCarpoolById()
        {
            Console.Clear();
            Console.WriteLine("Please enter the Id of the Carpool you would like to get: ");
            Console.Write("> "); int CarpoolID = Convert.ToInt32(Console.ReadLine());
            var GetCarpoolById = GetCarpoolByIdAsync(2).Result;

            //Get right "Existence of Driver: "
            string CarpoolDriverExists;
            if (GetCarpoolById.existenceOfDriver == 0)
            {
                CarpoolDriverExists = "false";
            }
            else
            {
                CarpoolDriverExists = "true";
            }

            Console.WriteLine
                (
                    GetCarpoolById.ToString(),
                    GetCarpoolById.carpoolName,
                    GetCarpoolById.start,
                    GetCarpoolById.destination,
                    GetCarpoolById.time,
                    GetCarpoolById.seatcount.ToString(),
                    CarpoolDriverExists,
                    GetCarpoolById.passengerInfoDto
                 );
            Console.ReadLine();
        }
        #endregion PRINT


        #region GETS
        public static async Task<List<Passenger>> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(baseUri + "Passenger/TecAllianceCarpoolApi/GetAllPassengers");
            var AllUsersJsonString = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Passenger>>(AllUsersJsonString);
        }

        public static async Task<Passenger> GetUserByIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(baseUri + "Passenger/TecAllianceCarpoolApi/GetPassengerById/" + id.ToString());
            var UserJsonString = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Passenger>(UserJsonString);
        }

        public static async Task<Carpool> GetCarpoolByIdAsync(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(baseUri + "Carpool/" + id.ToString());
            var UserJsonString = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Carpool>(UserJsonString);
        }

        #endregion GETS

        #region DELETES

        public static async Task DeleteAllPassanger()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(baseUri + "Passenger/TecallianceCarpoolApi/DeleteAllDrivers/");
            Console.WriteLine("test");
            Console.ReadKey();
        }

        public static async Task DeletePassengerById(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(baseUri + "Passenger/TecallianceCarpoolApi/DeletPassengerById/" + Id.ToString());
        }

        #endregion

        #region POST

        public static async Task<Passenger> CreateNewUser()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(baseUri + "Passenger/TecAllianceCarpoolApi/GetAllPassengers");
            var AllUsersJsonString = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Passenger>(AllUsersJsonString);
        }

        #endregion POST
    }
}