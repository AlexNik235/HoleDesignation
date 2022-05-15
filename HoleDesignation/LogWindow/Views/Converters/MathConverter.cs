namespace LogWindow.Views.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// Преобразователь значений, который выполняет арифметические вычисления над своим аргументом.
    /// </summary>
    /// <remarks>
    /// ConverterParameter должен содержать арифметическое выражение над аргументами преобразователя.
    /// Поддерживаемые операции: +, -, * и /
    /// Один аргумент преобразователя значений может обозначаться как x, a или {0}
    /// Аргументы многозначного преобразователя могут обозначаться как x, y, z, t
    /// (первый-четвертый аргумент) или a, b, c, d или {0}, {1}, {2}, {3 }, {4}, ...
    /// Конвертер поддерживает арифметические выражения произвольной сложности, включая вложенные подвыражения.
    /// </remarks>
    public class MathConverter : MarkupExtension, IMultiValueConverter, IValueConverter
    {
        private readonly Dictionary<string, IExpression> _storedExpressions = new Dictionary<string, IExpression>();

        /// <summary>
        /// Расширение.
        /// </summary>
        private interface IExpression
        {
            /// <summary>
            /// Реализация вычислителя.
            /// </summary>
            /// <param name="args">Входные параметры.</param>
            /// <returns></returns>
            decimal Eval(object[] args);
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(new[] { value }, targetType, parameter, culture);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var result = Parse(parameter.ToString())
                    .Eval(values);
                if (targetType == typeof(decimal))
                    return result;
                if (targetType == typeof(string))
                    return result.ToString(CultureInfo.CurrentCulture);
                if (targetType == typeof(int))
                    return (int)result;
                if (targetType == typeof(double))
                    return (double)result;
                if (targetType == typeof(long))
                    return (long)result;
                throw new ArgumentException($"Unsupported target type {targetType.FullName}");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// Обработка исключения.
        /// </summary>
        /// <param name="ex">Исключение.</param>
        protected static void ProcessException(Exception ex) => Console.WriteLine(ex.Message);

        private IExpression Parse(string s)
        {
            if (_storedExpressions.TryGetValue(s, out var result))
                return result;

            result = new Parser().Parse(s);
            _storedExpressions[s] = result;

            return result;
        }

        private class Constant : IExpression
        {
            private readonly decimal _value;

            public Constant(string text)
            {
                if (!decimal.TryParse(text, out _value))
                    throw new ArgumentException($"'{text}' is not a valid number");
            }

            public decimal Eval(object[] args) => _value;
        }

        private class Variable : IExpression
        {
            private readonly int _index;

            public Variable(string text)
            {
                if (!int.TryParse(text, out _index) || _index < 0)
                    throw new ArgumentException($"'{text}' is not a valid parameter index");
            }

            public Variable(int n) => _index = n;

            public decimal Eval(object[] args)
            {
                if (_index >= args.Length)
                {
                    throw new ArgumentException(
                        $"MathConverter: parameter index {_index} is out of range. {args.Length} parameter(s) supplied");
                }

                return System.Convert.ToDecimal(args[_index]);
            }
        }

        private class BinaryOperation : IExpression
        {
            private readonly Func<decimal, decimal, decimal> _operation;
            private readonly IExpression _left;
            private readonly IExpression _right;

            public BinaryOperation(char operation, IExpression left, IExpression right)
            {
                _left = left;
                _right = right;
                _operation = operation switch
                {
                    '+' => (a, b) => (a + b),
                    '-' => (a, b) => (a - b),
                    '*' => (a, b) => (a * b),
                    '/' => (a, b) => (a / b),
                    _ => throw new ArgumentException("Invalid operation " + operation)
                };
            }

            public decimal Eval(object[] args)
            {
                return _operation(_left.Eval(args), _right.Eval(args));
            }
        }

        private class Negate : IExpression
        {
            private readonly IExpression _param;

            public Negate(IExpression param)
            {
                _param = param;
            }

            public decimal Eval(object[] args)
            {
                return -_param.Eval(args);
            }
        }

        private class Parser
        {
            private string _text;
            private int _pos;

            public IExpression Parse(string text)
            {
                try
                {
                    _pos = 0;
                    _text = text;
                    var result = ParseExpression();
                    RequireEndOfText();
                    return result;
                }
                catch (Exception ex)
                {
                    var msg = $"MathConverter: error parsing expression '{text}'. {ex.Message} at position {_pos}";
                    throw new ArgumentException(msg, ex);
                }
            }

            private IExpression ParseExpression()
            {
                var left = ParseTerm();

                while (true)
                {
                    if (_pos >= _text.Length)
                        return left;

                    var c = _text[_pos];

                    if (c == '+' || c == '-')
                    {
                        ++_pos;
                        var right = ParseTerm();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseTerm()
            {
                var left = ParseFactor();

                while (true)
                {
                    if (_pos >= _text.Length)
                        return left;

                    var c = _text[_pos];

                    if (c == '*' || c == '/')
                    {
                        ++_pos;
                        var right = ParseFactor();
                        left = new BinaryOperation(c, left, right);
                    }
                    else
                    {
                        return left;
                    }
                }
            }

            private IExpression ParseFactor()
            {
                while (true)
                {
                    SkipWhiteSpace();
                    if (_pos >= _text.Length)
                        throw new ArgumentException("Unexpected end of text");

                    var c = _text[_pos];

                    switch (c)
                    {
                        case '+':
                            ++_pos;
                            continue;
                        case '-':
                            ++_pos;
                            return new Negate(ParseFactor());
                        case 'x':
                        case 'a':
                            return CreateVariable(0);
                        case 'y':
                        case 'b':
                            return CreateVariable(1);
                        case 'z':
                        case 'c':
                            return CreateVariable(2);
                        case 't':
                        case 'd':
                            return CreateVariable(3);
                        case '(':
                        {
                            ++_pos;
                            var expression = ParseExpression();
                            SkipWhiteSpace();
                            Require(')');
                            SkipWhiteSpace();
                            return expression;
                        }

                        case '{':
                        {
                            ++_pos;
                            var end = _text.IndexOf('}', _pos);
                            if (end < 0)
                            {
                                --_pos;
                                throw new ArgumentException("Unmatched '{'");
                            }

                            if (end == _pos)
                            {
                                throw new ArgumentException("Missing parameter index after '{'");
                            }

                            var result = new Variable(_text.Substring(_pos, end - _pos).Trim());
                            _pos = end + 1;
                            SkipWhiteSpace();
                            return result;
                        }
                    }

                    const string decimalRegEx = @"(\d+\.?\d*|\d*\.?\d+)";
                    var match = Regex.Match(_text.Substring(_pos), decimalRegEx);
                    if (!match.Success)
                        throw new ArgumentException($"Unexpected character '{c}'");
                    _pos += match.Length;
                    SkipWhiteSpace();
                    return new Constant(match.Value);
                }
            }

            private IExpression CreateVariable(int n)
            {
                ++_pos;
                SkipWhiteSpace();
                return new Variable(n);
            }

            private void SkipWhiteSpace()
            {
                while (_pos < _text.Length && char.IsWhiteSpace(_text[_pos]))
                    ++_pos;
            }

            private void Require(char c)
            {
                if (_pos >= _text.Length || _text[_pos] != c)
                    throw new ArgumentException("Expected '" + c + "'");
                ++_pos;
            }

            private void RequireEndOfText()
            {
                if (_pos != _text.Length)
                    throw new ArgumentException("Unexpected character '" + _text[_pos] + "'");
            }
        }
    }
}
