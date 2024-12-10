using RecipeManager.Models;
using RecipeManager.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RecipeManager.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; } = new();
        public Recipe NewRecipe { get; set; } = new();  // New property for the current recipe being added

        public ICommand AddRecipeCommand { get; }

        public MainWindowViewModel()
        {
            AddRecipeCommand = new RelayCommand(AddRecipe);

            // Load existing recipes from the database
            using var db = new AppDbContext();
            db.Database.EnsureCreated();
            foreach (var recipe in db.Recipes)
                Recipes.Add(recipe);
        }

        private void AddRecipe()
        {
            // Add the NewRecipe to the database and ObservableCollection
            using var db = new AppDbContext();
            db.Recipes.Add(NewRecipe);
            db.SaveChanges();
            Recipes.Add(NewRecipe);

            // Clear the NewRecipe after adding
            NewRecipe = new Recipe();
        }
    }
}
