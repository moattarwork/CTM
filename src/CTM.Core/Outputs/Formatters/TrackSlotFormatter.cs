using System;
using System.Text;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public class TrackSlotFormatter : ITrackSlotFormatter
    {
        private readonly ITrackSessionFormatter _trackSessionFormatter;

        public TrackSlotFormatter(ITrackSessionFormatter trackSessionFormatter)
        {
            _trackSessionFormatter = trackSessionFormatter ??
                                     throw new ArgumentNullException(nameof(trackSessionFormatter));
        }

        public string Format(TrackSlot trackSlot)
        {
            if (trackSlot == null) throw new ArgumentNullException(nameof(trackSlot));

            return trackSlot.IsPreScheduled == false
                ? FormatScheduled(trackSlot)
                : FormatPreScheduled(trackSlot);
        }

        private string FormatScheduled(TrackSlot trackSlot)
        {
            var builder = new StringBuilder();

            foreach (var session in trackSlot.TrackSessions)
            {
                var formattedSession = _trackSessionFormatter.Format(session);
                builder.AppendLine(formattedSession);
            }

            return builder.ToString();
        }

        private string FormatPreScheduled(TrackSlot trackSlot)
        {
            return $"{trackSlot.TimeSlot.ToTimeString()} {trackSlot.Title} {Environment.NewLine}";
        }
    }
}