using System.ComponentModel;

// https://stackoverflow.com/questions/74828557/how-to-disable-showupdown-and-calendar-drop-down-menu-as-well-in-datetimepicker
namespace textbox_with_time_validation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            maskedTextBox.CausesValidation = true;
            maskedTextBox.GotFocus += onMaskTextBoxFocus;
            maskedTextBox.Validating += onMaskTextBoxValidating;
            maskedTextBox.Validated += onMaskTextBoxValidated;
        }

        private void onMaskTextBoxFocus(object? sender, EventArgs e)
        {
            BeginInvoke(() => maskedTextBox.Clear());
        }

        private void onMaskTextBoxValidating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !(DateTime.TryParse(maskedTextBox.Text, out DateTime unused));
        }

        private void onMaskTextBoxValidated(object? sender, EventArgs e)
        {
            Text = $"Validated {DateTime.Parse(maskedTextBox.Text).ToShortTimeString()}";
        }
    }
    class MaskedTextBoxEx : MaskedTextBox
    {
        public MaskedTextBoxEx()
        {
            Text = "00:00";
            Mask = "00:00";
        }
        private string? _lastValid = null;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // Force a validation "without" having to lose focus.
                e.Handled = e.SuppressKeyPress = true;
                var ePlus = new CancelEventArgs();
                OnValidating(ePlus);
                if (ePlus.Cancel)
                {
                    if (_lastValid == null)
                    {
                        BeginInvoke(() => Clear());
                    }
                    else
                    {
                        BeginInvoke(() => Text = _lastValid);
                    }
                }
                else
                {
                    OnValidated(ePlus);
                    _lastValid = Text;
                }
            }
            base.OnKeyDown(e);
        }
    }
}