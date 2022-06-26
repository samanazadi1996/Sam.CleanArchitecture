using Domain.Common;

namespace Domain.ToDoListDomain.ValueObjects
{
    public class DescriptionValueObject: BaseValueObject<DescriptionValueObject>
    {
        public string Value { get; private set; }
        public DescriptionValueObject(string value)
        {
            this.Value = value;
            Validate();
        }
        public DescriptionValueObject()
        {

        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var temp = (DescriptionValueObject)obj;
            return Value.Equals(temp.Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool ObjectIsEqual(DescriptionValueObject otherObject)
        {
            return (Value == otherObject.Value);
        }

        public override int ObjectGetHashCode()
        {
            return base.GetHashCode();
        }
        private void Validate()
        {

        }
    }
}