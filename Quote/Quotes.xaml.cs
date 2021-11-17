using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Quote.Models;
using Quote.Services;
using System.Collections.ObjectModel;

namespace Quote
{
  /// <summary>
  /// Quotes: This View shows all quotes favorites and no favoites
  /// Favorites quotes are displayed first
  /// </summary>
  public partial class Quotes : ContentPage
  {
    public QuoteServices _quoteService;
    FileService FileDB = new FileService();
    public Quotes(QuoteServices quoteService)
    {
      _quoteService = quoteService;
      InitializeComponent();
      // FillListView();
    }
    protected override void OnAppearing()
    {
      base.OnAppearing();
      FillListView();
    }



    //Binding quoteListo to ListView
    public void FillListView()
    {
      listViewQuote.Children.Clear();
      foreach (Qquote quote in _quoteService.GetAllQuote())
      {
        StackLayout Row = new StackLayout();
        Row.Children.Add(new Label()
        {
          Text = quote.Quote,
          FontSize = 14
        });
        Row.Children.Add(new Label()
        {
          Text = quote.Author,
          FontSize = 12
        });
        Grid grid = new Grid();
        //favorite quote
        string fileExtencion = (Device.RuntimePlatform == Device.Android) ? "" : ".png";
        Image btnFavotire = new Image()
        {
          Source = quote.Favorite?$"heart{fileExtencion}": $"heart1{fileExtencion}",
          HorizontalOptions = LayoutOptions.Start,
          WidthRequest = 20,
          HeightRequest = 20,
        };
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += (object sender, EventArgs e) =>
        {
          BtnFavoriteQuote(quote);
        };
        btnFavotire.GestureRecognizers.Add(tap);

        grid.Children.Add(btnFavotire);
        //delete quete
        Button btnDelete = new Button()
        {
          Text = "Delete",
          HorizontalOptions = LayoutOptions.End,
          
        };
        btnDelete.Clicked += (object sender, EventArgs e) =>
        {
          BtnDeleteQuote(quote);
        };
        grid.Children.Add(btnDelete);
        Row.Children.Add(grid);
    
        //if quote is favorite insert first in Row
        if (quote.Favorite)
        {
          listViewQuote.Children.Insert(0, Row);
        }
        else
        {
          listViewQuote.Children.Add(Row);
        }

      }
    }
  
    //add new quote
    private void Button_Clicked(object sender, EventArgs e)
    {
      Navigation.PushModalAsync(new AddQuote(_quoteService));
    }
    //favorite quote
    private void BtnFavoriteQuote(Qquote quote)
    {
      _quoteService.SaveAsFavorite(quote);
      FillListView();
    }
    //delete quote with display alert
    private async void BtnDeleteQuote(Qquote quote)
    {
      bool answer = await DisplayAlert("Delete", $"Are you sure you want to delete {quote.Quote}", "Yes", "No");
      if (answer)
      {
        _quoteService.RemoveQuote(quote);
        FillListView();
      }
    }

    // //delete quote
    // private void BtnDeleteQuote(Qquote quote)
    // {
    //   _quoteService.RemoveQuote(quote);
    //   FillListView();
    // }
    async void BtnBack_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
    }
    private void BtnAddQuote_Clicked(System.Object sender, System.EventArgs e)
    {
      Navigation.PushModalAsync(new AddQuote(_quoteService));
    }
  }
}
