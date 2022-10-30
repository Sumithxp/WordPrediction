namespace WordPrediction.Application.Interfaces
{
    public interface IPredictedWordsService
    {
        Task<string[]> GetPredictions(string text);
    }
}
