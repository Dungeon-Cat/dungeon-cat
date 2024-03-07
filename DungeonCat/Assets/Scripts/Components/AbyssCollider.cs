using Scripts.Definitions.Items;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.Components
{
    /// <summary>
    /// Collider that is disabled if the cat is flying
    /// </summary>
    [RequireComponent(typeof(TilemapCollider2D))]
    public class AbyssCollider : MonoBehaviour
    {
        private TilemapCollider2D collider2d;
        private CompositeCollider2D compositeCollider2D;

        private void Awake()
        {
            collider2d = GetComponent<TilemapCollider2D>();
            compositeCollider2D = GetComponent<CompositeCollider2D>();
        }

        public void Update()
        {
            collider2d.enabled = !UnityState.Instance.cat.data.tags.Contains(Boots.FlyingTag);
            compositeCollider2D.enabled = !UnityState.Instance.cat.data.tags.Contains(Boots.FlyingTag);
        }
    }
}