using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoqMSFakes.Data;

namespace PoqMSFakes
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new PessoaRepositorio();

            var pessoa1 = new Pessoa { Email = "lmarques@morecodding.com", Nome = "Lucas Marques" };
            var pessoa2 = new Pessoa { Email = "thiago@morecodding.com", Nome = "Thiago Roque" };

            //repo.AdicionarPessoa(pessoa1);
           // repo.AdicionarPessoa(pessoa2);

            var pessoa3 = repo.BuscarPessoa((x => x.Email == "thiago@morecodding.com"));
            if (pessoa3 != null)
            {
                Console.WriteLine(string.Format("Nome: {0} - Email: {1}", pessoa3.Nome, pessoa3.Email));
            }

            Console.ReadLine();
        }
    }
}
