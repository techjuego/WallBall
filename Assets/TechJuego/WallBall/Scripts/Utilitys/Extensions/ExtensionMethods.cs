using UnityEngine;
namespace TechJuego.Framework
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object value)
        {
            return JsonUtility.ToJson(value);
        }
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}