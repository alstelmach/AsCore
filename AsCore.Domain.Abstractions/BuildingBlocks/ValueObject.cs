namespace AsCore.Domain.Abstractions.BuildingBlocks
{
    public abstract record ValueObject
    {
        public virtual ValueObject GetCopy() =>
            MemberwiseClone() as ValueObject;
    }
}
