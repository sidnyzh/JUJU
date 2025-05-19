using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Middlewares.GlobalExceptionMiddleware
{
    /// <summary>
    /// Error details with the StatusCode and the Message Error or Execption Message
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Error Type Description
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// Message Error or Exception Message
        /// </summary>
        public List<string> Errors { get; set; }
       
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
