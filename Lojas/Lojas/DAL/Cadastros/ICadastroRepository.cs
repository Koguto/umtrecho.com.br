using DAL.Modelos;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICadastroRepository
    {
        Task AddAsync(Cadastro cadastro);
        Task DeleteAsync(int id);
        Task DeleteByEmailAsync(string email);
        Task<IQueryable<Cadastro>> GetAllAsync();
        Task<Cadastro> GetByIdAsync(int id);
        Task UpdateAsync(Cadastro cadastro);

        Task<Cadastro> GetByEmailAsync(string email);
    }
}