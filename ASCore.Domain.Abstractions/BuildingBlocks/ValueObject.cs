using System.Collections.Generic;
using System.Linq;

namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject firstObject, ValueObject secondObject)
        {
            var areBothNull = ReferenceEquals(firstObject, null) && ReferenceEquals(secondObject, null);
            
            var areEqual = areBothNull
                           || !ReferenceEquals(firstObject, null)
                           && firstObject.Equals(secondObject);

            return areEqual;
        }

        public static bool operator !=(ValueObject firstObject, ValueObject secondObject) =>
            !(firstObject == secondObject);

        public override bool Equals(object @object) =>
            @object is ValueObject valueObject && valueObject
                .GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());

        public override int GetHashCode() =>
            GetEqualityComponents()
                .Select(equalityComponent => equalityComponent != null
                    ? equalityComponent.GetHashCode()
                    : 0)
                .Aggregate((x, y) => x ^ y);

        public ValueObject GetCopy() =>
            MemberwiseClone() as ValueObject;

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
