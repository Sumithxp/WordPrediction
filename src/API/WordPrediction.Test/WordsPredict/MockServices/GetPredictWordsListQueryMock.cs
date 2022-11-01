using System.Collections.Generic;
using System.Threading.Tasks;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.Tests.Integration.WordsPredict.MockServices
{
    public class GetPredictWordsListQueryMock : IGetPredictWordsListQuery
    { 

        public async Task<IReadOnlyCollection<PredictWordModel>> Execute(string text)
        {
            return new List<PredictWordModel>() {
                new PredictWordModel { Value = MockConstraint.LocalWord }
            };
        }
    }
}
