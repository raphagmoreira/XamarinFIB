using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Flurl;
using Flurl.Http;
using Xamarin.Forms;

namespace MultPlatProject
{
    class NewAuthorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ErrorResponse> RequestFailed;
        public event EventHandler AuthorAdded;

        string mName;
        public string Text
        {
            get => mName;
            set
            {
                mName = value;
                PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs("Text")
                    );
            }
        }

        public ICommand PostCommand { get; private set; }

        public NewAuthorViewModel()
        {
            PostCommand = new Command<String>(async (newText) =>
            {
                try
                {
                    await
                        Constants.BaseServiceUrl
                                 .AppendPathSegment(typeof(Author).Name)
                                 .PostJsonAsync(new Author(newText));

                    AuthorAdded?.Invoke(this, null);
                }
                catch (FlurlHttpException ex)
                {
                    var error = await ex.GetResponseJsonAsync<ErrorResponse>();
                    Console.WriteLine(error.Message);
                    RequestFailed?.Invoke(this, error);
                }
            });
        }
    }
}
