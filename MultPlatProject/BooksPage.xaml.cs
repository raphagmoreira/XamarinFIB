using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultPlatProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BooksPage : ContentPage
	{
		public BooksPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BooksViewModel).GetCommand.Execute(null);
        }

        // Executado quando a requisição de consulta dos autores falha.
        void Handle_RequestFailed(object sender, MultPlatProject.ErrorResponse e)
        {
            // Mostra alerta.
            DisplayAlert("Erro", e.Message, "ok");
        }

        private void Handle_RequestFailed(object sender)
        {

        }
    }
}