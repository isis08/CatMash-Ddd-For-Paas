
namespace CatMash.Core.Domain.Entity
{
    using System;
    using System.Collections.ObjectModel;
    using CatMash.Core.Domain.Business;
    using CatMash.Core.Domain.ValueObject;

    public class CatEntity : ICatEntity
    {
        public CatEntity()
        {
            FurTypes = new ObservableCollection<FurTypesValueObject>();
        }

        //--
        //-- ATTENTION le métier me dit : "L'algorithme de calcul de la pondération pourra évoluer"
        //-- 
        //-- (DSL) => pondération = probabilité qu'a un chat pour être choisis parmi les 2 chats à choisir
        //--
        private readonly IWeightCalculationStrategy weightCalculationStrategy;

        public CatEntity(IWeightCalculationStrategy weightCalculationStrategy)
        {
            this.weightCalculationStrategy = weightCalculationStrategy ?? throw new ArgumentNullException(nameof(weightCalculationStrategy));
        }

        #region Fields

        private double probabilityWeight;

        private int wins;

        private int viewsNumber;

        #endregion

        #region Properties

        public int Id { get; set; }

        /// <summary>
        /// On laisse les GETTER et SETTER pour :
        /// - faciliter les TU
        /// - Serialization par défaut
        /// - utilisation d'un ORM
        /// </summary>
        public string CatUrl { get; set; }

        public bool IsAStar { get; set; }

        public bool IsTopOne { get; set; }

        public bool IsAlone { get; set; }

        public double Rating => CalculRating();

        public int Wins
        {
            get => wins;
            set => wins = value;
        }

        public double ProbabilityWeight
        {
            get => probabilityWeight;
            set => probabilityWeight = value;
        }

        public int ViewsNumber
        {
            get => viewsNumber;
            set => viewsNumber = value;
        }

        /// <summary>
        /// Les collections ne doivent avoir que un GETTER
        /// </summary>
        /// <remarks>
        /// Le pattern observable permet d'être compatible avec le pattern AGGREGAT
        /// </remarks>
        public ObservableCollection<FurTypesValueObject> FurTypes { get; }

        #endregion

        #region Accessors

        public void AddFurType(FurTypesValueObject furType)
        {
            FurTypes.Add(furType);
        }

        public void RemoveFurType(FurTypesValueObject furType)
        {
            FurTypes.Remove(furType);
        }

        /// <summary>
        /// Implémenter des accesseurs facilitera l'utilisation du pattern AGGREGAT
        /// </summary>
        /// <param name="nbWin">The nb win.</param>
        public void SetWins(int nbWin)
        {
            // Il faut passer par le backField
            wins = nbWin;
        }

        #endregion

        #region Behaviors

        public void ApplySuccess(int totalViews)
        {
            AddNewWins(1);
            ApplyChoiceDone(totalViews);
        }

        public void ApplyLoose(int totalViews)
        {
            ApplyChoiceDone(totalViews);
        }

        #endregion

        #region Internal Behaviors

        /// <summary>
        /// Le calcul du rating ne changera pas car les ratings des applications "AnimalMash" doivent pouvoir êtgre comparés
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Division par 0 impossible</exception>
        private double CalculRating()
        {
            if (ViewsNumber == 0) throw new Exception("Division par 0 impossible");

            return Wins * 100 / ViewsNumber;
        }

        private void ApplyChoiceDone(int totalViews)
        {
            AddNewViewsNumber(1);
            CalculWeight(totalViews);
        }

        /// <summary>
        /// Logique métier qui permet l'ajout d'un like à un chat
        /// </summary>
        /// <remarks>
        /// Demain il faudra ajouter une étoile sur le colier du chat à chaque qu'il gagne (le métier appelle ca le "PredatorReward")
        /// </remarks>
        /// <param name="nbWin">The nb win.</param>
        private void AddNewWins(int nbWin)
        {
            SetWins(wins + nbWin);
        }

        /// <summary>
        /// Logique métier qui permet d'ajouterune vue à un chat
        /// </summary>
        /// <remarks>
        /// Demain il faudra prévenir le propriétaire du chat (par mail ?) au propriétaire
        /// </remarks>
        /// <param name="nbWin">The nb win.</param>
        private void AddNewViewsNumber(int viewsNumber)
        {
            ViewsNumber += 1;
        }

        private double CalculWeight(int totalViews)
        {
            if (totalViews == 0)
                totalViews = 1;

            probabilityWeight = weightCalculationStrategy.CalculateWeight(ViewsNumber, totalViews);

            return probabilityWeight;
        }

        #endregion

    }
}
