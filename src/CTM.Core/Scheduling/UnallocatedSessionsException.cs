using System;
using System.Collections.Generic;
using System.Text;
using CTM.Core.Inputs.Parsing;

namespace CTM.Core.Scheduling
{
    public class UnallocatedSessionsException : Exception
    {
        public UnallocatedSessionsException(IReadOnlyList<SessionDefinition> sessionDefinitions) : base((string) Format(sessionDefinitions))
        {
            
        }

        private static string Format(IReadOnlyList<SessionDefinition> sessionDefinitions)
        {
            if (sessionDefinitions == null) throw new ArgumentNullException(nameof(sessionDefinitions));

            var builder = new StringBuilder("Unallocated Sessions Error:");
            foreach (var definition in sessionDefinitions)
            {
                builder.AppendLine($"{definition.Duration} - {definition.Title}");
            }

            return builder.ToString();
        }
    }
}