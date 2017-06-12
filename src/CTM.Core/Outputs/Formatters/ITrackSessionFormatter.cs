using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public interface ITrackSessionFormatter
    {
        string Format(TrackSession trackSession);
    }
}