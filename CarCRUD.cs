using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentCar
{
    class CarCRUD
    {

        private static Dictionary<string, Car> carsInJson = new Dictionary<string, Car>();
        private static string _path = @"D:\Downloads\Cars.json";

        public static Car Create(Car car)
        {
            if (File.Exists(_path))
            {
                string jsonFile = File.ReadAllText(_path);
                var jsonContent = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson = jsonContent;
            }
            if (carsInJson.ContainsKey(car.LicensePlate))
            {
                Console.WriteLine("Ese auto ya se encuentra en la base de datos");
                return car;
            }

            var opcions = new JsonSerializerOptions { WriteIndented = true };
            carsInJson[car.LicensePlate] = car;
            string json = JsonSerializer.Serialize(carsInJson, opcions);
            File.WriteAllText(_path, json);
            return car;
        }

        public static Car Get(string LicensePlate)
        {

            if (File.Exists(_path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                string jsonFile = File.ReadAllText(_path);
                var jsonContent = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson = jsonContent;

                if (carsInJson.ContainsKey(id))
                    return carsInJson[id];
                else Console.WriteLine("No existe el auto con patente {0}", id);
            }
            Console.WriteLine("No existe la base de datos");
            return null;
        }

        public static Car Update(Car car)
        {
            if (File.Exists(_path))
            {
                string jsonFile = File.ReadAllText(_path);
                var jsonContent = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson = jsonContent;
                carsInJson[car.LicensePlate] = car;
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(_path, json);
                return car;
            }
            Console.WriteLine("No existe la base de datos");
            return null;
        }

        public static void Delete(string LicensePlate)
        {
            if (File.Exists(_path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                string jsonFile = File.ReadAllText(_path);
                var jsonContent = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson = jsonContent;
                carsInJson.Remove(id);
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(_path, json);

            }
            else Console.WriteLine("No existe la base de datos"); 
        }
    }
}
