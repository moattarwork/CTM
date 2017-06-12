using System;
using System.Text;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public class TrackFormatter : ITrackFormatter
    {
        private readonly ITrackSlotFormatter _trackSlotFormatter;

        public TrackFormatter(ITrackSlotFormatter trackSlotFormatter)
        {
            _trackSlotFormatter = trackSlotFormatter ?? throw new ArgumentNullException(nameof(trackSlotFormatter));
        }

        public string Format(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));

            var builder = new StringBuilder();

            builder.AppendLine(track.DefaultTrackName);
            builder.AppendLine();

            foreach (var slot in track.Slots)
            {
                var formattedSlot = _trackSlotFormatter.Format(slot);
                builder.Append(formattedSlot);
            }

            return builder.ToString();
        }
    }
}