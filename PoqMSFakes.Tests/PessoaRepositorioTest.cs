using Moq;
using PoqMSFakes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.SharePoint.Client;
using Xunit;
using Microsoft.SharePoint.Client.Fakes;

namespace PoqMSFakes.Tests
{
    public class PessoaRepositorioTest
    {

        [Fact]
        public void Adicionar_Pessoa_Com_Dados_Nulos()
        {
            //arrange
            var pessoa = new Pessoa { Email = "xxx@xxx.xxx" };
            var repo = new PessoaRepositorio();

            //act
            //repo.AdicionarPessoa(pessoa);
            var result = repo.BuscarPessoa((x => x.Email == "xxx@xxx.xxx"));

            //assert
            Assert.NotNull(result);
            Assert.Equal(pessoa.Email, result.Email);
            Assert.Equal(pessoa.Nome, result.Nome);
        }

        [Fact]
        public void Adicionar_Pessoa_Sem_SharePoint()
        {
            var nomelista = "Pessoas";

            using (ShimsContext.Create())
            {
                //arrange
                var ctx = new ShimClientContext
                {
                    ExecuteQuery = () => { },
                    WebGet = () => new ShimWeb
                    {
                        ListsGet = () => new ShimListCollection
                        {
                            GetByTitleString = (s) =>
                            {
                                Assert.Equal(nomelista, s);
                                return new ShimList
                                {
                                    TitleGet = () => nomelista,
                                    AddItemListItemCreationInformation = information => 
                                        new ShimListItem
                                    {
                                        Update = () =>
                                        {
                                            //o item foi atualizado
                                            Assert.True(true);
                                        },

                                        ItemSetStringObject = (s1, o) =>
                                        {
                                            //valida se houve uma inserção do item "Title"
                                            //somente Title pode ser inserido
                                            //Assert.Equal(s1, "Title");
                                            Assert.NotNull(o);
                                        }
                                    },
                                    Update = () =>
                                    {
                                        //houve atualizacao
                                        Assert.True(true);
                                    }
                                };
                            }
                        }
                    }
                };

                var ctxRuntime = new ShimClientRuntimeContext(ctx);
                ctxRuntime.LoadOf1M0ExpressionOfFuncOfM0ObjectArray<List>((a, b) => { });
                ctxRuntime.LoadQueryOf1ClientObjectCollectionOfM0<List>(delegate { return null; });
                ctxRuntime.LoadQueryOf1IQueryableOfM0<List>(delegate { return null; });

                ctxRuntime.LoadOf1M0ExpressionOfFuncOfM0ObjectArray<ListItem>((a, b) => { });
                ctxRuntime.LoadQueryOf1ClientObjectCollectionOfM0<ListItem>(delegate { return null; });
                ctxRuntime.LoadQueryOf1IQueryableOfM0<ListItem>(delegate { return null; });

                var repo = new PessoaRepositorio();

                //act
                repo.AdicionarPessoa(new Pessoa{Email = "lmarques@morecodding.com", Nome = "lucas"}, ctx);
            
                //assert
                var result = repo.BuscarPessoa((x=> x.Nome == "lucas" ));
            
                Assert.NotNull(result);
            }
        }

    }
}
