namespace LibAddressInput
{
    public abstract class AddressAutocompletePrediction
    {
        public virtual string? PlaceId { get; }

        public virtual string? PrimaryText { get; }

        public virtual string? SecondaryText { get; }

        public override bool Equals(object o)
        {
            if (!(o is AddressAutocompletePrediction p)) {
                return false;
            }

            return PlaceId != null &&  PlaceId.Equals(p.PlaceId)
                && PrimaryText != null && PrimaryText.Equals(p.PrimaryText)
                && SecondaryText != null && SecondaryText.Equals(p.SecondaryText);
        }

        public override int GetHashCode()
            => (PlaceId == null ? 0 : PlaceId.GetHashCode()) ^ (PrimaryText == null ? 0 : PrimaryText.GetHashCode()) ^ (SecondaryText == null ? 0 : SecondaryText.GetHashCode());
    }
}