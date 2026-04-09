using GymTracer.DataValidator;
using System.Collections;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GymTracer.Extensions
{
    public static partial class ValidatorExtension
    {
        #region STRING
        // string NotEmpty is moved to and is merged with collection NotEmpty
        public static ValidatorChain<T> MinLength<T>(this ValidatorChain<T> chain, int length) where T : IEnumerable<char>?
        {
            if (chain.ValidationField is not string stringValue)
                return chain;

            if (!chain.HasFailed && stringValue.Length < length)
                return chain.AddError($"A(z) {chain.DisplayName} legalább {length} hosszú kell legyen.");
            return chain;
        }
        public static ValidatorChain<T> MaxLength<T>(this ValidatorChain<T> chain, int length) where T : IEnumerable<char>?
        {
            if (chain.ValidationField is not string stringValue)
                return chain;

            if (!chain.HasFailed && stringValue.Length > length)
                return chain.AddError($"A(z) {chain.DisplayName} legfeljebb {length} hosszú kell legyen.");
            return chain;
        }

        [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        private static partial Regex EmailRegex();
        public static ValidatorChain<T> Email<T>(this ValidatorChain<T> chain) where T : IEnumerable<char>?
        {
            if (chain.ValidationField is not string stringValue)
                return chain;

            if (!chain.HasFailed && !EmailRegex().IsMatch(stringValue))
                return chain.AddError($"A(z) {chain.DisplayName} nem valid E-mail címként.");
            return chain;
        }
        public static ValidatorChain<T> Matches<T>(this ValidatorChain<T> chain, Regex regex, string? customMessage = null) where T : IEnumerable<char>?
        {
            if (chain.ValidationField is not string stringValue)
                return chain;

            if (!chain.HasFailed && !regex.IsMatch(stringValue))
            {
                string message = customMessage ?? $"A(z) {chain.DisplayName} nem felelt meg a validációs feltételnek";
                return chain.AddError(message);
            }
            return chain;
        }
        public static ValidatorChain<T> Matches<T>(this ValidatorChain<T> chain, string regexString, string? customMessage = null) where T : IEnumerable<char>?
        {
            if (chain.ValidationField is not string stringValue)
                return chain;

            if (!chain.HasFailed && !Regex.IsMatch(stringValue, regexString))
            {
                string message = customMessage ?? $"A(z) {chain.DisplayName} nem felelt meg a validációs feltételnek";
                return chain.AddError(message);
            }
            return chain;
        }

        #endregion
        #region NUMERIC
        public static ValidatorChain<TNum> Min<TNum>(this ValidatorChain<TNum> chain, TNum min) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField < min)
                return chain.AddError($"A(z) {chain.DisplayName} minimum {min} lehet");
            return chain;
        }
        public static ValidatorChain<TNum> GreaterThan<TNum>(this ValidatorChain<TNum> chain, TNum min) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField <= min)
                return chain.AddError($"A(z) {chain.DisplayName} nagyobb kell legyen, mint {min}");
            return chain;
        }
        public static ValidatorChain<TNum> Max<TNum>(this ValidatorChain<TNum> chain, TNum max) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField > max)
                return chain.AddError($"A(z) {chain.DisplayName} maximum {max} lehet");
            return chain;
        }
        public static ValidatorChain<TNum> LessThan<TNum>(this ValidatorChain<TNum> chain, TNum max) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField >= max)
                return chain.AddError($"A(z) {chain.DisplayName} kisebb kell legyen, mint {max}");
            return chain;
        }
        public static ValidatorChain<TNum> Between<TNum>(this ValidatorChain<TNum> chain, TNum min, TNum max) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed && 
                (chain.ValidationField < min || chain.ValidationField > max))
                return chain.AddError($"A(z) {chain.DisplayName} minimum {min} és maximum {max} lehet");
            return chain;
        }
        public static ValidatorChain<TNum> BetweenStrict<TNum>(this ValidatorChain<TNum> chain, TNum min, TNum max) where TNum : INumber<TNum>
        {
            if (!chain.HasFailed &&
                (chain.ValidationField <= min || chain.ValidationField >= max))
                return chain.AddError($"A(z) {chain.DisplayName} szigorúan {min} és {max} között kell legyen");
            return chain;
        }
        // Nullable numeric general overloads
        public static ValidatorChain<TNum?> Min<TNum>(this ValidatorChain<TNum?> chain, TNum min) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value < min)
                return chain.AddError($"A(z) {chain.DisplayName} minimum {min} lehet");
            return chain;
        }
        public static ValidatorChain<TNum?> GreaterThan<TNum>(this ValidatorChain<TNum?> chain, TNum min) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value <= min)
                return chain.AddError($"A(z) {chain.DisplayName} nagyobb kell legyen, mint {min}");
            return chain;
        }
        public static ValidatorChain<TNum?> Max<TNum>(this ValidatorChain<TNum?> chain, TNum max) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value > max)
                return chain.AddError($"A(z) {chain.DisplayName} maximum {max} lehet");
            return chain;
        }
        public static ValidatorChain<TNum?> LessThan<TNum>(this ValidatorChain<TNum?> chain, TNum max) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value >= max)
                return chain.AddError($"A(z) {chain.DisplayName} kisebb kell legyen, mint {max}");
            return chain;
        }
        public static ValidatorChain<TNum?> Between<TNum>(this ValidatorChain<TNum?> chain, TNum min, TNum max) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue &&
                (chain.ValidationField.Value < min || chain.ValidationField.Value > max))
                return chain.AddError($"A(z) {chain.DisplayName} minimum {min} és maximum {max} lehet");
            return chain;
        }
        public static ValidatorChain<TNum?> BetweenStrict<TNum>(this ValidatorChain<TNum?> chain, TNum min, TNum max) where TNum : struct, INumber<TNum>
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue &&
                (chain.ValidationField.Value <= min || chain.ValidationField.Value >= max))
                return chain.AddError($"A(z) {chain.DisplayName} szigorúan {min} és {max} között kell legyen");
            return chain;
        }
        #endregion
        #region DATETIME
        public static ValidatorChain<DateTime> Before(this ValidatorChain<DateTime> chain, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField > maxDate)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet {maxDate:yyyy.MM.dd. HH:mm} után");
            return chain;
        }
        public static ValidatorChain<DateTime> BeforeStrict(this ValidatorChain<DateTime> chain, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField >= maxDate)
                return chain.AddError($"A(z) {chain.DisplayName} korábban kell legyen, mint {maxDate:yyyy.MM.dd. HH:mm}");
            return chain;
        }
        public static ValidatorChain<DateTime> After(this ValidatorChain<DateTime> chain, DateTime minDate)
        {
            if (!chain.HasFailed && chain.ValidationField < minDate)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet {minDate:yyyy.MM.dd. HH:mm} előtt");
            return chain;
        }
        public static ValidatorChain<DateTime> AfterStrict(this ValidatorChain<DateTime> chain, DateTime minDate)
        {
            if (!chain.HasFailed && chain.ValidationField <= minDate)
                return chain.AddError($"A(z) {chain.DisplayName} később kell legyen, mint {minDate:yyyy.MM.dd. HH:mm}");
            return chain;
        }
        public static ValidatorChain<DateTime> Between(this ValidatorChain<DateTime> chain, DateTime minDate, DateTime maxDate)
        {
            if (!chain.HasFailed &&
                (chain.ValidationField < minDate || chain.ValidationField > maxDate))
                return chain.AddError($"A(z) {chain.DisplayName} {minDate:yyyy.MM.dd. HH:mm} és {maxDate:yyyy.MM.dd. HH:mm} között kell legyen");
            return chain;
        }
        public static ValidatorChain<DateTime> BetweenStrict(this ValidatorChain<DateTime> chain, DateTime minDate, DateTime maxDate)
        {
            if (!chain.HasFailed &&
                (chain.ValidationField <= minDate || chain.ValidationField >= maxDate))
                return chain.AddError($"A(z) {chain.DisplayName} szigorúan {minDate:yyyy.MM.dd. HH:mm} és {maxDate:yyyy.MM.dd. HH:mm} között kell legyen");
            return chain;
        }
        public static ValidatorChain<DateTime> NotDefault(this ValidatorChain<DateTime> chain)
        {
            if (!chain.HasFailed && chain.ValidationField == default)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet üres");
            return chain;
        }
        // Nullable DateTime overloads
        public static ValidatorChain<DateTime?> Before(this ValidatorChain<DateTime?> chain, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value > maxDate)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet {maxDate:yyyy.MM.dd. HH:mm} után");
            return chain;
        }
        public static ValidatorChain<DateTime?> BeforeStrict(this ValidatorChain<DateTime?> chain, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value >= maxDate)
                return chain.AddError($"A(z) {chain.DisplayName} korábban kell legyen, mint {maxDate:yyyy.MM.dd. HH:mm}");
            return chain;
        }
        public static ValidatorChain<DateTime?> After(this ValidatorChain<DateTime?> chain, DateTime minDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value < minDate)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet {minDate:yyyy.MM.dd. HH:mm} előtt");
            return chain;
        }
        public static ValidatorChain<DateTime?> AfterStrict(this ValidatorChain<DateTime?> chain, DateTime minDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value <= minDate)
                return chain.AddError($"A(z) {chain.DisplayName} később kell legyen, mint {minDate:yyyy.MM.dd. HH:mm}");
            return chain;
        }
        public static ValidatorChain<DateTime?> Between(this ValidatorChain<DateTime?> chain, DateTime minDate, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue &&
                (chain.ValidationField.Value < minDate || chain.ValidationField.Value > maxDate))
                return chain.AddError($"A(z) {chain.DisplayName} {minDate:yyyy.MM.dd. HH:mm} és {maxDate:yyyy.MM.dd. HH:mm} között kell legyen");
            return chain;
        }
        public static ValidatorChain<DateTime?> BetweenStrict(this ValidatorChain<DateTime?> chain, DateTime minDate, DateTime maxDate)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue &&
                (chain.ValidationField.Value <= minDate || chain.ValidationField.Value >= maxDate))
                return chain.AddError($"A(z) {chain.DisplayName} szigorúan {minDate:yyyy.MM.dd. HH:mm} és {maxDate:yyyy.MM.dd. HH:mm} között kell legyen");
            return chain;
        }
        public static ValidatorChain<DateTime?> NotDefault(this ValidatorChain<DateTime?> chain)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value == default)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet üres");
            return chain;
        }
        #endregion
        #region BOOLEAN
        public static ValidatorChain<bool> True(this ValidatorChain<bool> chain)
        {
            if(!chain.HasFailed && !chain.ValidationField)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet hamis");
            return chain;
        }
        public static ValidatorChain<bool> False(this ValidatorChain<bool> chain)
        {
            if (!chain.HasFailed && chain.ValidationField)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet igaz");
            return chain;
        }
        // Nullable boolean overloads
        public static ValidatorChain<bool?> True(this ValidatorChain<bool?> chain)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && !chain.ValidationField.Value)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet hamis");
            return chain;
        }
        public static ValidatorChain<bool?> False(this ValidatorChain<bool?> chain)
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && chain.ValidationField.Value)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet igaz");
            return chain;
        }
        #endregion
        #region COLLECTION
        // NotNullOrEmpty for both strings and collections
        public static ValidatorChain<TCollection> NotNullOrEmpty<TCollection>(this ValidatorChain<TCollection> chain) where TCollection : IEnumerable?
        {
            return chain.NotNull().NotEmpty();
        }
        // NotEmpty for both strings and collections
        public static ValidatorChain<TCollection> NotEmpty<TCollection>(this ValidatorChain<TCollection> chain) where TCollection : IEnumerable?
        {
            if (chain.HasFailed || chain.ValidationField is null)
                return chain;

            bool isEmpty;
            if(chain.ValidationField is string s)
                isEmpty = string.IsNullOrEmpty(s);
            else if(chain.ValidationField is ICollection c)
                isEmpty = c.Count == 0;
            else
            {
                var enumerator = chain.ValidationField.GetEnumerator();
                using var disposable = enumerator as IDisposable;

                isEmpty = !enumerator.MoveNext();
            }
                
            if (isEmpty)
                return chain.AddError($"A(z) {chain.DisplayName} nem lehet üres");
            return chain;
        }
        public static ValidatorChain<TCollection?> MinCount<TCollection>(this ValidatorChain<TCollection?> chain, int min) where TCollection : ICollection
        {
            if (!chain.HasFailed && chain.ValidationField is not null && chain.ValidationField.Count < min)
                return chain.AddError($"A(z) {chain.DisplayName} minimum {min} hosszú lehet");
            return chain;
        }
        public static ValidatorChain<TCollection?> MaxCount<TCollection>(this ValidatorChain<TCollection?> chain, int max) where TCollection : ICollection
        {
            if (!chain.HasFailed && chain.ValidationField is not null && chain.ValidationField.Count > max)
                return chain.AddError($"A(z) {chain.DisplayName} maximum {max} hosszú lehet");
            return chain;
        }
        public static ValidatorChain<List<TItem>?> ForEach<TItem>(this ValidatorChain<List<TItem>?> chain, Action<ValidatorChain<TItem>> chainItemCallback)
        {
            return GenericForEach(chain, chainItemCallback);
        }
        public static ValidatorChain<TItem[]?> ForEach<TItem>(this ValidatorChain<TItem[]?> chain, Action<ValidatorChain<TItem>> chainItemCallback)
        {
            return GenericForEach(chain, chainItemCallback);
        }
        public static ValidatorChain<ICollection<TItem>?> ForEach<TItem>(this ValidatorChain<ICollection<TItem>?> chain, Action<ValidatorChain<TItem>> chainItemCallback)
        {
            return GenericForEach(chain, chainItemCallback);
        }
        private static ValidatorChain<TCollection?> GenericForEach<TCollection, TItem>(this ValidatorChain<TCollection?> chain, Action<ValidatorChain<TItem>> chainItemCallback) where TCollection : ICollection<TItem>
        {
            if (!chain.HasFailed && chain.ValidationField is not null)
            {
                int index = 0;
                foreach (TItem item in chain.ValidationField)
                {
                    string fieldName = $"{chain.ValidationFieldName}.[{index}]";
                    string displayName = $"{index + 1}. {chain.DisplayName}";

                    var chainItem = new ValidatorChain<TItem>(item, fieldName, chain.Errors, displayName);

                    chainItemCallback(chainItem);

                    index++;
                }
            }
            return chain;
        }
        #endregion
        #region ENUM
        public static ValidatorChain<TEnum> InEnum<TEnum>(this ValidatorChain<TEnum> chain) where TEnum : struct, Enum
        {
            if (!chain.HasFailed && !Enum.IsDefined(chain.ValidationField))
                return chain.AddError($"A(z) {chain.DisplayName} értéke érvénytelen.");
            return chain;
        }
        // Nullable enum overloads
        public static ValidatorChain<TEnum?> InEnum<TEnum>(this ValidatorChain<TEnum?> chain) where TEnum : struct, Enum
        {
            if (!chain.HasFailed && chain.ValidationField.HasValue && !Enum.IsDefined(chain.ValidationField.Value))
                return chain.AddError($"A(z) {chain.DisplayName} értéke érvénytelen.");
            return chain;
        }
        #endregion
    }
}
