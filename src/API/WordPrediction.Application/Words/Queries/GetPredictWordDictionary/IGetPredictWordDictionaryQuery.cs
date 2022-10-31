namespace WordPrediction.Application.Words.Queries.GetPredictWordDictionary
{
    public interface IGetPredictWordDictionaryQuery
    {
        Task<IReadOnlyCollection<PredictWordDictionaryModel>> Execute(string text);
    }
}
