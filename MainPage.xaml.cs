using System.Globalization;

namespace KoutaCalculator;

public partial class MainPage : ContentPage
{
    private readonly CalculatorEngine _calculator = new();

    public MainPage()
    {
        InitializeComponent();
        RefreshDisplay();
    }

    private void OnDigitClicked(object sender, EventArgs e) => Apply(() => _calculator.AppendDigit(((Button)sender).Text));
    private void OnDecimalClicked(object sender, EventArgs e) => Apply(_calculator.AppendDecimal);
    private void OnOperatorClicked(object sender, EventArgs e) => Apply(() => _calculator.SetOperator(((Button)sender).Text));
    private void OnEqualsClicked(object sender, EventArgs e) => Apply(_calculator.Evaluate);
    private void OnClearClicked(object sender, EventArgs e) => Apply(_calculator.Clear);
    private void OnBackspaceClicked(object sender, EventArgs e) => Apply(_calculator.Backspace);
    private void OnSignClicked(object sender, EventArgs e) => Apply(_calculator.ToggleSign);
    private void OnPercentClicked(object sender, EventArgs e) => Apply(_calculator.Percent);

    private void Apply(Action action)
    {
        action();
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        DisplayLabel.Text = _calculator.Display;
        ExpressionLabel.Text = _calculator.Expression;
    }
}

public sealed class CalculatorEngine
{
    private decimal? _left;
    private string? _operator;
    private string _entry = "0";
    private bool _replaceEntry;
    private string? _error;

    public string Display => _error ?? _entry.Replace('.', ',');
    public string Expression => _left is null || _operator is null ? string.Empty : $"{Format(_left.Value)} {_operator}";

    public void AppendDigit(string digit)
    {
        ResetError();
        if (_replaceEntry) { _entry = digit; _replaceEntry = false; return; }
        _entry = _entry == "0" ? digit : _entry + digit;
    }

    public void AppendDecimal()
    {
        ResetError();
        if (_replaceEntry) { _entry = "0."; _replaceEntry = false; return; }
        if (!_entry.Contains('.')) _entry += ".";
    }

    public void SetOperator(string operation)
    {
        if (_error is not null) { Clear(); return; }
        if (_left is not null && !_replaceEntry) Evaluate();
        if (_error is not null) return;
        _left = CurrentValue();
        _operator = operation;
        _replaceEntry = true;
    }

    public void Evaluate()
    {
        if (_error is not null || _left is null || _operator is null || _replaceEntry) return;
        var right = CurrentValue();
        decimal result;
        try
        {
            result = _operator switch
            {
                "+" => _left.Value + right,
                "−" => _left.Value - right,
                "×" => _left.Value * right,
                "÷" when right == 0 => throw new DivideByZeroException(),
                "÷" => _left.Value / right,
                _ => right
            };
            _entry = Format(result);
            _left = null; _operator = null; _replaceEntry = true;
        }
        catch (DivideByZeroException) { _error = "Division par zéro"; _replaceEntry = true; }
        catch (OverflowException) { _error = "Nombre trop grand"; _replaceEntry = true; }
    }

    public void Clear() { _left = null; _operator = null; _entry = "0"; _replaceEntry = false; _error = null; }
    public void Backspace()
    {
        if (_error is not null) { Clear(); return; }
        if (_replaceEntry) return;
        _entry = _entry.Length <= 1 ? "0" : _entry[..^1];
        if (_entry is "" or "-") _entry = "0";
    }
    public void ToggleSign() { ResetError(); _entry = CurrentValue() == 0 ? "0" : Format(-CurrentValue()); _replaceEntry = false; }
    public void Percent() { ResetError(); _entry = Format(CurrentValue() / 100m); _replaceEntry = false; }

    private decimal CurrentValue() => decimal.Parse(_entry, CultureInfo.InvariantCulture);
    private void ResetError() { if (_error is not null) Clear(); }
    private static string Format(decimal value) => value.ToString("0.#############################", CultureInfo.InvariantCulture);
}
