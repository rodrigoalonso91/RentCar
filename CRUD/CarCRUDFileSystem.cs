using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentCar
{
    public class CarCRUDInFileSystem : ICarCRUD
    {
        
        private string Path { get; set; }

        public CarCRUDInFileSystem(string path)
        {
            this.Path = path;
        }
        public Car Create(Car car)
        {
            var carsInJson = new Dictionary<string, Car>();

            if (File.Exists(Path))
            {
                var jsonFile = File.ReadAllText(Path);
                carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
            }
            if (carsInJson.ContainsKey(car.LicensePlate))
            {
                Console.WriteLine("Ese auto ya se encuentra en el archivo Cars.json");
                return car;
            }

            var opcions = new JsonSerializerOptions { WriteIndented = true };
            carsInJson[car.LicensePlate] = car;
            var json = JsonSerializer.Serialize(carsInJson, opcions);
            File.WriteAllText(Path, json);
            return car;
        }

        public Car Get(string LicensePlate)
        {

            if (File.Exists(Path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                var jsonFile = File.ReadAllText(Path);
                var carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);

                if (carsInJson.ContainsKey(id)) return carsInJson[id];
                else Console.WriteLine("No existe el auto con patente {0}", id);
            }
            Console.WriteLine("No existe el archivo Cars.json");
            return null;
        }

        public Car Update(Car car)
        {
            if (File.Exists(Path))
            {
                var jsonFile = File.ReadAllText(Path);
                var carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson[car.LicensePlate] = car;
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(Path, json);
                return car;
            }
            Console.WriteLine("No existe Cars.json");
            return null;
        }

        public void Delete(string LicensePlate)
        {
            if (File.Exists(Path))
            {
                var id = LicensePlate.ToUpper().Trim().Replace(" ", String.Empty);
                var jsonFile = File.ReadAllText(Path);
                var carsInJson = JsonSerializer.Deserialize<Dictionary<string, Car>>(jsonFile);
                carsInJson.Remove(id);
                var opcions = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(carsInJson, opcions);
                File.WriteAllText(Path, json);
            }
            else Console.WriteLine("No existe el archivo Cars.json");
        }
    }
}
