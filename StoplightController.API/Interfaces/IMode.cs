namespace StoplightController.API.Interfaces
{
    public interface IMode
    {
        void Start(IStoplightStateManager stoplightStateManager);

        void End();
    }
}