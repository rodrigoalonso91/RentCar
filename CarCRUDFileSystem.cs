using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentCar
{
    class CarCRUDInFileSystem : ICarCRUD
    {
        //NOTE: Elegir la ruta donde desea guardar el archivo Cars.json
        private static string _path = @"D:\Downloads\Cars.json";

        public Car Create(Car car)
        {
            Dictionary<string, Car> carsInJson = new Dictionary<string, Car>();

            if (File.Exists(_path))
            {
                string jsonFile = File.ReadAllText(_path);
                carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
            }
            if (carsInJson.ContainsKey(car.LicensePlate))
            {
                Console.WriteLine("Ese auto ya se encuentra en el archivo Cars.json");
                return car;
            }

            var opcions = new JsonSerializerOptions { WriteIndented = true };
            carsInJson[car.LicensePlate] = car;
            string json = JsonSerializer.Serialize(carsInJson, opcions);
            File.WriteAllText(_path, json);
            return car;
        }

        public Car Get(string LicensePlate)
        {
            Dictionary<string, Car> carsInJson = new Dictionary<string, Car>();

            if (File.Exists(_path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                string jsonFile = File.ReadAllText(_path);
                carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);

                if (carsInJson.ContainsKey(id))
                    return carsInJson[id];
                else Console.WriteLine("No existe el auto con patente {0}", id);
            }
            Console.WriteLine("No existe el archivo Cars.json");
            return null;
        }

        public Car Update(Car car)
        {
            Dictionary<string, Car> carsInJson = new Dictionary<string, Car>();
            if (File.Exists(_path))
            {
                string jsonFile = File.ReadAllText(_path);
                carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson[car.LicensePlate] = car;
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(_path, json);
                return car;
            }
            Console.WriteLine("No existe Cars.json");
            return null;
        }

        public void Delete(string LicensePlate)
        {
            Dictionary<string, Car> carsInJson = new Dictionary<string, Car>();

            if (File.Exists(_path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                string jsonFile = File.ReadAllText(_path);
                carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson.Remove(id);
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(_path, json);
            }
            else Console.WriteLine("No existe el archivo Cars.json");
        }
    }
}
