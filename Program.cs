using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RentCar
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating Cars
            var car1 = new Car()
            {
                LicensePlate = "HWI464",
                Brand = BrandName.Chevrolet.ToString(),
                Model = "Corsa",
                Doors = 3,
                Gearbox = GearboxType.manual.ToString(),
                Color = "Verde"
            };
            var car2 = new Car()
            {
                LicensePlate = "JYZ487",
                Brand = BrandName.Peugeot.ToString(),
                Model = "Partner",
                Doors = 4,
                Gearbox = GearboxType.manual.ToString(),
                Color = "Blanco"
            };

            // Tests
            CarCRUD.Create(car1);
            var recoveredCar = CarCRUD.Get("hwi464");
            Console.WriteLine(recoveredCar.Brand);
            CarCRUD.Create(car2);
            CarCRUD.Delete("jYz   487     ");
            car1.Color = "Rosita";
            CarCRUD.Update(car1);
        } 
    }
}
