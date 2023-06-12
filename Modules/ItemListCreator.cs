using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Vermilion.EVE.Modules
{
  public class Item
  {
    public int Id { get; set; }
    public string Name { get; set; }
    // public string L1 { get; set; }
    // public string L2 { get; set; }
    // public string L3 { get; set; }
    // public string L4 { get; set; }
    // public string L5 { get; set; }
  }

  public class ItemGroup
  {
    public string Name { get; set; }
    public List<Item> Items { get; set; }
    public List<ItemGroup> Subgroups { get; set; }
  }

  public class ItemListCreator
  {
    public async Task<List<Item>> RunAsync()
    {
      // if you are using epplus for noncommercial purposes, see https://polyformproject.org/licenses/noncommercial/1.0.0/
      ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

      var url = @"https://www.ceve-market.org/dumps/evedata.xlsx";
      var items = new List<Item>();
      using (HttpClient client = new HttpClient())
      {
        var stream = await client.GetStreamAsync(url);
        using (var p = new ExcelPackage(stream))
        {
          var ws = p.Workbook.Worksheets["物品列表"];
          var row = 2;
          var maxRow = 65535;
          while (row <= maxRow)
          {
            var idStr = ws.Cells[row, 1].Value?.ToString() ?? "";
            if(int.TryParse(idStr, out var id) == false)
              break;
              
            var name = ws.Cells[row, 2].Value?.ToString() ?? "";
            // var l1 = ws.Cells[row, 4].Value?.ToString() ?? "";
            // var l2 = ws.Cells[row, 5].Value?.ToString() ?? "";
            // var l3 = ws.Cells[row, 6].Value?.ToString() ?? "";
            // var l4 = ws.Cells[row, 7].Value?.ToString() ?? "";
            // var l5 = ws.Cells[row, 8].Value?.ToString() ?? "";
            items.Add(new Item()
            {
              Id = id,
              Name = name,
              // L1 = l1,
              // L2 = l2,
              // L3 = l3,
              // L4 = l4,
              // L5 = l5,
            });

            row++;
          }
        }
      }

      return items;
    }
  }
}
