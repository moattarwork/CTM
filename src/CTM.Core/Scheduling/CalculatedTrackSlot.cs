using System;
using System.Linq;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;
using CTM.Core.Scheduling.Extensions;

namespace CTM.Core.Scheduling
{
    public class CalculatedTrackSlot
    {
        private readonly TrackSlot _trackSlot;

        public CalculatedTrackSlot(TrackSlot trackSlot)
        {
            _trackSlot = trackSlot ?? throw new ArgumentNullException(nameof(trackSlot));

            UnallocatedTime = trackSlot.Slot.DurationInMinute;
        }

        public int UnallocatedTime { get; private set; }

        public void AllocateSession(SessionDefinition sessionDefinition)
        {
            if (sessionDefinition == null) throw new ArgumentNullException(nameof(sessionDefinition));

            var lastSession = _trackSlot.TrackSessions.LastOrDefault();
            var slot = lastSession != null
                ? lastSession.GetNextSlot(sessionDefinition.Duration)
                : _trackSlot.GetNextSlot(sessionDefinition.Duration);

            var trackSession = new TrackSession(sessionDefinition.Title, slot);
            _trackSlot.TrackSessions.Add(trackSession);

            UnallocatedTime -= sessionDefinition.Duration;
        }
    }
}