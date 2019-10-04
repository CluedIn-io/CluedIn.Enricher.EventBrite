// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearBitVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ClearBitVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.ExternalSearch.Providers.EventBrite.Vocabularies
{
    /// <summary>The clear bit vocabulary.</summary>
    public static class EventBriteVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="EventBriteVocabulary" /> class.
        /// </summary>
        static EventBriteVocabulary()
        {
            Event = new EventBriteEventVocabulary();
        }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        public static EventBriteEventVocabulary Event { get; private set; }
    }
}