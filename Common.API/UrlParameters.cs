using System.Collections.Specialized;

namespace Common.Api
{
    public class QueryStringParameter
    {
        private NameValueCollection queryStringParameters;

        public QueryStringParameter()
        {
            this.queryStringParameters = new NameValueCollection();
        }

        public QueryStringParameter Add(string name, int value)
        {
            return this.Add(name, value.ToString());
        }
        public QueryStringParameter Add(string name, string value)
        {
            queryStringParameters.Add(name, value);
            return this;
        }

        public QueryStringParameter Add(NameValueCollection parameters)
        {
            queryStringParameters = parameters;
            return this;
        }

        public NameValueCollection Get()
        {
            return this.queryStringParameters;
        }

        public QueryStringParameter AddToType<T>(string name, T value) where T : class
        {
            return this.Add(name, System.Text.Json.JsonSerializer.Serialize(value));
        }


    }
}
