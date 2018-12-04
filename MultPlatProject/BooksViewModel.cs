using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Flurl;
using Flurl.Http;
using Xamarin.Forms;

namespace MultPlatProject
{
    class BooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ErrorResponse> RequestFailed;

        String mFiltroLivro;
        public String FiltroLivro
        {
            get => mFiltroLivro;
            set
            {
                mFiltroLivro = value;
                PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs("FiltroLivro")
                    );
            }
        }

        Boolean mIsLoading = true;
        public Boolean IsLoading
        {
            get => mIsLoading;
            set
            {
                mIsLoading = value;
                PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs("IsLoading")
                    );
            }
        }

        List<Book> mBooks;
        public List<Book> Books
        {
            get => mBooks;
            set
            {
                if (!Equals(mBooks, value))
                {
                    mBooks = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Books"));
                }
            }
        }
        
        public ICommand GetCommand { get; private set; }

        public BooksViewModel()
        {
            async void execute()
            {
                IsLoading = true;

                try
                {
                    Books = await
                        Constants.BaseServiceUrl
                                 .AppendPathSegment(typeof(Book).Name)
                                 .GetJsonAsync<List<Book>>();
                }
                catch (FlurlHttpException ex)
                {
                    var error = await ex.GetResponseJsonAsync<ErrorResponse>();
                    RequestFailed?.Invoke(this, error);
                }
                finally
                {
                    IsLoading = false;
                }
            }
            
            GetCommand = new Command(execute);
        }
    }
}
