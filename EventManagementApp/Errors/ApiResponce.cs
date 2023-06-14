namespace EventManagementApp.Errors
{
    public class ApiResponce
    {
        public ApiResponce(int statuscode,string message=null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode);
        }

        private string GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request you have made",
                401=>"NotAuthersied",
                404=>"not found",
                500=>"Server error",
                _=>null
            };

        }

        public int StatusCode { get;set; }
        public string Message { get;set; }
    }
}
