using System;
using System.Runtime.Serialization;

namespace ECA.CBF.Demo.Entities.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException()
    { }

    public ResourceNotFoundException(string message) : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}