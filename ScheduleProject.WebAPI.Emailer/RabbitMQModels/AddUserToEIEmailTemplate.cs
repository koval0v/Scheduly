namespace TokenService.RabbitMQModels
{
    public class AddUserToEIEmailTemplate
    {
        public string UserWantsToAdd { get; set; }
        public string UserToAdd { get; set; }
        public string EiName { get; set; }
        public int EiId { get; set; }
    }
}
