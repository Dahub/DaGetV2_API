using DaGetCore.Dal.Interface;

namespace DaGetCore.Service
{
    public abstract class ServiceBase
    {
        public IRepositoriesFactory Factory { get; set; }
        public string ConnexionString { get; set; }
    }
}
    