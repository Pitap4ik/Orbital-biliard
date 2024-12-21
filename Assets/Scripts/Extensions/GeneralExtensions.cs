using UnityEngine;

public static class GeneralExtensions
{
    public static bool HasComponent <T>(this GameObject obj) where T:Component
    {
        return obj.GetComponent<T>() != null;
    }
}
