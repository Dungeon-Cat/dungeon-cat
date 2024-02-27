namespace Scripts.Components.CommonEntities
{
    public interface IInteractable
    {
        public bool CanBeInteractedWith();

        public void Interact();
    }
}