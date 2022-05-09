using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TesteBackend
{
    public class BodyPayload
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }


    [TestFixture, Category("Usuario")]
    public class TestesUsuario
    {

        [TestCase(TestName = "Criar usuário - Sucesso")]
        public void CriarUsuario_Sucesso()
        {
            //Arrange
            string token = "4c97e60b3df0585dec6c0c67b40167544635f7e6e796ddadea302f2ff3ea2c44";
            
            Random random = new Random();
            int numeroAleatorio = random.Next(0, 10000);

            BodyPayload body = new BodyPayload();
            body.name = "Joao da Silva";
            body.email = $"joaodasilva{numeroAleatorio}@auditeste.com.br";
            body.gender = "male";
            body.status = "active";

            //Act
            RestClient client = new RestClient("https://gorest.co.in/public/v2/users");
            client.Authenticator = new JwtAuthenticator(token);
            RestRequest request = new RestRequest(Method.POST);
            request.AddJsonBody(body);
            
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);            
        }

        [TestCase(TestName = "Selecionar usuário - Sucesso")]
        public void SelecionarUsuario_Sucesso()
        {
            //Arrange
            int usuarioID = 3580;
            string token = "4c97e60b3df0585dec6c0c67b40167544635f7e6e796ddadea302f2ff3ea2c44";

            //Act
            RestClient client = new RestClient($"https://gorest.co.in/public/v2/users/{usuarioID}");
            client.Authenticator = new JwtAuthenticator(token);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(TestName = "Atualizar usuário - Sucesso")]
        public void AtualizarUsuario()
        {
            //Arrange 
            int usuarioID = 3580;
            string token = "4c97e60b3df0585dec6c0c67b40167544635f7e6e796ddadea302f2ff3ea2c44";

            BodyPayload body = new BodyPayload();
            body.status = "active";
            body.name = "Manuel da Silva";
            body.gender = "male";
            body.email = "manueldasilva@auditeste.com.br";

            //Act
            RestClient client = new RestClient($"https://gorest.co.in/public/v2/users/{usuarioID}");
            client.Authenticator = new JwtAuthenticator(token);
            RestRequest request = new RestRequest(Method.PATCH);
            request.AddJsonBody(body);

            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(TestName = "Exluir usuário - Sucesso")]
        public void ExcluirUsuario_Sucesso()
        {
            //Arrange
            int usuarioID = 3579;
            string token = "4c97e60b3df0585dec6c0c67b40167544635f7e6e796ddadea302f2ff3ea2c44";

            //Act
            RestClient client = new RestClient($"https://gorest.co.in/public/v2/users/{usuarioID}");
            client.Authenticator = new JwtAuthenticator(token);
            RestRequest request = new RestRequest(Method.DELETE);

            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
