using System.Threading.Tasks;
using Orleans;

namespace OIfs
{
    public interface IUserService:IGrainWithIntegerKey
    {
        Task<bool> Exist(string mobileNumber);
    }
}