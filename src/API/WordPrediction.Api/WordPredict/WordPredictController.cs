using Microsoft.AspNetCore.Mvc;
using WordPrediction.Api.Attributes;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.WordPredict
{
    [Route("api/[controller]s")]
    [Authorize]
    [ApiController]
    public class WordPredictController : ControllerBase
    {
        private readonly IGetPredictWordsListQuery _query;

        public WordPredictController(IGetPredictWordsListQuery query)
        {
            _query = query;
        }

        [HttpGet]
        public  IReadOnlyCollection<PredictWordModel> Get([FromQuery] string term)
        {
            return  _query.Execute(term);
        }
    }
}
