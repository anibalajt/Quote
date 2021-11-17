using System;
using System.Collections.Generic;
using Quote.Models;
using Quote.Services;
using Xamarin.Forms;

namespace Quote
{
  public partial class AddQuote : ContentPage
  {
    QuoteServices _quoteServices;
    public AddQuote(QuoteServices quoteService)
    {
      _quoteServices = quoteService;
      InitializeComponent();
    }
    // Add a new quote to the database
    async void OnSaveClicked(object sender, EventArgs e)
    {
      string author = authorEntry.Text;
      string quote = quoteEntry.Text;

      if (!string.IsNullOrWhiteSpace(author) && !string.IsNullOrWhiteSpace(quote))
      {
        // Create a new Quote object
        _quoteServices.AddQuote(quote, author);
        await Navigation.PopModalAsync();
      }

    }
    async void BtnBack_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopModalAsync();
    }
    protected override void OnSizeAllocated(double width, double height)
    {
      base.OnSizeAllocated(width, height); //must be called

      if (width > height)
      {
        if (Device.RuntimePlatform == Device.iOS)
        {
          form.Padding = new Thickness(50, 20);
        }
      }
      else
      {
        if (Device.RuntimePlatform == Device.iOS)
        {
          form.Padding = new Thickness(20, 50);
        }
      }
    }
  }
}
