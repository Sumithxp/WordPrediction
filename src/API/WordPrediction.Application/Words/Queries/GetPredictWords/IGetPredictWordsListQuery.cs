namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    public interface IGetPredictWordsListQuery
    {
        Task<IReadOnlyCollection<PredictWordModel>> Execute(string text);
    }
}
