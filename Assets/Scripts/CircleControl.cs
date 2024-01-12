using UnityEngine;

namespace ColorSwitch
{
    public class CircleControl : MonoBehaviour
    {
        [SerializeField] public bool solaDon = true;
        [SerializeField] private float MinDonmeHizi;
        [SerializeField] private float MaxDonmeHizi;
        private float donmeHizi;

        private void Start()
        {
            RandomDonmeHizi();
        }
        private void Update()
        {
            if (solaDon)
            {
                transform.Rotate(0f, 0f, donmeHizi * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0f, 0f, -donmeHizi * Time.deltaTime);
            }
        }
        public void RandomDonmeHizi()
        {
            donmeHizi = Random.Range(MinDonmeHizi, MaxDonmeHizi);
        }
    }
}