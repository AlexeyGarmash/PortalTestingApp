using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalTest.Objects
{
    public class OvenObject : InteractableObject
    {
        [SerializeField] private DoorObjectItem doorItem;

        protected override void DoInteract()
        {
            base.DoInteract();
            doorItem.OpenCloseDoor();
        }
    }
}
