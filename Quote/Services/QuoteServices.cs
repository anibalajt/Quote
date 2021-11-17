using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Quote.Models;
using Quote.Services;

namespace Quote.Services
{
  public class QuoteServices
  {
    FileService _fileService = new FileService();

    public List<Qquote> ListQuote;
    public QuoteServices()
    {

    }
    //fill the list with quotes
    public void AddFirstQuote()
    {
      Console.WriteLine("AddFirstQuote");
      Qquote _quote = new Qquote();
      _quote.Author = "Albert Einstein";
      _quote.Quote = "I have no special talent. I am only passionately curious.";
      _quote.Favorite = true;
      ListQuote.Add(_quote);
      _fileService.WriteFileInit(_quote);
    }
    //add quotes from file to list quote
    public void AddQuoteFromFile()
    {
      ListQuote = new List<Qquote>(_fileService.OpenFile());
    }
    //add quote
    public void AddQuote(string quote, string autor, bool favorite = false)
    {
      ListQuote.Add(new Qquote() { Quote = quote, Author = autor, Favorite = favorite });
      _fileService.WriteFile(new List<Qquote>(ListQuote));
    }
    //remove quote form ListQuote
    public void RemoveQuote(Qquote _quote)
    {
      for (int i = ListQuote.Count - 1; i >= 0; i--)
      {
        if (ListQuote[i].Author == _quote.Author && ListQuote[i].Quote == _quote.Quote)
        {
          ListQuote.RemoveAt(i);
          break;
        }
      }
      _fileService.WriteFile(new List<Qquote>(ListQuote));
    }
    //get quote from ListQuote
    public string GetQuote(string author)
    {
      foreach (var quote in ListQuote)
      {
        if (quote.Author == author)
        {
          return quote.Quote;
        }
      }
      return "";
    }
    //get random quote
    public Qquote GetRandomQuote()
    {
      List<Qquote> favoriteQuotes = new List<Qquote>(GetFavoriteQuotes());
      if (favoriteQuotes.Count > 0)
      {
        Random rnd = new Random();
        int index = rnd.Next(0, favoriteQuotes.Count);
        return favoriteQuotes[index];
      }
      else
      {
        Qquote quote = new Qquote();

        quote.Author = "";
        quote.Quote = "No Quote to display, Try adding a new favorite Quote";
        return quote;
      }
    }
    //get favorite quotes
    public List<Qquote> GetFavoriteQuotes()
    {
      List<Qquote> _favoriteQuotes = new List<Qquote>();
      foreach (var quote in ListQuote)
      {
        if (quote.Favorite == true)
        {
          _favoriteQuotes.Add(quote);
        }
      }
      return _favoriteQuotes;
    }
    //get all quote from ListQuote
    public List<Qquote> GetAllQuote()
    {
      return ListQuote;
    }
    //save as favorite
    public void SaveAsFavorite(Qquote _quote)
    {
      _quote.Favorite = !_quote.Favorite;
      _fileService.WriteFile(new List<Qquote>(ListQuote));
    }

  }
}
