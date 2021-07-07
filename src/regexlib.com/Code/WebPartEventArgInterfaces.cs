using System.Text.RegularExpressions;

namespace RegExLib {

    public enum RunatMode {
        None,
        Server,
        Client,
    }

    public interface IModeSettings {
        RegexOptions Options { get; }
        RunatMode RunatMode { get; }
    }

    public interface IInputSettings {
        string Source { get; }
        string Pattern { get; }
    }
}