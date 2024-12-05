namespace EchangeDeviseAPI.Model
{
    public class DeviseBase
    {
        public string Base { get; set; } // Devise de base
        public Dictionary<string, decimal> Rates { get; set; } // Paires de taux de change
    }
}
