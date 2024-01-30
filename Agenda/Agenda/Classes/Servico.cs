using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Classes
{
    public class Servico
    {
        public static string senhaFiraBase = "XC7uUTbM7BfdssKCzS50q4u7WHOzSvszh48SKiyt";
        FirebaseClient Cliente = new FirebaseClient("https://agenda-79821-default-rtdb.firebaseio.com/",
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(senhaFiraBase) });

        public async Task<bool> RegistraContato (string nome, string telefone1, string telefone2, string email, string cep, 
                                                string logradouro, string numero, string bairro, string complemento, string uf, string cidade)
        {
            try
            {
                await Cliente.Child("Contato").PostAsync(new Contato()
                {
                    nome = nome,
                    tel1 = telefone1,
                    tel2 = telefone2,
                    email = email,
                    cep = cep,
                    logradouro = logradouro,
                    numero = numero,
                    bairro = bairro,
                    complemento = complemento,
                    uf = uf,
                    cidade = cidade
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Contato>> Lista()
        {
            var listagem = (await Cliente.Child("Contato").OnceAsync<Contato>()).Select(f => new Contato()
            {
                nome = f.Object.nome
            }).ToList();

            return listagem;
        }
    }
}
