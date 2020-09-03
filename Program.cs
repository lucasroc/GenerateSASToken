using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace GenerateSASToken
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var parameters = CheckParameters(args);

                var id = parameters["id"];
                Console.WriteLine($"Identifier informado: {id}");
                var key = parameters["key"];
                Console.WriteLine($"Primary/Secondary key informado: {key}");

                var expiry = DateTime.UtcNow.AddDays(30); // 30 dias suportado

                using (var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
                {
                    var dataToSign = id + "\n" + expiry.ToString("O", CultureInfo.InvariantCulture);
                    var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                    var signature = Convert.ToBase64String(hash);
                    var encodedToken = string.Format("SharedAccessSignature uid={0}&ex={1:o}&sn={2}", id, expiry, signature);
                    Console.WriteLine(encodedToken);
                    Console.WriteLine("Atribuindo token gerado a variável SASToken...");
                    Console.WriteLine($"echo '##vso[task.setvariable variable=SASToken]{encodedToken}'");
                    //Environment.SetEnvironmentVariable("SASToken", encodedToken);
                    //foreach (var variableKey in Environment.GetEnvironmentVariables().Keys)
                    //{
                    //    Console.WriteLine(variableKey);
                    //}

                    //Console.WriteLine("Gerado o Token:{0}", Environment.GetEnvironmentVariable("SASToken"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve erro na geração do Token. Verifique o identifier e o key informados.", ex);
            }
        }

        private static IDictionary<string, string> CheckParameters(string[] args)
        {
            var parameters = new Dictionary<string, string>();

            Console.WriteLine("Verificando o identifier do APIM...");
            var id = args[0];
            if (string.IsNullOrWhiteSpace(id)) throw new Exception("Identifier do APIM não foi informado!");
            parameters.Add("id", id);

            Console.WriteLine("Verificando a PrimaryKey ou SecondaryKey do APIM:");
            var key = args[1];
            if (string.IsNullOrWhiteSpace(key)) throw new Exception("PrimaryKey ou SecondaryKey do APIM não foi informado!");
            parameters.Add("key", key);

            return parameters;
        }
    }
}
