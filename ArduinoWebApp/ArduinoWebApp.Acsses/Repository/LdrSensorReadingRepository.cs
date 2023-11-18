using ArduinoWebApp.Acsses.Data;
using ArduinoWebApp.Acsses.Repository.IRepository;
using ArduinoWebApp.Model;

namespace ArduinoWebApp.Acsses.Repository
{
    public class LdrSensorReadingRepository : Repository<TbLdrSensorReading>, ILdrSensorReadingRepository
    {
        #region dependency injection
        private ArduinoAppDbContext _db;
        public LdrSensorReadingRepository(ArduinoAppDbContext db) : base(db)
        {
            _db = db;
        }
        #endregion

        #region Update
        public void Update(TbLdrSensorReading obj)
        {
            _db.TbLdrSensorReadings.Update(obj);
        }
        #endregion
    }
}
}
