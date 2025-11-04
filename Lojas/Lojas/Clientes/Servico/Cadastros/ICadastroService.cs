using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Cliente
{
    public interface ICadastroService
    {
        Task CreateAsync(CadastroViewModel cadastro);
        Task DeleteAsync(int id);
        Task DeleteByEmailAsync(string email);
        Task<List<CadastroViewModel>> GetAllAsync();
        Task<CadastroViewModel> GetByIdAsync(int id);
        Task UpdateAsync(CadastroViewModel cadastro);

        Task<CadastroViewModel> GetAsync(string email);
    }
}