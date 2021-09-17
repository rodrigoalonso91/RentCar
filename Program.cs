using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RentCar.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RentCar
{

    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            //var car1 = new Car()
            //{
            //    LicensePlate = "HWI464",
            //    Brand = BrandName.Chevrolet.ToString(),
            //    Model = "Corsa",
            //    Doors = 3,
            //    Gearbox = GearboxType.Manual.ToString(),
            //    Color = "Verde"
            //};
            //var car2 = new Car()
            //{
            //    LicensePlate = "JYZ487",
            //    Brand = BrandName.Peugeot.ToString(),
            //    Model = "Partner",
            //    Doors = 4,
            //    Gearbox = GearboxType.Manual.ToString(),
            //    Color = "Blanco"
            //};

            //// Tests
            //var carCRUD = CreateCrud((int)DataStorage.Json);

            //carCRUD.Create(car1);
            //var recoveredCar = carCRUD.Get("hwi464");
            //Console.WriteLine(recoveredCar.Brand);
            //carCRUD.Create(car2);
            //carCRUD.Delete("jYz   487     ");
            //car1.Color = "Rosita";
            //carCRUD.Update(car1);

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, configuration) =>
               {
                   configuration.Sources.Clear();

                   IHostEnvironment env = hostingContext.HostingEnvironment;

                   configuration
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                   IConfigurationRoot configurationRoot = configuration.Build();

                   var options = new CRUD.SavePath();
                   configurationRoot.GetSection(nameof(CRUD.SavePath))
                                    .Bind(options);

                   Console.WriteLine($"El path es: {options.Path}");
               });

        public static CarCRUDInFileSystem CreateCrud(int id)
        {
            // Idea para cuando se implemente otro sistema de almacenamiento de datos.
            if (id == 0)
            {
                Console.WriteLine("Ingrese el directorio donde desea guardar el archivo: \n");
                var path = Console.ReadLine() + @"\Cars.json";

                return new CarCRUDInFileSystem(path);
            }
            else
            {
                // Some code
                return null;
            }
        }
    }
}
