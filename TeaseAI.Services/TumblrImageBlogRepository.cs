using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.Services
{
    public class TumblrImageBlogRepository : IImageBlogDownloadService
    {
        private static HttpClient httpClient = new HttpClient();

        public TumblrImageBlogRepository()
        {
        }

        public async Task<Result<List<ImageMetaData>>> GetBlogImagesAsync(Uri baseUrl, int offset, int count)
        {
            var queryUrl = new Uri(baseUrl, string.Format("api/read?start={0}&num={1}", offset.ToString(), count.ToString()));
            var request = await httpClient.GetAsync(queryUrl);
            if (!request.IsSuccessStatusCode)
                return Result.Fail<List<ImageMetaData>>(request.ReasonPhrase);

            var xmlString = await request.Content.ReadAsStringAsync();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            var posts = xmlDoc.DocumentElement.SelectNodes("//posts")[0].ChildNodes;

            var returnData = new List<ImageMetaData>();
            foreach (XmlElement post in posts)
            {
                var photoUrls = new Dictionary<int, string>();
                foreach (XmlElement child in post.ChildNodes)
                {
                    if (child.Name == "photo-url")
                    {
                        photoUrls[Convert.ToInt32(child.Attributes["max-width"].InnerText)] = child.InnerText;
                    }
                }
                var url = photoUrls[photoUrls.Keys.Max()];

                var imd = new ImageMetaData
                {
                    FullFileName = url.ToString(),
                    ItemName = Path.GetFileNameWithoutExtension(url.ToString()),
                    GenreId = ImageGenre.Blog,
                    SourceId = ImageSource.Remote,
                };
                returnData.Add(imd);
            }
            return Result.Ok(returnData);
        }
    }
}
