using UnityEngine;

namespace Scripts.Components
{
    public class AnimationCat : MonoBehaviour
    {
        Animator catAnimator;

        void Start()
        {
            //Get the Animator attached to the GameObject you are intending to animate.
            catAnimator = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            //Press WASD to trigger walk
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                catAnimator.SetBool("Walking", true);
            }

            if ( UnityEngine.Input.GetKey(KeyCode.A) ||
                UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.D))
            {
                catAnimator.SetBool("Walking", false);
            }
        }
    }
}