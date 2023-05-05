namespace NovoprojetoApi.Entidades
{
    public class DevEvent
    {
        public DevEvent() 
        {
           Palestrante = new List<DevEventSpeaker>();
            IsDeleted = false;
        }

        public Guid Id { get; set; } 

        public string Titulo { get; set; }

        public string Descricao { get; set; }
        
        public DateTime DataInicial { get; set; }
        
        public DateTime DataFinal { get; set; }
        
        public List<DevEventSpeaker> Palestrante { get; set; }

        public bool IsDeleted { get; set; }

        public void Update(string titulo , string descricao , DateTime datainicial, DateTime datafinal)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataInicial = datainicial;
            DataFinal = datafinal;

        }
        public void Delete() 
        {
            IsDeleted = true;        
        }

       
    }

   
}   
