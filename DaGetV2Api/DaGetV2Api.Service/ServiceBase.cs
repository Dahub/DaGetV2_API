using DaGetV2Api.Dal.Interface;

namespace DaGetV2Api.Service
{
    public abstract class ServiceBase
    {
        public string ConnexionString { get; set; }
        public IRepositoriesFactory Factory { get; set; }
    }
}
