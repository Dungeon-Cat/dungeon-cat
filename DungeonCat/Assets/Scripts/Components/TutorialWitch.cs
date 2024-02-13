using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Controls the behavior of the Witch in the tutorial.
    /// </summary>
    public class TutorialWitch : MonoBehaviour
    {
        // currently using basic item entity
        public ItemEntity witch;

        // update witch state
        //  should trigger dialogue on proximity
        //  should give key item to cat
        //  should disappear after dialogue
        private void FixedUpdate()
        {
            var cat = UnityState.Instance.cat;

            var touching = cat.collider.Distance(witch.collider).distance < 5;
            
            // trigger dialogue
            // if (touching != door.IsOpen)
            // {
            //     door.SetOpen(touching);
            // }
            
            // after dialogue
            // if (isDone) 
            // {
            //      Destroy(gameObject);
            // }
        }
    }
}