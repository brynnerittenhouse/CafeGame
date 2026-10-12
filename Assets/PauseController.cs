using System.Net.NetworkInformation;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPaused {get; private set;} = false;
    public static void Pause(bool pause)
    {
        isPaused = pause;
    }
}
