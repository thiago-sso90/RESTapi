namespace NovoprojetoApi.Entidades
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }  
         
        public string TituloPalestra { get; set;}

        public string TiuloDescricao { get; set;}

        public string PerfilLinkedin { get; set;}

        public Guid DevEventId { get; set;}
    }
}