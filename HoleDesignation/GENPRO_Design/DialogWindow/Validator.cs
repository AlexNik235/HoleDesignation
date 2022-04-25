using System.Collections.Generic;
using System.Linq;

namespace GENPRO_Design.DialogWindow
{
    public static class Validator
    {
        public static bool ValidateName(string newName, int minCountSymbols, int maxCountSymbols, out string errorMassege, List<string> existNames = null, string oldName = null)
        {
            errorMassege = string.Empty;
            List<string> specialSymbols = new List<string> { "_" };

            if (!string.IsNullOrWhiteSpace(newName))
            {
                if (existNames?.Contains(newName) == true)
                    errorMassege = "Такое имя уже существует.";
                else if (specialSymbols.Any(s => newName.Contains(s)))
                    errorMassege = $"Имя не должно содержать следующих символов: \"{string.Join(", ", specialSymbols.Select(a => a == "_" ? "__" : a).ToList())}.\"";
                else if (newName.Count() >= 0 && newName.Count() <= minCountSymbols)
                    errorMassege = $"Имя должно содержать более {minCountSymbols} символов.";
                else if (newName.Count()> maxCountSymbols)
                    errorMassege = $"Имя должно содержать не более {maxCountSymbols} символов.";
                else if (newName == oldName)
                    errorMassege = "Новое имя должно отличаться от старого.";
                else
                    return true;
            }
            else
                errorMassege = "Введите имя.";
            return default;
        }
    }
}
