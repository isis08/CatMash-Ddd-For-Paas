namespace CatMash.Core.Domain.Poco
{
    using CatMash.Core.Domain.Entity;

    /// <summary>
    /// Le poco est un object métier sans logique implémentée
    /// C'est une structure qui encapsule d'autres objets métiers + unitaires
    /// </summary>
    public class PayloadPoco
    {
        public CatEntity Winner { get; set; }
        public CatEntity Loser { get; set; }
    }
}
