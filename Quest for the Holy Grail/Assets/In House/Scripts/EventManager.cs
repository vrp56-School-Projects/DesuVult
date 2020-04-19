using System;

public class EventManager
{
    public static event Action<float> HealthPickup;
    public static void CallHealthPickup (float value) {
        HealthPickup?.Invoke(value);
    }

    public static event Action<float> StaminaPickup;
    public static void CallStaminaPickup (float value) {
        StaminaPickup?.Invoke(value);
    }

    public static event Action<float> DeusVultPickup;
    public static void CallDeusVultPickup (float value) {
        DeusVultPickup?.Invoke(value);
    }

}
