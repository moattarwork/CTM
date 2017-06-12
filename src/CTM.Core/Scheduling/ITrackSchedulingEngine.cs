using System.Collections.Generic;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public interface ITrackSchedulingEngine
    {
        IReadOnlyList<Track> Calculate(IEnumerable<SessionDefinition> sessionDefinitions);
    }
}