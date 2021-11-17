using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quote.Models;

namespace Quote.Services
{
  public class FileService
  {
    string name = "quote.json";

    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "quote.json");

    public bool CreateFile()
    {
      try
      {
        using (var writer = new StreamWriter(File.Create(fileName)))
        {
          // do work here.
          Console.WriteLine("created");
        }
        //File.Create(fileName);
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error creating {name}", ex);
        return false;
      }

    }
    public bool FileExists()
    {
      if (File.Exists(fileName))
      {
        Console.WriteLine("existe");
        return true;
      }
      else
      {
        Console.WriteLine("No existe");
        return false;
      }
    }
    //delete quote from file
    public bool DeleteQuote(Qquote quoteTodelete)
    {
      try
      {
        List<Qquote> quoteJson = OpenFile();
        bool quotedeleted = false;

        for (int i = quoteJson.Count - 1; i >= 0; i--)
        {
          if (quoteJson[i].Author == quoteTodelete.Author && quoteJson[i].Quote == quoteTodelete.Quote)
          {
            quotedeleted = true;
            quoteJson.RemoveAt(i);
          }
        }

        //write file
        if (quotedeleted)
        {
          File.WriteAllText(fileName, JsonConvert.SerializeObject(quoteJson));
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error deleting {name}", ex);
        return false;
      }
    }
    public void WriteFileInit(Qquote NewQuote)
    {
      List<Qquote> listQuote = new List<Qquote>();
      listQuote.Add(NewQuote);
      Console.WriteLine($"listQuote {listQuote.Count}");
      File.WriteAllText(fileName, JsonConvert.SerializeObject(listQuote));
    }
    public bool WriteFile(List<Qquote> ListQuote)
    {

      File.WriteAllText(fileName, JsonConvert.SerializeObject(ListQuote));
      return true;
    }
    public List<Qquote> OpenFile()
    {

      string json = File.ReadAllText(fileName);

      Console.WriteLine($"json {json}");
      List<Qquote> quoteJson = JsonConvert.DeserializeObject<List<Qquote>>(json);
     
      return quoteJson;
    }
  }
}
