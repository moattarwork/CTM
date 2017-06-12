using System.Collections.Generic;
using CTM.Core.Inputs.Parsing;

namespace CTM.Core.Inputs
{
    public interface ISessionDefinitionReader
    {
        IReadOnlyList<SessionDefinition> ReadDefinition();
    }
}