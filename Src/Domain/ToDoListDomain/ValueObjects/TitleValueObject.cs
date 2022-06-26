using Domain.Common;

namespace Domain.ToDoListDomain.ValueObjects
{
    public class TitleValueObject: BaseValueObject<TitleValueObject>
    {
        public string Value { get; set; }
        public TitleValueObject(string text)
        {
            Value = text;
        }
        public TitleValueObject()
        {

        }
        public override bool ObjectIsEqual(TitleValueObject otherObject)
        {
            return (Value == otherObject.Value);
        }

        public override int ObjectGetHashCode()
        {
            return base.GetHashCode();
        }
    }
}