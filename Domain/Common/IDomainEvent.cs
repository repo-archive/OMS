using System;
namespace Domain.Common
{
    public interface IDomainEvent
    {
       DateTimeOffset OccuredOn { get; }
    }
}
