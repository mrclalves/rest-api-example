using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Compuletra.RestApiExample.Web.Extensions {
    public static class HttpRequestExtensions {
        public static async Task<string> BodyAsStringAsync(this HttpRequest @this, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            using (var reader = new StreamReader(@this.Body, encoding)) {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
