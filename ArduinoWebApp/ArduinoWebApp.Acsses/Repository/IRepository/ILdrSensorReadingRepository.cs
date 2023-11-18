using ArduinoWebApp.Model;

namespace ArduinoWebApp.Acsses.Repository.IRepository
{
    public interface ILdrSensorReadingRepository : IRepository<TbLdrSensorReading>
    {
        void Update(TbLdrSensorReading obj);
    }
}
