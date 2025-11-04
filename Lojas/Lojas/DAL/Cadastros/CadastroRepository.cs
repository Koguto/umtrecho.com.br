using DAL.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class CadastroRepository : ICadastroRepository
    {

        private readonly LojasContext _context;

        public CadastroRepository(LojasContext context)
        {
            _context = context;
        }

        public async Task<Cadastro> GetByIdAsync(int id)
        {
            return await _context.Cadastros.FindAsync(id);
        }

        public async Task<IQueryable<Cadastro>> GetAllAsync()
        {
            return _context.Cadastros;
        }

        public async Task AddAsync(Cadastro cadastro)
        {
            await _context.Cadastros.AddAsync(cadastro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cadastro cadastro)
        {
            _context.Cadastros.Update(cadastro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cadastro = await GetByIdAsync(id);
            if (cadastro != null)
            {
                _context.Cadastros.Remove(cadastro);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteByEmailAsync(string email)
        {
            var cadastro = await GetByEmailAsync(email);
            if (cadastro != null)
            {
                _context.Cadastros.Remove(cadastro);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<Cadastro> GetByEmailAsync(string email)
        {
            return await _context.Cadastros.FirstOrDefaultAsync(c => c.Email == email);
        }




    }
}
