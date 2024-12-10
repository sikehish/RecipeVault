using RecipeManager.Models;
using RecipeManager.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace RecipeManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Recipe> Recipes { get; set; } = new();
        public Recipe NewRecipe { get; set; } = new();  // New property for the current recipe being added

        private string _nameError;
        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }

        private string _ingredientsError;
        public string IngredientsError
        {
            get => _ingredientsError;
            set
            {
                _ingredientsError = value;
                OnPropertyChanged(nameof(IngredientsError));
            }
        }

        private string _instructionsError;
        public string InstructionsError
        {
            get => _instructionsError;
            set
            {
                _instructionsError = value;
                OnPropertyChanged(nameof(InstructionsError));
            }
        }

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
            // Validate the input
            bool isValid = ValidateInputs();

            if (isValid)
            {
                // Add the NewRecipe to the database and ObservableCollection
                using var db = new AppDbContext();
                db.Recipes.Add(NewRecipe);
                db.SaveChanges();
                Recipes.Add(NewRecipe);

                // Clear the NewRecipe after adding
                NewRecipe = new Recipe();
                NameError = IngredientsError = InstructionsError = string.Empty; // Clear error messages
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            // Check if the Name is entered
            if (string.IsNullOrWhiteSpace(NewRecipe.Name))
            {
                NameError = "Recipe Name is required.";
                isValid = false;
            }
            else
            {
                NameError = string.Empty;
            }

            // Check if Ingredients are entered
            if (string.IsNullOrWhiteSpace(NewRecipe.Ingredients))
            {
                IngredientsError = "Ingredients are required.";
                isValid = false;
            }
            else
            {
                IngredientsError = string.Empty;
            }

            // Check if Instructions are entered
            if (string.IsNullOrWhiteSpace(NewRecipe.Instructions))
            {
                InstructionsError = "Instructions are required.";
                isValid = false;
            }
            else
            {
                InstructionsError = string.Empty;
            }

            return isValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
