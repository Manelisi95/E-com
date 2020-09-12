namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
               StatusCode = statusCode;
               Message =message?? GetDefaultMessageForStatuseCode(statusCode);
        }

        private string GetDefaultMessageForStatuseCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "not Authourized",
                404 => "Resourse not found",
                500 => "I hate errors",
                _ => null
            };
        }

        
        public int StatusCode {get;set;}
        public string Message{get ;set;}
        
    }
}