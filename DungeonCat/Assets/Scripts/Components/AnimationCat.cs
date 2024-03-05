using UnityEngine;

namespace Scripts.Components
{
    public class AnimationCat : MonoBehaviour
    {
        Animator cat_Animator;

        void Start()
        {
            //Get the Animator attached to the GameObject you are intending to animate.
            cat_Animator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            //Press WASD to trigger walk
            if (UnityEngine.Input.GetKey(KeyCode.W) || UnityEngine.Input.GetKey(KeyCode.A) ||
                UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.D))
            {
                // Reset Idle trigger
                cat_Animator.ResetTrigger("Idle");

                //Send the message to the Animator to activate the trigger parameter named "Walk"
                cat_Animator.SetTrigger("Walk");
            }

            // if (!(UnityEngine.Input.GetKey(KeyCode.W) || UnityEngine.Input.GetKey(KeyCode.A) ||
            //     UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.D)))
            // {
            //     //Reset the "Wall" trigger
            //     cat_Animator.ResetTrigger("Walk");
            //
            //     //Send the message to the Animator to activate the trigger parameter named "Idle"
            //     cat_Animator.SetTrigger("Idle");
            // }
        }
    }
}