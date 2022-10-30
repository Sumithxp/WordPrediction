using WordPrediction.Domain.Base;

namespace WordPrediction.Domain
{
    public class Word : IEntity<int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
       
    }
}
