using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs
{
    public interface ITrackOutputWriter
    {
        void Write(Track track);
    }
}