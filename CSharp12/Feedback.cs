namespace CSharp12;

// We receive warnings here because the parameters are used for initialization but are still referenced in methods, calculated properties, etc.
// public class Feedback(string username, string content)
// {
//     public string Username { get; } = username;
//     public string Content { get; } = content;
//
//     public int Sentiment => content.Length - username.Length * 10;
// }

public class Feedback(string username, string content)
{
    public string Username { get; } = username;
    public string Content { get; } = content;

    public int Sentiment => this.Content.Length - this.Username.Length * 10;
}