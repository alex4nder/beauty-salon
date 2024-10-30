using BeautySalonApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Service = BeautySalonApp.Models.Service;

namespace BeautySalonApp.Forms
{
    public partial class ServiceForm : Form
    {
        private readonly OfferingsService _serviceService;

        private Service _service;
        private bool _isEditMode;

        public ServiceForm(Service? service = null)
        {
            InitializeComponent();

            _serviceService = Program.ServiceProvider.GetRequiredService<OfferingsService>();

            if (service != null)
            {
                _service = service;
                _isEditMode = true;
                PreFillServiceData();
            }
            else
            {
                _service = new Service
                {
                    Title = "",
                    Description = "",
                    Price = 0.0m,
                    Duration = 0,
                };
                _isEditMode = false;
            }

            SetFormTitle();

        }

        private void PreFillServiceData()
        {
            serviceNameTextBox.Text = _service.Title;
            descriptionTextBox.Text = _service.Description;
            priceTextBox.Text = _service.Price.ToString("F2");
            durationTextBox.Text = _service.Duration.ToString();
        }
        private void SetFormTitle()
        {
            this.Text = _isEditMode ? GetEditModeTitle() : GetCreateModeTitle();
        }

        private string GetEditModeTitle()
        {
            return $"Услуга: {_service.Title}";
        }

        private string GetCreateModeTitle()
        {
            return "Новая услуга";
        }

        private void saveServiceBtn_Click(object sender, EventArgs e)
        {
            _service.Title = serviceNameTextBox.Text;
            _service.Description = descriptionTextBox.Text;
            _service.Price = decimal.TryParse(priceTextBox.Text, out var price) ? price : 0.0m;
            _service.Duration = int.TryParse(durationTextBox.Text, out var duration) ? duration : 0;

            if (_isEditMode)
            {
                _serviceService.ServiceEdit(_service);
            }
            else
            {
                _serviceService.ServiceAdd(_service);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
