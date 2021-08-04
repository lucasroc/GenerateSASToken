
# Generate SAS Token: 
- Criado para gerar SAS Token do Azure API Management (APIM), mas pode ser usado para qualquer serviço que atenda o formato gerado para o SAS Token.

# Como utilizar:
1. Abrir o prompt de comando e executar o arquivo GenerateSASToken.exe informando os seguintes argumentos separados por espaço:
- Argumento 1: Identifier. O identificador de credential obtido na configuração da REST API
- Argumento 2: Primary/Secondary key. Uma das keys disponíveis obtido na configuração da REST API.

Exemplo de execução: 
- `GenerateSASToken.exe integration rwrN5hOvS8a5o2sYQV/jsQikcyzJ5yMGuq3h4vrcaiDGoFWs...`

2. Após rodar é gerado o SAS token como no exemplo a seguir:
- `SharedAccessSignature uid=integration&ex=2020-10-02T15:11:42.0713427Z&sn=M9ZXHnWCT8S...+5DQ6VvaWOlD...+VIJUQWiPU6PNdfaF/...`
