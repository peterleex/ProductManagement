namespace ProductManagement.HttpApi.Client.WinFormTestApp
{
    public partial class LQWaiting : Form
    {
        private static LQWaiting _instance = null!;
        public static LQWaiting Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new LQWaiting();
                }
                return _instance;
            }
        }
        private Label lblMessage = null!;

        private LQWaiting()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            InitMessageLabel();
            InitForm();
        }

        private void InitForm()
        {
            Controls.Add(lblMessage);
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            ShowInTaskbar = false;
        }

        private void InitMessageLabel()
        {
            lblMessage = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BorderStyle = BorderStyle.Fixed3D,
            };
        }

        public void CenterTo(Form parent)
        {
            CenterToParent(parent);
            Show(parent);
        }
        private void CenterToParent(Form parent)
        {
            Location = new Point(
                parent.Location.X + (parent.ClientSize.Width - Width) / 2,
                parent.Location.Y + (parent.ClientSize.Height - (Height - SystemInformation.CaptionHeight)) / 2
            );
        }
        public void ShowMessage(string message)
        {
            ShowLabel(message);
            Refresh();
        }

        private void ShowLabel(string message)
        {
            lblMessage.Text = message;
            lblMessage.Location = new Point(
                (ClientSize.Width - lblMessage.Width) / 2, (ClientSize.Height - lblMessage.Height) / 2);
        }

        public void Release()
        {
            if (IsDisposed) return;

            Close();
            Dispose();
        }
    }
}
