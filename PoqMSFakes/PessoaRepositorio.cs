using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoqMSFakes.Data;
using System.Linq.Expressions;
using Microsoft.SharePoint.Client;

namespace PoqMSFakes
{
    public class PessoaRepositorio
    {
        // adicionar pessoas
        // buscar pessoas

        private readonly List<Pessoa> _pessoas;
        public PessoaRepositorio()
        {
            _pessoas = new List<Pessoa>();
        }

        public void AdicionarPessoa(Pessoa p, ClientContext ctx)
        {

            var listaPessoa = ctx.Web.Lists.GetByTitle("Pessoas");
            ctx.Load(listaPessoa);

            var lci = new ListItemCreationInformation();
            var item = listaPessoa.AddItem(lci);

            ctx.Load(item);

            item["Title"] = p.Nome;
            item["Email"] = p.Email;

            item.Update();

            ctx.ExecuteQuery();


            _pessoas.Add(p);
        }

        public Pessoa BuscarPessoa(Func<Pessoa, bool> predicate)
        {
            return _pessoas.FirstOrDefault(predicate);
        }

        
    }
}
