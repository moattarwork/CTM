using System;

namespace CTM.Core.Inputs.Parsing
{
    public class SessionDefinition
    {
        public SessionDefinition(string title, int duration)
        {
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

            Title = title ?? throw new ArgumentNullException(nameof(title));
            Duration = duration;
        }

        public string Title { get; }
        public int Duration { get; }
    }
}