namespace CTM.Core.Inputs.Parsing
{
    public interface ISessionDefinitionParser
    {
        ParsingResult Parse(string trackInput);
    }
}