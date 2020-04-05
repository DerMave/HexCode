
namespace HexCode.Common
{
    public interface IRadioMessage
    {
        Location Location { get; }

        byte[] Data { get; }
    }
}
