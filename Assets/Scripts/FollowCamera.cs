using UnityEngine;

namespace ColorSwitch
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform top;

        private void Update()
        {

            if (top.position.y >= transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, top.position.y, transform.position.z);
            }
        }
    }
}