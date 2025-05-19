using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RequestTimeOutException : BusinessException
    {
        public RequestTimeOutException()
        {
        }

        public RequestTimeOutException(string message) : base(message)
        {
        }

        public RequestTimeOutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected RequestTimeOutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
