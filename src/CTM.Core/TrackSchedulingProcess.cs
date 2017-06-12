using System;
using CTM.Core.Inputs;
using CTM.Core.Outputs;
using CTM.Core.Scheduling;

namespace CTM.Core
{
    public class TrackSchedulingProcess : ITrackSchedulingProcess
    {
        private readonly ISessionDefinitionReader _sessionDefinitionReader;
        private readonly ITrackOutputWriter _trackOutputWriter;
        private readonly ITrackSchedulingEngine _trackSchedulingEngine;

        public TrackSchedulingProcess(
            ISessionDefinitionReader sessionDefinitionReader,
            ITrackSchedulingEngine trackSchedulingEngine,
            ITrackOutputWriter trackOutputWriter)
        {
            _trackOutputWriter = trackOutputWriter ?? throw new ArgumentNullException(nameof(trackOutputWriter));
            _trackSchedulingEngine = trackSchedulingEngine ??
                                     throw new ArgumentNullException(nameof(trackSchedulingEngine));
            _sessionDefinitionReader = sessionDefinitionReader ??
                                       throw new ArgumentNullException(nameof(sessionDefinitionReader));
        }

        public void Run()
        {
            var sessionDefinitions = _sessionDefinitionReader.ReadDefinition();
            var scheduledTracks = _trackSchedulingEngine.Calculate(sessionDefinitions);

            foreach (var track in scheduledTracks)
                _trackOutputWriter.Write(track);
        }
    }
}