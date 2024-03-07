using System.Linq;
using Cainos.PixelArtTopDown_Basic;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.Room2
{

    [RequireComponent(typeof(PropsAltar))]
    public class PressurePlateAltar : MonoBehaviour
    {
        private PropsAltar altar;
        private Collider2D collider2d;

        public bool Active { get; private set; }

        private void Start()
        {
            altar = GetComponent<PropsAltar>();
            collider2d = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            Active = false;
            foreach (var entity in UnityState.CurrentScene.AllEntities.Append(UnityState.Instance.cat))
            {
                if (entity.GameObject.HasComponent(out Collider2D other) && collider2d.IsTouching(other))
                {
                    Active = true;
                    break;
                }
            }

            altar.targetColor = Active ? Color.white : Color.clear;
        }

    }

}