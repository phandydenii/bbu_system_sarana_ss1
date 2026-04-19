namespace BBU_SYSTEM.Helper;

public class MenuItem
{
    public string? Title { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Icon { get; set; }
    public List<MenuItem>? Children { get; set; } = new();
}

public class Menu
{
    public List<MenuItem> MenuItems()
    {
        List<MenuItem> menuItems =
        [
            new()
            {
                Title = "ទំព័រដើម",
                Controller = "Home",
                Action = "Index",
                Icon = "nav-icon fas fa-tachometer-alt",
                Children = null
            },

            new()
            {
                Title = "រដ្ឋបាល",
                Icon = "fa-solid fa-circle-info",
                Controller = "Payment",
                Action = "",
                Children =
                [
                    new MenuItem
                    {
                        Title = "ចូះឈ្មោះ",
                        Controller = "registry",
                        Action = "index",
                        Icon = "far fa-circle nav-icon",
                    },

                    new MenuItem
                    {
                        Title = "បង់ថ្លៃសិក្សា",
                        Controller = "Payment",
                        Action = "Create",
                        Icon = "far fa-circle nav-icon",
                    },

                    new MenuItem
                    {
                        Title = "បញ្ចុះថ្លៃសិក្សា",
                        Controller = "Administration",
                        Action = "Discount",
                        Icon = "far fa-circle nav-icon",
                    },

                    new MenuItem
                    {
                        Title = "កក់សម្លៀកបំពាក់",
                        Controller = "Administration",
                        Action = "BookingClothes",
                        Icon = "far fa-circle nav-icon",
                    },

                    new MenuItem
                    {
                        Title = "សញ្ញាបត្រ",
                        Icon = "far fa-circle nav-icon",
                        Children =
                        [
                            new MenuItem
                            {
                                Title = "លិខិតបញ្ចប់ការសិក្សា",
                                Controller = "Administration",
                                Action = "GraduateCertificate",
                                Icon = "far fa-dot-circle nav-icon",
                            },

                            new MenuItem
                            {
                                Title = "លិខិតរដ្ឋបាល",
                                Controller = "Administration",
                                Action = "AdminLetter",
                                Icon = "far fa-dot-circle nav-icon",
                            },

                            new MenuItem
                            {
                                Title = "ប្រភេទលិខិត",
                                Controller = "Administration",
                                Action = "CategoryLetter",
                                Icon = "far fa-dot-circle nav-icon",
                            }
                        ]
                    }
                ]
            }
        ];
        return menuItems;
    }
}