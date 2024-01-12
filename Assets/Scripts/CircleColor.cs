using UnityEngine;

namespace ColorSwitch
{
    public enum Renkler
    {
        Turkuaz, Sari, Kirmizi, Mor
    }

    public class CircleColor : MonoBehaviour
    {
        [SerializeField] public Renkler renk;
    }
}