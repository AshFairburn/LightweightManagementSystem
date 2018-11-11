namespace LightweightManagementSystem
{
    /// <summary>
    /// An interface to represent the general behaviour of a manager.
    /// </summary>
    public interface IManager
    {
        void OnManagerRegistered(CoreBehaviour coreBehaviour);
        void OnManagerUnregistered();
    }
}