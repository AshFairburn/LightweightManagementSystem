namespace LightweightManagementSystem
{
    /// <summary>
    /// An interface to represent the general behaviour of a manager.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Called when the manager has been registered into the core behaviour
        /// and it's passed a reference to the core behaviour.
        /// </summary>
        /// <param name="coreBehaviour"></param>
        void OnManagerRegistered(CoreBehaviour coreBehaviour);
        /// <summary>
        /// Called when the manager has been removed from the core behaviour.
        /// </summary>
        void OnManagerUnregistered();
    }
}