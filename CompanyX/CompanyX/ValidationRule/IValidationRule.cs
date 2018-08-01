namespace CompanyX.ValidationRule
{
    public interface IValidationRule<T>
    {
        string ValidationMessage(T value);
        bool IsRuleValid(T value);
    }
}
