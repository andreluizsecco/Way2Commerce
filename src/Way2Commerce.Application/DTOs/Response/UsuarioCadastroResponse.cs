namespace Way2Commerce.Application.DTOs.Response
{
    public class UsuarioCadastroResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }

        public UsuarioCadastroResponse() =>
            Erros = new List<string>();

        public UsuarioCadastroResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}