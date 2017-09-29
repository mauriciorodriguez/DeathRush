using UnityEngine;

public static class ColorTypeConverter
{
    /// <summary>
    /// Devuelve el codigo hexadecimal de un color desde un RGBA
    /// </summary>
    /// <param name="c">Color</param>
    /// <returns>Color en hexadecimal "#000000"</returns>
    public static string ToRGBHex(Color c)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    private static byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }
}