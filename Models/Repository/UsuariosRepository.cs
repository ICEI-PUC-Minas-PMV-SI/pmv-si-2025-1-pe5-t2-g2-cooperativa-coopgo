using Microsoft.EntityFrameworkCore;
using CadastroUsuarios.Models;
using COOPGO.Models;

namespace CadastroUsuarios.Models.Repository
{
    public class UsuariosRepository
    {
        private readonly AppDbContext _context;

        public UsuariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Salvar(Usuarios usuario)
        {
            try
            {
                if (usuario.id == 0)
                {
                    var usuarioExistente = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.nome == usuario.nome);

                    if (usuarioExistente != null)
                    {
                        throw new Exception("Já existe um usuário com este nome.");
                    }

                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    var usuarioExistente = await _context.Usuarios
                        .FirstOrDefaultAsync(u => u.nome == usuario.nome && u.id != usuario.id);

                    if (usuarioExistente != null)
                    {
                        throw new Exception("Já existe um usuário com este nome.");
                    }

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar usuário", ex);
            }
        }
        public async Task<List<Usuarios>> Listar()
        {
            try
            {
                return await _context.Usuarios
                    .OrderByDescending(u => u.nome)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar usuários", ex);
            }
        }

        public async Task<bool> Deletar(string nome)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.nome == nome);

                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar usuário {nome}", ex);
            }
        }

        public async Task DeletarPorId(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado para exclusão.");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar usuário com ID {id}", ex);
            }
        }

        public async Task<Usuarios> GetUsuario(string nome)
        {
            try
            {
                return await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.nome == nome);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar usuário {nome}", ex);
            }
        }

        public async Task Alterar(Usuarios usuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuario.id);

                if (usuarioExistente == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                usuarioExistente.nome = usuario.nome;
                usuarioExistente.senha = usuario.senha;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar usuário", ex);
            }
        }
    }
}