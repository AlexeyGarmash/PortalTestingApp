using UnityEngine;

namespace PortalTest.Objects
{
    public class RefrigeratorObject : InteractableObject
    {
        [SerializeField] private DoorObjectItem leftDoor;
        [SerializeField] private DoorObjectItem rightDoor;

        void Start()
        {
            
        }

        protected override void DoInteract()
        {
            base.DoInteract();
            leftDoor.OpenCloseDoor();
            rightDoor.OpenCloseDoor();
        }
    }
}
