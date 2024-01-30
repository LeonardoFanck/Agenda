using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Classes
{
    public class ServicoUsers
    {
        public static string senhaFiraBase = "XC7uUTbM7BfdssKCzS50q4u7WHOzSvszh48SKiyt";
        FirebaseClient Cliente = new FirebaseClient("https://agenda-79821-default-rtdb.firebaseio.com/",
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(senhaFiraBase) });
    
        public async Task<bool> registrarUsuario(string usuario, string senha)
        {
            try
            {
                await Cliente.Child("Usuario").PostAsync(new Usuario()
                {
                    usuario = usuario,
                    senha = senha
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> verificarLogin(string usuario, string senha)
        {
            var consultar = (await Cliente.Child("Usuario").OnceAsync<Usuario>())
                .Where(u => u.Object.usuario == usuario)
                .Where(u => u.Object.senha == senha)
                .FirstOrDefault();

            return consultar != null;
        }

        public async Task<bool> verificarDuplicado(string email)
        {
            var consulta = (await Cliente.Child("Usuario").OnceAsync<Usuario>())
                .Where(u => u.Object.usuario == email)
                .FirstOrDefault();

            return consulta != null;
        }
    }
}
