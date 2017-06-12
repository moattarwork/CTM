using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public interface ITrackFormatter
    {
        string Format(Track track);
    }
}