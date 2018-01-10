using System.Threading.Tasks;
using OIfs;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;

namespace OIps
{
    public class UserState
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    [StorageProvider(ProviderName = "Default")]
    public class UserService:Grain<UserState>, IUserService
    {
        public Task<bool> Exist(string mobileNumber)
        {
            State.Name = "Leon";
            State.Address = "Dalian";
            base.WriteStateAsync().Wait();
            this.GetLogger().Log(88888888,Severity.Error,"i served",null,null);
            return Task.FromResult(true);
        }
        
    }
}