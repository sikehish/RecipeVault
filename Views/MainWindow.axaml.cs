using Avalonia.Controls;
using RecipeManager.ViewModels;  // Add the missing semicolon here

namespace RecipeManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
