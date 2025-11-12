using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalTypes : Resource
{
    public enum Unit
    {
        NONE,
        MILLILITERS,
        GRAMS,
        TEASPOON,
        TABLESPOON,
        PACKET,
        BUNCH,
        DROP,
        PINCH

    };

    public static Godot.Collections.Dictionary<Unit, string> UnitLabels = new Godot.Collections.Dictionary<Unit, string>{
        {Unit.NONE, ""},
        {Unit.MILLILITERS, "mL"},
        {Unit.GRAMS, "g"},
        {Unit.TEASPOON, "Tl" },
        {Unit.TABLESPOON, "El"},
        {Unit.PACKET, "Pck"},
        {Unit.BUNCH, "Bd"},
        {Unit.DROP, "Tr"},
        {Unit.PINCH, "Pr"}
    };

    public static Godot.Collections.Dictionary<Unit, string> UnitText = new Godot.Collections.Dictionary<Unit, string>{
        {Unit.NONE, ""},
        {Unit.MILLILITERS, "Milliliter"},
        {Unit.GRAMS, "Gramm"},
        {Unit.TEASPOON, "Teelöffel" },
        {Unit.TABLESPOON, "Esslöffel"},
        {Unit.PACKET, "Päckchen"},
        {Unit.BUNCH, "Bund"},
        {Unit.DROP, "Tropfen"},
        {Unit.PINCH, "Prise"}
    };

    public enum Tag
    {
        SAVOURY,
        SWEET,
        SPICY,
        APPETIZER,
        MAIN_DISH,
        DESSERT,
        FINGERFOOD,
        SALAD,
        BAKE,
        SNACKS,
        SWEETS,
        VEGETARIAN,
        VEGAN,
        LACTOSE_FREE,
        FAST,
        EASY_TO_PREP,
        SOUP,
        DIP,
        COOKIES,
        CAKE,
        SAUCE,
        BREAD,
        GARNISH,
        DRINKS,
        FISH,
        MEAT,
        ALCOHOL,
    }

    public static Godot.Collections.Dictionary<Tag, string> Tags = new Godot.Collections.Dictionary<Tag, string>
    {

        {Tag.ALCOHOL, "alkoholisch"},
        {Tag.APPETIZER, "Vorspeise"},
        {Tag.BAKE, "Auflauf"},
        {Tag.BREAD, "Backwaren"},
        {Tag.CAKE, "Kuchen/Torte"},
        {Tag.COOKIES, "Kekse"},
        {Tag.DESSERT, "Dessert"},
        {Tag.DIP, "Dip/Aufstrich"},
        {Tag.DRINKS, "Getränke"},
        {Tag.EASY_TO_PREP, "gut vorzubereiten"},
        {Tag.FAST, "schnell" },
        {Tag.FINGERFOOD, "Fingerfood"},
        {Tag.FISH, "Fisch"},
        {Tag.GARNISH, "Beilage"},
        {Tag.LACTOSE_FREE, "lactosefrei"},
        {Tag.MAIN_DISH, "Hauptgericht"},
        {Tag.MEAT, "Fleisch"},
        {Tag.SALAD, "Salat"},
        {Tag.SAUCE, "Soße"},
        {Tag.SAVOURY, "herzhaft"},
        {Tag.SNACKS, "Knabberzeug"},
        {Tag.SOUP, "Suppe"},
        {Tag.SPICY, "scharf"},
        {Tag.SWEET, "süß"},
        {Tag.SWEETS, "Süßigkeiten"},
        {Tag.VEGETARIAN, "vegetarisch"},
        {Tag.VEGAN, "vegan"},
    };
}


