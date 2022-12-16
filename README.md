![objective]()

Your [post] seems to indicate that your only objection to MaskedTextBox has to do with limitations for validation. You cite the test case of '23:59' as being a string that _should_ validate and I will infer that '23:99' should _not_.

![valid]()

![not-valid]()

In my testing, I was able to obtain this result using the following custom class.

![]

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

For testing, use a naked `DateTime.Parse` and show the result in the Title bar.

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