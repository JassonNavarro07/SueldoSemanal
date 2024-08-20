namespace SueldoSemanal
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        public int _editResultadoId;


        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listView.ItemsSource = await _dbService.GetResultado());
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {

            if (_editResultadoId == 0)
            {
                int HT = Convert.ToInt32(EntryHorasTra.Text);
                int PH = Convert.ToInt32(EntryPagoHora.Text);
                int s = PH * HT;

                labelresultado.Text = s.ToString();
                //agrega cliente
                await _dbService.Create(new Resultado
                {

                    HorasTra = EntryHorasTra.Text,
                    PagoHo = EntryPagoHora.Text,

                    Sueldo = labelresultado.Text
                });

            }
            else
            {
                int HT = Convert.ToInt32(EntryHorasTra.Text);
                int PH = Convert.ToInt32(EntryPagoHora.Text);
                int s = PH * HT;

                labelresultado.Text = s.ToString();
                //editar cliente
                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    HorasTra = EntryHorasTra.Text,
                    PagoHo = EntryPagoHora.Text,
                    Sueldo = labelresultado.Text
                });
                _editResultadoId = 0;
            }
            EntryHorasTra.Text = string.Empty;
            EntryPagoHora.Text = string.Empty;
            labelresultado.Text = string.Empty;

            listView.ItemsSource = await _dbService.GetResultado();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    EntryHorasTra.Text = resultado.HorasTra;
                    EntryPagoHora.Text = resultado.PagoHo;
                    labelresultado.Text = resultado.Sueldo;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listView.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }
    }

}
