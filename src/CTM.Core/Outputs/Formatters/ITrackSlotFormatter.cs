using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Outputs.Formatters
{
    public interface ITrackSlotFormatter
    {
        string Format(TrackSlot trackSlot);
    }
}