namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    public interface IGetPredictWordsListQuery
    {
        IReadOnlyCollection<PredictWordModel> Execute(string term);
    }
}
