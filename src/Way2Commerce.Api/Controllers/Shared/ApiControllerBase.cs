using Microsoft.AspNetCore.Mvc;

namespace Way2Commerce.Api.Controllers.Shared;

// O ideal aqui seria termos um controller base para cada versão e setar o atributo ApiVersion. Ex: v1ControllerBase, v2ControllerBase, etc
// Isso só não é possível ainda pois a biblioteca de versionamento não suporta isso.
// Essa feature está em progresso e mapeada para vesão 3: https://github.com/dotnet/aspnet-api-versioning/issues/230
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiControllerBase : ControllerBase { }