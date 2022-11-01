using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPrediction.Application.Words.Queries.GetPredictWordDictionary;

namespace WordPrediction.Api.Tests.Integration.WordsPredict.MockServices
{
    public class GetPredictWordDictionaryQueryMock : IGetPredictWordDictionaryQuery
    {
        public async Task<IReadOnlyCollection<PredictWordDictionaryModel>> Execute(string text)
        {
            return new List<PredictWordDictionaryModel>() {
                new PredictWordDictionaryModel { Value = MockConstraint.CustomWord }
            };
        }
    }
}
