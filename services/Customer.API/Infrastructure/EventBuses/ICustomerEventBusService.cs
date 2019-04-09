namespace Customer.API.Infrastructure.EventBuses {
    public interface ICustomerEventBusService {
        void PublishAsync (string topic, string message);
    }
}