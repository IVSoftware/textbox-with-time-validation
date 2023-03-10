using System.ComponentModel;
using System.Windows.Forms;

// https://stackoverflow.com/questions/74828557/how-to-disable-showupdown-and-calendar-drop-down-menu-as-well-in-datetimepicker
// https://stackoverflow.com/users/16695705/ondialuc
// How to disable ShowUpDown and calendar drop down menu as well in DateTimePicker WinForms C#
namespace textbox_with_time_validation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            maskedTextBox.Validated += onMaskTextBoxValidated;
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
            Mask = "00:00";
            CausesValidation= true;
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
                BeginInvoke(async() =>
                {
                    SelectAll();
                    Parent.BackColor = ePlus.Cancel ? Color.LightSalmon : Color.LightGreen;
                    await Task.Delay(1000);
                    Parent.BackColor = SystemColors.Control;
                });
            }
            base.OnKeyDown(e);
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            e.Cancel = !(DateTime.TryParse(Text, out DateTime unused));
            base.OnValidating(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            BeginInvoke(() => SelectAll());
        }
    }
}