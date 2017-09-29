public interface IReusable
{
    /// <summary>
    /// Cuando se obtiene el objeto.
    /// </summary>
    void OnAcquire();

    /// <summary>
    /// Cuando se libera el objeto.
    /// </summary>
    void OnRelease();

    /// <summary>
    /// Cuando se crea el objeto.
    /// </summary>
    void OnCreate();
}