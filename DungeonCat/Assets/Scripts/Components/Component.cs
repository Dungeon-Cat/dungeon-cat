using Data;
using UnityEngine;

namespace Components
{
    public abstract class Component<T> : MonoBehaviour
    {
        public T data;
    }
}