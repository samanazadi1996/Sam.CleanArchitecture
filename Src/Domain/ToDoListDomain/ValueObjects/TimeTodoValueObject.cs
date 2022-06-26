using Domain.Common;
using Domain.ToDoListDomain.Exceptions;
using System;

namespace Domain.ToDoListDomain.ValueObjects
{
    public class TimeTodoValueObject : BaseValueObject<TimeTodoValueObject>
    {
        public DateTime Value { get; private set; }
        public TimeTodoValueObject(DateTime value)
        {

            Value = value;
            Validate();
        }
        public TimeTodoValueObject()
        {

        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var temp = (TimeTodoValueObject)obj;
            return Value.Equals(temp.Value);
        }
        public override bool ObjectIsEqual(TimeTodoValueObject otherObject)
        {
            return (Value == otherObject.Value);
        }

        public override int ObjectGetHashCode()
        {
            return base.GetHashCode();
        }
        private void Validate()
        {
            if (Value < DateTime.Now)
                throw new UnsupportedTimeToDoException();
        }
    }
}