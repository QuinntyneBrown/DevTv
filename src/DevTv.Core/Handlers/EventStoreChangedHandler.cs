using BuildingBlocks.EventStore;
using DevTv.Core.Data;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.Serialization.FormatterServices;

namespace DevTv.Core.Handlers
{
    public class EventStoreChangedHandler : INotificationHandler<EventStoreChanged>
    {
        private readonly IDevTvDbContext _context;
        private readonly Dictionary<Guid, IAggregateRoot> _inMemoryAggregates = new Dictionary<Guid, IAggregateRoot>();

        public EventStoreChangedHandler(IDevTvDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EventStoreChanged notification, CancellationToken cancellationToken)
        {
            foreach (var storedEvent in notification.Events)
            {
                var type = Type.GetType(storedEvent.AggregateDotNetType);

                var entity = await GetAggregateAsync(type, storedEvent.StreamId);

                entity.Apply(JsonConvert.DeserializeObject(storedEvent.Data, Type.GetType(storedEvent.DotNetType)) as IEvent);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<IAggregateRoot> GetAggregateAsync(Type type, Guid streamId)
        {
            var aggregate = _inMemoryAggregates.SingleOrDefault(x => x.Key == streamId).Value;

            aggregate ??= (await _context.FindAsync(type, streamId)) as IAggregateRoot;

            if (aggregate == null)
            {
                aggregate = GetUninitializedObject(type) as IAggregateRoot;

                _ = _context.Add(aggregate);
            }

            if (!_inMemoryAggregates.Any(x => x.Key == streamId))
            {
                _inMemoryAggregates.Add(streamId, aggregate);
            }

            return aggregate;
        }
    }
}
