using SaveFileExplorer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace SaveFileExplorer
{
    public sealed class FileExplorer
    {
        public IAsyncOperation<Response> Save(string title, string base64)
        {            
            return  GetFile(title, base64).AsAsyncOperation();            
        }

        private async Task<Response> GetFile(string title, string base64)
        {
            try
            {
                var picker = new Windows.Storage.Pickers.FolderPicker();
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                picker.FileTypeFilter.Add("*");
                var folder = await picker.PickSingleFolderAsync();

                var file = await folder.CreateFileAsync(title);

                var bytes = Convert.FromBase64String(base64);
                var memoryStream = new MemoryStream(bytes);
                await Windows.Storage.FileIO.WriteBytesAsync(file, bytes);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot create a file when that file already exists"))
                {
                    return new Response
                    {
                        Code = 400,
                        Message = "Cannot create a file when that file already exists"
                    };
                }
                else
                {
                    return new Response
                    {
                        Code = 400,
                        Message = ex.ToString()
                    };
                }
            }

            return new Response
            {
                Code = 200,
                Message = "Success"
            };
        }

    }
}
