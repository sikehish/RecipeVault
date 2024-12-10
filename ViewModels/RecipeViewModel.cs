using System.Collections.ObjectModel;
using RecipeManager.Models;
using RecipeManager.Data;

namespace RecipeManager.ViewModels;

public class RecipeViewModel
{
    public ObservableCollection<Recipe> Recipes { get; set; } = new();

    public RecipeViewModel()
    {
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        foreach (var recipe in db.Recipes)
            Recipes.Add(recipe);
    }

    public void AddRecipe(string name, string ingredients, string instructions)
    {
        var recipe = new Recipe { Name = name, Ingredients = ingredients, Instructions = instructions };
        using var db = new AppDbContext();
        db.Recipes.Add(recipe);
        db.SaveChanges();
        Recipes.Add(recipe);
    }
}
