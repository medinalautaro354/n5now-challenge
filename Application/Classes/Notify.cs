using System.Runtime.CompilerServices;

namespace N5NowApi.Application.Classes;

public class Notify
{
    public string? Code { get; set; }

    public string? Property { get; set; }

    public string? Message { get; set; }

    public override string ToString()
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Notify - Code: ");
        defaultInterpolatedStringHandler.AppendFormatted(Code);
        defaultInterpolatedStringHandler.AppendLiteral(", Property: ");
        defaultInterpolatedStringHandler.AppendFormatted(Property);
        defaultInterpolatedStringHandler.AppendLiteral(", Message: ");
        defaultInterpolatedStringHandler.AppendFormatted(Message);
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }
}
