namespace GraphIO
{
    public interface IGraphReader
    {
        /// <summary>Считать граф из текстового источника.</summary>
        Graph Read(System.IO.TextReader reader);
    }
}