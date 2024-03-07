namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Object that can be interacted with by the player character
    /// </summary>
    public interface IInteractable
    {
        public bool CanBeInteractedWith();

        public void Interact();
    }
}