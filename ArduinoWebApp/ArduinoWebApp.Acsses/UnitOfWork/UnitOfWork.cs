using ArduinoWebApp.Acsses.Data;
using ArduinoWebApp.Acsses.Repository;
using ArduinoWebApp.Acsses.Repository.IRepository;

namespace ArduinoWebApp.Acsses.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region dependency injection
        private ArduinoAppDbContext _db;
        public ILdrSensorReadingRepository TbLdrSensorReading { get; private set; }
       
        public UnitOfWork(ArduinoAppDbContext db)
        {
            _db = db;
            TbLdrSensorReading = new LdrSensorReadingRepository(_db);
        }
        #endregion

        #region Save
        public void Save()
        {
            try
            {
                int savedChanges = _db.SaveChanges();
                if (savedChanges > 0)
                {
                    Console.WriteLine("Data saved successfully. Number of affected rows: " + savedChanges);

                }
                else
                {
                    Console.WriteLine("No data changes to save.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while saving data to the database: " + ex.Message);
                // يمكنك التعامل مع الاستثناء هنا، مثلاً طباعة رسالة الخطأ أو تسجيلها
            }
        }
        #endregion
    }
}
