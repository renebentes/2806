namespace Blog.Controls;

public static class Menu
{
    public static MenuItem Create(IReadOnlyList<MenuItem> menuItems)
        => Prompt(new SelectionPrompt<MenuItem>()
                .Title("O que você deseja fazer?")
                .AddChoices(menuItems)
                .UseConverter((menu) => menu.Title));
}
