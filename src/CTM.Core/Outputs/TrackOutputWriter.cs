using System;
using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs
{
    public class TrackOutputWriter : ITrackOutputWriter
    {
        private readonly ITrackFormatter _formatter;
        private readonly IOutputWriter _outputWriter;

        public TrackOutputWriter(ITrackFormatter formatter, IOutputWriter outputWriter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        }

        public void Write(Track track)
        {
            if (track == null) throw new ArgumentNullException(nameof(track));

            var formattedTrack = _formatter.Format(track);
            _outputWriter.WriteLine(formattedTrack);
        }
    }
}