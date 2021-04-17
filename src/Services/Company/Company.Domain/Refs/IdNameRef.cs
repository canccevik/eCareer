using Career.Exceptions;

namespace Company.Domain.Refs
{
    public abstract class IdNameRef
    {
        public string RefId { get; protected set; }
        public string Name { get; protected set; }

        protected IdNameRef(string refId, string name)
        {
            Check.NotNullOrEmpty(refId, nameof(refId));
            Check.NotNullOrEmpty(name, nameof(name));

            RefId = refId;
            Name = name;
        }
    }
}