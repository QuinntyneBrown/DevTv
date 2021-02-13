using BuildingBlocks.EventStore;
using System;

namespace DevTv.Core.Models
{
    public class Video: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid VideoId { get; private set; }
        public DateTime? Deleted { get; private set; }

        public void Remove()
        {

        }

        public void Update()
        {

        }
    }
}
