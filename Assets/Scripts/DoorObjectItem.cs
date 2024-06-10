using DG.Tweening;
using UnityEngine;

namespace PortalTest.Objects
{
    public class DoorObjectItem : MonoBehaviour
    {
        [SerializeField] private Transform doorTransform;
        [SerializeField] private Vector3 doorClosedRotation;
        [SerializeField] private Vector3 doorOpenedRotation;
        [SerializeField] private bool isOpened = false;

        private void Awake()
        {
            if (doorTransform == null)
            {
                doorTransform = GetComponent<Transform>();
            }
        }

        public void OpenCloseDoor()
        {
            Vector3 neededRotation = isOpened ? doorClosedRotation : doorOpenedRotation;
            isOpened = !isOpened;
            doorTransform.DOLocalRotate(neededRotation, 1);
        }
    }
}
