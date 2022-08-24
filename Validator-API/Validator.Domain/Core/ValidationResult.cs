namespace Validator.Domain.Core
{
    public class ValidationResult
    {
        public bool IsValid { get { return !Notifications.Any(); } }
        public List<string> Notifications { get; private set; }

        public ValidationResult()
        {
            Notifications = new List<string>();
        }

        public ValidationResult Add(List<string> notifications)
        {
            foreach (var notification in notifications)
                Notifications.Add(notification);

            return this;
        }

        public ValidationResult Add(ValidationResult result)
        {
            foreach (var notification in result.Notifications)
                Notifications.Add(notification);

            return this;
        }

        public void Add(string message)
        {
            Notifications.Add(message);
        }

    }
}
