using N5NowApi.Domain.Common;

namespace N5NowApi.Domain.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _events = new();
        protected AggregateRoot(int id) : base(id)
        {
        }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public void RaiseEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }
    }
}
