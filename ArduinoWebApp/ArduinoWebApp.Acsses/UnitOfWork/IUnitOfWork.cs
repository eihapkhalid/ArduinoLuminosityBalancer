using ArduinoWebApp.Acsses.Repository.IRepository;

namespace ArduinoWebApp.Acsses.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
        public ILdrSensorReadingRepository TbLdrSensorReading { get; }
    }
}
