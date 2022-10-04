namespace Blog.Controls;

public class Message
{
    public static void Show(string message)
    {
        MarkupLine(message);
        Thread.Sleep(2000);
    }
}
