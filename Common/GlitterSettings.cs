namespace TeaseAI.Common
{
    /// <summary>
    /// Glitter specific settings.
    /// Be aware, that Domme settings are kept on the main page, not here (they are the same)
    /// </summary>
    public class GlitterSettings
    {
        /// <summary>
        /// Glitter contact one, the "Bratty" friend
        /// </summary>
        public DommeSettings Contact1
        {
            get => _contact1 ?? (_contact1 = new DommeSettings());
            set => _contact1 = value;
        }

        private DommeSettings _contact1;

        /// <summary>
        /// Glitter contact two, the "Cruel" friend
        /// </summary>
        public DommeSettings Contact2
        {
            get => _contact2 ?? (_contact2 = new DommeSettings());
            set => _contact2 = value;
        }

        private DommeSettings _contact2;

        /// <summary>
        /// Glitter contact three, the "Caring" friend
        /// </summary>
        public DommeSettings Contact3
        {
            get => _contact3 ?? (_contact3 = new DommeSettings());
            set => _contact3 = value;
        }

        private DommeSettings _contact3;
    }
}
