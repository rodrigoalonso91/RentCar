namespace RentCar
{
    interface ICarCRUD
    {
        Car Create(Car car);
        Car Get(string LicensePlate);
        Car Update(Car car);
        void Delete(string LicensePlate);
    }
}