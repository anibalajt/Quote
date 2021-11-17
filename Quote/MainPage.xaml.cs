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
using System.Threading;
using System.Timers;

namespace Quote
{
  /// <summary>
  /// MainPage: This view shows a favorite random quote
  /// </summary>
  public partial class MainPage : ContentPage
  {
    public QuoteServices quoteService = new QuoteServices();
    FileService FileDB = new FileService();
    System.Timers.Timer timer = new System.Timers.Timer(10000);
    public MainPage()
    {
      InitializeComponent();
      if (!FileDB.FileExists())
      {
        FileDB.CreateFile();
        quoteService.ListQuote = new List<Qquote>();
        quoteService.AddFirstQuote();
      }
      else
      {
        quoteService.AddQuoteFromFile();
      }
      DisplayQuote();
    }

    void DisplayQuote()
    {
      Qquote quote = quoteService.GetRandomQuote();
      lblAuthor.Text = quote.Author;
      lblQuote.Text = quote.Quote;
    }
    protected override void OnAppearing()
    {
      base.OnAppearing();
      DisplayQuote();

    }
    private void BtnRandomQuote_Clicked(System.Object sender, System.EventArgs e)
    {
      DisplayQuote();
    }
    private void BtnSeeQuote_Clicked(System.Object sender, System.EventArgs e)
    {
      Navigation.PushModalAsync(new Quotes(quoteService));
    }
    private void BtnAddQuote_Clicked(System.Object sender, System.EventArgs e)
    {
      Navigation.PushModalAsync(new AddQuote(quoteService));
    }
    protected override void OnSizeAllocated(double width, double height)
    {
      base.OnSizeAllocated(width, height); //must be called
      contentView.WidthRequest = width;
      footer.WidthRequest = width - 40;
      if (width > height)
      {
        if (Device.RuntimePlatform == Device.iOS)
        {
          quoteDisplayed.Padding = new Thickness(50, 0, 20, 20);
        }
      }
      else
      {

        if (Device.RuntimePlatform == Device.iOS)
        {
          quoteDisplayed.Padding = new Thickness(20);
        }
      }
    }
  }
}
