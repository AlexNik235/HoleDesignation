namespace HoleDesignation.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Класс логера
    /// </summary>
    public class Loger
    {
        private Dictionary<string, int> _errors = new Dictionary<string, int>();

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        /// <param name="error">Ошибка</param>
        public void AddProblem(string error)
        {
            if (_errors.ContainsKey(error))
            {
                _errors[error]++;
            }
            else
            {
                _errors.Add(error, 1);
            }
        }

        /// <summary>
        /// Получить сгруппированную строку
        /// </summary>
        /// <returns></returns>
        public string GetConbinatedProblem()
        {
            if (!_errors.Any())
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var error in _errors)
            {
                sb.Append($"\n{error.Key}, кол-во повторений ошибки: {error.Value}\n");
            }

            return sb.ToString();
        }
    }
}
