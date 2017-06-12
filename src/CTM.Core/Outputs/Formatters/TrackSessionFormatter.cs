using System;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public class TrackSessionFormatter : ITrackSessionFormatter
    {
        public string Format(TrackSession trackSession)
        {
            if (trackSession == null) throw new ArgumentNullException(nameof(trackSession));

            return $"{trackSession.Time.ToTimeString()} {trackSession.Title}";
        }
    }
}