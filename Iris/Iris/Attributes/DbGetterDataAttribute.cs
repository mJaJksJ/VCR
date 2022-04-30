namespace Iris.Attributes
{
    /// <summary>
    /// Атрибут показателя метода непосредственно взаимодействующего с бд
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DbGetterDataAttribute : Attribute
    {
    }
}
