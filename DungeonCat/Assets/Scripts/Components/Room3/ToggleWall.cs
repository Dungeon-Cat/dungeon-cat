using Scripts.Components.CommonEntities;
using Scripts.Data;

namespace Scripts.Components.Room3
{
    public class ToggleWall : OpenableEntityComponent<OpenableEntityData>
    {
        public new void SetOpen(bool isOpen)
        {
            base.SetOpen(isOpen);
            collider2d.isTrigger = isOpen;
        }
    }
}