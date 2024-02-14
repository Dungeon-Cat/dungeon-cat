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

            var touching = cat.collider2d.Distance(witch.collider2d).distance < 5;
            
            // trigger dialogue
            // if (touching != dialogue.isDone)
            // {
            //     dialogue.start();
            // }
            
            // after dialogue
            // if (dialogue.isDone) 
            // {
            //      Destroy(gameObject);
            // }
        }
    }
}