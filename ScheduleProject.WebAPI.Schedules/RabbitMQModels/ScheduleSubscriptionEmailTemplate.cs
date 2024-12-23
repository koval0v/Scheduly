namespace TokenService.RabbitMQModels
{
    public class ScheduleSubscriptionEmailTemplate
    {
        public string UserName { get; set; }
        public string EiName { get; set; }
        public string EiLink { get; set; }
        public int Semester { get; set; }
        public string GroupCipher { get; set; }
    }
}
