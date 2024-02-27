using System.Linq;
using Input;
using Pathfinding;
using Scripts.UI;
using Scripts.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Components
{
    public class InputManager : MonoBehaviour
    {
        private const float Speed = 50;
        private const float NextWaypointDistance = 3;

        private static InputActions actions;

        private Seeker seeker;
        private Path path;
        private int currentWaypoint;

        private void Start()
        {
            seeker = UnityState.Instance.cat.GetComponent<Seeker>();
            InvokeRepeating(nameof(CheckPath), 0, .2f);
        }

        public static InputActions Actions
        {
            get
            {
                if (actions == null)
                {
                    actions = new InputActions();
                    actions.Enable();
                }
                return actions;
            }
        }

        public void SetPathTarget(Vector2 target)
        {
            currentWaypoint = 0;
            path = null;
            seeker.StartPath(seeker.transform.position, target, p => path = p);
        }

        private void CheckPath()
        {
            if (!Actions.Player.TaptoMove.IsPressed() || UiManager.Instance.isDragging) return;

            var pos = Actions.Player.PointerPosition.ReadValue<Vector2>();
            var target = Camera.main!.ScreenToWorldPoint(pos);

            var uiResults = EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
            {
                position = pos,
                button = PointerEventData.InputButton.Left
            });

            if (uiResults.Any(hit => hit.gameObject.HasComponent<CanvasRenderer>())) return;

            var objectResults = Physics2D.RaycastAll(target, Vector2.zero);

            foreach (var result in objectResults)
            {
                if (result.transform.gameObject.HasComponent(out InteractableObject interactable) && interactable.CanBeInteractedWith())
                {
                    interactable.Interact();
                    return;
                }
            }

            SetPathTarget(target);
        }

        private void Update()
        {
            // Tap to Move
            if (Actions.Player.TaptoMove.WasPressedThisFrame())
            {
                CheckPath();
            }

            // WASD movement
            if (Actions.Player.Move.IsPressed())
            {
                ProcessManualMovement();
            }

            // Pathfinding
            if (path is {error: false})
            {
                ProcessTapToMove();
            }
        }

        private void ProcessManualMovement()
        {
            var input = Actions.Player.Move.ReadValue<Vector2>();
            var cat = UnityState.Instance.cat;
            cat.data.facing = input;

            var movement = input * (Time.deltaTime * Speed);

            cat.transform.Translate(movement.x, movement.y, 0);
            cat.SyncToData();
            path = null;
        }

        private void ProcessTapToMove()
        {
            // Check in a loop if we are close enough to the current waypoint to switch to the next one.
            // We do this in a loop because many waypoints might be close to each other and we may reach
            // several of them in the same frame.
            var reachedEndOfPath = false;
            // The distance to the next waypoint in the path
            float distanceToWaypoint;
            while (true)
            {
                distanceToWaypoint = Vector3.Distance(seeker.transform.position, path.vectorPath[currentWaypoint]);

                if (distanceToWaypoint > NextWaypointDistance) break;

                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    reachedEndOfPath = true;
                    break;
                }
            }
            // Slow down smoothly upon approaching the end of the path
            // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
            var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / NextWaypointDistance) : 1f;
            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            Vector3 dir = (path.vectorPath[currentWaypoint] - seeker.transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            Vector3 velocity = dir * (Speed * speedFactor);

            // If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
            var cat = UnityState.Instance.cat;
            cat.data.facing = dir;
            seeker.transform.Translate(velocity * Time.deltaTime);
            cat.SyncToData();
        }
    }
}