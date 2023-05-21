using UnityEngine;

namespace CodeBase.Game.Logic
{
    public abstract class Selectable : MonoBehaviour, ISelectable
    {
        public Transform Transform => transform;
    }
}
