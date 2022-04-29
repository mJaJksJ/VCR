namespace Iris.Api.Results
{
    public abstract class ResponseContract
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
