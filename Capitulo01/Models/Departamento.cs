namespace Capitulo01.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}
