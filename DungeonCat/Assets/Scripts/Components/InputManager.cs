using System;
using System.Collections.Generic;
using Input;
using Pathfinding;
using UnityEngine;

namespace Scripts.Components
{
    public class InputManager : MonoBehaviour
    {
        private Seeker seeker;
        private Path path;

        private float nextWaypointDistance = 3;
        private int currentWaypoint = 0;
        private float speed = 2;

        private static InputActions actions;

        private bool reachedEndOfPath;

        private void Start()
        {
            seeker = UnityState.Instance.cat.GetComponent<Seeker>();
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

        private void Update()
        {
            if (Actions.Player.TaptoMove.WasPressedThisFrame())
            {
                var pos = Actions.Player.PointerPosition.ReadValue<Vector2>();
                var target = Camera.main!.ScreenToWorldPoint(pos);
                Debug.Log($"Tap to move {target}");

                SetPathTarget(target);

                /*var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(EventSystem.current., results);
                */
            }

            if (Actions.Player.Move.IsPressed())
            {
                var input = Actions.Player.Move.ReadValue<Vector2>();
                var cat = UnityState.Instance.cat;
                cat.data.facing = input;

                var movement = input * (Time.deltaTime * 50);

                cat.transform.Translate(movement.x, movement.y, 0);
                cat.SyncToData();
                path = null;
            }

            if (path is {error: false})
            {
                // Check in a loop if we are close enough to the current waypoint to switch to the next one.
                // We do this in a loop because many waypoints might be close to each other and we may reach
                // several of them in the same frame.
                reachedEndOfPath = false;
                // The distance to the next waypoint in the path
                float distanceToWaypoint;
                while (true)
                {
                    distanceToWaypoint = Vector3.Distance(seeker.transform.position, path.vectorPath[currentWaypoint]);
                    if (distanceToWaypoint < nextWaypointDistance)
                    {
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
                    else
                    {
                        break;
                    }
                }
                // Slow down smoothly upon approaching the end of the path
                // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
                var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
                // Direction to the next waypoint
                // Normalize it so that it has a length of 1 world unit
                Vector3 dir = (path.vectorPath[currentWaypoint] - seeker.transform.position).normalized;
                // Multiply the direction by our desired speed to get a velocity
                Vector3 velocity = dir * (speed * speedFactor);

                // If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
                var cat = UnityState.Instance.cat;
                cat.data.facing = dir;
                seeker.transform.Translate(velocity * Time.deltaTime * 20);
                cat.SyncToData();
            }

        }
    }
}