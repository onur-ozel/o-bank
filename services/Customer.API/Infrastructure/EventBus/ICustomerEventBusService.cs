namespace Customer.API.Infrastructure.EventBus {
    public interface ICustomerEventBusService {
        void PublishAsync (string topic, string message);
    }
}