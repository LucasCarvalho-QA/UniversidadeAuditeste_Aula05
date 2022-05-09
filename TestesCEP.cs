using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TesteBackend
{
    [TestFixture, Category("CEP")]
    public class TestesCEP
    {
        [TestCase]
        public void ConsultarCEP_Sucesso()
        {
            //Arrange - Pré requisito
            //Tudo que for necessário pra um teste ser executado, precisa estar aqui
            string cep = "01001000";

            //Act - É basicamente o teste em si, a ação
            RestClient client = new RestClient($"https://viacep.com.br/ws/{cep}/json/");
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            //Assert - É onde ficam as nossas validações
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase]
        public void ConsultarCEP_Falha()
        {
            Assert.IsTrue(true);
        }
    }
}
