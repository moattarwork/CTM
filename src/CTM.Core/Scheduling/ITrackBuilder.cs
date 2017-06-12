using System.Collections.Generic;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public interface ITrackBuilder
    {
        IReadOnlyList<Track> Build(int number);
    }
}