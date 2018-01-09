using System.Threading.Tasks;
using OIfs;
using Orleans;
using Orleans.Runtime;

namespace OIps
{
    public class UserService:Grain, IUserService
    {
        public Task<bool> Exist(string mobileNumber)
        {
            this.GetLogger().Log(88888888,Severity.Error,"i served",null,null);
            return Task.FromResult(true);
        }
        
    }
}