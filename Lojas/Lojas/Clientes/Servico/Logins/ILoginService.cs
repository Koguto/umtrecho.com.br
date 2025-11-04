using System.Threading.Tasks;
using ViewModels;

namespace Cliente
{
    public interface ILoginService
    {
        Task<bool> LogarAsync(LoginViewModel login);
     
    }
}