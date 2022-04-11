namespace Cortex.Core.Abstractions.Extensions
{
    public interface IExtensionStartup
    {
        bool Handle(object content);
    }
}
