namespace Sam.CleanArchitecture.Domain.OutBoxEventItems.Dtos
{
    public class OutBoxEventItemDto
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public string EventPayload { get; set; }

    }
}
