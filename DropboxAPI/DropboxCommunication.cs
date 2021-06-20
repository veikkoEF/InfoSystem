using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using System.IO;
using System.Threading.Tasks;

namespace DropboxAPI
{
    public class DropboxCommunication
    {
        private string dropBoxAppToken;

        public DropboxCommunication(string dropBoxAppToken)
        {
            this.dropBoxAppToken = dropBoxAppToken;
        }

        public async Task<FolderMetadata> CreateFolderAsync(string path)
        {
            var folderArg = new CreateFolderArg(path);
            try
            {
                using (var dbx = new DropboxClient(dropBoxAppToken))
                {
                    var folder = await dbx.Files.CreateFolderV2Async(folderArg);
                    return folder.Metadata;
                }
            }
            catch
            {
                return null;
            }
        }

        public async void DeleteFolderAsync(string nameOfFolder)
        {
            try
            {
                using (var dbx = new DropboxClient(dropBoxAppToken))
                {
                    await dbx.Files.DeleteV2Async("/" + nameOfFolder);
                }
            }
            catch
            { }
        }

        public async void DeleteFileAsync(string dirName, string fileName)
        {
            try
            {
                using (var dbx = new DropboxClient(dropBoxAppToken))
                {
                    // http://dropbox.github.io/dropbox-sdk-dotnet/html/N_Dropbox_Api_Files.htmme;
                    var path = "/" + dirName + "/" + fileName;
                    await dbx.Files.DeleteV2Async(path);
                }
            }
            catch
            { }
        }

        public async Task<IDownloadResponse<FileMetadata>> DownloadFileAsync(string filePath)
        {
            using (var dbx = new DropboxClient(dropBoxAppToken))
            {
                return await dbx.Files.DownloadAsync(filePath);
            }
        }

        public async Task<ListFolderResult> GetListOfFilesAsync()
        {
            using (var dbx = new DropboxClient(dropBoxAppToken))
            {
                return await dbx.Files.ListFolderAsync(string.Empty, true);
            }
        }

        public async Task UploadFileAsync(string folderName, string fileName, string pathOfSourceFile)
        {
            // https://stackoverflow.com/questions/40970095/file-uploading-dropbox-v2-0-api
            try
            {
                using (var dbx = new DropboxClient(dropBoxAppToken))
                {
                    using (var mem = new MemoryStream(File.ReadAllBytes(pathOfSourceFile)))
                    {
                        var updated = await dbx.Files.UploadAsync(
                            "/" + folderName + "/" + fileName,
                            WriteMode.Overwrite.Instance,
                            body: mem);
                    }
                }
            }
            catch
            { }
        }
    }
}