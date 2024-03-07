using Scripts.Components.CommonEntities;
using Scripts.Data;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Wall in Room 3.
    /// Blocks player movement and can be opened to disappear
    /// </summary>
    public class ToggleWall : OpenableEntityComponent<OpenableEntityData>
    {
        public new void SetOpen(bool isOpen)
        {
            base.SetOpen(isOpen);
            collider2d.isTrigger = isOpen;
        }
    }
}