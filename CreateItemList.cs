using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vermilion.EVE.Modules;

namespace Vermilion.EVE.Functions
{
  public class CreateItemList
  {
    [FunctionName("CreateItemList")]
    public async Task RunAsync(
        [TimerTrigger("* * 2 * * *")] TimerInfo myTimer,
        [Blob("static-assets/items.json", FileAccess.Write), StorageAccount("VEAssetsStorage")] Stream output,
        ILogger log)
    {
      log.LogInformation($"CreateItemList started at: {DateTime.Now}");

      var creator = new ItemListCreator();
      var items = await creator.RunAsync();
      string json = JsonConvert.SerializeObject(items);
      var writer = new StreamWriter(output);
      await writer.WriteAsync(json);
      await writer.FlushAsync();

      log.LogInformation($"CreateItemList finished at: {DateTime.Now}");
    }
  }
}
