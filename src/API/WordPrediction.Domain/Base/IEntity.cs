namespace WordPrediction.Domain.Base
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
