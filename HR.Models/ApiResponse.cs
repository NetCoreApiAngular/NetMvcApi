namespace HR.Models
{
    public class ApiResponse<TContent>
    {
        public ApiResponse()
            : this(content: default(TContent), errorMessage: null)
        {
        }

        public ApiResponse(TContent content)
            : this(content: content, errorMessage: null)
        {
        }

        public ApiResponse(TContent content, string errorMessage)
        {
            Content = content;
            ErrorMessage = errorMessage;
        }

        public TContent Content { get; }

        public string ErrorMessage { get; }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public ApiResponse()
            : base(content: null, errorMessage: null)
        {
        }

        public ApiResponse(object content)
            : base(content: content, errorMessage: null)
        {
        }

        public ApiResponse(object content, string errorMessage)
            : base(content, errorMessage)
        {
        }
    }
}
